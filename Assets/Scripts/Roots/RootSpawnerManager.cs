using UnityEngine;

public class RootSpawnerManager : MonoBehaviour {

  private GameManager gameManagerRef_;
  private float spanwOffsetX_ = 2.0f;
  private int spawnSide_;
  [SerializeField] private GameObject vitPrefab_;

  private float currentTimeToSpawn_;
  private float nextTimeToSpawn_;
  [SerializeField] private float maxTimeToSpawn_;
  [SerializeField] private float minTimeToSpawn_;

  void Start(){
    gameManagerRef_ = FindObjectOfType<GameManager>();
    nextTimeToSpawn_ = Random.Range(minTimeToSpawn_, maxTimeToSpawn_);
        GameManager.Instance.AddDificultAction += UpdateDifficult;
  }

    private void UpdateDifficult()
    {
        currentTimeToSpawn_ = 0.0f;
        minTimeToSpawn_ += 1.25f * GameManager.Instance.currentDifficulty_;
        maxTimeToSpawn_ += 1.5f * GameManager.Instance.currentDifficulty_;
        nextTimeToSpawn_ = Random.Range(minTimeToSpawn_, maxTimeToSpawn_);
    }
  void Update(){
    if(currentTimeToSpawn_ > nextTimeToSpawn_){
      SetSpawnVitamin();
    }
    currentTimeToSpawn_ += Time.deltaTime;
  }

  void SetSpawnVitamin(){
    switch(Random.Range(0, 2)){
      case 0: spawnSide_ = 1; break;
      case 1: spawnSide_ = -1; break;
    }

    Vector3 spawnPosition = new Vector3((gameManagerRef_.screenWidth_ * spawnSide_) - (spanwOffsetX_ * spawnSide_), -3.0f, 0.0f);
    Instantiate(vitPrefab_, spawnPosition, Quaternion.identity);

    currentTimeToSpawn_ = 0.0f;
  }


}
