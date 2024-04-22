using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator _animator;
    private bool animInCourse;

    public bool AnimInCourse => animInCourse;
    
    void Start()
    {
        _animator = transform.GetChild(2).GetComponent<Animator>();
    }

    public void TriggerAnim(string triggerName)
    {
        animInCourse = true;
        _animator.SetTrigger(triggerName);
        Invoke("EndAnim", 0.75f);
    }
    
    public void BoolAnim(string boolName, bool state)
    {
        _animator.SetBool(boolName, state);
    }

    public bool GetBool(string boolName)
    {
        return _animator.GetBool(boolName);
    }

    public void EndAnim()
    {
        animInCourse = false;
    }
}
