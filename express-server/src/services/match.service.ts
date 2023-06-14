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

export async function createMultipleMatch(players: string[]): Promise<MatchDto> {
    const results = players.map((p) => ({
        player: p,
        score: 0
    }))

    const newMatch = await MatchModel.create({
        results,
        players
    })
    return newMatch
}


export async function getMatchHistory(userId: string): Promise<MatchDto[]> {
    const matches = await MatchModel
        .find({ players: userId })
        .sort({ createdAt: -1 })
        .limit(20)

    if (!matches) {
        throw new Exception({ message: 'Match not found', statusCode: 200 })
    }

    return matches
}

export async function getMatchById(matchId: string): Promise<MatchDto> {
    const match = await MatchModel
        .findById(matchId)
        .populate(['players', 'results.player'])
        .select({ "results.player.password": 0, "players.password": 0 })

    if (!match) {
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