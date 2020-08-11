using UnityEngine;
using UnityEngine.Rendering;
using UnityModManagerNet;
using Object = UnityEngine.Object;

namespace HDRP_Asset_Switcher
{
    public class Main
    {
        public static AssetBundle HDRPAssetBundle { get; set; }
        public static RenderPipelineAsset HDRPAsset_SDT { get; set; }
        public static RenderPipelineAsset DefaultHDRPAsset { get; set; }

        void Awake() // As soon as the mod loads, back up the default HDRP asset
        {
            if (HDRPAsset_SDT != null)
            {
                Debug.Log("[HDRP_Switcher] Storing Default HDRP Asset");

                DefaultHDRPAsset = GraphicsSettings.renderPipelineAsset; // saves off a copy so you can "revert"                                                                              //Your logic to assign that asset over
            }
        }

        void Start()
        {

        }

        // Buncha stolen code from Bill-O-Rumble

        public static bool Load(UnityModManager.ModEntry mod_entry)
        {
            mod_entry.OnToggle = OnToggle;

            SwitcherManager.LoadAssets();

            Debug.Log("[HDRP_Switcher] Assets Loaded");

            return true;
        }

        private static bool OnToggle(UnityModManager.ModEntry mod_entry, bool value)
        {
            if (value)
            {
                var go = new GameObject("SwitcherManager", typeof(SwitcherManager));


                Object.DontDestroyOnLoad(go);
            }
            else
            {

                Debug.Log("[HDRP_Switcher] OnToggle Value False");

                var rm = Object.FindObjectOfType<SwitcherManager>();
                if (rm != null)
                    Object.Destroy(rm.gameObject);
            }
            return true;
        }
    }
}