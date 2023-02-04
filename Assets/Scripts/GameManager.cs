using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;
    public int currentDifficulty_ = 10;
    public Vector3 leftScreen_;
    public Vector3 rightScreen_;
    public float screenHeight_;
    public float screenWidth_;
    public Action AddDificultAction;


    [Header("Fungis life")]
    [SerializeField] private int fungiLifes = 5;
    [SerializeField] private int fungiSisterLifes = 5;
    [SerializeField,Space(10)]private int enemyDefeatCount;

    public bool GameOver {get; set;}
    public int EnemiesCount { get; set; }

    public int vitaminCount_;

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
            AddDificultAction?.Invoke();
        }
    }
    void Update() {

        if(Input.GetKeyDown(KeyCode.Delete)){
            currentDifficulty_++;
        }
    }
    public void FungiDamage()
    {
        if (fungiLifes > 0)
            fungiLifes--;
        else
            GameOver = true;
    }
    public void FungiSisterDamage()
    {
        if (fungiSisterLifes > 0)
            fungiSisterLifes--;
        else
            GameOver = true;
    }
    public void AddEnemyDefeatCount()
    {
        AddDificultAction?.Invoke();
        enemyDefeatCount++;
        EnemiesCount++;
    }
}
