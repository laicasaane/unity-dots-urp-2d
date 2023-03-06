using ZBase.Collections.Pooled.Generic;
using UnityEngine;
using ZBase.Collections.Pooled.Generic.Internals;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _prefabs;

    [SerializeField]
    private int _spawnAmount;

    private readonly List<SpriteAnimator> _animators = new();
    private Vector3 _minPos, _maxPos;
    private int _currentPrefabIndex;
    private int _spawnTimes;
    private Unity.Mathematics.Random _random;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = (int)Screen.currentResolution.refreshRateRatio.value / Application.targetFrameRate;
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
    }

    private void Start()
    {
        var camera = Camera.main;
        _minPos = camera.ScreenToWorldPoint(new Vector3(128, 0, 0));
        _maxPos = camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height - 128, 0f));
        _random = new Unity.Mathematics.Random((uint)System.DateTime.Now.Ticks);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            _currentPrefabIndex++;

            if (_currentPrefabIndex >= _prefabs.Length)
                _currentPrefabIndex = 0;

            Spawn(_spawnAmount);
        }
    }

    private void LateUpdate()
    {
        var animators = _animators.AsReadOnlySpan();
        var length = animators.Length;

        for (var i = 0; i < length; i++)
        {
            animators[i].UpdateFrame();
        }
    }

    private void Spawn(int amount)
    {
        var prefab = _prefabs[_currentPrefabIndex];
        var animators = _animators;
        var pos = _random.NextFloat3(_minPos, _maxPos);

        for (var i = 0; i < amount; i++)
        {
            _spawnTimes++;

            var go = Instantiate(prefab);
            go.name = $"{prefab.name}_{_spawnTimes}";
            go.transform.position = new Vector3(pos.x, pos.y, 0f);

            var animator = go.GetComponent<SpriteAnimator>();
            animators.Add(animator);
        }
    }
}
