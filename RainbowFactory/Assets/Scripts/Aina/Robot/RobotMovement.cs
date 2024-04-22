using UnityEngine;
using UnityEngine.AI;

public class RobotMovement : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    //private RobotBattery _robotBattery;
    
    public bool playerInZone;
    public bool packagePlayer;

    [SerializeField] private Transform handRobot;
    [SerializeField] private float speedPatrol;
    [SerializeField] private float speedAttack;
    [SerializeField] private PoolingItemsEnum walkParticles;
    [SerializeField] private Transform walkParticlesSpawn;
    private float timerParticles;

    public Transform targetPatrol;
    private Transform targetAttack;
    [SerializeField] private GarbageCan garbageCan;
    
    private ObjectPickup _playerObjPickup;
    private PlayerController _playerController;
    public GameObject objPlayer;

    [SerializeField] private AudioClip attackRobot;
    [SerializeField] private AudioClip stealRobot;

    private Animator animator;
    public float SpeedPatrol => speedPatrol;
    
    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        //_robotBattery = GetComponent<RobotBattery>();
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    private void Update()
    {
        //if (!_robotBattery.HasBattery) return;
        
        if (playerInZone)
        {
            SetDestination(targetAttack, speedAttack);

            if (packagePlayer)
            {
                MovementGarbage();
            }
            else
            {
                MovementAttack();
            }
        }
        else
        {
            MovementPatrol(targetPatrol);
        }

        timerParticles += Time.deltaTime;
        if (timerParticles > 0.25f)
        {
            CreateRobotParticle();
            timerParticles = 0;
        }
    }

    public void MovementPatrol(Transform waypointPosition)
    {
        SetDestination(waypointPosition, speedPatrol);
        animator.SetBool("Attack", false);
    }

    private void MovementAttack()
    {
        if (_navMeshAgent.remainingDistance > _navMeshAgent.stoppingDistance && playerInZone) return;
        _playerController.MakePlayerDizzy();
        objPlayer.transform.SetParent(handRobot);
        objPlayer.transform.position = handRobot.transform.position;
        animator.SetTrigger("Package");
        objPlayer.transform.position = handRobot.position;
        targetAttack = garbageCan.transform;
        _playerObjPickup.alreadyWithObj = false;
        _playerObjPickup.GetComponent<PlayerAnimations>().BoolAnim("Grab", false);
        LevelManager.instance.PlaySound(stealRobot);
        packagePlayer = true;
    }
    
    private void MovementGarbage()
    {
        if (Vector3.Distance(transform.position, targetAttack.position) >= 3.1f) return;
        animator.SetTrigger("Package");
        
        if (objPlayer.GetComponent<Package>())
        {
            objPlayer.GetComponent<Package>().ThrowPackage();
        }
        else
        {
            objPlayer.GetComponent<PaintBucket>().ThrowBucket();
        }
        
        playerInZone = false;
        packagePlayer = false;
    }

    public void SetDestination(Transform target, float speed)
    {
        _navMeshAgent.speed = speed;
        _navMeshAgent.SetDestination(target.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (!_robotBattery.HasBattery) return;
        if (!other.TryGetComponent(out ObjectPickup player)) return;
        _playerObjPickup = player;
        _playerController = player.gameObject.GetComponent<PlayerController>();
        if(!_playerObjPickup.alreadyWithObj || playerInZone) return;
        if(!_playerObjPickup.ObjRobotAttack()) return;
        animator.SetBool("Attack", true);
        LevelManager.instance.PlaySound(attackRobot);
        targetAttack = _playerObjPickup.transform;
        objPlayer = _playerObjPickup.ObjectInHand.gameObject;
        playerInZone = true;
    }
    
    private void CreateRobotParticle()
    {
        var particles = PoolingManager.Instance.GetPooledObject((int)walkParticles);
        particles.transform.position = walkParticlesSpawn.position;
        particles.SetActive(true);
    }
}
