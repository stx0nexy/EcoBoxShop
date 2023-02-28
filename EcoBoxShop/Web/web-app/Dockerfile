FROM node:alpine
WORKDIR /app

COPY package.json /app

RUN npm install

COPY . .

# required for docker desktop port mapping

CMD ["npm","run", "start"]