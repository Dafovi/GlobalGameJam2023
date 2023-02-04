using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidSpawnManager : MonoBehaviour {

  //De prueba, habra que ajustar los tiempos a la animacion o algo asi
  public float timeToSpawn_ = 1.0f;
  public float currenTime = 0.0f;

  private GenericPool acidSpawnerPoolRef_;
  private float cameraHeight_;
  private float cameraWidth_;

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
    float final_x = Random.Range(-x, x);

    Vector3 new_position = new Vector3(final_x, y, 0.0f);
    acidSpawnerPoolRef_.GetFromPool(new_position);
  }


}
