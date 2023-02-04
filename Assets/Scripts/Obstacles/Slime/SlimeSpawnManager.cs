using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSpawnManager : MonoBehaviour {

  [SerializeField] private GameObject sisterFungiRef_;
  [SerializeField] private GameObject slimePrefab_;

  // private float maxTime_ = 1.0f;
  // private float currentTime_ = 0.0f;
  private Vector3 spawnPosition_;
  private Vector3 leftScreen;
  private Vector3 rightScreen;
  private float spawn_x;
  
  public int spawnDirection_;

  void Start() {
    spawn_x = Camera.main.orthographicSize * Camera.main.aspect;
    leftScreen = new Vector3(-spawn_x, -3.0f, 0.0f);
    rightScreen = new Vector3(spawn_x, -3.0f, 0.0f);
  }

  void Update(){
    // if(currentTime_ > maxTime_){
    //   SetSlimeSpawn();
    //   currentTime_ = 0.0f;
    // }
    // currentTime_ += Time.deltaTime;
  
    if(Input.GetButtonDown("Fire1")){
      SetSlimeSpawn();
    }
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
  }


}
