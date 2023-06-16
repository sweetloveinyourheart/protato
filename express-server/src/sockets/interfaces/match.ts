import { SocketUser } from "./user"

export interface InitMatchData {
    playersPos: {
        userId: string
        xPos: number
        yPos: number
    }[]
}

export interface MatchData {
    players: SocketUser[]
    matchConfigs: InitMatchData
}