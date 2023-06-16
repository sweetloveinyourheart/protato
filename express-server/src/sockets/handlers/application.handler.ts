import dgram from 'node:dgram';
import { ApplicationQuit } from '../interfaces/application';
import { closeMatch, getMatchInfo } from '../../libs/socket';
import { ServerEventType } from '../payload/message.payload';

export async function applicationQuit(server: dgram.Socket, application: ApplicationQuit) {
    const storedMatch = await getMatchInfo(application.matchId)
    if (!storedMatch) return;

    await closeMatch(application.matchId)
    const { userId } = application

    const payload = { data: userId, type: ServerEventType.UserLeft }
    const responseBuffer = Buffer.from(JSON.stringify(payload))

    // Send match info to other player
    const user = storedMatch.players.find((p) => p.userId !== application.userId)
    if (user) server.send(responseBuffer, user.port, user.address)

}