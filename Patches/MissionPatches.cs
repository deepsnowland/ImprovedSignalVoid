﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using MelonLoader;
using Il2Cpp;
using System.Reflection;
using ModSettings;
using UnityEngine;
using Il2CppParadoxNotion.Services;
using Il2CppNodeCanvas.Tasks.Conditions;
using ImprovedSignalVoid.GearSpawns;

namespace ImprovedSignalVoid.Patches.Patches
{
    internal class MissionPatches : MonoBehaviour
    {

        [HarmonyPatch(typeof(SaveGameSystem), nameof(SaveGameSystem.LoadSceneData))]

        internal class InventoryCheckOverride
        {

            public static void Postfix()
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

                        string item = null;
                        SaveDataManager sdm = new SaveDataManager();
                        string scene = sdm.LoadTaleStartRegion("startRegion");


                        switch (scene)
                        {
                            case "LakeRegion":
                                item = "GEAR_SignalVoidMlCollectible1";
                                break;
                            case "RadioControlHut":
                                item = "GEAR_SignalVoidPvCollectible1";
                                break;
                            case "WhalingWarehouseA":
                                item = "GEAR_SignalVoidDpCollectible1";
                                break;
                            case "BlackrockInteriorASurvival":
                                item = "GEAR_SignalVoidBrCollectible1";
                                break;
                            case "AirfieldRegion":
                                return;
                        }

                            condition.requirementsDict["_std"][0].name = item;
                    }
                    else
                    {
                        MelonLogger.Msg("Unable to find key/value");
                    }

                }
            }

        }

        [HarmonyPatch(typeof(Panel_MissionsStory), nameof(Panel_MissionsStory.Update))]

        internal class MissionsTabDeactivate
        {
            private static void Prefix(Panel_MissionsStory __instance)
            {
                if (!Settings.settings.enabledMissionTab)
                {
                    __instance.gameObject.SetActive(false);
                }
            }
        }

        [HarmonyPatch(typeof(Panel_Log), nameof(Panel_Log.Update))]

        internal class JournalMissionTabDisplay
        {

            private static void Postfix(Panel_Log __instance)
            {

                if (!Settings.settings.enabledMissionTab)
                {

                    GameObject navbarNormal = null;
                    GameObject navbarTale = null;

                    for (int i = 0; i < __instance.gameObject.transform.childCount; i++)
                    {
                        GameObject child = __instance.gameObject.transform.GetChild(i).gameObject;

                        if (child.name == "MiniTopNav")
                        {
                            navbarNormal = child;
                        }
                        else if (child.name == "MiniTopNavStory")
                        {
                            navbarTale = child;
                        }
                    }

                    if (navbarNormal != null && navbarTale != null)
                    {
                        navbarTale.SetActive(false);
                        navbarNormal.SetActive(true);
                        navbarNormal.transform.GetChild(0).gameObject.SetActive(true);
                    }
                    else
                    {
                        MelonLogger.Msg("Can't find navbar objects in log");
                    }


                }
            }

        }

        [HarmonyPatch(typeof(Panel_Inventory), nameof(Panel_Inventory.Update))]

        internal class InventoryMissionTabDisplay
        {
            private static void Postfix(Panel_Inventory __instance)
            {

                if (!Settings.settings.enabledMissionTab)
                {

                    GameObject navbarNormal = null;
                    GameObject navbarTale = null;

                    for (int i = 0; i < __instance.gameObject.transform.childCount; i++)
                    {
                        GameObject child = __instance.gameObject.transform.GetChild(i).gameObject;

                        if (child.name == "MiniTopNav")
                        {
                            navbarNormal = child;
                        }
                        else if (child.name == "MiniTopNavStory")
                        {
                            navbarTale = child;
                        }
                    }

                    if (navbarNormal != null && navbarTale != null)
                    {
                        navbarTale.SetActive(false);
                        navbarNormal.SetActive(true);
                        navbarNormal.transform.GetChild(0).gameObject.SetActive(true);
                    }
                }
            }
        }

        [HarmonyPatch(typeof(Panel_FirstAid), nameof(Panel_FirstAid.Update))]

        internal class StatusMissionTabDisplay
        {
            private static void Postfix(Panel_FirstAid __instance)
            {
                if (!Settings.settings.enabledMissionTab)
                {

                    GameObject navbarNormal = null;
                    GameObject navbarTale = null;

                    for (int i = 0; i < __instance.gameObject.transform.childCount; i++)
                    {
                        GameObject child = __instance.gameObject.transform.GetChild(i).gameObject;

                        if (child.name == "MiniTopNav")
                        {
                            navbarNormal = child;
                        }
                        else if (child.name == "MiniTopNavStory")
                        {
                            navbarTale = child;
                        }
                    }

                    if (navbarNormal != null && navbarTale != null)
                    {
                        navbarTale.SetActive(false);
                        navbarNormal.SetActive(true);
                        navbarNormal.transform.GetChild(0).gameObject.SetActive(true);
                    }
                }
            }

        }

        [HarmonyPatch(typeof(Panel_Clothing), nameof(Panel_Clothing.Update))]

        internal class ClothingMissionTabDisplay
        {
            private static void Postfix(Panel_Clothing __instance)
            {

                if (!Settings.settings.enabledMissionTab)
                {

                    GameObject nonPaperDoll = __instance.gameObject.transform.GetChild(1).gameObject;

                    GameObject navbarNormal = null;
                    GameObject navbarTale = null;

                    for (int i = 0; i < nonPaperDoll.transform.childCount; i++)
                    {
                        GameObject child = nonPaperDoll.transform.GetChild(i).gameObject;

                        if (child.name == "MiniTopNav")
                        {
                            navbarNormal = child;
                        }
                        else if (child.name == "MiniTopNavStory")
                        {
                            navbarTale = child;
                        }
                    }

                    if (navbarNormal != null && navbarTale != null)
                    {
                        navbarTale.SetActive(false);
                        navbarNormal.SetActive(true);
                        navbarNormal.transform.GetChild(0).gameObject.SetActive(true);
                    }
                }

            }

        }

        [HarmonyPatch(typeof(Panel_Crafting), nameof(Panel_Crafting.Update))]

        internal class CraftingMissionTabDisplay
        {
            private static void Postfix(Panel_Crafting __instance)
            {

                if (!Settings.settings.enabledMissionTab)
                {

                    GameObject root = __instance.gameObject.transform.GetChild(0).gameObject;

                    GameObject navbarNormal = null;
                    GameObject navbarTale = null;

                    for (int i = 0; i < root.transform.childCount; i++)
                    {
                        GameObject child = root.transform.GetChild(i).gameObject;

                        if (child.name == "MiniTopNavSandbox")
                        {
                            navbarNormal = child;
                        }
                        else if (child.name == "MiniTopNavStory")
                        {
                            navbarTale = child;
                        }
                    }

                    if (navbarNormal != null && navbarTale != null)
                    {
                        navbarTale.SetActive(false);
                        navbarNormal.SetActive(true);
                        navbarNormal.transform.GetChild(0).gameObject.SetActive(true);
                    }
                }

            }

        }

        [HarmonyPatch(typeof(Panel_Map), nameof(Panel_Map.Update))]

        internal class MapMissionTabDisplay
        {
            private static void Postfix(Panel_Map __instance)
            {

                if (!Settings.settings.enabledMissionTab)
                {

                    GameObject root = __instance.gameObject.transform.GetChild(0).gameObject;

                    GameObject navbarNormal = null;
                    GameObject navbarTale = null;

                    for (int i = 0; i < root.transform.childCount; i++)
                    {
                        GameObject child = root.transform.GetChild(i).gameObject;

                        if (child.name == "MiniTopNavSandbox")
                        {
                            navbarNormal = child;
                        }
                        else if (child.name == "MiniTopNavStory")
                        {
                            navbarTale = child;
                        }
                    }

                    if (navbarNormal != null && navbarTale != null)
                    {
                        navbarTale.SetActive(false);
                        navbarNormal.SetActive(true);
                        navbarNormal.transform.GetChild(0).gameObject.SetActive(true);
                    }
                }

            }

        }

        [HarmonyPatch(typeof(Panel_HUD), nameof(Panel_HUD.Update))]
        internal class HUDMissionPopupDisplay
        {

            private static void Postfix(Panel_HUD __instance)
            {
                GameObject EssentialHUD = __instance.gameObject.transform.GetChild(3).gameObject;
                GameObject MissionPopup = EssentialHUD.transform.GetChild(11).gameObject;

                if (!Settings.settings.enabledMissionPopups)
                {
                    MissionPopup.SetActive(false);
                }
            }

        }

    }
}