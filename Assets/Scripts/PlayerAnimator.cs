using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public enum Direction
    {
        Stationary,
        Forward,
        Backward,
        Left,
        Right,
        LeftForward,
        RightForward,
        LeftBackward,
        RightBackward
    }
    public static PlayerAnimator Instance;

    public Direction MoveDirection { get; set; }
	void Awake ()
    {
        Instance = this;
	}
	
	
	void Update ()
    {
		
	}

    public void DetermineCurrentMoveDirection()
    {
        //Set Up checkBox
        var forward = false;
        var backward = false;
        var left = false;
        var right = false;

        if (PlayerMotor.Instance.MoveVector.z > 0)
            forward = true;

        if (PlayerMotor.Instance.MoveVector.z < 0)
            backward = true;

        if (PlayerMotor.Instance.MoveVector.x > 0)
            right = true;

        if (PlayerMotor.Instance.MoveVector.x < 0)
            left = true;

        if (forward)
        {
            if (left)
                MoveDirection = Direction.LeftForward;
            else if (right)
                MoveDirection = Direction.RightForward;
            else
                MoveDirection = Direction.Forward;
        }
        else if (backward)
        {
            if (left)
                MoveDirection = Direction.LeftBackward;
            else if (right)
                MoveDirection = Direction.RightBackward;
            else
                MoveDirection = Direction.Backward;
        }
        else if (left)
        {
            MoveDirection = Direction.Left;
        }
        else if (right)
        {
            MoveDirection = Direction.Right;
        }
        else
            MoveDirection = Direction.Stationary;
    }

    
}
