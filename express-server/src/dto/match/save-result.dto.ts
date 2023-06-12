/**
 * @swagger
 * components:
 *   schemas:
 *     SaveResultDto:
 *       type: object
 *       properties:
 *         score:
 *           type: number
 *           description: The player's score
 *         victory:
 *           type: boolean
 *           description: Match victory
 */


export interface SaveMatchResultDto {
    score: number
    victory: boolean
}