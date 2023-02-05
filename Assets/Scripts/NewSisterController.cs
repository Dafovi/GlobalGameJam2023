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
    private Animator anim;

    [SerializeField] private List<AudioClip> sisterAudio_;

  void Start(){
    activeAcidList_ = new List<GameObject>();
    acidManager_.SearchAcidAction += SearchForTarget;
        anim = GetComponent<Animator>();
  }

  void Update(){
    if(isLocked_){
      transform.position = Vector3.MoveTowards(transform.position, acidPosition_, sisteSpeed_ * Time.deltaTime);
            anim.SetBool("Walk", true);
            GetComponent<SpriteRenderer>().flipX = transform.position.x < acidPosition_.x ? false : true;
      if(Vector3.Distance(transform.position, acidPosition_) < 0.1f){
        isLocked_ = false;
                anim.SetBool("Walk", false);
            }
    }

    if(acidRef_ != null && !acidRef_.gameObject.activeInHierarchy){
      isLocked_ = false;
       anim.SetBool("Walk", false);
      SearchForTarget();
    }

  }

  void SearchForTarget(){
    if(isLocked_)
      return;

    activeAcidList_.Clear();
    for(int i = 0; i < acidManager_.acidSpawnerPoolRef_.pool_.Count; ++i){
      if(acidManager_.acidSpawnerPoolRef_.pool_[i].activeInHierarchy){
        GameObject temp = acidManager_.acidSpawnerPoolRef_.pool_[i];
        activeAcidList_.Add(temp);
      }
    }
    if(activeAcidList_.Count != 0){
      int random_index = Random.Range(0, activeAcidList_.Count);
      acidPosition_ = new Vector3(activeAcidList_[random_index].transform.position.x, transform.position.y, transform.position.z);
      acidRef_ = activeAcidList_[random_index].GetComponent<AcidBehaviour>();
      isLocked_ = true;
    }
  }

    public void TakeDamage(){
        GameManager.Instance.FungiSisterDamage();
        StartCoroutine(DamageAnim());
    }
    IEnumerator DamageAnim()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        float t = 0;
        float speed = 10f;
        bool growing = false;
        int rounds = 0;
        bool ready = false;
        while (true)
        {
            t = Mathf.Clamp01(t + Time.deltaTime * speed * (growing ? -1 : 1));
            if (t == 1 || t == 0) { growing = !growing; rounds++; }

            sprite.color = new Color(255, 255, 255, t);

            if (rounds >= 4) ready = true;
            if (ready)
            {
                sprite.color = new Color(255, 255, 255, 1);
                yield return false;
            }

            yield return null;
        }
    }

    public void Move(){
      int rand = Random.Range(0, sisterAudio_.Count);
      GameManager.Instance.Sfx_.PlayOneShot(sisterAudio_[rand], 1);
    }

}
