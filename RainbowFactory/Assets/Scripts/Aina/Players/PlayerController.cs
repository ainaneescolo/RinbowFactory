using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("----- Movement Variables -----")]
    [SerializeField] private float playerSpeed = 5;
    private float lateralMovement;
    private float verticalMovement;
    
    private Vector2 movementInput = Vector2.zero;

    private CharacterController _characterController;
    private PlayerAnimations _playerAnimations;
    [SerializeField] private PoolingItemsEnum walkParticles;
    [SerializeField] private Transform walkParticlesSpawn;
    private float timerParticles;
    [SerializeField] private GameObject dizzyParticles;
    private AudioSource _audioSource;

    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float gravityValue = -9.81f;

    private bool playerDizzy;
    
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _playerAnimations = GetComponent<PlayerAnimations>();
        _audioSource = GetComponent<AudioSource>();
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        if(playerDizzy || _playerAnimations.AnimInCourse) return;
        movementInput = context.ReadValue<Vector2>();
        GetComponent<PlayerAnimations>().BoolAnim("Walk", true);
        _audioSource.enabled = true;
    }

    void Update()
    {
        timerParticles += Time.deltaTime;
        
        if(playerDizzy) return;
        groundedPlayer = _characterController.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // moviment vertical i horitzontal
        lateralMovement = movementInput.x;
        verticalMovement = movementInput.y;
        
        Vector3 moveDirection = new Vector3(verticalMovement, 0, lateralMovement *-1);
        _characterController.Move(moveDirection * (Time.deltaTime * playerSpeed));

        if (moveDirection != Vector3.zero)
        {
            gameObject.transform.forward = moveDirection;

            if (timerParticles > 0.25f)
            {
                CreateWalkParticle();
                timerParticles = 0;
            }

            if (_playerAnimations.GetBool("Walk")) return;
            _playerAnimations.BoolAnim("Walk", true);
            _audioSource.enabled = true;
        }
        else
        {
            _playerAnimations.BoolAnim("Walk", false);
            _audioSource.enabled = false;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        _characterController.Move(playerVelocity * Time.deltaTime);
    }

    public void MakePlayerDizzy()
    {
        dizzyParticles.SetActive(true);
        playerDizzy = true;
        _audioSource.enabled = false;
        _playerAnimations.TriggerAnim("Dizzy");
        StartCoroutine("MakePlayerDizzyCoroutine");
    }

    IEnumerator MakePlayerDizzyCoroutine()
    {
        // start animation
        yield return new WaitForSeconds(3f);
        playerDizzy = false;
        dizzyParticles.SetActive(false);
    }

    private void CreateWalkParticle()
    {
        var particles = PoolingManager.Instance.GetPooledObject((int)walkParticles);
        particles.transform.position = walkParticlesSpawn.position;
        particles.SetActive(true);
    }
}
