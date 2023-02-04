using UnityEngine;

public class RootSpawnerManager : MonoBehaviour {
  private float spanwOffsetX_ = 2.0f;
  private int spawnSide_;
    [SerializeField] private GameObject vitPrefab_;
    [SerializeField] private int maxSpawnCount=10;
    private int enemysCount;

    void Start(){
        GameManager.Instance.AddDificultAction += UpdateDifficult;
        enemysCount = maxSpawnCount;
    }
    private void UpdateDifficult()
    {
        if (GameManager.Instance.EnemiesCount!=0 && GameManager.Instance.EnemiesCount % maxSpawnCount == 0)
        {
            maxSpawnCount = enemysCount + GameManager.Instance.currentDifficulty_;
            GameManager.Instance.EnemiesCount = 0;
            SetSpawnVitamin();
        }
    }

  void SetSpawnVitamin(){
    switch(Random.Range(0, 2)){
      case 0: spawnSide_ = 1; break;
      case 1: spawnSide_ = -1; break;
    }
    Vector3 spawnPosition = new Vector3((GameManager.Instance.screenWidth_ * spawnSide_) - (spanwOffsetX_ * spawnSide_), -3.0f, 0.0f);
    Instantiate(vitPrefab_, spawnPosition, Quaternion.identity);
  }


}
