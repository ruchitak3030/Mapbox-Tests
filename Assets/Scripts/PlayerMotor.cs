using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{

    public static PlayerMotor Instance;

    public float MoveSpeed = 10f;
    public float JumpSpeed = 6f;
    public float Gravity = 21f;
    public float TerminalVelocity = 20f;
    public float SlideThreshold = 0.6f;
    public float MaxControllableSlideMagnitude = 0.4f;

    private Vector3 slideDirection;

    public Vector3 MoveVector { get; set; }
    public float VerticalVelocity { get; set; }
    
	void Awake ()
    {
        Instance = this;
	}
	
	
	public void UpdateMotor ()
    {
        SnapAlignCharacterCamera();
        ProcessMotion();
	}

    void ProcessMotion()
    {
        //Transform our moveVector into world space 
        MoveVector = transform.TransformDirection(MoveVector);

        //Normalize moveVector if magnitude > 1
        if (MoveVector.magnitude > 1)
            MoveVector = Vector3.Normalize(MoveVector);

        //Apply Slide if applicable
        ApplySlide();

        //Multiply moveVector by moveSpeed
        MoveVector *= MoveSpeed;

        //reapply vertical velocity to movevector.y
        MoveVector = new Vector3(MoveVector.x, VerticalVelocity, MoveVector.z);

        //Apply Gravity
        ApplyGravity();

        //move Character in World Space
        PlayerController.CharacterController.Move(MoveVector*Time.deltaTime);
    }


    void ApplyGravity()
    {
        if (MoveVector.y > -TerminalVelocity)
            MoveVector = new Vector3(MoveVector.x, MoveVector.y - Gravity * Time.deltaTime, MoveVector.z);

        if (PlayerController.CharacterController.isGrounded && MoveVector.y < -1)
            MoveVector = new Vector3(MoveVector.x, -1, MoveVector.z);
    }


    void ApplySlide()
    {
        //If Player is jumping or in air the return no slide
        if (!PlayerController.CharacterController.isGrounded)
            return;

        slideDirection = Vector3.zero;
        RaycastHit hitInfo;

        if(Physics.Raycast(transform.position+Vector3.up,Vector3.down,out hitInfo))
        {
            if (hitInfo.normal.y < SlideThreshold)
                slideDirection = new Vector3(hitInfo.normal.x, -hitInfo.normal.y, hitInfo.normal.z);
        }

        if (slideDirection.magnitude < MaxControllableSlideMagnitude)
            MoveVector += slideDirection;
        else
            MoveVector = slideDirection;
    }

    public void Jump()
    {
        if (PlayerController.CharacterController.isGrounded)
            VerticalVelocity = JumpSpeed;
    }

    void SnapAlignCharacterCamera()
    {
        if(MoveVector.x!=0 || MoveVector.z!=0)
        {
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, transform.eulerAngles.z);
        }
    }
}
