using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.World.Generation;

namespace ArchaeaMod.Biome
{
    public class MagnoBiome
    {
        public static void Generate(GenerationProgress progress)
        {
            int worldLeft = WorldGen.genRand.Next(Main.maxTilesX / 4, (int)(Main.maxTilesX * 0.75f));
            int worldTop = 400;
            int width = 600;
            int height = 800;
            List<Vector2> miner = new List<Vector2>();
            List<Vector2> cavern = new List<Vector2>();
            Vector2 start = new Vector2(worldLeft, worldTop + height * 0.75f);
            float radius = 20f;
            float tunnel = 10f;
            float move = radius * 0.30f;
            int ticks = 0, ticks2 = 0;
            int max = 20, max2 = 25;
            bool flag = false, flag2 = false;
            bool level = true;
            bool ash = false;
            while (start.X < width / 2)
            {
                if (WorldGen.genRand.NextDouble() >= 0.95f)
                {
                    radius = 40f;
                    cavern.Add(start);
                }
                else
                {
                    radius = 20f;
                }
                for (float r = 0; r < Math.PI * 2f; r += Draw.radian)
                {
                    for (float n = 0; n < radius; n += new Draw().radians(radius))
                    {
                        if (n >= tunnel)
                        {
                            float cos = start.X + n * (float)Math.Cos(r);
                            float sin = start.Y + n * (float)Math.Sin(r);
                            Main.tile[(int)cos, (int)sin].type = ArchaeaWorld.magnoStone;
                            if (ash && flag && r <= Math.PI && r >= 0f)
                            {
                                //Main.tile[(int)cos, (int)sin].type = ArchaeaWorld.Ash;
                            }
                        }
                    }
                }
                if (!flag && WorldGen.genRand.NextDouble() >= 0.99f)
                {
                    flag = true;
                }
                if (!level && WorldGen.genRand.NextDouble() >= 0.90f)
                {
                    level = true;
                }
                if (!ash && WorldGen.genRand.NextDouble() >= 0.95f)
                {
                    ash = true;
                }
                if (flag)
                {
                    if (ticks++ > max)
                    {
                        flag = false;
                        ticks = 0;
                    }
                }
                if (level)
                {
                    if (ticks++ > max)
                    {
                        level = false;
                        ticks = 0;
                    }
                }
                if (ash)
                {
                    if (ticks2++ > max2)
                    {
                        ash = false;
                        ticks2 = 0;
                    }
                }
                if (!level && start.Y < height - radius && start.Y > 0f + radius)
                {
                    start.Y += flag ? -1 * move : move;
                }
                miner.Add(start);
                start.X += move;
            }
            progress.Value = 0.2f;
            start = new Vector2(worldLeft, worldTop + height / 4);
            while (start.X < width / 2)
            {
                if (WorldGen.genRand.NextDouble() >= 0.95f)
                {
                    radius = 40f;
                    cavern.Add(start);
                }
                else
                {
                    radius = 20f;
                }
                for (float r = 0; r < Math.PI * 2f; r += Draw.radian)
                {
                    for (float n = 0; n < radius; n += new Draw().radians(radius))
                    {
                        if (n >= tunnel)
                        {
                            float cos = start.X + n * (float)Math.Cos(r);
                            float sin = start.Y + n * (float)Math.Sin(r);
                            Main.tile[(int)cos, (int)sin].type = ArchaeaWorld.magnoStone;
                            if (ash && flag && r <= Math.PI && r >= 0f)
                            {
                                //Main.tile[(int)cos, (int)sin].type = ArchaeaWorld.Ash;
                            }
                        }
                    }
                }
                if (!flag && WorldGen.genRand.NextDouble() >= 0.99f)
                {
                    flag = true;
                }
                if (!level && WorldGen.genRand.NextDouble() >= 0.90f)
                {
                    level = true;
                }
                if (!ash && WorldGen.genRand.NextDouble() >= 0.95f)
                {
                    ash = true;
                }
                if (flag)
                {
                    if (ticks++ > max)
                    {
                        flag = false;
                        ticks = 0;
                    }
                }
                if (level)
                {
                    if (ticks++ > max)
                    {
                        level = false;
                        ticks = 0;
                    }
                }
                if (ash)
                {
                    if (ticks2++ > max2)
                    {
                        ash = false;
                        ticks2 = 0;
                    }
                }
                if (!level && start.Y < height - radius && start.Y > 0f + radius)
                {
                    start.Y += flag ? -1 * move : move;
                }
                miner.Add(start);
                start.X += move;
            }
            progress.Value = 0.4f;
            flag = false;
            ticks = 0;
            bool back = true, forward = false;
            Vector2 branch = start;
            branch.X /= 2f;
            branch.Y = worldTop + height - radius;
            while (branch.Y > radius * 2f)
            {
                for (float r = 0; r < Math.PI * 2f; r += Draw.radian)
                {
                    for (float n = 0; n < radius; n += new Draw().radians(radius))
                    {
                        if (n >= tunnel)
                        {
                            float cos = branch.X + n * (float)Math.Cos(r);
                            float sin = branch.Y + n * (float)Math.Sin(r);
                            Main.tile[(int)cos, (int)sin].type = ArchaeaWorld.magnoStone;
                            if (ash && flag && r <= Math.PI && r >= 0f)
                            {
                                //Main.tile[(int)cos, (int)sin].type = ArchaeaWorld.Ash;
                            }
                        }
                    }
                }
                if (!flag && WorldGen.genRand.NextDouble() >= 0.995f)
                {
                    flag = true;
                }
                if (!level && WorldGen.genRand.NextDouble() >= 0.90f)
                {
                    level = true;
                }
                if (!ash && WorldGen.genRand.NextDouble() >= 0.95f)
                {
                    ash = true;
                }
                if (flag)
                {
                    if (ticks++ > max)
                    {
                        flag = false;
                        ticks = 0;
                    }
                }
                if (level)
                {
                    if (ticks++ > max)
                    {
                        level = false;
                        ticks = 0;
                    }
                }
                if (ash)
                {
                    if (ticks2++ > max2)
                    {
                        ash = false;
                        ticks2 = 0;
                    }
                }
                if (!level && start.Y < height - radius && start.Y > 0f + radius)
                {
                    branch.X += flag ? -1 * move : move;
                }
                miner.Add(branch);
                branch.Y -= move;
            }
            progress.Value = 0.6f;
            branch.X *= 2f;
            branch.Y = worldTop + height - radius;
            while (branch.Y > radius * 2f)
            {
                for (float r = 0; r < Math.PI * 2f; r += Draw.radian)
                {
                    for (float n = 0; n < radius; n += new Draw().radians(radius))
                    {
                        if (n >= tunnel)
                        {
                            float cos = branch.X + n * (float)Math.Cos(r);
                            float sin = branch.Y + n * (float)Math.Sin(r);
                            Main.tile[(int)cos, (int)sin].type = ArchaeaWorld.magnoStone;
                            if (ash && flag && r <= Math.PI && r >= 0f)
                            {
                                //Main.tile[(int)cos, (int)sin].type = ArchaeaWorld.Ash;
                            }
                        }
                    }
                }
                if (!flag && WorldGen.genRand.NextDouble() >= 0.995f)
                {
                    flag = true;
                }
                if (!level && WorldGen.genRand.NextDouble() >= 0.90f)
                {
                    level = true;
                }
                if (!ash && WorldGen.genRand.NextDouble() >= 0.95f)
                {
                    ash = true;
                }
                if (flag)
                {
                    if (ticks++ > max)
                    {
                        flag = false;
                        ticks = 0;
                    }
                }
                if (level)
                {
                    if (ticks++ > max)
                    {
                        level = false;
                        ticks = 0;
                    }
                }
                if (ash)
                {
                    if (ticks2++ > max2)
                    {
                        ash = false;
                        ticks2 = 0;
                    }
                }
                if (!level && start.Y < height - radius && start.Y > 0f + radius)
                {
                    branch.X += flag ? -1 * move : move;
                }
                miner.Add(branch);
                branch.Y -= move;
            }
            progress.Value = 0.8f;
            while (true)
            {
                if (!flag && WorldGen.genRand.NextDouble() >= 0.50f)
                {
                    flag = true;
                    flag2 = true;
                }
                for (float r = 0; r < Math.PI * 2f; r += Draw.radian)
                {
                    for (float n = 0; n < radius; n += new Draw().radians(radius))
                    {
                        if (n >= tunnel)
                        {
                            float cos = start.X + n * (float)Math.Cos(r);
                            float sin = start.Y + n * (float)Math.Sin(r);
                            Main.tile[(int)cos, (int)sin].type = ArchaeaWorld.magnoStone;
                            if (ash && flag && r <= Math.PI && r >= 0f)
                            {
                                //Main.tile[(int)cos, (int)sin].type = ArchaeaWorld.Ash;
                            }
                        }
                    }
                }
                if (ticks++ < max)
                {
                    if (start.Y >= height - radius)
                    {
                        start.Y -= move;
                        start.X -= move;
                    }
                    else if (start.Y <= 0f + radius)
                    {
                        start.Y += move;
                        start.X -= move;
                    }
                    else
                    {
                        if (flag2)
                        {
                            if (back)
                            {
                                start.Y -= move;
                                start.X -= move;
                            }
                            else if (!forward)
                            {
                                start.X -= move;
                            }
                            else
                            {
                                start.X += move;
                            }
                        }
                        else
                        {
                            if (back)
                            {
                                start.Y += move;
                                start.X -= move;
                            }
                            else if (!forward)
                            {
                                start.X -= move;
                            }
                            else
                            {
                                start.X += move;
                            }
                        }
                    }
                }
                else
                {
                    if (forward)
                    {
                        break;
                    }
                    if (!back)
                    {
                        forward = true;
                        max = 50;
                    }
                    back = false;
                    ticks = 0;
                }
                miner.Add(start);
            }
            max = 20;
            ticks = 0;
            while (start.X < width - radius)
            {
                if (WorldGen.genRand.NextDouble() >= 0.95f)
                {
                    radius = 40f;
                    cavern.Add(start);
                }
                else
                {
                    radius = 20f;
                }
                for (float r = 0; r < Math.PI * 2f; r += Draw.radian)
                {
                    for (float n = 0; n < radius; n += new Draw().radians(radius))
                    {
                        if (n >= tunnel)
                        {
                            float cos = start.X + n * (float)Math.Cos(r);
                            float sin = start.Y + n * (float)Math.Sin(r);
                            Main.tile[(int)cos, (int)sin].type = ArchaeaWorld.magnoStone;
                            if (ash && flag && r <= Math.PI && r >= 0f)
                            {
                                //Main.tile[(int)cos, (int)sin].type = ArchaeaWorld.Ash;
                            }
                        }
                    }
                }
                if (!flag && WorldGen.genRand.NextDouble() >= 0.995f)
                {
                    flag = true;
                }
                if (!level && WorldGen.genRand.NextDouble() >= 0.90f)
                {
                    level = true;
                }
                if (!ash && WorldGen.genRand.NextDouble() >= 0.95f)
                {
                    ash = true;
                }
                if (flag)
                {
                    if (ticks++ > max)
                    {
                        flag = false;
                        ticks = 0;
                    }
                }
                if (level)
                {
                    if (ticks++ > max)
                    {
                        level = false;
                        ticks = 0;
                    }
                }
                if (ash)
                {
                    if (ticks2++ > max2)
                    {
                        ash = false;
                        ticks2 = 0;
                    }
                }
                if (!level && start.Y < height - radius && start.Y > 0f + radius)
                {
                    start.Y += flag ? -1 * move : move;
                }
                miner.Add(start);
                start.X += move;
            }
            progress.Value = 0.9f;
            radius = 20f;
            foreach (var loc in miner)
            {
                tunnel = (float)WorldGen.genRand.Next(8, 15);
                for (float r = 0; r < Math.PI * 2f; r += Draw.radian)
                {
                    for (float n = 0; n < radius; n += new Draw().radians(radius))
                    {
                        if (n < tunnel)
                        {
                            float cos = loc.X + n * (float)Math.Cos(r);
                            float sin = loc.Y + n * (float)Math.Sin(r);
                            Main.tile[(int)cos, (int)sin].type = 0;
                            Main.tile[(int)cos, (int)sin].active(false);
                        }
                    }
                }
            }
            radius = 40f;
            progress.Value = 0.95f;
            foreach (var loc in cavern)
            {
                tunnel = (float)WorldGen.genRand.Next(20, 35);
                for (float r = 0; r < Math.PI * 2f; r += Draw.radian)
                {
                    for (float n = 0; n < radius; n += new Draw().radians(radius))
                    {
                        if (n < tunnel)
                        {
                            float cos = loc.X + n * (float)Math.Cos(r);
                            float sin = loc.Y + n * (float)Math.Sin(r);
                            Main.tile[(int)cos, (int)sin].type = 0;
                            Main.tile[(int)cos, (int)sin].active(false); 
                        }
                    }
                }
            }
            progress.Value = 1f;
        }
    }
}