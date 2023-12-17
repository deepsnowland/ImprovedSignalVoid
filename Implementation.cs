using ImprovedSignalVoid;
using ImprovedSignalVoid.GearSpawns;
using MelonLoader;
using ModSettings;

namespace Main
{
	public sealed class Implementation : MelonMod
	{

		internal static SaveDataManager sdm = new SaveDataManager();

		public override void OnInitializeMelon()
		{
			MelonLogger.Msg("Improved Tales is online!");
            Settings.OnLoad();
            ItemSpawnManager.InitializeCustomHandler();
		}
	}
}
