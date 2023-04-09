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


            string scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

            MelonLogger.Msg("Handler: Current scene: {0}", scene);

            SaveDataManager sdm = new SaveDataManager();

            MelonLogger.Msg("Getting region from mod data to set item spawns");
            if (scene == sdm.LoadTaleStartRegion("startRegion"))
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
