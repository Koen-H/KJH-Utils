using System.IO;
using Unity.Plastic.Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class PackageDependencyInstaller
{
    static PackageDependencyInstaller()
    {
        string packagePath = Path.Combine(Application.dataPath, "../Packages/com.kjh.utils/package.json");
        string manifestPath = Path.Combine(Application.dataPath, "../Packages/manifest.json");

        if (File.Exists(packagePath))
        {
            // Read the package.json to find thirdPartyPackages
            string packageContent = File.ReadAllText(packagePath);
            JObject packageJson = JObject.Parse(packageContent);

            // Check for thirdPartyPackages field
            JToken thirdPartyPackages = packageJson["thirdPartyPackages"];
            if (thirdPartyPackages != null)
            {
                // Read the manifest.json content
                string manifestContent = File.ReadAllText(manifestPath);
                JObject manifestJson = JObject.Parse(manifestContent);
                JObject dependencies = (JObject)manifestJson["dependencies"];

                // Loop through each thirdPartyPackage and add to manifest.json if not already present
                foreach (var package in thirdPartyPackages)
                {
                    string packageName = package.Path;
                    string packageUrl = package.First.ToString();

                    if (dependencies[packageName] == null)
                    {
                        dependencies[packageName] = packageUrl;
                        Debug.Log($"Added third-party package: {packageName} from {packageUrl}");
                    }
                }

                // Save the modified manifest.json
                File.WriteAllText(manifestPath, manifestJson.ToString());
            }

            // Add a define symbol if you need to
            AddDefineSymbol("USE_EFLATUN_SCENEREFERENCE");
        }
    }

    static void AddDefineSymbol(string symbol)
    {
        var buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
        var currentSymbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);

        if (!currentSymbols.Contains(symbol))
        {
            PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, currentSymbols + ";" + symbol);
            Debug.Log($"Added scripting define symbol: {symbol}");
        }
    }
}
