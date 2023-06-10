import express from 'express';
import authRoutes from './auth'
import userRoutes from './user'
import { AuthGuard } from '../middlewares/jwt';

const router = express.Router();

router.use('/auth', authRoutes)
router.use('/user', AuthGuard, userRoutes)

export default router;