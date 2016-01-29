using UnityEngine;
using System.Collections;
[RequireComponent (typeof(CharacterController))]

public class PlayerController : MonoBehaviour {

    public enum MovingState { Stop, MovingLeft, MovingRight, Stopping };
    public enum DodgingState { Stay, Dodging };
    public enum Place { Mid, LeftEnd, RightEnd };
    public GameObject Player;


    public float Gravity = 20.0f;
    public float DodgeSpeed = 4.0f;

    private MovingState movingState = MovingState.Stop;
    //private float stopPosX = -1;

    private bool isControllable = true;
    private Place place = Place.Mid;
    private DodgingState dodgingState = DodgingState.Stay;

    //private Vector3 moveDirection = Vector3.zero;
    //private float horizontalDirection = 0.0f;
    private float verticalSpeed = 0.0f;
	//private float moveSpeed = 0.0f;

    private float posY;

    Vector2 touchStartPoint = new Vector2(0,0);

    void Start () {
	}

    void Update(){
        Vector3 movement, horizotalMove, verticalMove;

        if (!isControllable)
        {
            Input.ResetInputAxes();
        }

        horizotalMove = getHorizontalMove();
        verticalMove = getVerticalMove();

        movement = horizotalMove + verticalMove;
        movePlayer(movement);
    }

    Vector3 getHorizontalMove()
    {
        return Vector3.zero;
    }

    void keyDownStateCange(float key)
    {
        if (movingState != MovingState.Stop)
            return;

        if (dodgingState == DodgingState.Dodging)
            return;

        if (key > 0 && place != Place.RightEnd)
        {
            movingState = MovingState.MovingRight;
        }
        else if (key < 0 && place != Place.LeftEnd)
        {
            movingState = MovingState.MovingLeft;
        }
    }

    bool checkMovingRight()
    {
        return movingState == MovingState.MovingRight;
    }

    bool checkMovingLeft()
    {
        return movingState == MovingState.MovingLeft;
    }

    bool checkStopping()
    {
        return movingState == MovingState.Stopping;
    }
    
    void setNoHorizontalMove()
    {
        //moveDirection = Vector3.zero;
        //moveSpeed = 0.0f;
    }

    Vector3 getVerticalMove()
    {
        if (isControllable)
        {
            backForce();
            dodge();
            return new Vector3(0, verticalSpeed, 0);
        }
        return Vector3.zero;
    }

    void dodge()
    {
        float keyDownAxisVertical = Input.GetAxisRaw("Vertical");

        if (Application.isConsolePlatform || Application.isEditor)
            keyDownAxisVertical = Input.GetAxisRaw("Vertical");
        else if (Application.isMobilePlatform)
            keyDownAxisVertical = getMobileVerticalMove();

        if (keyDownAxisVertical < 0 && dodgingState == DodgingState.Stay)
        {
            verticalSpeed = -DodgeSpeed;
            dodgingState = DodgingState.Dodging;
        }
    }

    float getMobileVerticalMove()
    {
        float deltaX, deltaY;
        if (Input.touchCount <= 0)
            return 0;

        if (Input.touchCount == 1)
        {
            deltaX = Input.touches[0].position.x - touchStartPoint.x;
            deltaY = Input.touches[0].position.y - touchStartPoint.y;

            if (Input.touches[0].phase == TouchPhase.Began)
            {
                touchStartPoint = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Moved)
            {
                if (Mathf.Abs(deltaY) > Mathf.Abs(deltaX) && Mathf.Abs(deltaY) > 10.0f)
                {
                    Debug.Log("Vertical: " + deltaY);
                    return deltaY;
                }
            }
            else if (Input.touches[0].phase == TouchPhase.Ended)
            {
                Debug.Log("Vertical end: " + deltaY);
                if (Mathf.Abs(deltaY) > Mathf.Abs(deltaX) && Mathf.Abs(deltaY) > 1.0f)
                {
                    Debug.Log("Vertical: " + deltaY);
                    return deltaY;
                }
            }
        }
        return 0;
    }

    void backForce()
    {
        if (isBack())
        {
            verticalSpeed = 0;
            dodgingState = DodgingState.Stay;
        }
        else
        {
            verticalSpeed += Gravity * Time.deltaTime;
        }
    }

    void movePlayer(Vector3 movement)
    {
        movement *= Time.deltaTime;

        CharacterController controller = GetComponent<CharacterController>();
        controller.Move(movement);
    }


    //------------------------------------------

    // call by EatByPlayer.cs
    void eatItem(Collection.CollectionType type)
    {
        
    }

    //------------------------------------------


    bool isBack()
    {
        return transform.position.y > 0.25f;
    }
    
    public bool isDodging()
    {
        return dodgingState == DodgingState.Dodging;
    }

    public void setControllable(bool Controllable)
	{
		//Debug.Log("Set: "+Controllable);
		isControllable = Controllable;
	}

}

