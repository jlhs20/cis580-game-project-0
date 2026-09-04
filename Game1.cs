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
    private Texture2D _pisces;
    private int _animationFrame = 0;
    private double _animationTimer = 0.0f;

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
        _pisces = Content.Load<Texture2D>("piscesfilm");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        foreach (CuteBallGames cbg in _cbgs)
        {
            cbg.Update(gameTime);
        }
        _animationTimer += gameTime.ElapsedGameTime.TotalSeconds;
        if (_animationTimer > 0.07f) 
        {
            _animationFrame = (_animationFrame + 1)%36;
            _animationTimer -= 0.07f;
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        //GraphicsDevice.Clear(Color.Green);

        _spriteBatch.Begin();
        for (int x = 0; x < GraphicsDevice.Viewport.Width; x += 300)
        {
            for (int y = 0; y < GraphicsDevice.Viewport.Height; y += 300)
            {
                _spriteBatch.Draw(_pisces, new Vector2(x, y), new Rectangle(((_animationFrame+x+y)%36)*300, 0, 300, 300), Color.White);
            }
        }
        _spriteBatch.Draw(_background, new Vector2(0, 0), null, new Color(63, 63, 63, 63));
        foreach (CuteBallGames cbg in _cbgs)
        {
            cbg.Draw(gameTime, _spriteBatch);
        }
        _spriteBatch.DrawString(_font, "Exit game with ESC", new Vector2(0, 0), Color.Black);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
