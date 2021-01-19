using JetBrains.Annotations;
using Robust.Server.Interfaces.Console;
using Robust.Server.Interfaces.Player;
using Robust.Shared.Maths;

namespace Robust.Server.Console.Commands
{
    [UsedImplicitly]
    public class CameraCommand : IClientCommand
    {
        public string Command => "camera";
        public string Description => "Open viewport";
        public string Help => "Open viewport";

        public void Execute(IConsoleShell shell, IPlayerSession? player, string[] args)
        {
            shell.ExecuteCommand("camera2");
            shell.SendText(player, "test test");

            player?.AddServerEyeComponent(new Vector2(0, 0));
        }
    }
}
