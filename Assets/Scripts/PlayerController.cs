using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public static CharacterController characterController;
    public static PlayerController instance;

	void Awake ()
    {
        characterController = GetComponent<CharacterController>();
        instance = this;
        CameraController.UseExistingOrCreateNewMainCamera();
	}
	

	void Update ()
    {
        //If main camera not found don't do anything.
        if (Camera.main == null)
            return;

        GetLocomotionInput();

        PlayerMotor.instance.UpdateMotor();
	}

    void GetLocomotionInput()
    {
        var deadZone = 0.1f;
        PlayerMotor.instance.moveVector = Vector3.zero;

        if (Input.GetAxis("Vertical") > deadZone || Input.GetAxis("Vertical") < -deadZone)
            PlayerMotor.instance.moveVector += new Vector3(0, 0, Input.GetAxis("Vertical"));


        if (Input.GetAxis("Horizontal") > deadZone || Input.GetAxis("Horizontal") < -deadZone)
            PlayerMotor.instance.moveVector += new Vector3(Input.GetAxis("Horizontal"), 0, 0);
    }
}
