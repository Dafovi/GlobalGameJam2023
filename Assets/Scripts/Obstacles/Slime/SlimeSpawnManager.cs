using UnityEngine;

public class SlimeSpawnManager : MonoBehaviour {

  [SerializeField] private GameObject sisterFungiRef_;
  [SerializeField] private GenericPool pool_;

  private Vector3 spawnPosition_;
  private int difficultToSpawn_;
  public int spawnDirection_;

  void Start() {
    pool_ = GetComponent<GenericPool>();

  }

  void Update(){
    if(GameManager.Instance.currentDifficulty_ > difficultToSpawn_){

    }

    if(Input.GetButtonDown("Jump")){
      SetSlimeSpawn();
    }

  }

  void SetSlimeSpawn(){
    float distanceFromLeftScreen = Vector3.Distance(sisterFungiRef_.transform.position, GameManager.Instance.leftScreen_);
    float distanceFromRightScreen = Vector3.Distance(sisterFungiRef_.transform.position, GameManager.Instance.rightScreen_);

    spawnDirection_ = 1;
    if(distanceFromLeftScreen > distanceFromRightScreen){
      spawnDirection_ = -1;
    }

    spawnPosition_ = new Vector3(GameManager.Instance.screenWidth_ * spawnDirection_, -4.4f, 0.0f);
    pool_.GetFromPool(spawnPosition_);
  }


}
