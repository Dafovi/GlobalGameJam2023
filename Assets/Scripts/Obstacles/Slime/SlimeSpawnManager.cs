using UnityEngine;

public class SlimeSpawnManager : MonoBehaviour {

  [SerializeField] private GameObject sisterFungiRef_;
  [SerializeField] private GameObject slimePrefab_;
  private GameManager gameManagerRef_;

  private Vector3 spawnPosition_;
  public int spawnDirection_;

  private float currentTimeToSpawn_;
  private float nextTimeToSpawn_;
  [SerializeField] private float minTimeToSpawn_;
  [SerializeField] private float maxTimeToSpawn_;

  [SerializeField] private int difficultToSpawn_;

  void Start() {
    gameManagerRef_ = FindObjectOfType<GameManager>();

    nextTimeToSpawn_ = Random.Range(minTimeToSpawn_, maxTimeToSpawn_);
  }

  void Update(){
    if(gameManagerRef_.currentDifficulty_ > difficultToSpawn_){
      if(currentTimeToSpawn_ > nextTimeToSpawn_){
        SetSlimeSpawn();
      }
    }
    currentTimeToSpawn_ += Time.deltaTime;
  }

  void SetSlimeSpawn(){
    float distanceFromLeftScreen = Vector3.Distance(sisterFungiRef_.transform.position, gameManagerRef_.leftScreen_);
    float distanceFromRightScreen = Vector3.Distance(sisterFungiRef_.transform.position, gameManagerRef_.rightScreen_);

    spawnDirection_ = 1;
    if(distanceFromLeftScreen > distanceFromRightScreen){
      spawnDirection_ = -1;
    }

    spawnPosition_ = new Vector3(gameManagerRef_.screenWidth_ * spawnDirection_, -3.0f, 0.0f);
    Instantiate(slimePrefab_, spawnPosition_, Quaternion.identity);

    minTimeToSpawn_ -= (0.15f * GameManager.Instance.currentDifficulty_);
    maxTimeToSpawn_ -= (0.15f * GameManager.Instance.currentDifficulty_);

    nextTimeToSpawn_ = Random.Range(minTimeToSpawn_, maxTimeToSpawn_);
    currentTimeToSpawn_ = 0.0f;
  }


}
