import { Exception } from "../middlewares/exception";
import UserModel from "../models/user.model";

export async function getProfile(userId: string) {
    const user = await UserModel.findById(userId).select({ password: 0, isVerified: 0 })
    if(!user) {
        throw new Exception({ message: "Profile not found", statusCode: 404 })
    }

    return user
}