using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace cis580_game_project_0;

public class CuteBallGames
{
    private Texture2D _texture;
    private float _frequency;
    private float _amplitude;
    private float _phase;
    private float _hSpeed;
    private int _width;
    private int _height;
    private Vector2 _position = new Vector2(0.0f, 0.0f);
    private Vector2 _oldPosition;
    private Color _color;

    public CuteBallGames(MathHelper.Random random, int width, int height)
    {
        _width = width;
        _height = height;
        _frequency = random.NextFloat() * 3.0f + 1.0f;
        _amplitude = random.NextFloat() * _height * 0.6f + 50;
        _phase = random.NextFloat()*MathHelper.Pi;
        _hSpeed = random.NextFloat()*50.0f + 50.0f;
        _position.X = width*random.NextFloat();
        _color = new Color(random.Next(63, 255), random.Next(63, 255), random.Next(63, 255), 255);
    }

    public void LoadContent(ContentManager content)
    {
        _texture = content.Load<Texture2D>("cbg-crop");
    }

    public void Update(GameTime gameTime)
    {
        _oldPosition = _position;
        _position.X += _hSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (_position.X - _texture.Width/2 > _width) _position.X -= _width + _texture.Width;
        _position.Y = _height - _amplitude * (float) Math.Abs(Math.Sin(_frequency*gameTime.TotalGameTime.TotalSeconds + _phase)) - _texture.Height/2;
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, _position, null, _color, (float)Math.Atan((_position.Y - _oldPosition.Y)/(_position.X - _oldPosition.X)), new Vector2(_texture.Width/2, _texture.Height/2), 1.0f, SpriteEffects.None, 0);
    }
}