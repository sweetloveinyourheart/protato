import dgram from 'node:dgram';
import { PlayerMovement } from '../interfaces/player';
import { getMatchInfo } from '../../libs/cache';
import { MessagePayload, ServerEventType } from '../payload/message.payload';

export async function playerMove(server: dgram.Socket, movement: PlayerMovement) {
    const storedMatch = await getMatchInfo(movement.matchId)
    if (!storedMatch) return;

    const { matchId, ...playerMovement } = movement

    const payload: MessagePayload = {
        type: ServerEventType.PlayerMoved,
        data: playerMovement
    }
    const msg = Buffer.from(JSON.stringify(payload))

    storedMatch.players.forEach((player) => {
        server.send(msg, player.port, player.address)
    })
}