import dgram from 'node:dgram';
import { PlayerState } from '../interfaces/player';
import { getMatchInfo } from '../../libs/socket';
import { MessagePayload, ServerEventType } from '../payload/message.payload';

export async function playerMove(server: dgram.Socket, playerState: PlayerState) {
    const storedMatch = await getMatchInfo(playerState.matchId)
    if (!storedMatch) return;

    const { matchId, ...data } = playerState

    const payload: MessagePayload = {
        type: ServerEventType.PlayerStateUpdated,
        data
    }
    const msg = Buffer.from(JSON.stringify(payload))

    storedMatch.players.forEach((player) => {
        server.send(msg, player.port, player.address)
    })
}