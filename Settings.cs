using ModSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Unity.Burst.CompilerServices;

namespace ImprovedSignalVoid
{

    public enum Active
    {
        Disabled, Enabled
    }

    internal class CustomSettings : JsonModSettings
    {

        [Section("Handheld Shortwave Settings")]

        [Name("Handheld Shortwave Location")]
        [Description("Choose the starting location of the Tale. The region here will determine where the Handheld Shortwave will spawn and where the Tale will start.")]
        [Choice("Forsaken Airfield", "Pleasant Valley", "Desolation Point", "Mystery Lake", "Blackrock", "Random")]
        public int shortwaveRegion = 5;

        [Section("Tale Settings")]

        [Name("Journal Missions")]
        [Description("Enables or disables the Tale missions in the journal page.")]
        [Choice("Disabled", "Enabled")]
        public bool enabledMissionTab = true;

        [Name("Mission Popups")]
        [Description("Enables or disables the mission popups.")]
        [Choice("Disabled", "Enabled")]
        public bool enabledMissionPopups = true;

        [Name("Enable All Bunkers")]
        [Description("Enables Bunker Omega in Forsaken Airfield even if you haven't completed the other objectives in the Tale yet.")]
        [Choice("Disabled", "Enabled")]
        public bool enableBunkerOmega = false;

        protected override void OnChange(FieldInfo field, object oldValue, object newValue)
        {
            if (field.Name == nameof(enabledMissionTab) ||
               field.Name == nameof(enabledMissionPopups) ||
               field.Name == nameof(enableBunkerOmega))
            {
                RefreshSections();
            }
        }

        internal void RefreshSections()
        {
            SetFieldVisible(nameof(enableBunkerOmega), Settings.settings.enabledMissionTab != true && Settings.settings.enabledMissionPopups != true);
        }

    }

    static class Settings
    {
        internal static CustomSettings settings;
        internal static void OnLoad()
        {
            settings = new CustomSettings();
            settings.AddToModSettings("Improved Signal Void", MenuType.Both);
            settings.RefreshSections();
        }
    }
}
