using UnityEngine;

public class SlimeSpawnManager : MonoBehaviour {

  [SerializeField] private GameObject sisterFungiRef_;
  [SerializeField] private GameObject slimePrefab_;
  private GameManager gameManagerRef_;

  private Vector3 spawnPosition_;
  private float spawn_x;
  public int spawnDirection_;

  private float currentTimeToSpawn_;
  private float nextTimeToSpawn_;
  [SerializeField] private float minTimeToSpawn_;
  [SerializeField] private float maxTimeToSpawn_;

  void Start() {
    gameManagerRef_ = FindObjectOfType<GameManager>();
    spawn_x = Camera.main.orthographicSize * Camera.main.aspect;
    leftScreen = new Vector3(-spawn_x, -3.0f, 0.0f);
    rightScreen = new Vector3(spawn_x, -3.0f, 0.0f);

    nextTimeToSpawn_ = Random.Range(minTimeToSpawn_, maxTimeToSpawn_);
  }

  void Update(){
    if(gameManagerRef_.currentDifficulty_ > 2){
      if(currentTimeToSpawn_ > nextTimeToSpawn_){
        SetSlimeSpawn();
      }
    }
    currentTimeToSpawn_ += Time.deltaTime;
  }

  void SetSlimeSpawn(){
    float distanceFromLeftScreen = Vector3.Distance(sisterFungiRef_.transform.position, leftScreen);
    float distanceFromRightScreen = Vector3.Distance(sisterFungiRef_.transform.position, rightScreen);

    spawnDirection_ = 1;
    if(distanceFromLeftScreen > distanceFromRightScreen){
      spawnDirection_ = -1;
    }

    spawnPosition_ = new Vector3(spawn_x * spawnDirection_, -3.0f, 0.0f);
    Instantiate(slimePrefab_, spawnPosition_, Quaternion.identity);

    nextTimeToSpawn_ = Random.Range(minTimeToSpawn_, maxTimeToSpawn_);
    currentTimeToSpawn_ = 0.0f;
  }


}
