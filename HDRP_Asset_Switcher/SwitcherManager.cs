using UnityEngine;
using UnityModManagerNet;
using UnityEngine.Rendering;

namespace HDRP_Asset_Switcher
{

    public class SwitcherManager : MonoBehaviour
    {
        private bool assetSwap;
        public RenderPipelineAsset customHDRPAsset_1;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F10))
            {
                Debug.Log("[HDRP_Switcher] F10 key was pressed.");
                {
                    GraphicsSettings.renderPipelineAsset = customHDRPAsset_1;
                    Debug.Log("[HDRP_Switcher] Active render pipeline asset is: " + GraphicsSettings.renderPipelineAsset.name);
                }
            }
            if (Input.GetKeyDown(KeyCode.F11))
            {
                Debug.Log("[HDRP_Switcher] Active render pipeline asset is: " + GraphicsSettings.renderPipelineAsset.name);
                Debug.Log("[HDRP_Switcher] F11 Key was pressed");
            }
        }
        public static byte[] ExtractResource("HDRP_Asset_Switcher.HDRPAsset_SDT.asset")
        {
            Assembly a = Assembly.GetExecutingAssembly();
            using (var resFilestream = a.GetManifestResourceStream("HDRP_Asset_Switcher.HDRPAsset_SDT.asset"))
            {
                if (resFilestream == null) return null;
                byte[] ba = new byte[resFilestream.Length];
                resFilestream.Read(ba, 0, ba.Length);
                return ba;
            }
        }
    }
}