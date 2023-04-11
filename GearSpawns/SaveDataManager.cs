using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using ModData;

namespace ImprovedSignalVoid.GearSpawns
{
    internal class SaveDataManager
    {

        ModDataManager dm = new ModDataManager("Improved Signal Void", false);

        public void Save(string data, string suffix)
        {
            dm.Save(data, suffix);
        }

        public string LoadTaleStartRegion(string suffix)
        {
            string? region = dm.Load(suffix);

            if(region == null)
            {
                MelonLogger.Msg("NO SAVE DATA. SAVING NEW DATA");
                int regionToSaveInt = Settings.settings.shortwaveRegion;
                string regionToSave = null;

                if(regionToSaveInt == 5)
                {
                    regionToSaveInt = GetRandomRegion();
                }
               
                switch (regionToSaveInt)
                {
                    case 0:
                        regionToSave = "AirfieldRegion";
                        break;
                    case 1:
                        regionToSave = "RadioControlHut";
                        break;
                    case 2:
                        regionToSave = "WhalingWarehouseA";
                        break;
                    case 3:
                        regionToSave = "LakeRegion";
                        break;
                    case 4:
                        regionToSave = "BlackrockInteriorASurvival";
                        break;
                    default:
                        regionToSave = "AirfieldRegion";
                        break;

                }

                MelonLogger.Msg("REGION BEING SAVED AND LOADED: {0}", regionToSave);

                Save(regionToSave, suffix);
                region = regionToSave;
            }
            else
            {
                MelonLogger.Msg("REGION BEING LOADED: {0}", region);
            }

            return region;
        }

        public int GetRandomRegion()
        {
            Random rand = new Random();
            int min = 0;
            if (Settings.settings.airfieldRegionAvailable) min = 1;
            return rand.Next(min, 5); 
        }


    }
}
