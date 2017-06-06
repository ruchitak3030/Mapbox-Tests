using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{

    public static PlayerMotor instance;
    public float moveSpeed = 10f;
    public Vector3 moveVector { get; set; }
    
	void Awake ()
    {
        instance = this;
	}
	
	
	public void UpdateMotor ()
    {
        SnapAlignCharacterCamera();
        ProcessMotion();
	}

    void ProcessMotion()
    {
        //Transform our moveVector into world space 
        moveVector = transform.TransformDirection(moveVector);

        //Normalize moveVector if magnitude > 1
        if (moveVector.magnitude > 1)
            moveVector = Vector3.Normalize(moveVector);

        //Multiply moveVector by moveSpeed
        moveVector *= moveSpeed;

        //multiply moveVector by deltaTime
        moveVector *= Time.deltaTime;


        //move Character in World Space
        PlayerController.characterController.Move(moveVector);
    }

    void SnapAlignCharacterCamera()
    {
        if(moveVector.x!=0 || moveVector.z!=0)
        {
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, transform.eulerAngles.z);
        }
    }
}
