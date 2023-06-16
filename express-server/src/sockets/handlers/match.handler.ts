import { getMatchInfo } from "../../libs/socket";
import dgram from 'node:dgram';
import { InitMatchData } from "../interfaces/match";
import { MessagePayload, ServerEventType } from "../payload/message.payload";

export async function initMatch(server: dgram.Socket, matchId: string) {
    const storedMatch = await getMatchInfo(matchId)
    if (!storedMatch) return;

    const matchData: InitMatchData = storedMatch.matchConfigs

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