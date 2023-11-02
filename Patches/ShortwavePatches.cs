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
using ImprovedSignalVoid.GearSpawns;
using Il2CppTLD.Gameplay.Tunable;
using Il2CppNodeCanvas.Tasks.Conditions;
using Il2CppParadoxNotion.Services;
using Main;

namespace ImprovedSignalVoid.Patches.Patches
{
    internal class ShortwavePatches : MonoBehaviour
    {

        [HarmonyPatch(typeof(PlayerManager), nameof(PlayerManager.Update))]

        internal class ShortwaveFPHActivator
        {
            private static void Postfix(PlayerManager __instance)
            {

                GameObject rig = GameObject.Find("CHARACTER_FPSPlayer/NEW_FPHand_Rig/GAME_DATA/Origin/HipJoint/Chest_Joint/Camera_Weapon_Offset/Shoulder_Joint/Shoulder_Joint_Offset/Right_Shoulder_Joint_Offset/RightClavJoint/RightShoulderJoint/RightElbowJoint/RightWristJoint/RightPalm/right_prop_point");

                if(rig == null)
                {
                    return;
                }

                GameObject shortwaveFPH = rig.transform.GetChild(16).gameObject;

                if (__instance == null || __instance.m_Gear == null) return;

                if (__instance.m_Gear.name.Contains("GEAR_SignalVoid"))
                {
                    if (__instance.m_InspectModeActive)
                    {
                        SetTriggerItem(__instance.m_Gear.name);
                        shortwaveFPH.SetActive(true);
                    }
                }

            }

            private static void SetTriggerItem(string gearItem)
            {

                if (gearItem.Contains("Tale1ChiefNote1")) return;

                if (gearItem.Contains("GEAR_SignalVoid"))
                {
                    GameObject sideTale1 = GameObject.Find("sideTale1");

                    if (sideTale1 != null)
                    {
                        MessageRouter msgRouter = sideTale1.GetComponent<MessageRouter>();

                        if (msgRouter == null)
                        {
                            MelonLogger.Msg("Message Router is null");
                            return;
                        }

                        if (msgRouter.listeners.ContainsKey("OnCustomEvent"))
                        {
                            var list = msgRouter.listeners["OnCustomEvent"];

                            Condition_PlayerHasInventoryItems condition = list[0].Cast<Condition_PlayerHasInventoryItems>();

                            condition.requirementsDict["_std"][0].name = gearItem;
                        }  
                    }
                }
            }

        }

        [HarmonyPatch(typeof(PlayerManager), nameof(PlayerManager.ProcessPickupItemInteraction))]

        internal class ShortwaveFPHDeactivator
        {

            private static void Postfix(PlayerManager __instance)
            {

                string currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

                /*
                MelonLogger.Msg("Current scene is: {0}", currentScene);
                MelonLogger.Msg("Player Manager is {0}", __instance);
                MelonLogger.Msg("Player Manager Gear Item is {0}", __instance.m_Gear.name);
                */

                GameObject rig = GameObject.Find("CHARACTER_FPSPlayer/NEW_FPHand_Rig/GAME_DATA/Origin/HipJoint/Chest_Joint/Camera_Weapon_Offset/Shoulder_Joint/Shoulder_Joint_Offset/Right_Shoulder_Joint_Offset/RightClavJoint/RightShoulderJoint/RightElbowJoint/RightWristJoint/RightPalm/right_prop_point");
                GameObject shortwaveFPH = rig.transform.GetChild(16).gameObject;

                if (__instance.m_Gear == null) return;

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

                SaveDataManager sdm = Implementation.sdm;
                if (sdm.HasPickedUpShortwave()) return;

                //Removes collider from object so only the collectible can be interacted with
                GameObject shortwaveActual = GameObject.Find("GEAR_HandheldShortwave");

                if (shortwaveActual == null) return;

                //If the shortwave is a Gear Item in DDOL. Get out of here
                if (shortwaveActual.transform.parent != null) return;

                BoxCollider bc = shortwaveActual.GetComponent<BoxCollider>();
                if (bc) Destroy(bc);

                //remove the shortwave from the scene after picking it up
                Inventory inv = GameManager.GetInventoryComponent();
                if (inv.GetBestGearItemWithName("GEAR_HandheldShortwave"))
                {
                   sdm.Save("true", "hasPickedUpShortwave");
                   shortwaveActual.SetActive(false);
                }
            }
        }

        [HarmonyPatch(typeof(QualitySettingsManager), nameof(QualitySettingsManager.ApplyCurrentQualitySettings))]

        internal class ShortwaveInAirfieldSceneManager
        {


            private static void Postfix()
            {

                string currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

                if (currentScene.Contains("MainMenu") || currentScene == "" || currentScene == null)
                {
                    return;
                }

                SaveDataManager sdm = Implementation.sdm;
                string taleScene = sdm.LoadTaleStartRegion("startRegion");

                for (int i = 0; i < UnityEngine.SceneManagement.SceneManager.sceneCount; i++)
                {
                    UnityEngine.SceneManagement.Scene scene = UnityEngine.SceneManagement.SceneManager.GetSceneAt(i);

                    if (scene.name == "AirfieldRegion_SANDBOX")
                    {

                        GameObject[] rootObjects = scene.GetRootGameObjects();
                        GameObject parent = null;

                        foreach (var obj in rootObjects)
                        {

                            if (obj.name == "Design")
                            {
                                parent = obj;
                                break;
                            }
                        }

                        if (parent != null)
                        {

                            GameObject tales = null;
                            GameObject trackables = null;

                            for (int j = 0; j < parent.transform.childCount; j++)
                            {
                                GameObject child = parent.transform.GetChild(j).gameObject;

                                if (child.name == "Tales")
                                {
                                    tales = child;
                                }
                                else if (child.name == "TrackableHiddenCaches")
                                {
                                    trackables = child;
                                }
                            }

                            if (tales != null)
                            {
                                if (!scene.name.Contains(taleScene))
                                {
                                    tales.transform.GetChild(0).gameObject.SetActive(false);
                                }
                            }
                            else
                            {
                                MelonLogger.Msg("Unable to find tales object in scene");
                            }
                        }

                    }
                }
            }
        }
    }
}
