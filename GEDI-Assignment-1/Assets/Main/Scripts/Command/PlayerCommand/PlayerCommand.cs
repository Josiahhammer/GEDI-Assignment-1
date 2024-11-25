using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]


/////////////////////////////////////////////////////////////////////////////////////////////
// Player Command CLASS /////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////
public class PlayerCommand : MonoBehaviour
{
    [SerializeField] private float playerspeed = 5f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private float controllerDeadzone = 0.1f;
    [SerializeField] private float saveInterval = 1f; // Interval in seconds for saving undo states
    [SerializeField] private float positionThreshold = 0.5f; // Minimum distance to save a new state

    private CharacterController controller;
    private Vector2 movement;
    private Vector2 aim;
    public float shoot;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float shootDelay = 0.1f;
    private float lastShootTime;




    public float currentHealth;
    [SerializeField] private float maxHealth = 100;

    // Event to notify observers about health changes
    public event Action<float> OnHealthChanged;
    /////////////////////////////////////////////

    private float Health
    {
        get => currentHealth;
        set
        {
            currentHealth = Mathf.Clamp(value, 0, maxHealth);
            OnHealthChanged?.Invoke(currentHealth / maxHealth); // Notify observers
        }
    }

    //Destroy enemy that hit player
    //Damage player
    //Play Damage SFX




    private PlayerControls playerControls;
    private PlayerInput playerInput;

    // Command and Undo system
    private Stack<ICommand> commandStack = new Stack<ICommand>();
    private Vector3 lastSavedPosition;
    private float lastSaveTime;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerControls = new PlayerControls();
        playerInput = GetComponent<PlayerInput>();
        currentHealth = maxHealth;
    }

    private void OnEnable()
    {
        playerControls.Enable();

    }

    private void OnDisable()
    {
        playerControls.Disable();

    }
    
    // Save Position for Undo /////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        lastSavedPosition = transform.position;
        lastSaveTime = Time.time;
    }
    
    // Recieve input, handle the movement, rotation, and shooting /////////////////////////////////////////////////
    void Update()
    {
        HandleInput();
        HandleMovement();
        HandleRotation();
        HandleShoot();

        // Undo last movement if "Undo" input is pressed
        if (playerControls.Controls.Undo.triggered)
        {
            UndoLastCommand();
        }
    }
    // Handle Inputs coming in from player //////////////////////////////////////////////////////////////////////////
    void HandleInput()
    {
        movement = playerControls.Controls.Move.ReadValue<Vector2>();
        aim = playerControls.Controls.Target.ReadValue<Vector2>();
        shoot = playerControls.Controls.Shoot.ReadValue<float>();
    }

    // Movement Method /////////////////////////////////////////////////////////////////////////////////////////////
    void HandleMovement()
    {
        if (movement.magnitude > controllerDeadzone)
        {
            Vector3 move = new Vector3(movement.x, 0, movement.y);
            ICommand moveCommand = new MoveCommand(controller, move, playerspeed, gravityValue);
            moveCommand.Execute();// Excecute command sent to ICommand

            // Save the position state if the interval and threshold are met
            if (Time.time >= lastSaveTime + saveInterval &&
                Vector3.Distance(transform.position, lastSavedPosition) >= positionThreshold)
            {
                commandStack.Push(moveCommand);
                lastSavedPosition = transform.position;// Save undo position
                lastSaveTime = Time.time;
            }
        }
    }

    // Player Rotate method /////////////////////////////////////////////////////////////////////////////////////////////
    void HandleRotation()
    {
        Ray ray = Camera.main.ScreenPointToRay(aim);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            LookAt(point);
        }
    }

    // Look at mouse, determine rotation ////////////////////////////////////////////////////////////////////////////////////
    public void LookAt(Vector3 lookPoint)
    {
        Vector3 heightCorrectedPoint = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);
        transform.LookAt(heightCorrectedPoint);
    }

    // Player Shoot Method ////////////////////////////////////////////////////////////////////////////////////////////////////
    void HandleShoot()
    {
        if (shoot == 1 && Time.time >= lastShootTime + shootDelay)
        {
            lastShootTime = Time.time;
            GameManager.instance.ShootGun(firePoint, bulletPrefab);
            GameManager.instance.PlayAudio();
        }
    }

    void UndoLastCommand()
    {
        if (commandStack.Count > 0)
        {
            ICommand lastCommand = commandStack.Pop();
            lastCommand.Undo();
        }
    }


    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Enemy"))
        {
            // Damage player
            Health -= 25f;

            // Destroy the enemy that collided
            Enemy enemy = collider.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.Die();
            }
        }
    }



}