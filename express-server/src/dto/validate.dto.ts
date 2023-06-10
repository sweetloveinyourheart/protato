/**
 * @swagger
 * components:
 *   schemas:
 *     ValidateDto:
 *       type: object
 *       properties:
 *         email:
 *           type: string
 *           description: The email of user
 *         code:
 *           type: string
 *           description: The validation code received from email
 */


export interface ValidateDto {
    email: string
    code: string
}