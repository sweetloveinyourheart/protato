import { Schema, model, Document } from 'mongoose'
import { UserDto } from '../dto/user/user.dto'

interface Result {
    player: UserDto
    score: number
    victory: boolean
}

export interface Match extends Document {
    _id: string
    results: Result[]
    players: UserDto[]
    createdAt: Date
}

const ResultSchema = new Schema({
    player: { type: Schema.Types.ObjectId, ref: 'Users' },
    score: { type: Number },
    victory: { type: Boolean, default: false }
});

const MatchSchema = new Schema({
    results: {
        type: [ResultSchema],
        required: true
    },
    players: {
        type: [Schema.Types.ObjectId],
        ref: 'Users'
    },
    createdAt: {
        type: Date,
        default: new Date()
    }
})

const MatchModel = model<Match>('Matches', MatchSchema)
export default MatchModel