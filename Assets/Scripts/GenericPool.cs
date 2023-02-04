using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GenericPool : MonoBehaviour {

  private List<GameObject> pool_;
  [SerializeField] private int totalPoolSize_;
  [SerializeField] private GameObject prefab_;

  void Start() {
    pool_ = new List<GameObject>();
    for(int i = 0; i < totalPoolSize_; ++i){
      GameObject temp = Instantiate(prefab_, Vector3.zero, Quaternion.identity);
      temp.SetActive(false);
      pool_.Add(temp);
    }
  }

  public GameObject GetFromPool(Vector3 position){
    for(int i = 0; i < pool_.Count; ++i){
      if(!pool_[i].activeInHierarchy){
        pool_[i].SetActive(true);
        pool_[i].transform.position = position;
        return pool_[i];
      }
    }
    GameObject temp = Instantiate(prefab_, position, Quaternion.identity);
    pool_.Add(temp);
    return pool_.Last();
  }

}
