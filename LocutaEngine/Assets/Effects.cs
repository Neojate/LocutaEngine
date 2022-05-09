using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace LocutaEngine.Assets
{
    public class Effects : Asset
    {
        private static readonly Lazy<Effects> lazy = new Lazy<Effects>(() => new Effects());

        public static Effects Instance { get { return lazy.Value; } }

        private Dictionary<string, Effect> data = new Dictionary<string, Effect>();

        public void AddEffect(string effectName)
        {
            data.Add(effectName, content.Load<Effect>($"effects/{effectName}"));
        }

        public void AddEffect(string effectName, string fileRoute)
        {
            data.Add(effectName, content.Load<Effect>($"{fileRoute}/{effectName}"));
        }

        public void AddEffect(string effectName, Effect effect)
        {
            data.Add(effectName, effect);
        }

        public Effect GetEffect(string effectName)
        {
            return data[effectName];
        }

        public void RemoveEffect(string effectName)
        {
            data.Remove(effectName);
        }
    }
}
