import { NextFunction, Request, Response } from "express"
import * as jwt from "jsonwebtoken"

declare global {
    namespace Express {
        export interface Request {
            user?: RequestUser
        }
    }
}

interface RequestUser {
    id: string
    email: string
}

export function AuthGuard(req: Request, res: Response, next: NextFunction) {
    try {
        const authHeader = req.header('Authorization')
        const token = authHeader && authHeader.split(' ')[1]

        if (!token) return res.sendStatus(401)

        const secret = process.env.JWT_SECRET || "";
        const decoded: any = jwt.verify(token, secret)

        const user: RequestUser = {
            id: decoded.sub,
            email: decoded.email
        }
        req.user = user

        next()
    } catch (error) {
        return res.sendStatus(403)
    }
}