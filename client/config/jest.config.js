module.exports = {
    rootDir: '../',
    setupFilesAfterEnv: ["./config/jest.setup.js"],
    testEnvironment: 'jest-environment-jsdom',
    transform: {
        '\\.js$': ['babel-jest', { configFile: './config/.babelrc' }]
    },
    moduleNameMapper: {
        "\\.(css|less|scss|sss|styl)$": "./node_modules/jest-css-modules"
    },
    verbose: true,
    collectCoverage: true,
    coveragePathIgnorePatterns: [ "./tests/test-utils.js","./tests/mock-hub.js" ]
};