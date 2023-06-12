import express from 'express';
import authRoutes from './auth'
import userRoutes from './user'
import matchRoutes from './match'
import { AuthGuard } from '../middlewares/jwt';

const router = express.Router();

router.use('/auth', authRoutes)
router.use('/user', AuthGuard, userRoutes)
router.use('/match',AuthGuard, matchRoutes)

export default router;