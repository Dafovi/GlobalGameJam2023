using UnityEngine;

public class SlimeBehaviour : MonoBehaviour {

  [SerializeField, Range(2.0f, 10.0f)] private float slimeSpeed_;
  private SlimeSpawnManager spawnManagerRef_;
  private int slimeDirection_;
  private Animator anim_;

  [SerializeField] private AudioClip moveClip_;

  void Awake() {
    anim_ = GetComponent<Animator>();
    spawnManagerRef_ = FindObjectOfType<SlimeSpawnManager>();
  }

  void OnEnable(){
    slimeDirection_ = spawnManagerRef_.spawnDirection_;
  }

  void Update() {
    GetComponent<SpriteRenderer>().flipX = slimeDirection_ < 0 ? false :  true;
    if(anim_.GetCurrentAnimatorStateInfo(0).IsName("SlimeWalking")){
      transform.position += transform.right * slimeSpeed_ * slimeDirection_ * Time.deltaTime;
    }

  }

  void OnTriggerEnter2D(Collider2D collider){
    if(collider.gameObject.GetComponent<IDamageable>() != null){
      collider.gameObject.GetComponent<IDamageable>().TakeDamage();
      gameObject.SetActive(false);
    }
  }

  public void Move(){
    GameManager.Instance.Sfx_.PlayOneShot(moveClip_, 1);
  }
}
