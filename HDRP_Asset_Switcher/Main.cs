using UnityEngine;
using UnityEngine.Rendering;
using UnityModManagerNet;
using Object = UnityEngine.Object;

namespace HDRP_Asset_Switcher
{
    public class Main
    {

        public AssetBundle HDRPAssetBundle { get; private set; }
        public static RenderPipelineAsset HDRPAsset_SDT { get; private set; }
        public static RenderPipelineAsset DefaultHDRPAsset { get; set; }

        void Awake() // As soon as the mod loads, back up the default HDRP asset
        {
            if (Main.HDRPAsset_SDT != null)
            {
                Debug.Log("Storing Default HDRP Asset");

                DefaultHDRPAsset = GraphicsSettings.renderPipelineAsset; // saves off a copy so you can "revert"                                                                              //Your logic to assign that asset over
            }
        }

        void Start()
        {

        }

        public static bool Load(UnityModManager.ModEntry mod_entry)
        {
            mod_entry.OnToggle = OnToggle;

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

                Debug.Log("OnToggle Value False");

                var rm = Object.FindObjectOfType<SwitcherManager>();
                if (rm != null)
                    Object.Destroy(rm.gameObject);
            }
            return true;
        }
    }
}