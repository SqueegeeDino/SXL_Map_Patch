using UnityEngine;
using UnityModManagerNet;
using Object = UnityEngine.Object;

namespace HDRP_Asset_Switcher
{
    public class Main
    {
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

                Debug.Log("TestMod - Value False");

                var rm = Object.FindObjectOfType<SwitcherManager>();
                if (rm != null)
                    Object.Destroy(rm.gameObject);
            }
            return true;
        }
    }
}