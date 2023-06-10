import { NextFunction, Request, Response } from "express";

interface ExceptionPayload {
    message: string,
    statusCode: number
}

export class Exception extends Error {
    statusCode: number

    constructor(payload: ExceptionPayload) {
        super(payload.message);
        this.statusCode = payload.statusCode;
    }
}

export function GlobalException(err: Exception, req: Request, res: Response, next: NextFunction) {
    if (err instanceof Exception) {
        if (!err.statusCode) {
            return res.status(500).json({ message: 'Internal Server Error' })
        }

        // Handle the custom exception by returning a JSON response with the error message and status code
        return res.status(err.statusCode).json({ message: err.message });
    } else {
        // Pass the error to the next error handling middleware
        return next(err);
    }
}