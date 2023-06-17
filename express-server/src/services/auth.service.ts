import { LoginDto, TokenResponse } from "../dto/auth/login.dto";
import { MessageDto } from "../dto/message.dto";
import { RegisterDto } from "../dto/auth/register.dto";
import { ValidateDto } from "../dto/auth/validate.dto";
import { checkLoginSession, checkValidationCode, clearLoginSession, saveLoginSession, storeValidationCode } from "../libs/cache";
import { generateUsername, generateValidationCode } from "../libs/generator";
import { sendMail } from "../libs/mailer";
import { comparePassword, hashPassword } from "../libs/password";
import { Exception } from "../middlewares/exception";
import UserModel from "../models/user.model";
import jwt from 'jsonwebtoken'

export async function register(account: RegisterDto): Promise<MessageDto> {
    const isExist = await UserModel.findOne({ email: account.email })
    if (isExist) {
        throw new Exception({ message: 'Email is already exist', statusCode: 400 })
    }

    const hashedPassword = await hashPassword(account.password)
    const username = generateUsername()

    // Send validation code to email
    const validationCode = generateValidationCode()
    const mail = {
        subject: 'Validate your Protato account',
        content: `Your validation code is ${validationCode}`
    }

    await sendMail(account.email, mail)
    await storeValidationCode(account.email, validationCode)

    // Create and save new account
    const newAccount = await UserModel.create({
        email: account.email,
        password: hashedPassword,
        username
    })

    await newAccount.save()

    return { message: 'Register successfully' }
}

export async function validateAccount(data: ValidateDto): Promise<MessageDto> {
    const account = await UserModel.findOne({ email: data.email })
    if (!account) {
        throw new Exception({ message: 'User not found', statusCode: 400 })
    }

    if (account.isVerified) {
        throw new Exception({ message: 'This account was verified!', statusCode: 400 })
    }

    const isValidCode = await checkValidationCode(data.email, data.code)
    if (!isValidCode) {
        throw new Exception({ message: 'Validation code not valid', statusCode: 403 })
    }

    await UserModel.findOneAndUpdate({ email: data.email }, { isVerified: true })

    return { message: 'Your account has been verified' }
}

export async function login(user: LoginDto): Promise<TokenResponse> {
    const account = await UserModel.findOne({ email: user.email })
    if (!account) {
        throw new Exception({ message: 'User not found', statusCode: 400 })
    }

    if (!account.isVerified) {
        throw new Exception({ message: 'This account is not verified yet!', statusCode: 401 })
    }

    const isValidPass = await comparePassword(user.password, account.password)
    if (!isValidPass) {
        throw new Exception({ message: 'Username or password is not valid', statusCode: 401 })
    }

    // const isInAnotherSession = await checkLoginSession(user.email)
    // if(isInAnotherSession) {
    //     throw new Exception({ message: "You are not allowed to log in from multiple browsers.", statusCode: 403 })
    // } else {
    //     saveLoginSession(user.email);
    // }

    // generate access token
    const payload = {
        sub: account.id,
        email: account.email
    }

    const JWT_SECRET = process.env.JWT_SECRET as string
    const accessToken = jwt.sign(payload, JWT_SECRET, { expiresIn: '1d' })

    return { accessToken }
}

export async function logout(email: string): Promise<MessageDto> {
    const isActive = await checkLoginSession(email)
    if(isActive) {
        clearLoginSession(email)
    }

    return { message: "User logout successfully !" }
}