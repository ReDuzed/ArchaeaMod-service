﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ArchaeaMod.Walls
{
    public class sky_brickwall : ModWall
    {
        public override void SetStaticDefaults()
        {
            Main.wallHouse[Type] = true;
            TileID.Sets.HousingWalls[Type] = true;
            ItemDrop = ModContent.ItemType<Items.Walls.sky_brickwall>();
            AddMapEntry(new Color(80, 10, 10));
        }
    }
}
