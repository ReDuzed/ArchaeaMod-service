﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ArchaeaMod.Merged.Buffs;

namespace ArchaeaMod.Merged.Items
{
    public class magno_summonstaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rubidium Staff");
            Tooltip.SetDefault("Summons Magno minion"
                    +   "\nCauses shock waves "
                    +   "\nwhen attached to enemy");
        }
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 32;
            Item.useTime = 24;
            Item.useAnimation = 18;
            Item.useStyle = 1;
            Item.mana = 10;
            Item.damage = 12;
            Item.knockBack = 3f;
            Item.value = 4000;
            Item.crit = 9;
            Item.rare = 2;
            Item.autoReuse = false;
            Item.consumable = false;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Summon;
            Item.CountsAsClass(DamageClass.Summon);
            Item.buffType = ModContent.BuffType<magno_summon>();
            Item.buffTime = 18000;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("magno_minion").Type] < player.maxMinions && player.numMinions < player.maxMinions)
            {
                return true;
            }
            else return false;
        }
        public override bool? UseItem(Player player)/* Suggestion: Return null instead of false */
        {
            int projMinion = Projectile.NewProjectile(Projectile.GetSource_None(), player.position, Vector2.Zero, Mod.Find<ModProjectile>("magno_minion").Type, 5, 3f, player.whoAmI, 0f, 0f);
            Main.projectile[projMinion].netUpdate = true;
            return true;
        }
    }
}
