import { Schema, model, Document } from 'mongoose'

export interface User extends Document {
    id: string
    email: string
    password: string
    username: string
    isVerified: boolean
    createdAt: Date
}

const UserSchema = new Schema({
    email: {
        type: String,
        unique: true,
        required: true
    },
    password: {
        type: String,
        required: true
    },
    username: {
        type: String,
        required: true,
        unique: true
    },
    isVerified: {
        type: Boolean,
        default: false
    },
    createdAt: {
        type: Date,
        default: new Date()
    }
})

const UserModel = model<User>('Users', UserSchema)
export default UserModel