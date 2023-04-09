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

        [Name("Default Shortwave Location")]
        [Description("Choose whether the Handheld Shortwave will be available in it's default location if you chose random.")]
        [Choice("Yes", "No")]
        public bool airfieldRegionAvailable = false;

        [Section("Tale Settings")]

        [Name("Journal Missions")]
        [Description("Enables or disables the Tale missions in the journal page.")]
        [Choice("Disabled", "Enabled")]
        public bool enabledMissionTab = true;

        [Name("Mission Popups")]
        [Description("Enables or disables the mission popups.")]
        [Choice("Disabled", "Enabled")]
        public bool enabledMissionPopups = true;

        protected override void OnChange(FieldInfo field, object oldValue, object newValue)
        {
            if (field.Name == nameof(shortwaveRegion))
            {
                RefreshSections();
            }
        }

        internal void RefreshSections()
        {
            SetFieldVisible(nameof(airfieldRegionAvailable), Settings.settings.shortwaveRegion == 5);
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
