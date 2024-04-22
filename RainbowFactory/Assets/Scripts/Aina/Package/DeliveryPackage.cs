using System.Collections;
using UnityEngine;

public class DeliveryPackage : MonoBehaviour
{
    [SerializeField] private string continentDelivery;
    [SerializeField] private Light greenLight;
    [SerializeField] private Light redLight;
    [SerializeField] private GameObject confetiObj;

    [SerializeField] private AudioClip entregaOk;
    [SerializeField] private AudioClip entregaNo;

    public void CheckPackageInfo(Package package)
    {     
        if (package.FinalCheckerOk() && package.CountryPackage.continentName == continentDelivery)
        {
            LevelManager.instance.AddPointsPlayer(8);
            ++LevelManager.instance.PackagesDelivered;
            greenLight.gameObject.SetActive(true);
            confetiObj.SetActive(true);
            StartCoroutine(TurnOffLight(greenLight));
            LevelManager.instance.PlaySound(entregaOk);
        }
        else
        {
            ++LevelManager.instance.PackageLost;
            redLight.gameObject.SetActive(true);
            StartCoroutine(TurnOffLight(redLight));
            LevelManager.instance.PlaySound(entregaNo);
        }
        
        package.ThrowPackage();
    }

    IEnumerator TurnOffLight(Light light)
    {
        yield return new WaitForSeconds(2f);
        light.gameObject.SetActive(false);
        confetiObj.SetActive(false);
    }
}
