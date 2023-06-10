/**
 * @swagger
 * components:
 *   schemas:
 *     RegisterDto:
 *       type: object
 *       properties:
 *         email:
 *           type: string
 *           format: email
 *           description: The user's email address
 *         password:
 *           type: string
 *           description: The user's password
 */

export interface RegisterDto {
    email: string;
    password: string;
}