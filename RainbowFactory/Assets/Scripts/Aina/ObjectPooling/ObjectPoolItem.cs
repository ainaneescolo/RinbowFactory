using UnityEngine;

[System.Serializable]
public class ObjectPoolItem
{
    [SerializeField, Tooltip("Prefab que queremos añadir a un Pool de objetos")] 
    private GameObject prefabToPool;
    
    [SerializeField, Tooltip("Cantidad de objetos que queremos crear dentro de una Pool")]
    private int amountToPool;
    
    [SerializeField, Tooltip("Flag para indicar si en caso de estar utilizados todos los prefabs en escena" +
                             "queremos crear otro en la lista o no")]
    private bool shouldExpand = true;

    
    // GETTERS

    public GameObject PrefabToPool => prefabToPool;

    public int AmountToPool => amountToPool;

    public bool ShouldExpand => shouldExpand;
    
}
