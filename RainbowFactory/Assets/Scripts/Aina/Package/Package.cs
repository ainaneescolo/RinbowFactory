using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Package : MonoBehaviour
{
    [Header("----- Package Variables -----")]
    private ColorPackage color1Package;
    private ColorPackage color2Package;
    private Country countryPackage;
    private PackageMesh packageMesh;
    private PackageUI packageUI;

    [SerializeField] private MeshFilter meshPackage;
    [SerializeField] private MeshFilter meshTape;
    [SerializeField] private MeshFilter meshSticker;
    [SerializeField] private Material grey1;
    [SerializeField] private Material grey2;
    
    [Space]
    [Header("----- Player Input Variables -----")]
    public ColorPackage color1Player;
    public ColorPackage color2Player;
    
    [Space]
    [Header("----- Timer Variables -----")] 
    [SerializeField] private float packageLifetime = 20;
    private bool packageRunning;
    
    //Get
    public ColorPackage Color1Package => color1Package;
    public ColorPackage Color2Package => color2Package;
    public Country CountryPackage => countryPackage;
    public PackageMesh PackageMesh => packageMesh;
    public PackageUI PackageUI
    {
        get {return packageUI; }
        set {packageUI = value; }
    }
    
    public float PackageLifetime => packageLifetime;

    #region Generate New Package
    
    public void SetNewPackage()
    {
        transform.localScale = new Vector3(1, 1, 1);
        
        color1Package = LevelManager.instance.colorList[Random.Range(0, LevelManager.instance.colorList.Count)];
        color2Package = LevelManager.instance.colorList[Random.Range(0, LevelManager.instance.colorList.Count)];

        if (color1Package == color2Package)
        {
            SetNewPackage();
            return;
        }

        countryPackage = LevelManager.instance.countryList[Random.Range(0, LevelManager.instance.countryList.Count)];

        packageMesh = LevelManager.instance.meshList[Random.Range(0, LevelManager.instance.meshList.Count)];
        meshPackage.mesh = packageMesh.meshPackage;
        meshTape.mesh = packageMesh.meshTape;
        meshSticker.mesh = packageMesh.meshSticker;

        transform.GetChild(0).GetComponent<MeshRenderer>().material = grey1;
        transform.GetChild(1).GetComponent<MeshRenderer>().material = grey2;
        
        UIGameManager.instance.CreatePackageUI(gameObject.GetComponent<Package>());
        StartTimer();
    }
    
    #endregion

    #region Final Stage

    public void ThrowPackage()
    {
        //Animation Package desapear
        GetComponent<PackageMoveLogic>().ResetMovement();
        LevelManager.instance.AddPointsPlayer(-2);
        ++LevelManager.instance.PackageLost;
        if (transform.parent != null) {transform.parent.DetachChildren();}
        transform.localScale = new Vector3(1, 1, 1);
        packageUI.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
    
    public bool FinalCheckerOk()
    {
        if (color1Package.colorName == color1Player.colorName && color2Package.colorName == color2Player.colorName)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    #endregion
    
    #region Timer

    private void StartTimer()
    {
        packageRunning = true;
        StartCoroutine("PackageTimer");
    }
    
    IEnumerator PackageTimer()
    {
        float time = packageLifetime;
        
        while (packageRunning)
        {
            time -= Time.deltaTime;
            packageUI.packageLifetime.value = time;

            if (time <= 0)
            {
                packageRunning = false;
                ThrowPackage();
            }
            yield return null;
        }
    }
    
    #endregion
}
