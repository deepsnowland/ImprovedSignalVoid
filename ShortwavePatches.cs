using Il2Cpp;
using MelonLoader;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using HarmonyLib;
using System.Collections;

namespace ImprovedSignalVoid
{
    internal class ShortwavePatches : MonoBehaviour
    {

        [HarmonyPatch(typeof(PlayerManager), nameof(PlayerManager.Update))]

        internal class ShortwaveFPHActivator
        {
            private static void Postfix(PlayerManager __instance)
            {

                GameObject rig = GameObject.Find("CHARACTER_FPSPlayer/NEW_FPHand_Rig/GAME_DATA/Origin/HipJoint/Chest_Joint/Camera_Weapon_Offset/Shoulder_Joint/Shoulder_Joint_Offset/Right_Shoulder_Joint_Offset/RightClavJoint/RightShoulderJoint/RightElbowJoint/RightWristJoint/RightPalm/right_prop_point");
                GameObject shortwaveFPH = rig.transform.GetChild(16).gameObject;

                if (__instance.m_Gear.name.Contains("GEAR_SignalVoid"))
                {
                    if (__instance.m_InspectModeActive)
                    {
                        shortwaveFPH.SetActive(true);
                    }
                }

            }
        }

        [HarmonyPatch(typeof(PlayerManager), nameof(PlayerManager.ProcessPickupItemInteraction))]

        internal class ShortwaveFPHDeactivator
        {

            private static void Postfix(PlayerManager __instance)
            {

                GameObject rig = GameObject.Find("CHARACTER_FPSPlayer/NEW_FPHand_Rig/GAME_DATA/Origin/HipJoint/Chest_Joint/Camera_Weapon_Offset/Shoulder_Joint/Shoulder_Joint_Offset/Right_Shoulder_Joint_Offset/RightClavJoint/RightShoulderJoint/RightElbowJoint/RightWristJoint/RightPalm/right_prop_point");
                GameObject shortwaveFPH = rig.transform.GetChild(16).gameObject;

                if (__instance.m_Gear.name.Contains("GEAR_SignalVoid"))
                {
                    //wait 5 seconds
                    MelonCoroutines.Start(DisableShortwaveFPH(shortwaveFPH));
                }
            }

            //unsure
            private static IEnumerator DisableShortwaveFPH(GameObject shortwaveFPH)
            {

                if (shortwaveFPH.active)
                {
                    float waitSeconds = 5f;
                    for (float t = 0f; t < waitSeconds; t += Time.deltaTime) yield return null;
                    shortwaveFPH.SetActive(false);
                }
            }

        }


        [HarmonyPatch(typeof(GameManager), nameof(GameManager.Update))]

        internal class ShortwaveInSceneManager
        {

            private static void Postfix()
            {

                //Removes collider from object so only the collectible can be interacted with
                GameObject shortwaveActual = GameObject.Find("GEAR_HandheldShortwave");
                BoxCollider bc = shortwaveActual.GetComponent<BoxCollider>();
                if (bc) Destroy(bc);

                Inventory inv = GameManager.GetInventoryComponent();
                if (inv.GetBestGearItemWithName("GEAR_HandheldShortwave"))
                {
                    shortwaveActual.SetActive(false);
                }

            }

        }


    }
}
