import * as redis from 'redis'
import { SocketUser } from '../sockets/interfaces/user';
import { MatchDto } from '../dto/match/match.dto';

const client = redis.createClient()

client
    .connect()
    .then(() => console.log('Redis connected'))
    .catch(() => console.log('Connect to redis failed !'))

export async function storeValidationCode(email: string, code: string) {
    await client.set(email, code);
    await client.expire(email, 15 * 60)
}

export async function checkValidationCode(email: string, code: string) {
    const storedCode = await client.get(email);
    const result = code === storedCode
    if (result) {
        await client.del(email)
    }
    return result
}

// Socket functions
export async function joinMatchQueue(socketUser: SocketUser): Promise<SocketUser | null> {
    const canMakeAMatch = await client.get("matchQueue")

    // The match can start with 2 player
    if (canMakeAMatch) {
        await client.del("matchQueue")

        const opponent = JSON.parse(canMakeAMatch)
        return opponent
    }

    const waiter = JSON.stringify(socketUser)
    await client.set("matchQueue", waiter)
    await client.expire("matchQueue", 15 * 60)

    return null
}

export async function leaveMatchQueue(socketUser: SocketUser): Promise<void> {
    const canMakeAMatch = await client.get("matchQueue")

    // Check and remove user in queue
    if (canMakeAMatch) {
        const user: SocketUser = JSON.parse(canMakeAMatch)
        if (user.userId === socketUser.userId) {
            await client.del("matchQueue")
        }
    }
}

export async function storeMatchInfo(match: MatchDto, players: SocketUser[]): Promise<void> {
    const stringify = JSON.stringify({ players })

    await client.set(match._id.toString(), stringify)
    await client.expire(match._id.toString(), 15 * 60)
}

export async function getMatchInfo(matchId: string): Promise<{ players: SocketUser[] } | null> {
    const matchStringify = await client.get(matchId)

    if (matchStringify) {
        const matchInfo: { players: SocketUser[] } = JSON.parse(matchStringify)
        return matchInfo
    }

    return null
}