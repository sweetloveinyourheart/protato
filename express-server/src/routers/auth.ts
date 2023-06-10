import express from 'express';
import * as authControllers from '../controllers/auth.controller';
import { body } from 'express-validator';
const router = express.Router();

router.post(
    '/login',
    body('email').notEmpty().isEmail().withMessage('Email mus be a valid email address'),
    body('password').notEmpty().isLength({ min: 6, max: 50 }).withMessage('Password must have at least 3 - 50 characters'),
    authControllers.login
);

router.post(
    '/register',
    body('email').notEmpty().isEmail().withMessage('Email mus be a valid email address'),
    body('password').notEmpty().isLength({ min: 6, max: 50 }).withMessage('Password must have at least 3 - 50 characters'),
    authControllers.register
);

router.post(
    '/verify-account',
    body('email').notEmpty().isEmail().withMessage('Email mus be a valid email address'),
    body('code').notEmpty().isLength({ min: 6, max: 6 }).withMessage('Validation code must have 6 character'),
    authControllers.validateAccount
)

export default router;
