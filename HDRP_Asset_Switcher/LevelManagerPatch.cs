using HarmonyLib;
using UnityEngine;

namespace HDRP_Asset_Switcher
{
    public class LevelManagerPatch
    {
        [HarmonyPatch(typeof(LevelManager), nameof(LevelManager.LoadLevelScene))]
        public static class LoadLevelScenePatch
        {
            static void Postfix(LevelInfo level)
            {
                Debug.Log("[HDRP_Switcher] Post Fix Hit");
                if (level.isAssetBundle) // custom
                {
                    Debug.Log("[HDRP_Switcher] (Harmony) Custom Map");
                }
                else // built in
                {
                    Debug.Log("[HDRP_Switcher] (Harmony) Official Map");
                }
            }
        }
    }
}