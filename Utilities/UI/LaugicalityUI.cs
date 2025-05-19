﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.UI;
using Terraria;
using System;
using ReLogic.Graphics;
using Laugicality.Utilities.Players;
using Terraria.ModLoader;
using WebmilioCommons.Extensions;

namespace Laugicality.Utilities.UI
{
    public class LaugicalityUI : UIState
    {
        //Thanks for the help, DayOrk <3
        private const int BAR_WIDTH = 96;

        public static bool visible = true;
        public static SpriteProgressBar LuxBar;
        public static SpriteProgressBar OverflowLuxBar;
        public static Texture2D LuxBGTexture;
        public static Texture2D LuxBarTexture;
        public static Texture2D LuxFGTexture;
        public static SpriteProgressBar MundusBar;
        public static SpriteProgressBar OverflowMundusBar;
        public static Texture2D MundusBGTexture;
        public static Texture2D MundusBarTexture;
        public static Texture2D MundusFGTexture;
        public static SpriteProgressBar VisBar;
        public static SpriteProgressBar OverflowVisBar;
        public static Texture2D VisBGTexture;
        public static Texture2D VisBarTexture;
        public static Texture2D VisFGTexture;
        public static Texture2D OverflowBGTexture;
        public static Texture2D OverflowBarTexture;
        public static Texture2D OverflowFGTexture;
        public static SpriteProgressBar MysticBurstBar;
        public static Texture2D MysticBurstBGTexture;
        public static Texture2D MysticBurstBarTexture;

        private Vector2 TopPosBase = new Vector2((Main.screenWidth / 2 - BAR_WIDTH / 2 + (float)Math.Cos(3f / 2f * Math.PI) * 100) / Main.UIScale, (Main.screenHeight / 2 - BAR_WIDTH / 2 + (float)Math.Sin(3f / 2f * Math.PI) * 100 + 25) / Main.UIScale);
        private Vector2 MidPosBase = new Vector2((Main.screenWidth / 2 - BAR_WIDTH / 2 + (float)Math.Cos(5f / 6f * Math.PI) * 100) / Main.UIScale, (Main.screenHeight / 2 - BAR_WIDTH / 2 + (float)Math.Sin(5f / 6f * Math.PI) * 100 + 25) / Main.UIScale);
        private Vector2 BotPosBase = new Vector2((Main.screenWidth / 2 - BAR_WIDTH / 2 + (float)Math.Cos(1f / 6f * Math.PI) * 100) / Main.UIScale, (Main.screenHeight / 2 - BAR_WIDTH / 2 + (float)Math.Sin(1f / 6f * Math.PI) * 100 + 25) / Main.UIScale);
        private Vector2 TopPos = new Vector2(0, 0);
        private Vector2 MidPos = new Vector2(0, 0);
        private Vector2 BotPos = new Vector2(0, 0);
        private Vector2 MysticBurstPos = new Vector2(0, 0);

        int position = 1;
        float topTheta = -100;
        float midTheta = -100;
        float botTheta = -100;
        float topMag = 0;
        float midMag = 0;
        float botMag = 0;
        //float magMax = 5;
        float rotation = (float)Math.PI * 2f;
        float rotationGoal = (float)Math.PI * 2;

        int targetPosition = 1;
        int mysticBurstCooldownMax = 0;

        static LaugicalityUI()
        {
              visible = true;
        }

        public void Load()
        {

            LuxBGTexture = ModContent.Request<Texture2D>("Laugicality/Utilities/UI/LuxBG").Value;
            LuxBarTexture = ModContent.Request<Texture2D>("Laugicality/Utilities/UI/LuxBar").Value;
            LuxFGTexture = ModContent.Request<Texture2D>("Laugicality/Utilities/UI/LuxFG").Value;
            MundusBGTexture = ModContent.Request<Texture2D>("Laugicality/Utilities/UI/MundusBG").Value;
            MundusBarTexture = ModContent.Request<Texture2D>("Laugicality/Utilities/UI/MundusBar").Value;
            MundusFGTexture = ModContent.Request<Texture2D>("Laugicality/Utilities/UI/MundusFG").Value;
            VisBGTexture = ModContent.Request<Texture2D>("Laugicality/Utilities/UI/VisBG").Value;
            VisBarTexture = ModContent.Request<Texture2D>("Laugicality/Utilities/UI/VisBar").Value;
            VisFGTexture = ModContent.Request<Texture2D>("Laugicality/Utilities/UI/VisFG").Value;
            OverflowBGTexture = ModContent.Request<Texture2D>("Laugicality/Utilities/UI/OverflowBG").Value;
            OverflowBarTexture = ModContent.Request<Texture2D>("Laugicality/Utilities/UI/OverflowBar").Value;
            OverflowFGTexture = ModContent.Request<Texture2D>("Laugicality/Utilities/UI/OverflowFG").Value;
            MysticBurstBGTexture = ModContent.Request<Texture2D>("Laugicality/Utilities/UI/MysticBurstBG").Value;
            MysticBurstBarTexture = ModContent.Request<Texture2D>("Laugicality/Utilities/UI/MysticBurstBar").Value;

            LuxBar = new SpriteProgressBar
            (
                LuxBGTexture,
                LuxBarTexture,
                LuxFGTexture,
                TopPos,
                new Vector2(0, 0)
            );

            OverflowLuxBar = new SpriteProgressBar
            (
                OverflowBGTexture,
                OverflowBarTexture,
                OverflowFGTexture,
                TopPos,
                new Vector2(94, 0)
            );

            MundusBar = new SpriteProgressBar
            (
                MundusBGTexture,
                MundusBarTexture,
                MundusFGTexture,
                MidPos,
                new Vector2(0, 0)
            );

            OverflowMundusBar = new SpriteProgressBar
            (
                OverflowBGTexture,
                OverflowBarTexture,
                OverflowFGTexture,
                MidPos,
                new Vector2(94, 0)
            );

            VisBar = new SpriteProgressBar
            (
                VisBGTexture,
                VisBarTexture,
                VisFGTexture,
                BotPos,
                new Vector2(0, 0)
            );

            OverflowVisBar = new SpriteProgressBar
            (
                OverflowBGTexture,
                OverflowBarTexture,
                OverflowFGTexture,
                BotPos,
                new Vector2(94, 0)
            );

            MysticBurstBar = new SpriteProgressBar
            (
                MysticBurstBGTexture,
                MysticBurstBarTexture,
                OverflowFGTexture,
                MysticBurstPos,
                new Vector2(0, 0)
            );
        }

        public void Unload()
        {
            LuxBGTexture = null;
            LuxBarTexture = null;
            LuxFGTexture = null;
            MundusBGTexture = null;
            MundusBarTexture = null;
            MundusFGTexture = null;
            VisBGTexture = null;
            VisBarTexture = null;
            VisFGTexture = null;
            OverflowBGTexture = null;
            OverflowBarTexture = null;
            OverflowFGTexture = null;
            MysticBurstBGTexture = null;
            MysticBurstBarTexture = null;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            LaugicalityPlayer mysticPlayer = Main.LocalPlayer.GetModPlayer<LaugicalityPlayer>();

            visible = (mysticPlayer.MysticHold > 0);
            float luxTemp = mysticPlayer.Lux;

            if (luxTemp > mysticPlayer.LuxMax)
                luxTemp = mysticPlayer.LuxMax;

            if (LuxBar != null)
                LuxBar.DrawSelf(spriteBatch, (int)luxTemp, (int)(mysticPlayer.LuxMax));

            if (OverflowLuxBar != null && mysticPlayer.Lux > mysticPlayer.LuxMax)
                OverflowLuxBar.DrawSelf(spriteBatch, (int)(mysticPlayer.Lux - mysticPlayer.LuxMax), (int)(mysticPlayer.LuxMax));

            float mundusTemp = mysticPlayer.Mundus;

            if (mundusTemp > mysticPlayer.MundusMax)
                mundusTemp = mysticPlayer.MundusMax;

            if (MundusBar != null)
                MundusBar.DrawSelf(spriteBatch, (int)mundusTemp, (int)(mysticPlayer.MundusMax));

            if (OverflowMundusBar != null && mysticPlayer.Mundus > mysticPlayer.MundusMax)
                OverflowMundusBar.DrawSelf(spriteBatch, (int)(mysticPlayer.Mundus - mysticPlayer.MundusMax), (int)(mysticPlayer.MundusMax));

            float visTemp = mysticPlayer.Vis;

            if (visTemp > mysticPlayer.VisMax)
                visTemp = mysticPlayer.VisMax;

            if (VisBar != null)
                VisBar.DrawSelf(spriteBatch, (int)visTemp, (int)(mysticPlayer.VisMax));

            if (OverflowVisBar != null && mysticPlayer.Vis > mysticPlayer.VisMax)
                OverflowVisBar.DrawSelf(spriteBatch, (int)(mysticPlayer.Vis - mysticPlayer.VisMax), (int)(mysticPlayer.VisMax));

            if (MysticBurstBar != null && mysticPlayer.MysticSwitchCool > 0 && mysticBurstCooldownMax >= mysticPlayer.MysticSwitchCool)
                MysticBurstBar.DrawSelf(spriteBatch, mysticPlayer.MysticSwitchCool, mysticBurstCooldownMax);

            UpdateHover(spriteBatch);
        }

        public void CyclePositions(int setPosition)
        {
            rotationGoal += (float)(2f / 3 * Math.PI);
            targetPosition = setPosition;
            switch (targetPosition)
            {
                case 1:
                    rotationGoal = 6f / 3f * (float)Math.PI;
                    break;
                case 2:
                    rotationGoal = 4f / 3f * (float)Math.PI;
                    break;
                default:
                    rotationGoal = 8f / 3f * (float)Math.PI;
                    break;
            }
        }

        public void Update()
        {
            UpdateIdle();
            UpdateRotations();
            UpdatePositions();
            UpdateMysticBurst();
        }

        private void UpdateIdle()
        {
            UpdateTop();
            UpdateMid();
            UpdateBot();
        }

        private void UpdateRotations()
        {
            if (rotation < rotationGoal)
                rotation += (float)(Math.PI / 10f);
            else
            {
                rotation = rotationGoal;
                position = targetPosition;
            }
            if (rotationGoal > (float)Math.PI * 2 && rotation > (float)Math.PI * 2)
            {
                rotationGoal -= (float)Math.PI * 2;
                rotation -= (float)Math.PI * 2;
            }
            TopPosBase.X = (Main.screenWidth / 2 - LuxBGTexture.Width / 2 + (float)Math.Cos(rotation + 3f / 2f * Math.PI) * 100) / Main.UIScale;
            TopPosBase.Y = (Main.screenHeight / 2 - LuxBGTexture.Height / 2 + (float)Math.Sin(rotation + 3f / 2f * Math.PI) * 100 + 25) / Main.UIScale;
            MidPosBase.X = (Main.screenWidth / 2 - LuxBGTexture.Width / 2 + (float)Math.Cos(rotation + 5f / 6f * Math.PI) * 100) / Main.UIScale;
            MidPosBase.Y = (Main.screenHeight / 2 - LuxBGTexture.Height / 2 + (float)Math.Sin(rotation + 5f / 6f * Math.PI) * 100 + 25) / Main.UIScale;
            BotPosBase.X = (Main.screenWidth / 2 - LuxBGTexture.Width / 2 + (float)Math.Cos(rotation + 1f / 6f * Math.PI) * 100) / Main.UIScale;
            BotPosBase.Y = (Main.screenHeight / 2 - LuxBGTexture.Height / 2 + (float)Math.Sin(rotation + 1f / 6f * Math.PI) * 100 + 25) / Main.UIScale;
            MysticBurstPos.X = (Main.screenWidth / 2 - MysticBurstBGTexture.Width / 2) / Main.UIScale;
            MysticBurstPos.Y = (Main.screenHeight / 2 + 125) / Main.UIScale;
        }

        private void UpdateTop()
        {
            if (topTheta == -100)
                topTheta = (float)(Main.rand.NextDouble() * Math.PI * 2);
            topTheta += (float)Math.PI / 90 + (float)(Math.PI / 120 * Main.rand.NextDouble());
            if (topTheta > (float)Math.PI * 2)
                topTheta -= (float)Math.PI * 2;
            if (Main.rand.Next(20) == 0)
            {
                if (Main.rand.Next(2) == 0)
                    topMag *= .9f;
                else
                    topMag *= 1.1f;
            }
            if (topMag < 2)
                topMag = 2;
            if (topMag > 5)
                topMag = 5;
            TopPos.X = TopPosBase.X + topMag * (float)Math.Cos(topTheta);
            TopPos.Y = TopPosBase.Y + topMag * (float)Math.Sin(topTheta * 2);
        }

        private void UpdateMid()
        {
            if (midTheta == -100)
                midTheta = (float)(Main.rand.NextDouble() * Math.PI * 2);
            midTheta += (float)Math.PI / 90 + (float)(Math.PI / 120 * Main.rand.NextDouble());
            if (midTheta > (float)Math.PI * 2)
                midTheta -= (float)Math.PI * 2;
            if (Main.rand.Next(20) == 0)
            {
                if (Main.rand.Next(2) == 0)
                    midMag *= .9f;
                else
                    midMag *= 1.1f;
            }
            if (midMag < 2)
                midMag = 2;
            if (midMag > 5)
                midMag = 5;
            MidPos.X = MidPosBase.X + midMag * (float)Math.Cos(midTheta);
            MidPos.Y = MidPosBase.Y + midMag * (float)Math.Sin(midTheta * 2);
        }

        private void UpdateBot()
        {
            if (botTheta == -100)
                botTheta = (float)(Main.rand.NextDouble() * Math.PI * 2);
            botTheta += (float)Math.PI / 90 + (float)(Math.PI / 120 * Main.rand.NextDouble());
            if (botTheta > (float)Math.PI * 2)
                botTheta -= (float)Math.PI * 2;
            if (Main.rand.Next(20) == 0)
            {
                if (Main.rand.Next(2) == 0)
                    botMag *= .9f;
                else
                    botMag *= 1.1f;
            }
            if (botMag < 2)
                botMag = 2;
            if (botMag > 5)
                botMag = 5;
            BotPos.X = BotPosBase.X + botMag * (float)Math.Cos(botTheta);
            BotPos.Y = BotPosBase.Y + botMag * (float)Math.Sin(botTheta * 2);
        }

        private void UpdatePositions()
        {
            LuxBar.Position = TopPos;
            OverflowLuxBar.Position = TopPos;
            VisBar.Position = BotPos;
            OverflowVisBar.Position = BotPos;
            MundusBar.Position = MidPos;
            OverflowMundusBar.Position = MidPos;
        }

        private void UpdateMysticBurst()
        {
            LaugicalityPlayer mysticPlayer = Main.LocalPlayer.GetModPlayer<LaugicalityPlayer>();
            if (mysticBurstCooldownMax < mysticPlayer.MysticSwitchCool)
                mysticBurstCooldownMax = mysticPlayer.MysticSwitchCool;
            if (mysticPlayer.MysticSwitchCool == 0 && mysticBurstCooldownMax != 0)
            {
                mysticBurstCooldownMax = 0;
                SoundEngine.PlaySound(SoundID.MaxMana, Main.LocalPlayer.position);
            }
            MysticBurstBar.Position = MysticBurstPos;
        }

        private void UpdateHover(SpriteBatch spriteBatch)
        {
            LaugicalityPlayer mysticPlayer = Main.LocalPlayer.GetModPlayer<LaugicalityPlayer>();

            float posX = Main.MouseWorld.X - Main.screenPosition.X;
            float posY = Main.MouseWorld.Y - Main.screenPosition.Y;

            Vector2 drawPos = (Main.MouseWorld - Main.screenPosition);

            drawPos.X += 20;
            drawPos.Y += 8;
            int widthMod = 12;
            int heightMod = 8;

            if (posX > TopPosBase.X - widthMod && posX < TopPosBase.X + LuxBGTexture.Width + widthMod && posY > TopPosBase.Y - heightMod && posY < TopPosBase.Y + LuxBGTexture.Height + heightMod)
                spriteBatch.DrawString(FontAssets.MouseText.Value, Math.Round(mysticPlayer.Lux).ToString() + "/" + Math.Round(mysticPlayer.LuxMax + mysticPlayer.LuxMaxPermaBoost).ToString() + " Lux", drawPos, Color.White);
            if (posX > BotPosBase.X - widthMod && posX < BotPosBase.X + LuxBGTexture.Width + widthMod && posY > BotPosBase.Y - heightMod && posY < BotPosBase.Y + LuxBGTexture.Height + heightMod)
                spriteBatch.DrawString(FontAssets.MouseText.Value, Math.Round(mysticPlayer.Vis).ToString() + "/" + Math.Round(mysticPlayer.VisMax + mysticPlayer.VisMaxPermaBoost).ToString() + " Vis", drawPos, Color.White);
            if (posX > MidPosBase.X - widthMod && posX < MidPosBase.X + LuxBGTexture.Width + widthMod && posY > MidPosBase.Y - heightMod && posY < MidPosBase.Y + LuxBGTexture.Height + heightMod)
                spriteBatch.DrawString(FontAssets.MouseText.Value, Math.Round(mysticPlayer.Mundus).ToString() + "/" + Math.Round(mysticPlayer.MundusMax + mysticPlayer.MundusMaxPermaBoost).ToString() + " Mundus", drawPos, Color.White);
        }
    }
}

public class SpriteProgressBar : UIElement
{
    public Texture2D MainTexture = TextureAssets.MagicPixel.Value;
    public Texture2D BarTexture = TextureAssets.MagicPixel.Value;
    public Texture2D DecoTexture = TextureAssets.MagicPixel.Value;

    public Vector2 Position;
    public Vector2 PosOffset;

    public float Alpha = 1f;
    public int FrameCount = 0;
    public int FrameTimer = 0;
    public int CurrentFrame;
    public int FrameSpeed = 4;

    public Color[] Colors = new Color[] { Color.White, Color.White };

    public float[] Scales = new float[] { 1f, 1f };
    public int[] Widths = new int[] { 0, 0 };
    public int[] Heights = new int[] { 0, 0 };
    public int MaxVal;
    public int CurVal;

    public SpriteProgressBar(Texture2D mainTexture, Texture2D barTexture, Texture2D decoTexture, Vector2 pos, Vector2 offset = new Vector2(), int frameCount = 1, Color c1 = new Color(), Color c2 = new Color())
    {
        MainTexture = mainTexture;
        BarTexture = barTexture;
        DecoTexture = decoTexture;
        Position = pos;
        PosOffset = offset;

        if (c1 != new Color())
            Colors[0] = c1;

        if (c2 != new Color())
            Colors[1] = c2;
        FrameCount = frameCount;
    }

    public SpriteProgressBar(int width1, int height1, int width2, int height2, Vector2 pos, Vector2 offset)
    {
        Widths[0] = width1;
        Widths[1] = width2;
        Heights[0] = height1;
        Heights[1] = height2;
        Position = pos;
        PosOffset = offset;
    }

    public void DrawSelf(SpriteBatch spriteBatch)
    {
        #region Drawing
        int frameHeight = FrameCount > 1 ? MainTexture.Height / FrameCount : MainTexture.Height;
        Rectangle rect1 = new Rectangle(0, CurrentFrame * frameHeight, MainTexture.Width, frameHeight);
        if (Widths[0] != 0)
            rect1 = new Rectangle(0, 0, Widths[0], Heights[0]);
        Rectangle rect2 = new Rectangle(0, 0, CalcLength(CurVal, MaxVal, BarTexture.Width), BarTexture.Height);
        if (Widths[1] != 0)
            rect2 = new Rectangle(0, 0, CalcLength(CurVal, MaxVal, Widths[1]), Heights[1]);
        spriteBatch.Draw
            (
                MainTexture,
                new Vector2((int)Position.X, (int)Position.Y),
                rect1,
                Colors[0] * Alpha,
                0f,
                Vector2.Zero,
                Scales[0],
                SpriteEffects.None,
                1f
            );
        spriteBatch.Draw
            (
                BarTexture,
                new Vector2((int)Position.X, (int)Position.Y) + new Vector2((int)PosOffset.X, (int)PosOffset.Y),
                rect2,
                Colors[1] * Alpha,
                0f,
                Vector2.Zero,
                Scales[1],
                SpriteEffects.None,
                1f
            );
        spriteBatch.Draw
        (
            DecoTexture,
            new Vector2((int)Position.X, (int)Position.Y),
            rect1,
            Colors[0] * Alpha,
            0f,
            Vector2.Zero,
            Scales[0],
            SpriteEffects.None,
            1f
        );
        #endregion

        if (FrameCount > 1)
        {
            if (++FrameTimer > FrameSpeed)
            {
                if (CurrentFrame < FrameCount - 1)
                    CurrentFrame++;
                else
                    CurrentFrame = 0;
                FrameTimer = 0;
            }
        }
    }
    public void DrawSelf(SpriteBatch spriteBatch, int currentValue, int maxValue)
    {
        #region Drawing
        int frameHeight = FrameCount > 1 ? MainTexture.Height / FrameCount : MainTexture.Height;

        Rectangle rect1 = new Rectangle(0, CurrentFrame * frameHeight, MainTexture.Width, frameHeight);
        if (Widths[0] != 0)
            rect1 = new Rectangle(0, 0, Widths[0], Heights[0]);

        Rectangle rect2 = new Rectangle(0, 0, CalcLength(currentValue, maxValue, BarTexture.Width), BarTexture.Height);
        if (Widths[1] != 0)
            rect2 = new Rectangle(0, 0, CalcLength(currentValue, maxValue, Widths[1]), Heights[1]);

        spriteBatch.Draw
            (
                MainTexture,
                new Vector2((int)Position.X, (int)Position.Y),
                rect1,
                Colors[0] * Alpha,
                0f,
                Vector2.Zero,
                Scales[0],
                SpriteEffects.None,
                1f
            );

        spriteBatch.Draw
            (
                BarTexture,
                new Vector2((int)Position.X, (int)Position.Y) + new Vector2((int)PosOffset.X, (int)PosOffset.Y),
                rect2,
                Colors[1] * Alpha,
                0f,
                Vector2.Zero,
                Scales[1],
                SpriteEffects.None,
                1f
            );

        spriteBatch.Draw
            (
                DecoTexture,
                new Vector2((int)Position.X, (int)Position.Y),
                rect1,
                Colors[0] * Alpha,
                0f,
                Vector2.Zero,
                Scales[0],
                SpriteEffects.None,
                1f
            );
        #endregion

        if (FrameCount > 1)
        {
            if (++FrameTimer > FrameSpeed)
            {
                if (CurrentFrame < FrameCount - 1)
                    CurrentFrame++;
                else
                    CurrentFrame = 0;
                FrameTimer = 0;
            }
        }
    }
    public int CalcLength(int val, int maxVal, int TextureWidth)
    {
        return (val * TextureWidth) / maxVal;
    }
}
