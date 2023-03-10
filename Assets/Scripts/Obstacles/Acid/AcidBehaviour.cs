using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidBehaviour : MonoBehaviour {

  private Rigidbody2D rb_;
    private CircleCollider2D collider2d;

    [SerializeField] private AudioClip splashClip_;

    private void Awake()
    {
        collider2d = GetComponent<CircleCollider2D>();
        rb_ = GetComponent<Rigidbody2D>();
    }
    void Start(){
        rb_.gravityScale = 0.0f;
    }

  void OnEnable(){
      rb_.gravityScale = 0.0f;
      collider2d.enabled = true;
      rb_.velocity = Vector2.zero;
    }

  void OnTriggerEnter2D(Collider2D collider){
    if(collider.gameObject.GetComponent<IDamageable>() != null){
      collider.gameObject.GetComponent<IDamageable>().TakeDamage();
    }
        if (collider.CompareTag("Ground"))
        {
            GameManager.Instance.AddDificult();
            GetComponent<Animator>().SetTrigger("Splash");
            rb_.gravityScale = 0.0f;
            collider2d.enabled = false;           
        }
  }

  void OnDisable(){
        rb_.velocity = Vector2.zero;
        GameManager.Instance.Sfx_.PlayOneShot(splashClip_, 1);
    }

  public void Fall(){
    rb_.gravityScale = 0.5f;
  }
    public void DisableObject()
    {
        gameObject.SetActive(false);
    }

}
