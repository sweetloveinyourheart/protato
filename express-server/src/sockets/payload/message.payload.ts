export enum ClientEventType {
    FindMatch = "FindMatch",
    InitMatch = "InitMatch",
    LeaveQueue = "LeaveQueue", 
    Move = "Move",
    Attack = "Attack"
}

export enum ServerEventType {
    MatchJoined = "MatchJoined",
    MatchCreated = "MatchCreated",
    PlayerMoved = "PlayerMoved"
}

export interface MessagePayload {
    type: ClientEventType | ServerEventType
    data: any
}