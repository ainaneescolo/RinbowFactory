using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class PackageSpawn : MonoBehaviour
{
    [SerializeField] private Vector3 packageSpawn;
    private bool packageRunning;
    
    [SerializeField] private int minSpawnTime;
    [SerializeField] private int maxSpawnTime;
    
    public PoolingItemsEnum packagePrefabEnum;
    private GameObject package;

    private void Start()
    {
        StartSpawnPackage();
        InstantiatePackage();
    }

    private void StartSpawnPackage()
    {
        packageRunning = true;
        StartCoroutine("PackageSpawnCoroutine");
    }
    
    private void InstantiatePackage()
    {
        package = PoolingManager.Instance.GetPooledObject((int)packagePrefabEnum);
        package.transform.position = packageSpawn;
        
        package.gameObject.SetActive(true);
        package.GetComponent<Package>().SetNewPackage();
        ++LevelManager.instance.PackageSpawned;
    }

    IEnumerator PackageSpawnCoroutine()
    {
        float time = 0;
        int spawnTimer = Random.Range(minSpawnTime, maxSpawnTime);
        
        while (packageRunning)
        {
            time += Time.deltaTime;

            if (time >= spawnTimer)
            {
                InstantiatePackage();
                //packageRunning = false;
                time = 0;
                spawnTimer = Random.Range(minSpawnTime, maxSpawnTime);
            }
            yield return null;
        }
    }
    
    
}
