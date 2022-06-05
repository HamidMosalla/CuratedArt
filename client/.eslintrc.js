const orders = [
    "react",
    "react-dom",
    "react-router-dom",
    "webpack",
    "css"
];

module.exports = {
    extends: [
     'react-app',
     'airbnb',
     'plugin:@typescript-eslint/recommended',
     'prettier/@typescript-eslint',
    ],
    globals: {
        Atomics: 'readonly',
        SharedArrayBuffer: 'readonly',
      },
    plugins: ["filenames", "react", "import"],
    rules: {
        "jest/no-mocks-import": "off",
        "no-duplicate-imports": ["error"],
        quotes: ["error", "double"],
        "filenames/match-regex": [2, "^[a-z0-9-.]+$", true],
        "@typescript-eslint/no-explicit-any": ["error"],
        "@typescript-eslint/no-non-null-assertion": ["error"],
        "import/no-cycle": ["error"],
        "import/order": [
            "error",
            {
                groups: ["builtin", "external", "internal", "parent", "sibling", "index"],
                pathGroups: orders.map((order) => ({
                    pattern: order,
                    group: "external",
                    position: "before",
                })),
                pathGroupsExcludedImportTypes: orders,
                alphabetize: {
                    order: "asc",
                    caseInsensitive: true,
                },
            },
        ],
    },
    settings: {
        jest: {
            version: 28,
        },
    },
};