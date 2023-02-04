using UnityEngine;

public class BugBehaviour : MonoBehaviour {

  private FungiSisterController sisterFungiRef_;
  [SerializeField, Range(1.0f, 10.0f)] private float bugSpeed_;

  void Start() {
    sisterFungiRef_ = FindObjectOfType<FungiSisterController>();
      
  }

  void Update() {
    // float offsetY = Random.Range(-0.2f, 0.2f);
    // Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y - offsetY, transform.position.z);
    transform.position = Vector3.MoveTowards(transform.position, sisterFungiRef_.transform.position, bugSpeed_ * Time.deltaTime);
  }

  void OnTriggerEnter2D(Collider2D collider){
    if(collider.gameObject.GetComponent<IDamageable>() != null){
      collider.gameObject.GetComponent<IDamageable>().TakeDamage();
      gameObject.SetActive(false);
    }
  }

}
