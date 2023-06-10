/**
 * @swagger
 * components:
 *   schemas:
 *     User:
 *       type: object
 *       properties:
 *         id:
 *           type: string
 *           description: The user's ID
 *         email:
 *           type: string
 *           format: email
 *           description: The user's email address
 *         password:
 *           type: string
 *           description: The user's password
 *         username:
 *           type: string
 *           description: The user's username
 *         isVerified:
 *           type: boolean
 *           description: Indicates if the user is verified
 *         createdAt:
 *           type: string
 *           format: date-time
 *           description: The user's creation date and time
 */

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