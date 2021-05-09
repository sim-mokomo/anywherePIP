#!/bin/bash

npm install -g agentkeepalive --save
npm install -g npm@7.12.0
npm install -D typescript \
        electron \
        prettier \
        eslint \
        @types/node \
        @typescript-eslint/parser \
        @typescript-eslint/eslint-plugin \
        @electron-forge/cli
npx electron-forge import