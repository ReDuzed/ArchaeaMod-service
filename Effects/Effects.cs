﻿using System;
using System.Collections.Generic;
using PointF = System.Drawing.PointF;
using Bitmap = System.Drawing.Bitmap;
using Graphics = System.Drawing.Graphics;
using Color = System.Drawing.Color;
using Pen = System.Drawing.Pen;
using Brushes = System.Drawing.Brushes;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ArchaeaMod.Effects
{
    public sealed class FxID
    {
        public const byte
            WaveForm = 0,
            Polygon = 1;
    }
    public static class Ext
    {
        public static Vector2 ToXnaVector2(in PointF a)
        {
            return new Vector2(a.X, a.Y);
        }
    }
    public class Fx
    {
        public static Particle[] particle = new Particle[20];
        #pragma warning disable CA1416 // Validate platform compatibility
        public static MemoryStream GenerateImage(byte index, int width, int height, bool inUse, Color transparency, bool style = true)
        {
            PointF[] oldPoints = new PointF[] { };
            int distance = width;
            MemoryStream mem = new MemoryStream();

            using (Bitmap bitmap = new Bitmap(distance, height))
            {
                using (Graphics graphic = Graphics.FromImage(bitmap))
                {
                    switch (index)
                    {
                        case FxID.WaveForm:
                            #region waveform
                            var data = _Buffer(width);

                            float num = data.Max();
                            float num2 = data.Min();
                            float num3 = data.Average();
                            int[] indexArray = new int[3];
                            for (int i = 0; i < data.Length; i++)
                            {
                                if (num == data[i])
                                    indexArray[0] = i;
                                if (num2 == data[i])
                                    indexArray[1] = i;
                                if (num3 == data[i])
                                    indexArray[2] = i;
                            }
                            int length = indexArray.Max() - indexArray.Min();
                            if (length + indexArray[2] < width)
                                length += indexArray[2];

                            PointF[] points = new PointF[width];
                            if (inUse)
                            {
                                for (int i = 0; i < points.Length; i += points.Length / Math.Max(length, 1))
                                {
                                    float y = height / 2 * (float)(data[i] * (style ? Math.Sin((float)i / width * Math.PI) : 1f)) + height / 2;
                                    points[i] = new PointF(Math.Min(i, points.Length), y);
                                }
                                PointF begin = new PointF();
                                bool flag = false;
                                int num4 = 0;
                                for (int i = 1; i < points.Length; i++)
                                {
                                    if (points[i] == default(PointF) && !flag)
                                    {
                                        begin = points[i - 1];
                                        num4 = i;
                                        flag = true;
                                    }
                                    if ((points[i] != default(PointF) || i == points.Length - 2) && flag)
                                    {
                                        for (int j = num4; j < i; j++)
                                        {
                                            points[j] = new PointF(begin.X, begin.Y);
                                        }
                                        flag = false;
                                    }
                                }
                                for (int i = points.Length - 1; i > 0; i--)
                                {
                                    if (points[i].X == 0f)
                                        points[i].X = i;
                                    if (points[i].Y == 0f)
                                        points[i].Y = points[i - 1].Y;
                                }
                                points[points.Length - 1] = points[points.Length - 2];
                            }
                            graphic.FillRectangle(System.Drawing.Brushes.Black, new System.Drawing.Rectangle(0, 0, width, height));
                            if (points.Length > 1)
                            {
                                var pen = new System.Drawing.Pen(System.Drawing.Brushes.White);
                                pen.Width = 1;
                                graphic.DrawCurve(pen, points);
                                oldPoints = points;
                            }
                            #endregion
                            break;
                        case FxID.Polygon:

                            break;
                    }
                    bitmap.MakeTransparent(transparency);
                    bitmap.Save(mem, System.Drawing.Imaging.ImageFormat.Png);
                    return mem;
                }
            }
        }
        public static MemoryStream GenerateImage(Polygon polygon, int width, int height, Pen pen, Color transparency)
        {
            PointF[] oldPoints = new PointF[] { };
            int distance = width;
            MemoryStream mem = new MemoryStream();

            using (Bitmap bitmap = new Bitmap(distance, height))
            {
                using (Graphics graphic = Graphics.FromImage(bitmap))
                {
                    var points = Polygon.Update(polygon, width, height, 300);
                    graphic.FillRectangle(new System.Drawing.SolidBrush(transparency), new System.Drawing.Rectangle(0, 0, width, height));
                    graphic.FillPolygon(Brushes.Black, points);
                    graphic.DrawLines(pen, points);
                    Particle.Update(particle, width, height, Color.White);
                    for (int i = 0; i < particle.Length; i++)
                    {
                        if (particle[i] != null && particle[i].active)
                        {
                            graphic.DrawRectangle(new Pen(particle[i].color), (int)particle[i].position.X, (int)particle[i].position.Y, 1, 1);
                            if (particle[i].position.X >= width - 1 || particle[i].position.X <= 1 ||
                                particle[i].position.Y >= height - 1 || particle[i].position.Y <= 1 || 
                                bitmap.GetPixel((int)Math.Min(particle[i].position.X, width - 1), (int)Math.Min(particle[i].position.Y, height - 1)).G == transparency.G)
                            {
                                particle[i].active = false;
                            } 
                        }
                    }
                    bitmap.MakeTransparent(transparency);
                    bitmap.Save(mem, System.Drawing.Imaging.ImageFormat.Png);
                    return mem;
                }
            }
        }
        #pragma warning restore CA1416 // Validate platform compatibility
        public static Texture2D FromStream(MemoryStream stream) => Texture2D.FromStream(Main.graphics.GraphicsDevice, stream);
        private static float[] _Buffer(int length)
        {
            try
            {
                byte[] buffer = new byte[length];
                float[] array = new float[length];
                Main.rand.NextBytes(buffer);
                for (int i = 0; i < length; i++)
                    array[i] = (float)buffer[i] / byte.MaxValue * Main.rand.Next(new[] { -1, 1 });
                return array;
            }
            catch
            {
                return new float[] { 0f };
            }
        }
    }
    public class Polygon
    {
        private Vector2 x, y, z;
        private Vector2 newX, newY, newZ;
        private int ticks;
        public static PointF[] Update(Polygon polygon, int width, int height, int maxTicks)
        {
            if (polygon.ticks++ > maxTicks)
            {
                polygon.ticks = 0;
                do
                {
                    polygon.newX = new Vector2(Main.rand.Next(0, width), Main.rand.Next(0, height));
                    polygon.newY = new Vector2(Main.rand.Next(0, width), Main.rand.Next(0, height));
                    polygon.newZ = new Vector2(Main.rand.Next(0, width), Main.rand.Next(0, height));
                } while (polygon.newX.X >= polygon.newY.X && polygon.newX.Y >= polygon.newY.Y && polygon.newX.Y <= polygon.newZ.Y 
                && polygon.newY.X <= polygon.newX.X && polygon.newY.Y <= polygon.newX.Y && polygon.newY.Y <= polygon.newZ.Y
                && polygon.newZ.X <= polygon.newX.X && polygon.newZ.Y >= polygon.newX.Y && polygon.newZ.Y >= polygon.newY.Y);
            }
            polygon.x = Vector2.Lerp(polygon.x, polygon.newX, (float)polygon.ticks / maxTicks);
            polygon.y = Vector2.Lerp(polygon.y, polygon.newY, (float)polygon.ticks / maxTicks);
            polygon.z = Vector2.Lerp(polygon.z, polygon.newZ, (float)polygon.ticks / maxTicks);
            return new PointF[] { new PointF(polygon.x.X, polygon.x.Y), new PointF(polygon.y.X, polygon.y.Y), new PointF(polygon.z.X, polygon.z.Y), new PointF(polygon.x.X, polygon.x.Y) };
        }
    }
    public class Particle
    {
        public bool active;
        public float speed;
        public float angle;
        public Color color;
        public Vector2 position;
        public static void Update(Particle[] particle, int width, int height, Color color)
        {
            for (int i = 0; i < particle.Length; i++)
            {
                if (particle[i] == null || !particle[i].active)
                {
                    particle[i] = new Particle()
                    {
                        color = color,
                        position = new Vector2(width / 2, height / 2),
                        speed = Main.rand.NextFloat() + 1f,
                        angle = (float)Math.PI * 2f * Main.rand.NextFloat(),
                        active = true
                    };
                }
                particle[i].position += NPCs.ArchaeaNPC.AngleToSpeed(particle[i].angle, particle[i].speed);
            }
        }
    }
}
