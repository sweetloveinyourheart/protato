export enum ClientEventType {
    FindMatch = "FindMatch",
    InitMatch = "InitMatch",
    LeaveQueue = "LeaveQueue",
    UpdatePlayerState = "UpdatePlayerState",
    ApplicationQuit = "ApplicationQuit"
}

export enum ServerEventType {
    MatchJoined = "MatchJoined",
    MatchCreated = "MatchCreated",
    PlayerStateUpdated = "PlayerStateUpdated",
    UserLeft = "UserLeft"
}

export interface MessagePayload {
    type: ClientEventType | ServerEventType
    data: any
}