using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : MonoBehaviour
{
    // Patron SINGLETON para poder ser llamado desde cualquier script
    private static PoolingManager _instance;
    
    [SerializeField, Tooltip("Lista de los items para añadir a la lista de Pools")] 
    private List<ObjectPoolItem> itemPoolList;
    
    
    // Private variables //
 	private List<List<GameObject>> pooledObjectsList;
 	private List<GameObject> pooledObjects_TMP;
 	private List<int> positions;

    
    // Getter para poder acceder al script desde otros
    public static PoolingManager Instance => _instance;

    
 	void Awake()
 	{
	    if (_instance == null)
	    {
		    _instance = this;
		    
		    // Inicializamos todas las listas
		    pooledObjectsList = new List<List<GameObject>>();
		    pooledObjects_TMP = new List<GameObject>();
		    positions = new List<int>();
		    
		    
		    for (int i = 0; i < itemPoolList.Count; i++)
		    {
			    ObjectPoolItemToPooledObject(i);
		    }
		    
	    }
	    else
	    {
		    Destroy(gameObject);
	    }
	    
 	}

    
    /// <summary>
    /// Método que devuelve un elemento de la lista de Pool creada, en caso de no existir elementos disponibles
    /// se creará uno nuevo
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public GameObject GetPooledObject(int index)
 	{
 
 		int curSize = pooledObjectsList[index].Count;
 		for (int i = positions[index] + 1; i < positions[index] + pooledObjectsList[index].Count; i++)
 		{
 
 			if (!pooledObjectsList[index][i % curSize].activeInHierarchy)
 			{
 				positions[index] = i % curSize;
 				return pooledObjectsList[index][i % curSize];
 			}
 		}
 
        // Si hemos llegado aquí es porque todos los prefabs están activados en pantalla
        // Por lo que miraremos si el ObjectPoolItem tiene marcado la opción de shouldExpand
 		if (itemPoolList[index].ShouldExpand)
 		{
	        GameObject obj = (GameObject)Instantiate(itemPoolList[index].PrefabToPool);
 			obj.SetActive(false);
            obj.transform.SetParent(transform, true);
 			pooledObjectsList[index].Add(obj);
 			return obj;
 
 		}
 		return null;
 	}
    
 	
    /// <summary>
    /// Método que pòr cada crear una lista objetos en POOL a partir de una lista de objetos a añadir
    /// donde indican el numero de elementos a crear, el tipo de prefab y si se pueden crear mas en caso de faltar
    /// elementos en escena
    /// </summary>
    /// <param name="index">Indice del objeto de la lista de prefabs a añadir a la pool</param>
 	private void ObjectPoolItemToPooledObject(int index)
 	{
	    ObjectPoolItem item = itemPoolList[index];
 
	    pooledObjects_TMP = new List<GameObject>();
	    
	    // Creamos tantos GameObjects como cantidad tenga indicada en el 
	    // atributo amountToPool
 		for (int i = 0; i < item.AmountToPool; i++)
 		{
 			GameObject obj = (GameObject)Instantiate(item.PrefabToPool);
 			obj.SetActive(false);
 			obj.transform.SetParent(transform, true);
 			pooledObjects_TMP.Add(obj);
 		}
        // Añadimos la lista TMP a la lista final que contiene todos los Objetos para 
        // utilizar en la escena
 		pooledObjectsList.Add(pooledObjects_TMP);
        // También agragamos un elemento en la lista de posiciones que indicará que último elemento hemos activado
 		positions.Add(0);
 
 	}
}
