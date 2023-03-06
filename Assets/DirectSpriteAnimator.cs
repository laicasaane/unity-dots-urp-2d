using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public sealed class DirectSpriteAnimator : SpriteAnimator
{
    [SerializeField]
    private Sprite[] _sprites;

    [SerializeField]
    private float _frameDuration;

    private SpriteRenderer _spriteRenderer;
    private int _currentFrameIndex;
    private float _elapsed;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _currentFrameIndex = 0;
        _elapsed = 0f;

        SetSprite(_currentFrameIndex);
    }

    public override void UpdateFrame()
    {
        _elapsed += Time.deltaTime;

        if (_elapsed < _frameDuration)
            return;

        _elapsed = 0f;
        _currentFrameIndex++;

        if (_currentFrameIndex >= _sprites.Length)
            _currentFrameIndex = 0;

        SetSprite(_currentFrameIndex);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private void SetSprite(int index)
    {
        _spriteRenderer.sprite = _sprites[index];
    }
}
