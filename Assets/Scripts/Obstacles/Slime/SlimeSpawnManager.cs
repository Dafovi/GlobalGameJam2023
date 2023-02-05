using UnityEngine;

public class SlimeSpawnManager : MonoBehaviour {

  [SerializeField] private GameObject sisterFungiRef_;
  [SerializeField] private GenericPool pool_;

  public GameObject leftSpawn_;
  public GameObject rightSpawn_;

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

    switch(Random.Range(0, 2)){
      case 0: 
        spawnPosition_ = leftSpawn_.transform.position;
        spawnDirection_ = 1;
        break;
      case 1:
        spawnPosition_ = rightSpawn_.transform.position;
        spawnDirection_ = -1;
        break;
    }

    pool_.GetFromPool(spawnPosition_);
  }


}
