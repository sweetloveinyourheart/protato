import { MatchDto } from "../dto/match/match.dto";
import { SaveMatchResultDto } from "../dto/match/save-result.dto";
import { MessageDto } from "../dto/message.dto";
import { Exception } from "../middlewares/exception";
import MatchModel from "../models/match.model";

export async function createSingleMatch(userId: string): Promise<MatchDto> {
    const newMatch = await MatchModel.create({
        results: [
            { player: userId, score: 0 }
        ],
        players: [userId]
    })
    return newMatch
}

export async function getMatchHistory(userId: string): Promise<MatchDto[]> {
    const matches = await MatchModel.find({ players: userId })
    if(!matches) {
        throw new Exception({ message: 'Match not found', statusCode: 200 })
    }

    return matches
}

export async function getMatchById(matchId: string): Promise<MatchDto> {
    const match = await MatchModel.findById(matchId).populate(['players', 'results.player'])
    if(!match) {
        throw new Exception({ message: 'Match not found', statusCode: 200 })
    }

    return match
}

export async function saveMatchHistory(matchId: string, userId: string, matchResult: SaveMatchResultDto): Promise<MessageDto> {
    const matchFound = await MatchModel.findById(matchId)
    if (!matchFound) {
        throw new Exception({ message: "Match not found", statusCode: 404 })
    }

    await MatchModel.updateOne(
        { _id: matchId, 'results.player': userId },
        { $set: { 'results.$.score': matchResult.score }, victory: matchResult.victory }
    )

    return { message: 'Match result saved !' }
}