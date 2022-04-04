﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

using ArchaeaMod.NPCs.Bosses;

namespace ArchaeaMod.Items
{
    public class m_fossil : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Scorched Fossil");
            Tooltip.SetDefault("");
        }
        public override void SetDefaults()
        {
            item.width = 48;
            item.height = 48;
            item.rare = 2;
            item.value = 2000;
            item.useTime = 45;
            item.useAnimation = 45;
            item.useStyle = 4;
            item.autoReuse = false;
            item.consumable = true;
            item.noMelee = true;
            bossType = ModContent.NPCType<Magnoliac_head>();
        }

        private int bossType;
        public override bool CanUseItem(Player player)
        {
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.type == bossType && (npc.active || npc.life > 0))
                    return false;
            }
            if (!player.GetModPlayer<ArchaeaPlayer>().MagnoBiome)
                return false;
            return true;
        }
        public override bool UseItem(Player player)
        {
            NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<Magnoliac_head>());
            Main.PlaySound(SoundID.Roar, player.Center, 0);
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.Anvils);
            recipe.AddIngredient(ModContent.ItemType<Merged.Items.Materials.magno_core>(), 5);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
