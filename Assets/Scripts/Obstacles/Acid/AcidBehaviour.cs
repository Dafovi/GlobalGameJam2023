using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidBehaviour : MonoBehaviour {

  private Rigidbody2D rb_;

  void Start(){
    rb_ = GetComponent<Rigidbody2D>();
    rb_.gravityScale = 0.0f;
  }

  void OnEnable(){
    StartCoroutine(Wait());
  }

  void OnTriggerEnter2D(Collider2D collider){
    if(collider.gameObject.GetComponent<IDamageable>() != null){
      collider.gameObject.GetComponent<IDamageable>().TakeDamage();
    }
        if (collider.CompareTag("Ground"))
        {
            GameManager.Instance.AddDificult();
        }
    gameObject.SetActive(false);
  }

  IEnumerator Wait(){
    yield return new WaitForSeconds(2.0f);
    rb_.gravityScale = 0.5f;
  }

}
