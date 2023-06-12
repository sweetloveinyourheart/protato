/**
 * @swagger
 * components:
 *   schemas:
 *     LoginDto:
 *       type: object
 *       properties:
 *         email:
 *           type: string
 *           format: email
 *           description: User's email
 *         password:
 *           type: string
 *           description: User's password
 *     TokenResponse:
 *       type: object
 *       properties:
 *         accessToken:
 *           type: string
 *           description: Access token use for authentication
 */    

export interface LoginDto {
    email: string
    password: string
}

export interface TokenResponse {
    accessToken: string
}