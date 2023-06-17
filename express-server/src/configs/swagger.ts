import { Options } from 'swagger-jsdoc'

export const options: Options = {
    definition: {
        openapi: "3.0.0",
        info: {
            title: "Protato Express API with Swagger",
            version: "0.1.0",
            description:
                "CRUD API application made with Express and documented with Swagger",
            contact: {
                name: "Protato Game",
                url: "https://sweetdev.fun/"
            },
        },
        components: {
            securitySchemes: {
                BearerAuth: {
                    type: "http",
                    scheme: "bearer",
                    bearerFormat: "JWT"
                }
            }
        }
    },
    apis: ['./**/*.ts'], // for dev mode
    // apis: ['./**/*.js'], // for build
};
