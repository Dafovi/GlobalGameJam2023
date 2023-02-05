using UnityEngine;

public class SlimeBehaviour : MonoBehaviour {

  [SerializeField, Range(2.0f, 10.0f)] private float slimeSpeed_;
  private SlimeSpawnManager spawnManagerRef_;
  private int slimeDirection_;

  void Start() {
    spawnManagerRef_ = FindObjectOfType<SlimeSpawnManager>();
    slimeDirection_ = -spawnManagerRef_.spawnDirection_;
  }

  void Update() {
    transform.position += transform.right * slimeSpeed_ * slimeDirection_ * Time.deltaTime;
    GetComponent<SpriteRenderer>().flipX = slimeDirection_ < 0 ? false :  true;
  }

  void OnTriggerEnter2D(Collider2D collider){
    if(collider.gameObject.GetComponent<IDamageable>() != null){
      collider.gameObject.GetComponent<IDamageable>().TakeDamage();
      gameObject.SetActive(false);
    }
  }
}
