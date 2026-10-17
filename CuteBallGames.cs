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
    private float _scale;
    MathHelper.Random _random;

    public CuteBallGames(MathHelper.Random random, int width, int height)
    {
        _width = width;
        _height = height;
        _random = random;
        _position.X = random.Next(0, width);
        Randomize();
    }

    private void Randomize()
    {
        _frequency = _random.NextFloat() * 3.0f + 1.0f;
        _amplitude = _random.NextFloat() * _height * 0.6f + 50;
        _phase = _random.NextFloat()*MathHelper.Pi;
        _hSpeed = _random.NextFloat()*100.0f + 100.0f;
        _color = new Color(_random.Next(63, 255), _random.Next(63, 255), _random.Next(63, 255), 255);
        _scale = _random.NextFloat()*0.5f + 0.5f;
    }

    public void LoadContent(ContentManager content)
    {
        _texture = content.Load<Texture2D>("cbg-crop");
    }

    public void Update(GameTime gameTime)
    {
        _oldPosition = _position;
        _position.X += _hSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (_position.X - _texture.Width/2*_scale > _width) 
        {
            Randomize();
            _position.X = -_texture.Width*_scale;
        }
        _position.Y = _height - _amplitude * (float) Math.Abs(Math.Sin(_frequency*gameTime.TotalGameTime.TotalSeconds + _phase)) - _texture.Height*0.47f*_scale;
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, _position, null, _color, (float)Math.Atan2(_position.Y - _oldPosition.Y, _position.X - _oldPosition.X), new Vector2(_texture.Width/2, _texture.Height/2), _scale, SpriteEffects.None, 0);
    }
}