/**
 * @swagger
 * components:
 *   schemas:
 *     UserDto:
 *       type: object
 *       properties:
 *         _id: 
 *           type: string
 *         email:
 *           type: string
 *           format: email
 *           description: The user's email address
 *         username:
 *           type: string
 *           description: The username of that account, was generated automatically
 *         createdAt:
 *           type: string
 *           format: date-time
 *           description: The created date of account
 */

export interface UserDto {
    email: string
    username: string
    createdAt: Date
}