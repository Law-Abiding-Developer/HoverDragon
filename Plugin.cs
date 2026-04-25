using System.Collections;
using System.IO;
using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using Nautilus.Assets;
using Nautilus.Handlers;
using UnityEngine;

namespace HoverDragon;

[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
[BepInDependency("com.snmodding.nautilus")]
public class Plugin : BaseUnityPlugin
{
    public new static ManualLogSource Logger { get; private set; }

    private static Assembly Assembly { get; } = Assembly.GetExecutingAssembly();
    
    public static AssetBundle Bundle { get; private set; }

    private void Awake()
    {
        Logger = base.Logger;

        /* ways to register patches:
         * Option 1:
         * Harmony.CreateAndPatchAll(Assembly OR typeof(PATCHES CLASS HERE), PluginInfo.PLUGIN_GUID);
         * OPTION 2:
         * var harmony = new Harmony(PluginInfo.PLUGIN_GUID);
         * harmony.PatchAll(Assembly or typeof(PATCHES CLASS HERE);
         */
        WaitScreenHandler.RegisterEarlyAsyncLoadTask("HoverDragon", LoadBundle, "LOADING HOVER DRAGON");
        ConsoleCommandsHandler.RegisterConsoleCommand("echo", typeof(ConsoleCommands), nameof(ConsoleCommands.Echo), new []{typeof(string[])});
    }

    private static IEnumerator LoadBundle(WaitScreenHandler.WaitScreenTask task)
    {
        var assetBundleRequest = AssetBundle.LoadFromFileAsync(Path.Combine(Path.GetDirectoryName(Assembly.Location), "Assets", "ddohassetbundleedition"));
        yield return assetBundleRequest;
        Bundle = assetBundleRequest.assetBundle;
        var pI1 = PrefabInfo.WithTechType("DragonFish")
            .WithIcon(Bundle.LoadAsset<Sprite>("DragonFish_Icon"))
            .WithSizeInInventory(new(2,2));
        new HoverDragon(pI1).Register();
    }
}