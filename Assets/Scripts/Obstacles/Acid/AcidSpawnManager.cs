using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AcidSpawnManager : MonoBehaviour {

  public GenericPool acidSpawnerPoolRef_;
  private float cameraHeight_;
  private float cameraWidth_;

  public Action SearchAcidAction;

  private float nextTime_;
  private float currentTime_;
  [SerializeField] private float initialTime_;

  void Start() {
    acidSpawnerPoolRef_ = GetComponent<GenericPool>();
    cameraHeight_ = Camera.main.orthographicSize;
    cameraWidth_ = cameraHeight_ * Camera.main.aspect;
    nextTime_ = 1.0f;
  }

  void Update() {
    if(currentTime_ > nextTime_){
      SetSpawnPoint();
      currentTime_ = 0.0f;
    }
    currentTime_ += Time.deltaTime;
  }

  void SetSpawnPoint(){
    float y = Camera.main.orthographicSize*0.24f;
    float x = cameraHeight_ * Camera.main.aspect - 2.0f;
    float final_x = UnityEngine.Random.Range(-x, x);

    Vector3 new_position = new Vector3(final_x, y, 0.0f);
    acidSpawnerPoolRef_.GetFromPool(new_position);

    nextTime_ = initialTime_ - 0.15f * GameManager.Instance.currentDifficulty_;

    SearchAcidAction?.Invoke();
  }


}
