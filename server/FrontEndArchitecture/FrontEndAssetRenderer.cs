namespace CuratedArt.FrontEndArchitecture
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text.Json;
    using System.Text.Json.Nodes;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;

    public class FrontEndAssetRenderer
    {
        private readonly List<string> _jsFiles;
        private readonly List<string> _cssFiles;

        public FrontEndAssetRenderer(IWebHostEnvironment webHostEnvironment)
        {
            var manifestSource = File.ReadAllText(Path.Combine(webHostEnvironment.ContentRootPath, "wwwroot/assets-manifest.json"));
            var jsonObject = JsonNode.Parse(manifestSource);
            var assetsNode = jsonObject?["entrypoints"]?["main"]?["assets"];

            _cssFiles = assetsNode?["css"].Deserialize<List<string>>() ?? new List<string>();
            _jsFiles = assetsNode?["js"].Deserialize<List<string>>() ?? new List<string>();
        }

        public IEnumerable<string> GetScripts(HttpRequest request) => _jsFiles.Select(jsFile => GetModule(request, jsFile));

        public IEnumerable<string> GetStyles(HttpRequest request) => _cssFiles.Select(cssFile => GetModule(request, cssFile));

        private static string GetModule(HttpRequest request, string fileName) => $"{request.PathBase}/{fileName}";
    }
}