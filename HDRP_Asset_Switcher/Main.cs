using UnityEngine;
using UnityEngine.Rendering;
using UnityModManagerNet;
using Object = UnityEngine.Object;
using HarmonyLib;
using UnityEngine.Rendering.HighDefinition;
using System.Reflection;
using System.Linq;

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

        void Start()
        {

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

                Main.MapPatch_Bundle = AssetBundle.LoadFromMemory(ExtractResource("MapPatch.mappatch_bundle"));
                Main.MapPatch_UI = Main.MapPatch_Bundle.LoadAllAssets<GameObject>()?.FirstOrDefault();
                Main.MapPatch_Bundle.Unload(false);

                UnityModManager.Logger.Log("[Map Patch] (Main) LoadAssets_ui run");
                Debug.Log("[MapPatch] (Main) LoadAssets_ui run");

                GameObject newMenuObject = GameObject.Instantiate(Main.MapPatch_UI);

                UnityModManager.Logger.Log("[MapPatch] (Main) UI Object Created");

                MapPatch_UI.AddComponent<MapPatchManager>();
                MapPatch_UI.AddComponent<UIManager>();

                UnityModManager.Logger.Log("[MapPatch] (Main) Components attached");

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

        public void ChildFinder()
        {
            //Finds and assigns the child of the player named "Gun".
            MapPatch_UI.transform.Find("MapPatch_EventSystem").gameObject;

            //If the child was found.
            if (MapPatch_UI  != null)
            {
                //Find the child named "ammo" of the gameobject "magazine" (magazine is a child of "gun").
                ammo = gun.transform.Find("magazine/ammo");
            }
            else Debug.Log("No child with the name 'Gun' attached to the player");
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