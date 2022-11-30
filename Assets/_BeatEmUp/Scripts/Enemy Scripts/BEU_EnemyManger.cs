using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BEU_EnemyManger : MonoBehaviour
{
    public static BEU_EnemyManger instance;

    [SerializeField] private GameObject enemyPrefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemy();    
    }

    public void SpawnEnemy()
    {
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    
}
