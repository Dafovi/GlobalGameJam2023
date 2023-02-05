using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class RootSpawnerManager : MonoBehaviour {
    [SerializeField] private GameObject vitPrefab_;
    [SerializeField] private int maxAnimationCount = 10;
    [SerializeField] private SpriteRenderer root1;
    [SerializeField] private SpriteRenderer root2;
    [SerializeField] private List<Sprite> _rootAnim;
    [SerializeField] private Transform root1Spwan;
    [SerializeField] private Transform root2Spwan;

    private int currentRoot;

    private int rootCountToInstace;

    private int addRoot = 0;

    private int AddTO;
    private Transform spawnPoint;

    void Start(){
        GameManager.Instance.EnemyHit += UpdateDifficult;
        GameManager.Instance.EnemyDie += UpdateAnims;

        rootCountToInstace = maxAnimationCount;
        currentRoot = addRoot;
        root1.sprite = _rootAnim[currentRoot];
        root2.sprite = _rootAnim[currentRoot];
        AddTO = Random.Range(0, 2);
    }
    private void UpdateDifficult()
    {
        AddCount(-1);
        rootCountToInstace = maxAnimationCount + GameManager.Instance.currentDifficulty_;    
    }
    void UpdateAnims()
    {
        AddCount(1); 
        if (GameManager.Instance.EnemiesCount != 0 && GameManager.Instance.EnemiesCount % rootCountToInstace == 0)
        {
            GameManager.Instance.EnemiesCount = 0;
            SetSpawnVitamin();
            addRoot = 0;
            AddTO = Random.Range(0, 2);
        }
    }
    void AddCount(int value)
    {
        addRoot+=value;
        if (AddTO == 0)
        {
            currentRoot = (int)GameManager.Remap(addRoot, 0, rootCountToInstace, 0, _rootAnim.Count - 1);
            if(currentRoot >=  0)
                root1.sprite = _rootAnim[currentRoot];
        }
        else
        {
            currentRoot = (int)GameManager.Remap(addRoot, 0, rootCountToInstace, 0, _rootAnim.Count - 1);
            if(currentRoot >=  0)
                root2.sprite = _rootAnim[currentRoot];
        }
    }
    void SetSpawnVitamin(){
        if (AddTO == 0) { spawnPoint = root1Spwan; root1.sprite = _rootAnim[0]; }
        else { spawnPoint = root2Spwan; root2.sprite = _rootAnim[0]; }
        GameObject vitamin = Instantiate(vitPrefab_, spawnPoint.position, Quaternion.identity);
        vitamin.GetComponent<SpriteRenderer>().flipX = AddTO != 0 ? false : true;
    }

    void Update(){
        if(Input.GetButtonDown("Jump")){
            SetSpawnVitamin();
        }
    }

}
