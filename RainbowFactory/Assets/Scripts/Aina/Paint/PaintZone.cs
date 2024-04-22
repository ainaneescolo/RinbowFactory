using UnityEngine;
using UnityEngine.UI;

public class PaintZone : MonoBehaviour
{
    [Header("----- Paint Variables -----")]
    public PaintBucket paintBucket;
    public Package package;
    private bool playerInZone; 
    public GameObject particlesPaint;

    public bool PlayerInZone => playerInZone;
    
    [Header("----- Snap Variables -----")] 
    public Transform snapPoint1;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ObjectPickup playerObj))
        {
            if (playerObj.ObjectInHand == null || !playerObj.ObjectInHand.GetComponent<PaintBucket>()) return;
            playerInZone = true;
            paintBucket = playerObj.ObjectInHand.GetComponent<PaintBucket>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<ObjectPickup>())
        {
            playerInZone = false;
            paintBucket = null;
        }
    }

    private bool CheckToPaint(Transform snapPoint)
    {
        return snapPoint.transform.childCount > 0;
    }
    
    public void PaintPackage()
    {
        if (CheckToPaint(snapPoint1) && playerInZone )
        {
            if (package == null || paintBucket == null) return;
            if (package.color1Player.colorName == "")
            {
                package.color1Player = paintBucket.colorFinal;
                package.transform.GetChild(0).GetComponent<MeshRenderer>().material = paintBucket.colorFinal.material;
                
                CheckRightColor(package.color1Player.color, package.PackageUI.packageColor1.color,
                    package.PackageUI.packageColor1Check);
            }
            else
            {
                package.color2Player = paintBucket.colorFinal;
                package.transform.GetChild(1).GetComponent<MeshRenderer>().material = paintBucket.colorFinal.material;

                CheckRightColor(package.color2Player.color, package.PackageUI.packageColor2.color,
                    package.PackageUI.packageColor2Check);
            }

            paintBucket.ChangeState(true);
            paintBucket.CleanBucket();
            GetComponent<Animator>().SetTrigger("Paint");
        }
    }

    private void CheckRightColor(Color primaryColor, Color secondaryColor, Image checkUI)
    {
        checkUI.color = primaryColor == secondaryColor ? Color.green : Color.red;
    }
}