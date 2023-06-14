import { getMatchInfo } from "../../libs/cache";
import dgram from 'node:dgram';
import { InitMatchData } from "../interfaces/match-init";
import { MessagePayload, ServerEventType } from "../payload/message.payload";

export async function initMatch(server: dgram.Socket, matchId: string) {
    const storedMatch = await getMatchInfo(matchId)
    if (!storedMatch) return;

    const matchData: InitMatchData = {
        playersPos: storedMatch.players.map((player, index) => {
            return { userId: player.userId, xPos: 0 + index, yPos: 0 + index }
        })
    }

    const payload: MessagePayload = {
        data: matchData,
        type: ServerEventType.MatchCreated
    }
    // Send match initial data info to all players
    const msg = Buffer.from(JSON.stringify(payload))

    storedMatch.players.forEach((player) => {
        server.send(msg, player.port, player.address)
    })
}