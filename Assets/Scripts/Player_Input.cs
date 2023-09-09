using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Input : MonoBehaviour
{
    public PlayerInputActions inputActions;

    #region Singleton

    public static Player_Input Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    #endregion

    [Header("Dependencies")]

    [SerializeField] private FPS_Controller playerController;
    //[SerializeField] private PlayerCombat playerCombat;

    [SerializeField] private Vector2 movementInput;
    [SerializeField] private bool jumpState;

    private void OnEnable()
    {
        if (inputActions == null)
            inputActions = new PlayerInputActions();

        inputActions.Enable();

        inputActions.Locomotion.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
        inputActions.Locomotion.Movement.canceled += i => movementInput = i.ReadValue<Vector2>();

        inputActions.Locomotion.Slide.performed += s => playerController.StartSlide();
        inputActions.Locomotion.Slide.canceled += s => playerController.EndSlide();

        inputActions.Locomotion.Jump.performed += j => jumpState = true;
        inputActions.Locomotion.Jump.canceled += j => jumpState = false;

        inputActions.Locomotion.Crouch.performed += j => playerController.ToggleCrouch();
        //playerInputs.Movement.Dash.performed += d => playerLocomotion.HandleDash();
        //playerInputs.Actions.TargetLockOnSwitch.performed += t_l => targetLockOn.LockTarget();
        //playerInputs.Combat.WeaponSwitch.performed += w_s => weaponSwitching.ChangeWeapon();
    }

    private void OnDisable() 
    {
        inputActions.Disable();    
    }
    public bool JumpState()
    {
        return jumpState;
    }
    public float VerticalAXIS()
    {
        return movementInput.y;
    }
    public float HorizontalAXIS()
    {
        return movementInput.x;
    }


}
