using UnityEngine;

public class AcidBehaviour : MonoBehaviour {

  void OnCollisionEnter2D(Collision2D collision){
    gameObject.SetActive(false);
  }

}
