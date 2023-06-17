import dgram from 'node:dgram';
import { ClientEventType, MessagePayload } from './payload/message.payload';
import { findMatch, leaveQueue } from './handlers/find-match.handler';
import { initMatch } from './handlers/match.handler';
import { playerMove } from './handlers/player.handler';
import { applicationQuit } from './handlers/application.handler';

const server = dgram.createSocket('udp4');
const PORT = Number(process.env.UDP_SERVER_PORT || 3333);

server.on('message', (message, remote) => {
    const { address, port } = remote

    // Parse the received message
    const receivedData: MessagePayload = JSON.parse(message.toString());
    const type = receivedData.type
    const data = receivedData.data

    switch (type) {
        case ClientEventType.FindMatch:
            var socketUser = { address, port, userId: data.userId }
            return findMatch(server, socketUser)

        case ClientEventType.LeaveQueue:
            var socketUser = { address, port, userId: data.userId }
            return leaveQueue(socketUser)

        case ClientEventType.InitMatch:
            return initMatch(server, data.matchId)

        case ClientEventType.UpdatePlayerState:
            return playerMove(server, data)

        case ClientEventType.ApplicationQuit:
            return applicationQuit(server, data)

        default:
            return;
    }
});

server.on('listening', () => {
    const address = server.address();
    console.log(`⚡️[socket]: UDP Server listening on ${address.address}:${address.port}`);
});

server.on('close', () => {
    console.log("UDP Server closed");
})

server.bind(PORT);
