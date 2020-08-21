using UnityEngine;
using UnityEngine.Rendering;
using UnityModManagerNet;
using Object = UnityEngine.Object;
using HarmonyLib;
using UnityEngine.Rendering.HighDefinition;
using System.Reflection;

namespace MapPatch
{
    public class Main
    {
        public static AssetBundle MapPatch_Bundle { get; set; }
        public static RenderPipelineAsset HDRPAsset_SDT { get; set; }
        public static RenderPipelineAsset DefaultHDRPAsset { get; set; }

        public static GameObject MapPatch_UI { get; set; }

        private static Harmony Harmony { get; set; }

        void OnEnable() // As soon as the mod loads, back up the default HDRP asset
        {
            Debug.Log("[MapPatch] OnEnable Main");
            if (HDRPAsset_SDT != null)
            {
                Debug.Log("[MapPatch] Storing Default HDRP Asset");

                DefaultHDRPAsset = GraphicsSettings.renderPipelineAsset; // saves off a copy so you can "revert"     
            }
        }

        // Buncha stolen code from Bill-O-Rumble

        public static bool Load(UnityModManager.ModEntry mod_entry)
        {
            mod_entry.OnToggle = OnToggle;

            MapPatchManager.LoadAssets();

            UIManager.LoadAssets_UI();

            Debug.Log("[MapPatch] (Main) Assets Loaded");

            HDRenderPipelineAsset hdrp = GraphicsSettings.renderPipelineAsset as HDRenderPipelineAsset;
            RenderPipelineSettings settings = hdrp.currentPlatformRenderPipelineSettings;
            settings.supportDecals = true;

            if (HDRPAsset_SDT != null)
            {
                Debug.Log("[MapPatch] Storing Default HDRP Asset");

                DefaultHDRPAsset = GraphicsSettings.renderPipelineAsset; // saves off a copy so you can "revert"                                                                              //Your logic to assign that asset over
            }
                return true;
        }

        private static bool OnToggle(UnityModManager.ModEntry mod_entry, bool value)
        {
            if (value)
            {
                var go = new GameObject("MapPatchManager_Object", typeof(MapPatchManager));

                Harmony = new Harmony(mod_entry.Info.Id);
                Harmony.PatchAll(Assembly.GetExecutingAssembly());

                Object.DontDestroyOnLoad(go);
            }
            else
            {

                Debug.Log("Map] OnToggle Value False");

                Harmony.UnpatchAll(Harmony.Id);

                var rm = Object.FindObjectOfType<MapPatchManager>();
                if (rm != null)
                    Object.Destroy(rm.gameObject);
            }
            return true;
        }
    }
}