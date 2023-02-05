using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Events;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;
    public int currentDifficulty_ = 10;
    public Vector3 leftScreen_;
    public Vector3 rightScreen_;
    public float screenHeight_;
    public float screenWidth_;
    public Action EnemyHit;
    public Action EnemyDie;
    


    [Header("Fungis life")]
    [SerializeField] private List<GameObject> fungiLifes;
    [SerializeField] private List<GameObject> fungiSisterLifes;
    [SerializeField,Space(10)]private int enemyDefeatCount;
    [SerializeField] private int maxVitaminas=5;
    [SerializeField] private UnityEvent OnEndGame;
    [Header("BackGround")]
    [SerializeField] private List<Sprite> backgrounds;
    [SerializeField] private SpriteRenderer bg;

    [Header("LoseScreens")]
    [SerializeField] private GameObject hongo;
    [SerializeField] private GameObject honga;


    private int currentVitaminas;
    public bool GameOver {get; set;}
    public int EnemiesCount {get; set;}

    [SerializeField] private AudioSource sfx_;
    public AudioSource Sfx_ { get=>sfx_; set=>sfx_ = value; } 

    [SerializeField] private AudioClip vitaminaAudioClip_;
    [SerializeField] private AudioClip hitAudioClip_;

    private void Awake()
    {
        Instance = this;
    }
    void Start() {
        screenHeight_ = Camera.main.orthographicSize;
        screenWidth_ = screenHeight_ * Camera.main.aspect;
        leftScreen_ = new Vector3(-screenWidth_, -3.0f, 0.0f);
        rightScreen_ = new Vector3(screenWidth_, -3.0f, 0.0f);
    }
    public void AddDificult()
    {
        if (currentDifficulty_ < 10)
        {
            currentDifficulty_++;
            EnemyHit?.Invoke();
        }
    }
    void Update() {

        if(Input.GetKeyDown(KeyCode.Delete)){
            currentDifficulty_++;
        }
    }
    public void FungiDamage()
    {
        if (fungiLifes.Count > 1)
        {
            fungiLifes[fungiLifes.Count - 1].SetActive(false);
            fungiLifes.RemoveAt(fungiLifes.Count - 1);
        }
        else
        {
            hongo.SetActive(true);
            fungiLifes[0].SetActive(false);
            GameOver = true;
            StartCoroutine(SceneController.Instance.ChangeSceneDelay(3f, "IntialScene"));
        }
    }
    public void FungiSisterDamage()
    {
        sfx_.PlayOneShot(hitAudioClip_, 1);
        if (fungiSisterLifes.Count > 1)
        {
            fungiSisterLifes[fungiSisterLifes.Count - 1].SetActive(false);
            fungiSisterLifes.RemoveAt(fungiSisterLifes.Count - 1);
        }
        else
        {
            honga.SetActive(true);
            fungiSisterLifes[0].SetActive(false);
            GameOver = true;
            StartCoroutine(SceneController.Instance.ChangeSceneDelay(3f, "IntialScene"));
        }
    }
    public void AddAnimationCount()
    {
        EnemyDie?.Invoke();
        enemyDefeatCount++;
        EnemiesCount++;
    }
    public void AddVitaminas()
    {
        sfx_.PlayOneShot(vitaminaAudioClip_, 1);
        currentVitaminas++;
        if (currentVitaminas == maxVitaminas)
        {
            OnEndGame.Invoke();
        }
        bg.sprite = backgrounds[currentVitaminas];

        AddDificult();
    }
    public static float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
