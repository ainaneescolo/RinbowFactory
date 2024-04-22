using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPaint : MonoBehaviour
{
    [Header("----- Variables Colors -----")]
    private ColorPackage _colorPaint;
    private GameObject particles;
    public PaintBucket paintBucket;
    private bool playerInZone;
    private bool sinkInZone;
    private PaintZone _paintZone;
    private GameObject sinkObj;
    [SerializeField] private AudioClip bucketPlayer;
    [SerializeField] private AudioClip paintPlayer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PaintBucket>())
        {
            //paintBucket = other.GetComponent<PaintBucket>();
        }
        else if (other.TryGetComponent(out PaintBowl paintBowl))
        {
            playerInZone = true;
            _colorPaint = paintBowl._color;
            particles = paintBowl.paintParticles;
        }
        else if (other.TryGetComponent(out PaintZone paintZone))
        {
            _paintZone = paintZone;
        }
        else if (other.CompareTag("Sink"))
        {
            sinkInZone = true;
            sinkObj = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PaintBowl>())
        {
            playerInZone = false;
        }
        else if (other.CompareTag("Sink"))
        {
            sinkInZone = false;
        }
    }

    public void PassColor()
    {
        if (!playerInZone || paintBucket.colors.Count >= 2 || paintBucket.DirtyState) return;
        paintBucket.colors.Add(_colorPaint);
        playerInZone = false;
        paintBucket.MixColors();
        GetComponent<PlayerAnimations>().TriggerAnim("Bucket");
        LevelManager.instance.PlaySound(bucketPlayer);
    }

    public void PaintPackage(InputAction.CallbackContext callbackContext)
    {
        if (!callbackContext.performed || !_paintZone || !_paintZone.PlayerInZone) return;
        //elinimar el null en separar rols de jugadors
        _paintZone.PaintPackage();
        particles = _paintZone.particlesPaint;
        GetComponent<PlayerAnimations>().TriggerAnim("Paint");
        LevelManager.instance.PlaySound(paintPlayer);
        particles.SetActive(true);
        Invoke("DeactivateVFX", 0.5f);
    }

    private void DeactivateVFX()
    {
        particles.SetActive(false);
    }
    
    public void CleanBucket(InputAction.CallbackContext callbackContext)
    {
        if (!callbackContext.performed || !sinkInZone || paintBucket == null) return;
        GetComponent<PlayerAnimations>().TriggerAnim("Clean");
        paintBucket.ChangeState(false);
        paintBucket.CleanBucket();
        StartCoroutine("VFXClean");
    }

    IEnumerator VFXClean()
    {
        foreach (Transform child in sinkObj.transform)
        {
            child.gameObject.SetActive(true);
        }
        yield return new WaitForSeconds(2f);
        foreach (Transform child in sinkObj.transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
