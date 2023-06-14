
export interface SocketRemote {
    address: string
    port: number
}

export interface SocketUser extends SocketRemote{
    userId: string
} 