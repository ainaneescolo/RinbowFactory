using System.Collections.Generic;
using UnityEngine;

public class PaintBucket : MonoBehaviour
{
    [Header("----- Variables Bucket -----")]
    public Transform respawn;
    private bool dirtyState;
    public bool DirtyState => dirtyState;
    [SerializeField] private GameObject cleanBucketObj;
    [SerializeField] private GameObject dirtyBucketObj;
    
    [Header("----- Variables Colors -----")]
    public List<ColorPackage> colors = new List<ColorPackage>();
    
    [Header("----- Final Color -----")]
    public ColorPackage colorFinal;
    
    public void MixColors()
    {
        if (colors.Count < 2)
        {
            colorFinal = colors[0];
        }
        else if (colors[0] == colors[1])
        {
            colorFinal = colors[0];
        }
        else if ((colors[0] == LevelManager.instance.colorList[0] && colors[1] == LevelManager.instance.colorList[1])
                 || (colors[1] == LevelManager.instance.colorList[0] && colors[0] == LevelManager.instance.colorList[1]))
        {
            colorFinal = LevelManager.instance.colorList[5];
        }
        else if ((colors[0] == LevelManager.instance.colorList[0] && colors[1] == LevelManager.instance.colorList[2])
                 || (colors[1] == LevelManager.instance.colorList[0] && colors[0] == LevelManager.instance.colorList[2]))
        {
            colorFinal = LevelManager.instance.colorList[3];
        }
        else if ((colors[0] == LevelManager.instance.colorList[1] && colors[1] == LevelManager.instance.colorList[2])
                 || (colors[1] == LevelManager.instance.colorList[1] && colors[0] == LevelManager.instance.colorList[2]))
        {
            colorFinal = LevelManager.instance.colorList[4];
        }
        
        gameObject.transform.GetChild(1).transform.gameObject.SetActive(true);
        gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().material = colorFinal.material;
    }

    public void CleanBucket()
    {
        colors.Clear();
        colorFinal = null;
        transform.GetChild(1).transform.gameObject.SetActive(false);
    }

    public void ChangeState(bool state)
    {
        dirtyState = state;

        if (dirtyState)
        {
            dirtyBucketObj.SetActive(true);
            cleanBucketObj.SetActive(false);
        }
        else
        {
            dirtyBucketObj.SetActive(false);
            cleanBucketObj.SetActive(true);
        }
    }
    
    public void ThrowBucket()
    {
        if (transform.parent != null) {transform.parent.DetachChildren();}
        GetComponent<PackageMoveLogic>().ResetMovement();
        ChangeState(true);
        colors.Clear();
        colorFinal = null;
        transform.GetChild(1).gameObject.SetActive(false);
        transform.position = respawn.position;
        LevelManager.instance.AddPointsPlayer(-1);
    }
}
