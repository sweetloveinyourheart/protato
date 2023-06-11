import express, { Express, NextFunction, Request, Response } from 'express';
import dotenv from 'dotenv';
import swaggerUi from 'swagger-ui-express'
import swaggerJsdoc from 'swagger-jsdoc'
import { options as swaggerOptions } from './configs/swagger';
import mongoose from 'mongoose';
import bodyParser from 'body-parser';
import routers from './routers';
import { GlobalException } from './middlewares/exception';
import cors from 'cors'

dotenv.config();

const app: Express = express();
const port = process.env.PORT;

app.use(cors())
app.use(bodyParser.json())

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