using UnityEngine;

public class AcidBehaviour : MonoBehaviour {

  void OnTriggerEnter2D(Collider2D collider){
    if(collider.gameObject.GetComponent<IDamageable>() != null){
      collider.gameObject.GetComponent<IDamageable>().TakeDamage();
    }
    gameObject.SetActive(false);
  }

}
