using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public Camera MainCamera { get; private set; }

    public static event Action OnLivesChanged = delegate { };

    [SerializeField]
    private int lives = 3;
    public int Lives
    {
        get => lives;
        set
        {
            lives = value;
            OnLivesChanged.Invoke();
            if (lives <= 0)
                StartCoroutine(EndGame());
        }
    }

    
    protected override void Awake()
    {
        base.Awake();
        MainCamera = Camera.main;
    }

    private void OnEnable()
    {
        Tree.OnTreesCountChanged += CheckForWin;
    }

    private void OnDisable()
    {
        Tree.OnTreesCountChanged -= CheckForWin;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        HandleSowing();
    }

    private void HandleSowing()
    {
        if (Time.timeScale == 0f)
            return;

        SowingManager.Instance.TryChangeCurrentPlant();

        if (!SowingManager.Instance.IsSowing)
            return;

        SowingManager.Instance.ShootRaycast();

        if (Input.GetButtonDown("Fire1"))
            SowingManager.Instance.TrySow();
    }

    private void CheckForWin()
    {
        if (Tree.Trees < 100)
            return;

        StartCoroutine(EndGame());
    }

    private IEnumerator EndGame()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(2f);
        var op = SceneManager.LoadSceneAsync("Game");
        Time.timeScale = 1f;
    }
}
