import express from 'express';
import * as userControllers from '../controllers/user.controller';
const router = express.Router();

router.get('/profile', userControllers.getProfile)

export default router;
