using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using Game.NPC;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private UnityEvent onEnemyDie;
    
    
    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Update()
    {
        if (GameObject.FindGameObjectsWithTag(TagConfig.Enemy).Length == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void EnemyDie()
    {
        onEnemyDie.Invoke();
    }
}
