using Microsoft.Xna.Framework.Content;

namespace LocutaEngine.Assets
{
    public class Asset
    {
        internal ContentManager content;

        internal void SetContent(ContentManager content)
        {
            this.content = content;
        }
    }
}
