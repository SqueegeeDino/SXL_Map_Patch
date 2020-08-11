using UnityEngine;
using UnityModManagerNet;
using UnityEngine.Rendering;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HDRP_Asset_Switcher
{

    public class SwitcherManager : MonoBehaviour
    {

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F10))
            {
                Debug.Log("[HDRP_Switcher] F10 key was pressed.");
                {
                    GraphicsSettings.renderPipelineAsset = Main.HDRPAsset_SDT;
                    Debug.Log("[HDRP_Switcher] Active render pipeline asset is: " + GraphicsSettings.renderPipelineAsset.name);
                }
            }
            if (Input.GetKeyDown(KeyCode.F11))
            {
                Debug.Log("[HDRP_Switcher] Active render pipeline asset is: " + GraphicsSettings.renderPipelineAsset.name);
                Debug.Log("[HDRP_Switcher] F11 Key was pressed");
            }
        }

        public static void LoadAssets()
        {
            Main.HDRPAssetBundle = AssetBundle.LoadFromMemory(ExtractResource("HDRP_Asset_Switcher.hdrpassets"));
            Main.HDRPAsset_SDT = Main.HDRPAssetBundle.LoadAllAssets<RenderPipelineAsset>()?.FirstOrDefault();
            Main.HDRPAssetBundle.Unload(false);

            Debug.Log("[HDRP_Switcher] LoadAssets runn");
        }

        public static byte[] ExtractResource(string filename)
        {
            Assembly a = Assembly.GetExecutingAssembly();
            using (var resFilestream = a.GetManifestResourceStream(filename))
            {
                if (resFilestream == null) return null;
                byte[] ba = new byte[resFilestream.Length];
                resFilestream.Read(ba, 0, ba.Length);
                return ba;
            }
        }

    }
}