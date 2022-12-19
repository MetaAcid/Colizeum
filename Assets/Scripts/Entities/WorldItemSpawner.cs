using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Entities
{
    public class WorldItemSpawner : MonoBehaviour
    {
        [SerializeField] private WorldItem[] worldItemToSpawn;
        private void Awake()
        {
            
            int randIndex = Random.Range(0, worldItemToSpawn.Length);
            if (!worldItemToSpawn[randIndex]) return;
            Instantiate(worldItemToSpawn[randIndex], transform.position, worldItemToSpawn[randIndex].transform.rotation);
        }
    }
}