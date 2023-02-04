using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

  public int currentDifficulty_ = 10;
  private Vector3 leftScreen;
  private Vector3 rightScreen;
  private float spawn_x;

  void Start() {
    leftScreen = Camera.main.orthographicSize;
      
  }

  void Update()
  {

    if(Input.GetKeyDown(KeyCode.Delete)){
      currentDifficulty_++;
    }
      
  }
}
