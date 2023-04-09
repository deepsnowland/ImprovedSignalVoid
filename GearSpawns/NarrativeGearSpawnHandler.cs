using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearSpawner;
using MelonLoader;

namespace ImprovedSignalVoid.GearSpawns
{
    internal class NarrativeGearSpawnHandler : GearSpawnHandler
    {
        public override bool ShouldSpawn(DifficultyLevel difficultyLevel, FirearmAvailability firearmAvailability, GearSpawnInfo gearSpawnInfo)
        {

            MelonLogger.Msg("Checking if items should spawn");

            string scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

            SaveDataManager sdm = new SaveDataManager();

            if(scene == sdm.LoadTaleStartRegion("startRegion"))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
