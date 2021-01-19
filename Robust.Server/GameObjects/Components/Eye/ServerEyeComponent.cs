using Robust.Shared.GameObjects;
using Robust.Shared.Maths;

namespace Robust.Server.GameObjects.Components.Eye
{
    [RegisterComponent]
    public class ServerEyeComponent : Component
    {
        public Vector2 Position { get; }

        public ServerEyeComponent(Vector2 position)
        {
            Name = "test";
            Position = position;
        }

        public ServerEyeComponent()
        {
            Name = "test";
            Position = new Vector2(0, 0);
        }

        public override string Name { get; }
    }
}
