using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class RobotBattery : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    private RobotMovement robotMovement;
    
    [SerializeField] private int robotBattery;
    private bool hasBattery;

    [SerializeField] private Transform station;
    
    private AudioSource _audioSource;
    [SerializeField] private AudioClip nobatteryRobot;
    [SerializeField] private AudioClip chargeRobot;
    [SerializeField] private GameObject chargeVFX;
    
    private Coroutine _currentCoroutine;

    private Animator animator;

    public bool HasBattery => hasBattery;
    
    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        robotMovement = GetComponent<RobotMovement>();
        _audioSource = GetComponent<AudioSource>();
        hasBattery = true;
        _currentCoroutine = null;
        _currentCoroutine = StartCoroutine(ConsumeBattery());
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    IEnumerator ConsumeBattery()
    {
        _navMeshAgent.speed = robotMovement.SpeedPatrol;
        _navMeshAgent.enabled = true;
        
        yield return new WaitForSeconds(robotBattery);
        animator.SetBool("Battery", true);
        hasBattery = false;
        _currentCoroutine = null;
        _currentCoroutine = StartCoroutine(ChargeBattery());
        LevelManager.instance.PlaySound(nobatteryRobot);
    }
    
    IEnumerator ChargeBattery()
    {
        while (!hasBattery)
        {
            robotMovement.SetDestination(station, robotMovement.SpeedPatrol);
            
            if (Vector3.Distance(transform.position, station.position) <= 3f)
            {
                chargeVFX.SetActive(true);
                _navMeshAgent.enabled = false;
                transform.position = station.position;
                transform.rotation = new Quaternion(0f, 0, 0f, 1);
                LevelManager.instance.PlaySound(chargeRobot);
                yield return new WaitForSeconds(5f);
                chargeVFX.SetActive(false);
                _audioSource.Play();
                hasBattery = true;
                animator.SetBool("Battery", false);
                _currentCoroutine = null;
                _currentCoroutine = StartCoroutine(ConsumeBattery());
                yield break;
            }
            yield return null;
        }
    }
}
