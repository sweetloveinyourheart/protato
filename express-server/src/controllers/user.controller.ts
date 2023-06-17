/**
 * @swagger
 * tags:
 *   name: User
 *   description: The user managing API

 * /api/user/profile:
 *   get:
 *     security:
 *       - BearerAuth: []
 *     summary: Get user profile
 *     tags: [User]
 *     responses:
 *       200:
 *         description: Account information.
 *         content:
 *           application/json:
 *             schema:
 *               $ref: '#/components/schemas/UserDto'
 *
 */

import { NextFunction, Request, Response } from "express";
import { Exception } from "../middlewares/exception";
import * as UserService from '../services/user.service'

export async function getProfile(request: Request, response: Response, next: NextFunction) {
    try {
        const userId = request.user?.id
        if(!userId) { throw new Exception({ message: 'Unauthorized', statusCode: 401 }) }

        const userData = await UserService.getProfile(userId)
        return response.status(200).json(userData)
    } catch (error) {
        next(error)    
    }
}