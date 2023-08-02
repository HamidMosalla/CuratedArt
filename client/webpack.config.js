const path = require('path');
const { CleanWebpackPlugin } = require("clean-webpack-plugin");
const miniCssExtractPlugin = require("mini-css-extract-plugin");
const forkTsCheckerWebpackPlugin = require("fork-ts-checker-webpack-plugin");
const webPackAssetsManifest = require("webpack-assets-manifest");
const node_notifier = require("node-notifier");

let stripAnsi;
import('strip-ansi').then(module => {
  stripAnsi = module.default;
});

const webpack = require('webpack')

function notify(type, message) {
    node_notifier.notify({
        message: message,
        title: type,
        sound: false,
        icon: "none",
        appName: "CuratedArt",
    });
}

function getErrorMessage(message) {
    return "❌ Error: " + message;
}

function getWarningMessage(message) {
    return "⚠️ Warning: " + message;
}

function getSuccessMessage() {
    return "✅ Successful";
}

var WebpackNotifierPlugin = function () {
    this.isLaunchedFromWatch = false;
};

WebpackNotifierPlugin.prototype.compileMessage = function (stats) {
    function findFirstDFS(compilation, key) {
        var match = compilation[key][0];
        if (match) {
            return match;
        }

        var children = compilation.children;
        for (var i = 0; i < children.length; ++i) {
            match = findFirstDFS(children[i], key);
            if (match) {
                return match;
            }
        }
    }

    function formatError(msg) {
        msg = stripAnsi(msg);
        var sourceIndex = msg.lastIndexOf("client\\src");
        return (
            " in " +
            msg.substring(sourceIndex + 17, msg.length - 1).replace(/ +/g, " ")
        );
    }

    var error;
    if (stats.hasErrors()) {
        error = findFirstDFS(stats.compilation, "errors");
    } else if (stats.hasWarnings()) {
        error = findFirstDFS(stats.compilation, "warnings");
    } else {
        return getSuccessMessage();
    }

    if (error.error) {
        return getErrorMessage(formatError(error.error.toString()));
    } else if (error.warning) {
        return getWarningMessage(formatError(error.warning.toString()));
    } else {
        return getWarningMessage(formatError(error.message.toString()));
    }
};

WebpackNotifierPlugin.prototype.apply = function (compiler) {
    compiler.hooks.watchRun.tap("onBuildPlugin", () => {
        this.isLaunchedFromWatch = true;
    });
    compiler.hooks.done.tap({ name: "Notifier" }, (stats) => {
        if (this.isLaunchedFromWatch) {
            notify("Transpilation", this.compileMessage(stats));
        }
    });

    const hooks = forkTsCheckerWebpackPlugin.getCompilerHooks(compiler);
    hooks.issues.tap("onTypeCheckingPlugin", (issues) => {
        if (issues.length > 0) {
            notify("Type Checking", getErrorMessage(issues[0].message));
        } else {
            notify("Type Checking", getSuccessMessage());
        }
    });
};

module.exports = (_, argv) => {
    var options = {
        entry: "./src/mainEntry.tsx",

        output: {
            filename: "[contenthash].js",
            assetModuleFilename: "files/[contenthash].[ext]",
            path: __dirname + "/../server/wwwroot/",
            publicPath: "/",
        },
        resolve: {
            extensions: [".ts", ".tsx", ".js", ".css"],
        },

        module: {
            rules: [
                {
                    test: /\.tsx?$/,
                    use: {
                        loader: "swc-loader",
                        options: {
                            jsc: {
                                parser: {
                                    syntax: "typescript",
                                    decorators: true,
                                },
                            },
                        },
                    },
                },
                {
                    test: /\.css$/,
                    use: [miniCssExtractPlugin.loader, "css-loader"],
                    sideEffects: true,
                },
                {
                    test: /\.(gif|png|jpg|ico|svg)(\?.*)?$/,
                    type: "asset/resource",
                },
                {
                    test: /\.(woff(2)?|eot|ttf|otf|)$/,
                    type: "asset/resource",
                },
            ],
        },

        optimization: {
            splitChunks: {
                maxSize: 250000,
                cacheGroups: {
                    vendor_3: {
                        name: "vendor-3",
                        test: /[\\/]node_modules[\\/](@material-ui)[\\/]/,
                        chunks: "all",
                        enforce: true,
                        priority: 2,
                    },
                    vendor_2: {
                        name: "vendor-2",
                        test: /[\\/]node_modules[\\/](react|react-dom)[\\/]/,
                        chunks: "all",
                        enforce: true,
                        priority: 2,
                    },
                    vendor_1: {
                        name: "vendor-1",
                        test: /[\\/]node_modules[\\/](?!d3|delaunator|internmap)/,
                        chunks: "all",
                        enforce: true,
                        priority: 1,
                    },
                },
            },
        },

        watchOptions: {
            ignored: /node_modules/,
        },

        plugins: [
            new CleanWebpackPlugin({
                dry: false,
                dangerouslyAllowCleanPatternsOutsideProject: true,
                cleanOnceBeforeBuildPatterns: [
                    "**/*",
                    "!manifest.json",
                    "!images/**",
                ],
            }),
            new miniCssExtractPlugin({ filename: "[contenthash].css" }),
            new webPackAssetsManifest({
                entrypoints: true,
                customize(entry) {
                    return entry.key === "entrypoints";
                },
            }),
            new webpack.ProvidePlugin({
                "React": "react",
             }),
        ],
    };

    if (argv.mode === "development") {
        options.devtool = "eval-cheap-module-source-map";
        options.plugins.push(new WebpackNotifierPlugin());
    }

    // Must be called after WebpackNotifierPlugin
    options.plugins.push(new forkTsCheckerWebpackPlugin());

    if (argv.mode === "production" || argv.mode === "none") {
        const CircularDependencyPlugin = require("circular-dependency-plugin");
        const visualizer = require("circular-dependency-plugin-visualizer");

        options.plugins.push(
            new CircularDependencyPlugin(
                visualizer(
                    {
                        failOnError: true,
                        exclude: /node_modules/,
                        cwd: process.cwd(),
                    },
                    {
                        filepath: path.join(
                            __dirname,
                            "circular-dependency-visualization.html"
                        ),
                    }
                )
            )
        );
    }

    if (argv.mode === "none") {
        const BundleAnalyzerPlugin =
            require("webpack-bundle-analyzer").BundleAnalyzerPlugin;
        options.plugins.push(new BundleAnalyzerPlugin());
    }

    return options;
};
