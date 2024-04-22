using UnityEngine;

public class PaintBowl : MonoBehaviour
{
    [Header("----- Variables Colors -----")]
    [SerializeField] private int colorIndex;
    public ColorPackage _color;
    public GameObject paintParticles;

    private void Start()
    {
        _color = LevelManager.instance.colorList[colorIndex];
    }
}
