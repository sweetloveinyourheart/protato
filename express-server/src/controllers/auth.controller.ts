/**
 * @swagger
 * tags:
 *   name: Auth
 *   description: The auth managing API
 * /auth/register:
 *   post:
 *     summary: Register new account
 *     tags: [Auth]
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *           schema:
 *             $ref: '#/components/schemas/RegisterDto'
 *     responses:
 *       201:
 *         description: Create result in message.
 *         content:
 *           application/json:
 *             schema:
 *               $ref: '#/components/schemas/MessageDto'
 *
 * /auth/verify-account:
 *   post:
 *     summary: Verify account
 *     tags: [Auth]
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *           schema:
 *             $ref: '#/components/schemas/ValidateDto'
 *     responses:
 *       200:
 *         description: Validate result in message.
 *         content:
 *           application/json:
 *             schema:
 *               $ref: '#/components/schemas/MessageDto'
 * 
 * /auth/login:
 *   post:
 *     summary: Login to your account
 *     tags: [Auth]
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *           schema:
 *             $ref: '#/components/schemas/LoginDto'
 *     responses:
 *       200:
 *         description: Validate result in message.
 *         content:
 *           application/json:
 *             schema:
 *               $ref: '#/components/schemas/TokenResponse'
 */

import { NextFunction, Request, Response } from "express";
import * as authServices from '../services/auth.service'
import { RegisterDto } from "../dto/register.dto";
import { validationResult } from "express-validator";
import { ValidateDto } from "../dto/validate.dto";
import { LoginDto } from "../dto/login.dto";

export async function login(request: Request, response: Response, next: NextFunction) {
  try {
    // Check for validation errors
    const errors = validationResult(request);
    if (!errors.isEmpty()) {
      return response.status(400).json({ errors: errors.array() });
    }

    const body: LoginDto = request.body
    const token = await authServices.login(body)

    return response.status(200).json(token)
  } catch (error) {
    next(error)
  }
}

export async function register(request: Request, response: Response, next: NextFunction) {
  try {
    // Check for validation errors
    const errors = validationResult(request);
    if (!errors.isEmpty()) {
      return response.status(400).json({ errors: errors.array() });
    }

    const body: RegisterDto = request.body
    const msg = await authServices.register(body)

    return response.status(201).json(msg)
  } catch (error) {
    return next(error)
  }
}

export async function validateAccount(request: Request, response: Response, next: NextFunction) {
  try {
    // Check for validation errors
    const errors = validationResult(request);
    if (!errors.isEmpty()) {
      return response.status(400).json({ errors: errors.array() });
    }

    const body: ValidateDto = request.body
    const msg = await authServices.validateAccount(body)

    return response.status(200).json(msg)
  } catch (error) {
    return next(error)
  }
}