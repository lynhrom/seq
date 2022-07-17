# Libraries required
npm install react react-dom next

npm install bootstrap

npm install axios

npm install moment

npm install @microsoft/signalr@next

# Create an image React app on Docker
docker apply -t lynhrom/client .

# Run our React app
npm run dev

# Libraries required for tests 
npm install --save-dev @testing-library/react @testing-library/jest-dom

npm install --save-dev jest jest-environment-jsdom

npm install --save-dev babel-jest jest-css-modules

npm test


