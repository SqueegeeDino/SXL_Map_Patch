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
    class UIManager : MonoBehaviour
    {
        public static void LoadAssets_UI()
        {
            Main.MapPatch_Bundle = AssetBundle.LoadFromMemory(ExtractResource("MapPatch.mappatch_bundle"));
            Main.MapPatch_UI = Main.MapPatch_Bundle.LoadAllAssets<GameObject>()?.FirstOrDefault();
            Main.MapPatch_Bundle.Unload(false);

            UnityModManager.Logger.Log("[Map Patch] LoadAssets_ui run");
            Debug.Log("[MapPatch] LoadAssets_ui run");
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                Debug.Log("[MapPatch] D key pressed.");
                {
                    GameObject newMenuObject = GameObject.Instantiate(Main.MapPatch_UI);
                }
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                Debug.Log("[MapPatch] S Key pressed");
                {

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
