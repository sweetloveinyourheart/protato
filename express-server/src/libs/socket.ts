import { SocketUser } from '../sockets/interfaces/user';
import { MatchDto } from '../dto/match/match.dto';
import { InitMatchData, MatchData } from '../sockets/interfaces/match';
import { client } from '..';

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

export async function storeMatchInfo(match: MatchDto, players: SocketUser[], matchConfigs: InitMatchData): Promise<void> {
    const stringify = JSON.stringify({ players, matchConfigs })

    await client.set(match._id.toString(), stringify)
    await client.expire(match._id.toString(), 15 * 60)
}

export async function getMatchInfo(matchId: string): Promise<MatchData | null> {
    const matchStringify = await client.get(matchId)

    if (matchStringify) {
        const matchInfo: MatchData = JSON.parse(matchStringify)
        return matchInfo
    }

    return null
}

export async function closeMatch(matchId: string) {
    await client.del(matchId)
    return true
}