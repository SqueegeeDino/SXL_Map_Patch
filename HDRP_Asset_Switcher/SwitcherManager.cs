using UnityEngine;
using UnityModManagerNet;
using UnityEngine.Rendering;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine.SceneManagement;
using HarmonyLib;

namespace HDRP_Asset_Switcher
{
    public class SwitcherManager : MonoBehaviour
    {
        /*
        void OnEnable()
        {
            Debug.Log("[HDRP_Switcher] OnEnable called");
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Debug.Log("[HDRP_Switcher] OnSceneLoaded: " + scene.name);

            AssetBundle bundle = Traverse.Create(LevelManager.Instance).Field("loadedAssetBundle").GetValue() as AssetBundle;

            if (bundle == null)
            {
                Debug.Log("[HDRP_Switcher] Level Manager: Built-in level");
                // built-in level
            }
            else
            {
                // custom level
                Debug.Log("[HDRP_Switcher] Level Manager: Custom Level");
            }
            
            LevelInfo info = LevelManager.Instance.currentLevel;
            if (info.isAssetBundle)
            {
                Debug.Log("[HDRP_Switcher] Custom Map");
                GraphicsSettings.renderPipelineAsset = Main.HDRPAsset_SDT;
                Debug.Log("[HDRP_Switcher] Active render pipeline asset is: " + GraphicsSettings.renderPipelineAsset.name);
            }
            else
            {
                Debug.Log("[HDRP_Switcher] Official Map");
                GraphicsSettings.renderPipelineAsset = Main.DefaultHDRPAsset;
                Debug.Log("[HDRP_Switcher] Active render pipeline asset is: " + GraphicsSettings.renderPipelineAsset.name);
            }
            
        }
    */
        public static void LoadAssets()
        {
            Main.HDRPAssetBundle = AssetBundle.LoadFromMemory(ExtractResource("HDRP_Asset_Switcher.hdrpassets"));
            Main.HDRPAsset_SDT = Main.HDRPAssetBundle.LoadAllAssets<RenderPipelineAsset>()?.FirstOrDefault();
            Main.HDRPAssetBundle.Unload(false);

            Debug.Log("[HDRP_Switcher] LoadAssets run");
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F10))
            {
                Debug.Log("[HDRP_Switcher] F10 key pressed.");
                {
                    GraphicsSettings.renderPipelineAsset = Main.HDRPAsset_SDT;
                    Debug.Log("[HDRP_Switcher] Active render pipeline asset is: " + GraphicsSettings.renderPipelineAsset.name);
                }
            }
            if (Input.GetKeyDown(KeyCode.F11))
            {
                Debug.Log("[HDRP_Switcher] F11 Key pressed");
                {
                    GraphicsSettings.renderPipelineAsset = Main.DefaultHDRPAsset;
                    Debug.Log("[HDRP_Switcher] Active render pipeline asset is: " + GraphicsSettings.renderPipelineAsset.name);
                }
            }
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