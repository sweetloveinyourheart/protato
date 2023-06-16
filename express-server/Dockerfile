FROM node:18

WORKDIR /server

COPY package*.json yarn.lock ./

RUN yarn install --only=production

COPY . .

RUN yarn build

EXPOSE 6789

CMD ["yarn", "start"]
