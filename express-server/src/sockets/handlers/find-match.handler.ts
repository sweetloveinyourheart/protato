import dgram from 'node:dgram';
import { SocketUser } from "../interfaces/user";
import { joinMatchQueue, leaveMatchQueue, storeMatchInfo } from '../../libs/cache';
import * as matchServices from '../../services/match.service'
import { MessagePayload, ServerEventType } from '../payload/message.payload';

export async function findMatch(server: dgram.Socket, user: SocketUser) {
    const opponent = await joinMatchQueue(user)
    if (opponent) {
        // create a match
        const players = [user.userId, opponent.userId]
        const newMatch = await matchServices.createMultipleMatch(players)

        // Store match to cache
        await storeMatchInfo(newMatch, [user, opponent])

        // Convert the JSON object to bytes
        const joinedMatch: MessagePayload = { type: ServerEventType.MatchJoined, data: { matchId: newMatch._id } }
        const responseBuffer = Buffer.from(JSON.stringify(joinedMatch))

        // Send match info to all players
        server.send(responseBuffer, user.port, user.address)
        server.send(responseBuffer, opponent.port, opponent.address)
    }
}

export async function leaveQueue(user: SocketUser) {
    await leaveMatchQueue(user)
}