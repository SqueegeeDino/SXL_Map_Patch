using UnityEngine;
using UnityModManagerNet;
using UnityEngine.Rendering;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine.SceneManagement;
using HarmonyLib;

namespace MapPatch
{
    public class MapPatchManager : MonoBehaviour
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
        public bool CustomHDRP = false;

        public static void LoadAssets()
        {
            Main.MapPatch_Bundle = AssetBundle.LoadFromMemory(ExtractResource("MapPatch.mappatch_bundle"));
            Main.HDRPAsset_SDT = Main.MapPatch_Bundle.LoadAllAssets<RenderPipelineAsset>()?.FirstOrDefault();
            Main.MapPatch_Bundle.Unload(false);

            UnityModManager.Logger.Log("[Map Patch] LoadAssets run");
            Debug.Log("[MapPatch] LoadAssets run");
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F9))
            {
                UnityModManager.Logger.Log("[MapPatch] F9 key pressed.");
                if (CustomHDRP == false)
                {
                    GraphicsSettings.renderPipelineAsset = Main.HDRPAsset_SDT;
                    UnityModManager.Logger.Log("[MapPatch] Active render pipeline asset is: " + GraphicsSettings.renderPipelineAsset.name);
                    CustomHDRP = true;
                }
                else
                {
                    GraphicsSettings.renderPipelineAsset = Main.DefaultHDRPAsset;
                    UnityModManager.Logger.Log("[MapPatch] Active render pipeline asset is: " + GraphicsSettings.renderPipelineAsset.name);
                    CustomHDRP = false;
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