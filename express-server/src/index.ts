import express, { Express, NextFunction, Request, Response } from 'express';
import dotenv from 'dotenv';
import * as redis from 'redis'
import swaggerUi from 'swagger-ui-express'
import swaggerJsdoc from 'swagger-jsdoc'
import { options as swaggerOptions } from './configs/swagger';
import mongoose from 'mongoose';
import bodyParser from 'body-parser';
import routers from './routers';
import { GlobalException } from './middlewares/exception';
import cors from 'cors'
import "./sockets/server"

dotenv.config();
const port = process.env.PORT;
const redisHost = process.env.REDIS_HOST;
const redisPort = process.env.REDIS_PORT;

const app: Express = express();

app.use(cors())
app.use(bodyParser.json())

// Config redis connection
export const client = redis.createClient({ url: `redis://${redisHost}:${redisPort}` })
client
    .connect()
    .then(() => console.log('Redis connected'))
    .catch(() => console.log('Connect to redis failed !'))

// Config mongo connection
const connectionString = process.env.MONGO_URI as string
mongoose
  .connect(connectionString)
  .then(() => console.log(`Database connected`))
  .catch(() => console.log(`Connect to database failed`))

// Config swagger document
const specs = swaggerJsdoc(swaggerOptions);
app.use('/api', swaggerUi.serve, swaggerUi.setup(specs));

// Routers
app.use('/', routers)

// Add an error handling middleware
app.use(GlobalException);

app.listen(port, () => {
  console.log(`⚡️[server]: Server is running at http://localhost:${port}`);
});