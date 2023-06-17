FROM node:18

WORKDIR /protato-client

COPY package.json .

RUN yarn install

COPY . .

EXPOSE 3000

CMD [ "yarn", "start" ]