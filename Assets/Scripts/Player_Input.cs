using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void OnEnable()
    {
        if (inputActions == null)
            inputActions = new PlayerInputActions();

        inputActions.Enable();

        inputActions.Locomotion.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
        inputActions.Locomotion.Movement.canceled += i => movementInput = i.ReadValue<Vector2>();

        //playerInputs.Movement.Dash.performed += d => playerLocomotion.HandleDash();
        //playerInputs.Actions.TargetLockOnSwitch.performed += t_l => targetLockOn.LockTarget();
        //playerInputs.Combat.WeaponSwitch.performed += w_s => weaponSwitching.ChangeWeapon();
    }

    private void OnDisable() 
    {
        inputActions.Disable();    
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
