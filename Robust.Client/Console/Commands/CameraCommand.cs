using JetBrains.Annotations;
using Robust.Client.Graphics.Drawing;
using Robust.Client.Interfaces.Console;
using Robust.Client.Interfaces.Graphics;
using Robust.Client.Player;
using Robust.Client.UserInterface;
using Robust.Client.UserInterface.CustomControls;
using Robust.Server.GameObjects.Components.Eye;
using Robust.Shared.GameObjects;
using Robust.Shared.GameObjects.EntitySystemMessages;
using Robust.Shared.Interfaces.GameObjects;
using Robust.Shared.IoC;
using Robust.Shared.Map;
using Robust.Shared.Maths;
using EyeComponent = Robust.Client.GameObjects.EyeComponent;

namespace Robust.Client.Console.Commands
{
    [UsedImplicitly]
    public class CameraCommand : IConsoleCommand
    {
        public string Command => "camera2";
        public string Description => "Open a camera viewport";
        public string Help => "Opens a camera viewport";

        public class CameraTestWindow : SS14Window
        {
            public CameraTestWindow()
            {
                var clyde = IoCManager.Resolve<IClyde>();
                var playerMgr = IoCManager.Resolve<IPlayerManager>();
                var pos = playerMgr.LocalPlayer!.ControlledEntity!.Transform.MapID;
                var cameraPos = new MapCoordinates(Vector2.Zero, pos);
                var cameraEnt = IoCManager.Resolve<IEntityManager>().SpawnEntity(null, cameraPos);
                var eyeComp = cameraEnt.AddComponent<EyeComponent>();
                //var sEyeComp = cameraEnt.AddComponent<ServerEyeComponent>();

                var vp = clyde.CreateViewport((400, 400), "CameraTest");
                vp.Eye = eyeComp.Eye;

                Contents.AddChild(new CameraControl(vp));
            }

            private sealed class CameraControl : Control
            {
                private readonly IClydeViewport _viewport;

                public CameraControl(IClydeViewport viewport)
                {
                    _viewport = viewport;
                }

                protected override Vector2 CalculateMinimumSize()
                {
                    return _viewport.Size / UIScale * 2;
                }

                protected internal override void Draw(DrawingHandleScreen handle)
                {
                    _viewport.Render();
                    handle.DrawTextureRect(_viewport.RenderTarget.Texture,
                                           UIBox2.FromDimensions(Vector2.Zero, _viewport.Size * 2));
                }
            }
        }


        public bool Execute(IDebugConsole console, params string[] args)
        {
            CameraTestWindow window = new();

            window.OpenCentered();
            return false;
        }
    }
}
