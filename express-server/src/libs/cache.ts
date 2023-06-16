import { client } from "..";

export async function storeValidationCode(email: string, code: string) {
    await client.set(email, code);
    await client.expire(email, 15 * 60)
}

export async function checkValidationCode(email: string, code: string) {
    const storedCode = await client.get(email);
    const result = code === storedCode
    if (result) {
        await client.del(email)
    }
    return result
}

export async function saveLoginSession(email: string) {
    const sessionsCache = await client.get("login-session")
    if (sessionsCache) {
        const sessions: string[] = JSON.parse(sessionsCache)
        sessions.push(email)

        await client.set("login-session", JSON.stringify(sessions));
        await client.expire("login-session", 24 * 60 * 60)

        return;
    }

    await client.set("login-session", JSON.stringify([email]))
    await client.expire("login-session", 24 * 60 * 60)
}

export async function checkLoginSession(email: string) {
    const sessionsCache = await client.get("login-session")
    if (sessionsCache) {
        const sessions: string[] = JSON.parse(sessionsCache)
        const found = sessions.includes(email)
        if (found) { return true }

        return false
    }

    return false
}

export async function clearLoginSession(email: string) {
    const sessionsCache = await client.get("login-session")
    if (sessionsCache) {
        const sessions: string[] = JSON.parse(sessionsCache)
        const found = sessions.findIndex(v => v === email)
        sessions.splice(found, 1);

        await client.set("login-session", JSON.stringify(sessions));

        if (found) { return true }
    }
}