using UnityEngine;

public class BugSpawnerManager : MonoBehaviour {

  private GenericPool bugPool_;
  private GameManager gameManagerRef_;
  private float spawn_x;

  [SerializeField] private float minTimeToSpawn_;
  [SerializeField] private float maxTimeToSpawn_;
  [SerializeField] private int dificultyToSpawn_;

  private float nextTimeToSpawn_;
  private float currentTimeToSpawn_;

  void Start() {
    bugPool_ = GetComponent<GenericPool>();
    gameManagerRef_ = FindObjectOfType<GameManager>();

    nextTimeToSpawn_ = Random.Range(minTimeToSpawn_, maxTimeToSpawn_);
  }

  void Update() {
    if(gameManagerRef_.currentDifficulty_ > dificultyToSpawn_){
      if(currentTimeToSpawn_ > nextTimeToSpawn_){
        SetBugSpawn();
      }
      currentTimeToSpawn_ += Time.deltaTime;
    }
  }

  void SetBugSpawn(){
    float spanwOffsetY = Random.Range(1.0f, 3.0f);
  
    switch(Random.Range(0, 2)){
      case 0: spawn_x = gameManagerRef_.screenWidth_; break;
      case 1: spawn_x = -gameManagerRef_.screenWidth_; break;
    }

    Vector3 spanwPosition = new Vector3(spawn_x, gameManagerRef_.screenHeight_ - spanwOffsetY, 0.0f);
    bugPool_.GetFromPool(spanwPosition);

    nextTimeToSpawn_ = Random.Range(minTimeToSpawn_, maxTimeToSpawn_);
    currentTimeToSpawn_ = 0.0f;
  }


}
