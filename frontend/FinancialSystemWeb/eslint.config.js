import js from "@eslint/js";
import globals from "globals";
import tseslint from "typescript-eslint";
import pluginReact from "eslint-plugin-react";
import { defineConfig } from "eslint/config";

export default defineConfig([
  {
    files: ["**/*.{js,mjs,cjs,ts,mts,cts,jsx,tsx}"],
    languageOptions: {
      parser: tseslint.parser,
      parserOptions: {
        ecmaVersion: 2021,
        sourceType: "module",
        ecmaFeatures: { jsx: true },
        // project: "./tsconfig.eslint.json", // opcional si usas paths o reglas estrictas
      },
      globals: globals.browser,
    },
    plugins: {
      js,
      "@typescript-eslint": tseslint.plugin,
      react: pluginReact,
    },
    settings: {
      react: {
        version: "detect",
      },
    },
    rules: {
      "react/react-in-jsx-scope": "off",
      "react/prop-types": "off",

      // Reglas para auto-fix
      "semi": ["error", "always"],                  // punto y coma obligatorio
      "quotes": ["error", "double"],                // comillas dobles
      "indent": ["error", 2],                        // indentación 2 espacios
      "no-trailing-spaces": "error",                 // no espacios al final de línea
      "eol-last": ["error", "always"],               // salto línea al final archivo
      "comma-dangle": ["error", "always-multiline"], // coma final en objetos multilínea
    },
    extends: [
      "js/recommended",
      tseslint.configs.recommended,
      pluginReact.configs.flat.recommended,
    ],
  },
]);
