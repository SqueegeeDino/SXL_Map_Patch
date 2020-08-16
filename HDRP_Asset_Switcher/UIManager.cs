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

        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                Debug.Log("[MapPatch] D key pressed.");
                {
                    
                }
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                Debug.Log("[MapPatch] S Key pressed");
                {

                }
            }
        }


    }
}
