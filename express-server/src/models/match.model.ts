import { Schema, model, Document } from 'mongoose'
import { UserDto } from '../dto/user/user.dto'

interface Result {
    player: UserDto
    score: number
}

export interface Match extends Document {
    _id: string
    results: Result[]
    players: UserDto[]
    createdAt: Date
}

const ResultSchema = new Schema({
    player: { type: Schema.Types.ObjectId, ref: 'Users' },
    score: { type: Number }
});

const MatchSchema = new Schema({
    victory: {
        type: Boolean,
        default: false
    },
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