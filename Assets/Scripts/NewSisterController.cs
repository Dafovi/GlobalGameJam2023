using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSisterController : MonoBehaviour, IDamageable {

  [SerializeField] private AcidSpawnManager acidManager_;
  public List<GameObject> activeAcidList_;
  private AcidBehaviour acidRef_;

  private Vector3 acidPosition_;
  private bool isLocked_ = false;

  [SerializeField, Range(1.0f, 10.0f)] private float sisteSpeed_;

  void Start(){
    activeAcidList_ = new List<GameObject>();
    acidManager_.SearchAcidAction += SearchForTarget;
  }

  void Update(){
    if(isLocked_){
      transform.position = Vector3.MoveTowards(transform.position, acidPosition_, sisteSpeed_ * Time.deltaTime);
      if(Vector3.Distance(transform.position, acidPosition_) < 0.1f){
        isLocked_ = false;
        acidRef_.hasFallen_ = false;
        activeAcidList_.Clear();
      }
    }
  }

  void SearchForTarget(){
    if(isLocked_)
      return;

    for(int i = 0; i < acidManager_.acidSpawnerPoolRef_.pool_.Count; ++i){
      if(acidManager_.acidSpawnerPoolRef_.pool_[i].activeInHierarchy){
        GameObject temp = acidManager_.acidSpawnerPoolRef_.pool_[i];
        activeAcidList_.Add(temp);
      }
    }
    int random_index = Random.Range(0, activeAcidList_.Count);
    acidPosition_ = new Vector3(activeAcidList_[random_index].transform.position.x, transform.position.y, transform.position.z);
    acidRef_ = activeAcidList_[random_index].GetComponent<AcidBehaviour>();
    isLocked_ = true;
  }

  public void TakeDamage(){
    GameManager.Instance.FungiSisterDamage();
  }

}
