﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WzComparerR2.Animation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WzComparerR2.Rendering
{
    public class AnimationGraphics
    {
        public AnimationGraphics(GraphicsDevice graphicsDevice)
            : this (graphicsDevice, new SpriteBatch(graphicsDevice))
        {
        }

        public AnimationGraphics(GraphicsDevice graphicsDevice, SpriteBatch sprite)
        {
            this.GraphicsDevice = graphicsDevice;
            this.sprite = sprite;
            this.spineRenderer = new Spine.SkeletonMeshRenderer(graphicsDevice);
        }

        public GraphicsDevice GraphicsDevice { get; private set; }

        private SpriteBatch sprite;
        private Spine.SkeletonMeshRenderer spineRenderer;

        public void Draw(FrameAnimator animator, Matrix world)
        {
            Frame frame = animator.CurrentFrame;
            if (frame != null && frame.Texture != null)
            {
                if (animator.Position != Point.Zero)
                {
                    world *= Matrix.CreateTranslation(animator.Position.X, animator.Position.Y, 0);
                }

                sprite.Begin(SpriteSortMode.Deferred, BlendEx.NonPremultipled_Hidef, transformMatrix: world);
                sprite.Draw(frame.Texture,
                    position: Vector2.Zero,
                    origin: frame.Origin.ToVector2(),
                    rotation: 0,
                    color: new Color(Color.White, frame.A0));
                sprite.End();
            }
        }

        public void Draw(SpineAnimator animator, Matrix world)
        {
            Spine.Skeleton skeleton = animator.Skeleton;

            if (skeleton != null)
            {
                if (animator.Position != Point.Zero)
                {
                    world *= Matrix.CreateTranslation(animator.Position.X, animator.Position.Y, 0);
                }

                spineRenderer.PremultipliedAlpha = animator.Data.PremultipliedAlpha;
                spineRenderer.Effect.World = world;
                skeleton.X = 0;
                skeleton.Y = 0;
                spineRenderer.Begin();
                spineRenderer.Draw(animator.Skeleton);
                spineRenderer.End();
            }
        }

    }
}
