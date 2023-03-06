using UnityEngine;
using UnityEngine.U2D.Animation;

[RequireComponent(typeof(SpriteResolver))]
public class SpriteAnimator : MonoBehaviour
{
    [SerializeField]
    private float _frameDuration;

    [SerializeField]
    private string _animName;

    [SerializeField]
    private int _maxFrameIndex;

    private SpriteResolver _resolver;
    private int _currentFrameIndex;
    private float _elapsed;

    private void Awake()
    {
        _resolver = GetComponent<SpriteResolver>();
    }

    private void Start()
    {
        _currentFrameIndex = 0;
        _elapsed = 0f;
    }

    public void UpdateFrame()
    {
        _elapsed += Time.deltaTime;

        if (_elapsed < _frameDuration)
            return;

        _elapsed = 0f;
        _currentFrameIndex++;

        if (_currentFrameIndex > _maxFrameIndex)
            _currentFrameIndex = 0;

        _resolver.SetCategoryAndLabel(_animName, $"{_animName}_{_currentFrameIndex}");
    }
}
