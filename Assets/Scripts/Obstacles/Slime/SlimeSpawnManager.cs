using UnityEngine;

public class SlimeSpawnManager : MonoBehaviour {

  [SerializeField] private GameObject sisterFungiRef_;
  [SerializeField] private GenericPool slimePoolRef_;

  public GameObject leftSpawn_;
  public GameObject rightSpawn_;

  private Vector3 spawnPosition_;
  public int spawnDirection_;
  public int spawnedSlimes_;

  void Start() {
    slimePoolRef_ = GetComponent<GenericPool>();
    GameManager.Instance.EnemyHit += SetSlimeSpawn;
  }

  void SetSlimeSpawn(){
    if(spawnedSlimes_ < slimePoolRef_.pool_.Count){
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
      spawnedSlimes_++;
      slimePoolRef_.GetFromPool(spawnPosition_);
    }
  }


}
