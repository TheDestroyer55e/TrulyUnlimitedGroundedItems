using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.Config;

namespace UnlimitedGroundedItems {
    internal class Config : ModConfig {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [DefaultValue(400)]
        [Range(2, 10000)]
        [ReloadRequired]
        public int groundedItemsCap;
    }
}
