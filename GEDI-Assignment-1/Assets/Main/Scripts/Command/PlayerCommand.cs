using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]


///
/// This is my script for the command pattern. Here it takes the info from the action inputs and delivers them packages to be used for player.
///
///
///




public class PlayerCommand : MonoBehaviour
{
    [SerializeField] private float playerspeed = 5f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private float controllerDeadzone = 0.1f;
    [SerializeField] private float gamepadRotateSmoothing = 1000f;
    [SerializeField] private bool isGamepad;

    private CharacterController controller;
    private Vector2 playerVelocity;
    private Vector2 movement;
    private Vector2 aim;
    public float shoot;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float shootDelay = 0.1f;
    private float lastShootTime;




    private PlayerControls playerControls;
    private PlayerInput playerInput;


    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerControls = new PlayerControls();
        playerInput = GetComponent<PlayerInput>();

    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    void Update()
    {
        HandleInput();
        HandleMovement();
        HandleRotation();
        HandleShoot();
    }

    void HandleInput()
    {
        movement = playerControls.Controls.Move.ReadValue<Vector2>();
        aim = playerControls.Controls.Target.ReadValue<Vector2>();
        shoot = playerControls.Controls.Shoot.ReadValue<float>();
    }

    void HandleMovement()
    {
        Vector3 move = new Vector3(movement.x, 0, movement.y);
        controller.Move(move * Time.deltaTime * playerspeed);

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

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

    public void LookAt(Vector3 lookPoint)
    {
        Vector3 heightCorrectedPoint = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);
        transform.LookAt(heightCorrectedPoint);
    }


    //Handles if the player wants to left mouse 
    void HandleShoot()
    {
        //All of our shooting logic
        if (shoot == 1 && Time.time >= lastShootTime + shootDelay)
        {
            lastShootTime = Time.time;
            GameManager.instance.ShootGun(firePoint, bulletPrefab);
            //Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            GameManager.instance.PlayAudio();
        }

    }
}
