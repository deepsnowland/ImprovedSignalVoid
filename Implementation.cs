using ImprovedSignalVoid;
using ImprovedSignalVoid.GearSpawns;
using MelonLoader;
using ModSettings;

namespace ModNamespace;
internal sealed class Implementation : MelonMod
{
	public override void OnInitializeMelon()
	{
		MelonLogger.Msg("Improved Signal Void is online!");
		ItemSpawnManager.InitializeCustomHandler();
        Settings.OnLoad();
    }
}
