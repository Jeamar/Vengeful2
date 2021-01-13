using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : MonoBehaviour
{
    private EnemyMain enMain;
    public float attackRange;

    // Start is called before the first frame update
    void Start()
    {
        enMain = GetComponent<EnemyMain>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
