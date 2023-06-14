/**
 * @swagger
 * components:
 *   schemas:
 *     MatchResultDto:
 *       type: object
 *       properties:
 *         player:
 *           $ref: '#/components/schemas/UserDto'
 *           description: The player join in the match
 *         score:
 *           type: number
 *           description: The player's score
 * 
 *     MatchDto:
 *       type: object
 *       properties:
 *         _id:
 *           type: string
 *           description: The user's ID
 *         players:
 *           type: array
 *           items:
 *               $ref: '#/components/schemas/UserDto'
 *           description: The player's id
 * 
 *         results:
 *           type: array
 *           items:
 *               $ref: '#/components/schemas/MatchResultDto'
 *           description: Player's result
 * 
 *         createdAt:
 *           type: string
 *           format: date-time
 *           description: The user's creation date and time
 */

import { Match } from "../../models/match.model"

export interface MatchDto extends Match {}
