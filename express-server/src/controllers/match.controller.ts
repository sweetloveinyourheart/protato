/**
 * @swagger
 * tags:
 *   name: Match
 *   description: The match managing API
 
 * /match/get-history:
 *   get:
 *     security:
 *       - BearerAuth: []
 *     summary: Get match history by user
 *     tags: [Match]
 *     responses:
 *       200:
 *         description: A match after create in single mode.
 *         content:
 *           application/json:
 *             schema:
 *               $ref: '#/components/schemas/MatchDto'

 * /match/get-by-id/{matchId}:
 *   get:
 *     security:
 *       - BearerAuth: []
 *     parameters:
 *       - in: path
 *         name: matchId
 *         schema:
 *           type: string
 *         required: true
 *         description: ID of the match
 *     summary: Get match detail by match id
 *     tags: [Match]
 *     responses:
 *       200:
 *         description: A match after create in single mode.
 *         content:
 *           application/json:
 *             schema:
 *               $ref: '#/components/schemas/MatchDto'

 * /match/single-player/create:
 *   post:
 *     security:
 *       - BearerAuth: []
 *     summary: Crate a match in single player mode
 *     tags: [Match]
 *     responses:
 *       200:
 *         description: A match after create in single mode.
 *         content:
 *           application/json:
 *             schema:
 *               $ref: '#/components/schemas/MatchDto'
 * 
 * /match/save-result/{matchId}:
 *   put:
 *     security:
 *       - BearerAuth: []
 *     summary: Save player match result
 *     tags: [Match]
 *     parameters:
 *       - in: path
 *         name: matchId
 *         schema:
 *           type: string
 *         required: true
 *         description: ID of the match
 * 
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *           schema:
 *             $ref: '#/components/schemas/SaveResultDto'
 *     responses:
 *       200:
 *         description: Server response message.
 *         content:
 *           application/json:
 *             schema:
 *               $ref: '#/components/schemas/MessageDto'
 *
 *
 */

import { NextFunction, Request, Response } from "express";
import { Exception } from "../middlewares/exception";
import * as MatchService from '../services/match.service'
import { SaveMatchResultDto } from "../dto/match/save-result.dto";

export async function getMatchById(request: Request, response: Response, next: NextFunction) {
    try {
        const userId = request.user?.id
        if(!userId) { throw new Exception({ message: 'Unauthorized', statusCode: 401 }) }

        const matchId = request.params.matchId
        const result = await MatchService.getMatchById(matchId)
        return response.status(200).json(result)

    } catch (error) {
        next(error)    
    }
}

export async function getMatchHistory(request: Request, response: Response, next: NextFunction) {
    try {
        const userId = request.user?.id
        if(!userId) { throw new Exception({ message: 'Unauthorized', statusCode: 401 }) }

        const result = await MatchService.getMatchHistory(userId)
        return response.status(200).json(result)

    } catch (error) {
        next(error)    
    }
}

export async function createSingleMatch(request: Request, response: Response, next: NextFunction) {
    try {
        const userId = request.user?.id
        if(!userId) { throw new Exception({ message: 'Unauthorized', statusCode: 401 }) }

        const result = await MatchService.createSingleMatch(userId)
        return response.status(200).json(result)

    } catch (error) {
        next(error)    
    }
}

export async function saveMatchResult(request: Request, response: Response, next: NextFunction) {
    try {
        const userId = request.user?.id
        if(!userId) { throw new Exception({ message: 'Unauthorized', statusCode: 401 }) }

        const matchId = request.params.matchId
        const body: SaveMatchResultDto = request.body;
        const result = await MatchService.saveMatchHistory(matchId, userId, body)

        return response.status(200).json(result)
    } catch (error) {
        next(error)    
    }
}