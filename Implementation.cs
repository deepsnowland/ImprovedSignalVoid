using ImprovedSignalVoid;
using MelonLoader;
using ModSettings;

namespace ModNamespace;
internal sealed class Implementation : MelonMod
{
	public override void OnInitializeMelon()
	{
		MelonLogger.Msg("Improved Signal Void is online!");
        Settings.OnLoad();
    }
}
