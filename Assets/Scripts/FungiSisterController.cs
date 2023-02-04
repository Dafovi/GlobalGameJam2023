using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FungiSisterController : MonoBehaviour {

  [SerializeField] private List<GameObject> distractionList_;

  private float minDistanceToMove_;
  private FungiController fungiControllerRef_;

  // private byte currentTimesToGetDistracted_ = 0;
  // private byte maxTimesToGetDistracted_ = 3;
  private bool isDistracted_ = false;
  private bool isSorted_ = false;
  private bool isMoving_ = false;

  private Vector3 distractionPosition_;
  [SerializeField, Range(1.0f, 10.0f)] private float distractionSpeed_; 

  void Start() {
    // distractionList_ = new List<GameObject>();
    fungiControllerRef_ = FindObjectOfType<FungiController>();
    minDistanceToMove_ = fungiControllerRef_.transform.localScale.x;
  }

  void Update() {

    if(Input.GetButtonDown("Jump")){
      isDistracted_ = true;
    }

    if(!isDistracted_){
      float distance = Vector2.Distance(transform.position, fungiControllerRef_.transform.position);
      if(distance > minDistanceToMove_){
        transform.position = Vector2.Lerp(transform.position, fungiControllerRef_.transform.position, Time.deltaTime);
      } 
    }

    if(isDistracted_ && !isSorted_)
      SortFurtherDistraction();

    if(isSorted_ && isDistracted_ && isMoving_)
      MoveToDistraction();

  }

  void SortFurtherDistraction(){
    float finalDistance = 0.0f;
    for(int i = 0; i < distractionList_.Count; ++i){
      float distrancionDistance = Vector2.Distance(transform.position, distractionList_[i].transform.position);
      if(distrancionDistance > finalDistance){
        finalDistance = distrancionDistance;
        distractionPosition_ = distractionList_[i].transform.position;
      }
    }
    isSorted_ = true;
    isMoving_ = true;
  }

  void MoveToDistraction(){
    transform.position = Vector2.MoveTowards(transform.position, distractionPosition_, distractionSpeed_ * Time.deltaTime);
    if(Vector2.Distance(transform.position, distractionPosition_) < 0.2f){
      isMoving_ = false;
    }
  }

}
