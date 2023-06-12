import { body } from 'express-validator';
import { Router } from 'express'
import * as matchController from '../controllers/match.controller'

const router = Router()

router.get(
    '/get-history',
    matchController.getMatchHistory
)

router.get(
    '/get-by-id/:matchId',
    matchController.getMatchById
)

router.post(
    '/single-player/create',
    matchController.createSingleMatch
)

router.put(
    '/save-result/:matchId',
    body('results.*.player').isString().notEmpty().withMessage('Result cannot be empty'),
    body('results.*.score').isNumeric().withMessage('Score must be a number'),
    body('players').isArray({ min: 1 }).withMessage('At least one player is required'),
    matchController.saveMatchResult
);

export default router