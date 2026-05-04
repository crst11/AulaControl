FROM node:22-alpine AS build
WORKDIR /app

COPY package*.json ./
RUN npm install

COPY . .
RUN npm run build -- --configuration production

RUN mkdir -p /out && cp -r $(dirname $(find /app/dist -name index.html | head -n 1))/* /out/

FROM nginx:alpine
COPY nginx.conf /etc/nginx/conf.d/default.conf
COPY --from=build /out /usr/share/nginx/html

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]