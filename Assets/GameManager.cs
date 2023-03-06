using ZBase.Collections.Pooled.Generic;
using UnityEngine;
using ZBase.Collections.Pooled.Generic.Internals;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _prefabsA;

    [SerializeField]
    private GameObject[] _prefabsB;

    [SerializeField]
    private int _spawnAmount;

    private readonly List<SpriteAnimator> _animators = new();
    private Vector3 _minPos, _maxPos;
    private int _currentPrefabAIndex;
    private int _currentPrefabBIndex;
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
        if (Input.GetKeyUp(KeyCode.A))
        {
            _currentPrefabAIndex++;

            if (_currentPrefabAIndex >= _prefabsA.Length)
                _currentPrefabAIndex = 0;

            Spawn(_spawnAmount, _currentPrefabAIndex, _prefabsA);
        }

        if (Input.GetKeyUp(KeyCode.B))
        {
            _currentPrefabBIndex++;

            if (_currentPrefabBIndex >= _prefabsB.Length)
                _currentPrefabBIndex = 0;

            Spawn(_spawnAmount, _currentPrefabBIndex, _prefabsB);
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

    private void Spawn(int amount, int currentIndex, GameObject[] prefabs)
    {
        var prefab = prefabs[currentIndex];
        var animators = _animators;

        for (var i = 0; i < amount; i++)
        {
            _spawnTimes++;

            var go = Instantiate(prefab);
            go.name = $"{prefab.name}_{_spawnTimes}";

            var pos = _random.NextFloat3(_minPos, _maxPos);
            go.transform.position = new Vector3(pos.x, pos.y, 0f);

            var animator = go.GetComponent<SpriteAnimator>();
            animators.Add(animator);
        }
    }
}
