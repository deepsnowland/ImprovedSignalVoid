using Il2Cpp;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using HarmonyLib;

namespace ImprovedSignalVoid
{
    internal class ShortwavePatches : MonoBehaviour
    {

        [HarmonyPatch(typeof(GameManager), nameof(GameManager.Awake))]

        internal class RemoveHandheldActualCollider
        {

            private static void Postfix()
            {

                //Remove Handheld Shortwave GearItem collider
                GameObject handheld = GameObject.Find("GEAR_HandheldShortwave");
                if (handheld != null) Destroy(handheld.GetComponent<BoxCollider>());

            }
        }


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


        [HarmonyPatch(typeof(GameManager), nameof(GameManager.Update))]

        internal class ShortwaveInSceneManager
        {

            private static void Postfix()
            {

                DisableShortwaveFPH();

                Inventory inv = GameManager.GetInventoryComponent();
                if (inv.GetBestGearItemWithName("GEAR_HandheldShortwave"))
                {
                    GameObject shortwaveActual = GameObject.Find("GEAR_HandheldShortwave");
                    shortwaveActual.SetActive(false);
                }

            }

            private static void DisableShortwaveFPH()
            {

                GameObject rig1 = GameObject.Find("CHARACTER_FPSPlayer/NEW_FPHand_Rig/GAME_DATA/Origin/HipJoint/Chest_Joint/Camera_Weapon_Offset/Shoulder_Joint/Shoulder_Joint_Offset/Right_Shoulder_Joint_Offset/RightClavJoint/RightShoulderJoint/RightElbowJoint/RightWristJoint/RightPalm/right_prop_point");
                GameObject rig2 = GameObject.Find("CHARACTER_FPSPlayer/WorldView");

                GameObject shortwaveFPH = rig1.transform.GetChild(16).gameObject;

                bool shortWaveInHand = false;
                bool nonShortWaveInHand = false;

                if (shortwaveFPH.active == true) shortWaveInHand = true;

                for (int i = 0; i < rig2.transform.childCount; i++)
                {
                    GameObject child = rig2.transform.GetChild(i).gameObject;

                    if (child.gameObject.active == true)
                    {

                        if (child.name.Contains("GEAR_"))
                        {

                            if (child.name.Contains("GEAR_HandheldShortwave"))
                            {
                                nonShortWaveInHand = false;
                                break;
                            }

                            nonShortWaveInHand = true;
                        }

                        /*
                        if (child.name.Contains("GEAR_Rifle"))
                        {
                            nonShortWaveInHand2 = true;
                        }
                        else if (child.name.Contains("GEAR_Revolver"))
                        {
                            nonShortWaveInHand2 = true;
                        }
                        else if (child.name.Contains("GEAR_Bow"))
                        {
                            nonShortWaveInHand2 = true;
                        }
                        else if (child.name.Contains("GEAR_Flare"))
                        {
                            nonShortWaveInHand2 = true;
                        }
                        else if (child.name.Contains("GEAR_Stone"))
                        {
                            nonShortWaveInHand2 = true;
                        }
                        else if (child.name.Contains("GEAR_Stim"))
                        {
                            nonShortWaveInHand2 = true;
                        }
                        else if (child.name.Contains("GEAR_Charcoal"))
                        {
                            nonShortWaveInHand2 = true;
                        }*/
                    }
                }

                if (shortWaveInHand && nonShortWaveInHand)
                {
                    shortwaveFPH.SetActive(false);
                }


            }
        }


    }
}
