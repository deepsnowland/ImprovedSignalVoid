using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearSpawner;
using MelonLoader;
using Main;

namespace ImprovedSignalVoid.GearSpawns
{
    internal class NarrativeGearSpawnHandler : GearSpawnHandler
    {
        public override bool ShouldSpawn(DifficultyLevel difficultyLevel, FirearmAvailability firearmAvailability, GearSpawnInfo gearSpawnInfo)
        {

            string scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

            SaveDataManager sdm = Implementation.sdm;

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
