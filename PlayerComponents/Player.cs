using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Psilibrary.TileEngine;
using Psilibrary;
using Psilibrary.CharacterComponents;

namespace EyesOfTheDragon.PlayerComponents
{
    public class Player : DrawableGameComponent
    {
        #region Field Region

        private Game1 gameRef;
        private string name;
        private bool gender;
        private Character character;
        private Dictionary<string, string> charactersMet = new Dictionary<string, string>();
        private Dictionary<int, string> keysFound = new Dictionary<int, string>();

        private AnimatedSprite sprite;
        private float speed = 250f;
        private long gold;
        private long experience;
        private TimeSpan elapsedTime = TimeSpan.Zero;
        private TimeSpan totalTime = TimeSpan.Zero;

        #endregion

        #region Property Region

        public TimeSpan ElapasedTime
        {
            get { return elapsedTime; }
        }

        public TimeSpan TotalTime
        {
            get { return totalTime; }
        }

        public long Gold
        {
            get { return gold; }
        }

        public long Experience
        {
            get { return experience; }
        }

        public Dictionary<string, string>  CharactersMet
        {
            get { return charactersMet; }
        }

        public Dictionary<int, string> KeysFound
        {
            get { return keysFound; }
        }

        public Vector2 Position
        {
            get { return sprite.Position; }
            set { sprite.Position = value; }
        }

        public AnimatedSprite Sprite
        {
            get { return sprite; }
            private set { sprite = value; }
        }

        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        #endregion

        #region Constructor Region

        private Player(Game game)
            : base(game)
        {
        }

        public Player(Game game, string name, bool gender, Character character)
            : base(game)
        {
            gameRef = (Game1)game;

            this.name = name;
            this.gender = gender;
            this.character = character;

            this.sprite = new AnimatedSprite(
                game.Content.Load<Texture2D>(
                    @"CharacterSprites\healer_f"),
                Game1.Animations);
            this.sprite.CurrentAnimation = AnimationKey.WalkDown;
        }

        #endregion

        #region Method Region

        public void SavePlayer()
        {
        }

        public static Player Load(Game game)
        {
            Player player = new Player(game);

            return player;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            sprite.Update(gameTime);
            totalTime += gameTime.ElapsedGameTime;
            elapsedTime = gameTime.ElapsedGameTime;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            sprite.Draw(gameTime, gameRef.SpriteBatch);
        }

        #endregion

        public void UpdateGold(long amount)
        {
            gold += amount;
        }

        public void UpdateExp(long amount)
        {
            experience += amount;
        }

        public void SetClass(string className)
        {
        }

        public void AddKey(int id, string name)
        {
            if (!keysFound.ContainsKey(id))
                keysFound.Add(id, name);
        }

        public void RemoveKey(int id)
        {
            if (keysFound.ContainsKey(id))
                keysFound.Remove(id);
        }

        public void ResetTime()
        {
            elapsedTime = TimeSpan.Zero;
            totalTime = TimeSpan.Zero;
        }
    }
}
