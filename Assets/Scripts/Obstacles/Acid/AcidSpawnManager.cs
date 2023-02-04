using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AcidSpawnManager : MonoBehaviour {

  public float timeToSpawn_ = 1.0f;
  public float currenTime = 0.0f;

  public GenericPool acidSpawnerPoolRef_;
  private float cameraHeight_;
  private float cameraWidth_;

  public Action SearchAcidAction;

  void Start() {
    acidSpawnerPoolRef_ = GetComponent<GenericPool>();
    cameraHeight_ = Camera.main.orthographicSize * 2.0f;
    cameraWidth_ = cameraHeight_ * Camera.main.aspect;
  }

  void Update() {
    if(currenTime > timeToSpawn_){
      SetSpawnPoint();
      currenTime = 0.0f;
    }
    currenTime += Time.deltaTime;
  }

  void SetSpawnPoint(){
    float y = Camera.main.orthographicSize;
    float x = y * Camera.main.aspect - 2.0f;
    float final_x = UnityEngine.Random.Range(-x, x);

    Vector3 new_position = new Vector3(final_x, y, 0.0f);
    acidSpawnerPoolRef_.GetFromPool(new_position);
    SearchAcidAction?.Invoke();
  }


}
