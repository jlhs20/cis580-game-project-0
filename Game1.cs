using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace cis580_game_project_0;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private CuteBallGames[] _cbgs;
    private MathHelper.Random _random = new();
    private Texture2D _background;
    private SpriteFont _font;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _cbgs = new[]
        {
            new CuteBallGames(_random, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height),
            new CuteBallGames(_random, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height),
            new CuteBallGames(_random, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height),
            new CuteBallGames(_random, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height)
        };

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        foreach (CuteBallGames cbg in _cbgs)
        {
            cbg.LoadContent(Content);
        }

        _background = Content.Load<Texture2D>("noiseTexture-1024-4");
        _font = Content.Load<SpriteFont>("arial");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        foreach (CuteBallGames cbg in _cbgs)
        {
            cbg.Update(gameTime);
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        _spriteBatch.Draw(_background, new Vector2(0, 0), null, Color.CornflowerBlue);
        foreach (CuteBallGames cbg in _cbgs)
        {
            cbg.Draw(gameTime, _spriteBatch);
        }
        _spriteBatch.DrawString(_font, "Exit game with ESC", new Vector2(0, 0), Color.Gold);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
