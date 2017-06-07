using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public static CharacterController CharacterController;
    public static PlayerController Instance;

	void Awake ()
    {
        CharacterController = GetComponent<CharacterController>();
        Instance = this;
        CameraController.UseExistingOrCreateNewMainCamera();
	}
	

	void Update ()
    {
        //If main camera not found don't do anything.
        if (Camera.main == null)
            return;

        GetLocomotionInput();
        HandleActionInput();

        PlayerMotor.Instance.UpdateMotor();
	}

    void GetLocomotionInput()
    {
        var deadZone = 0.1f;

        PlayerMotor.Instance.VerticalVelocity = PlayerMotor.Instance.MoveVector.y;
        PlayerMotor.Instance.MoveVector = Vector3.zero;

        if (Input.GetAxis("Vertical") > deadZone || Input.GetAxis("Vertical") < -deadZone)
            PlayerMotor.Instance.MoveVector += new Vector3(0, 0, Input.GetAxis("Vertical"));


        if (Input.GetAxis("Horizontal") > deadZone || Input.GetAxis("Horizontal") < -deadZone)
            PlayerMotor.Instance.MoveVector += new Vector3(Input.GetAxis("Horizontal"), 0, 0);
    }

    void HandleActionInput()
    {
        //Default unity Jump button (Space)
        if(Input.GetButton("Jump"))
        {
            //Calls PlayerController Jump function
            Jump();
        }
    }

    void Jump()
    {
        //Instructs the PlayerMotor to execute the Jump function where actual Jump functionality is defined
        PlayerMotor.Instance.Jump();
    }
}
