using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SlimeSpawnManager : MonoBehaviour {

  [SerializeField] private GameObject sisterFungiRef_;
  [SerializeField] private GenericPool slimePoolRef_;

  public GameObject leftSpawn_;
  public GameObject rightSpawn_;

  private Vector3 spawnPosition_;
  public int spawnDirection_;

  [SerializeField] private int timeToSpawn_;

    IEnumerator Start() {
    slimePoolRef_ = GetComponent<GenericPool>();
    GameManager.Instance.EnemyHit += SetSlimeSpawn;
        while (true)
        {
            yield return new WaitForSeconds(5);
            SetSlimeSpawn();
            yield return null;
        }
  }

  void SetSlimeSpawn(){
    int spawn = Random.Range(0, timeToSpawn_ - GameManager.Instance.currentDifficulty_);
    if(spawn == 0){
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
      slimePoolRef_.GetFromPool(spawnPosition_);
    }
  }


}
