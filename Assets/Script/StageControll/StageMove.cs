using UnityEngine;
using System.Collections;

public class StageMove : MonoBehaviour {

    enum SpinState { Stop, Right, Left };

    public float fowardSpeed = 10.0f;
    public float spinSpeed = 1.4f;

    PlayerController player;
    GridManager gridManager;
    //Mileage mileage;

    //bool isMovable = true;
    SpinState spinState = SpinState.Stop;
    int totalLine = 4;
    int curLine = 0;
    int targetLine = 0;

    Transform floorTransform;

    //Mobile 
    Vector2 touchStartPoint = new Vector2(0,0);

	void Start () {
        floorTransform = gameObject.transform.GetChild(0);

        if (!floorTransform)
            Debug.Log("Floor not Found.");

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        if (!player)
            Debug.Log("Player not Found!");

        gridManager = GameObject.FindObjectOfType<GridManager>();
        if (!gridManager)
            Debug.Log("Grid Manager not Found!");

        /*mileage = GameObject.FindObjectOfType<Mileage>();
        if (!mileage)
            Debug.Log("Mileage Data not Found!");*/
    }
	
	void Update () {
        MoveFloor();
	}

    public void MoveFloor()
    {
        if(!gridManager.isInMovingState())
        {
            return;
        }

        forward();
        spin();
        rotation();
    }
    
    void forward()
    {
        floorTransform.position = floorTransform.TransformPoint(Vector3.back * Time.deltaTime * fowardSpeed);
    }

    void spin()
    {
        float keyDownAxisHorizontal = 0;
        
        if (Application.isConsolePlatform || Application.isEditor)
            keyDownAxisHorizontal = Input.GetAxisRaw("Horizontal");
        else if (Application.isMobilePlatform)
            keyDownAxisHorizontal = getMobileHorizontalMove();

        keyDownStateCange(keyDownAxisHorizontal);
        if (checkMovingRight())
        {
            setMovingRight();
        }
        else if (checkMovingLeft())
        {
            setMovingLeft();
        }
    }

    float getMobileHorizontalMove()
    {
        float deltaX, deltaY;
        if (Input.touchCount <= 0)
            return 0;
        if (Input.touchCount == 1)
        {
            deltaX = Input.touches[0].position.x - touchStartPoint.x;
            deltaY = Input.touches[0].position.y - touchStartPoint.y;

            Debug.Log("Horizontal init: " + deltaX);
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                touchStartPoint = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Moved)
            {
                if(Mathf.Abs(deltaX) > Mathf.Abs(deltaY) && Mathf.Abs(deltaX) > 10.0f)
                {
                    Debug.Log("Horizontal: " + deltaX);
                    return deltaX;
                }
            }
            else if (Input.touches[0].phase == TouchPhase.Ended)
            {
                Debug.Log("Horizontal end: " + deltaX);
                if (Mathf.Abs(deltaX) > Mathf.Abs(deltaY) && Mathf.Abs(deltaX) > 1.0f)
                {
                    Debug.Log("Horizontal: " + deltaX);
                    return deltaX;
                }
            }
        }
        return 0;
    }

    void keyDownStateCange(float key)
    {
        if (spinState != SpinState.Stop || player.isDodging())
        {
            return;
        }

        if ( key > 0 )
        {
            spinState = SpinState.Right;
            targetLine = (curLine + 3) % 4;
        }
        else if ( key < 0)
        {
            spinState = SpinState.Left;
            targetLine = (curLine + 1) % 4;
        }
    }

    bool checkMovingRight()
    {
        return spinState == SpinState.Right; 
    }

    bool checkMovingLeft()
    {
        return spinState == SpinState.Left;
    }

    void setMovingRight()
    {
        float targetRotation = (targetLine / (float)totalLine);
        Quaternion target = Quaternion.Euler(0, 0, targetRotation * 360.0f);
        
        if (Mathf.Abs(transform.rotation.z - target.z) % Mathf.PI < 0.0001)
        {
            curLine = targetLine;
            spinState = SpinState.Stop;
            return;
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, target, spinSpeed);
    }

    void setMovingLeft()
    {
        float targetRotation = (targetLine / (float)totalLine);
        Quaternion target = Quaternion.Euler(0, 0, targetRotation * 360.0f);
        
        if (Mathf.Abs(transform.rotation.z - target.z) % Mathf.PI < 0.0001)
        {
            curLine = targetLine;
            spinState = SpinState.Stop;
            return;
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, target, spinSpeed);
    }

    void rotation()
    {
        RotateTrackDataReader rtDataReader = gameObject.GetComponent<RotateTrackDataReader>();
    }


    //------------------------------------------

    public float getFowardSpeed()
    {
        return fowardSpeed;
    }

    public void setFowardSpeed(float speed)
    {
        fowardSpeed = speed;
        gridManager.updateSpeed();
        //mileage.updateSpeed();
    }

    //------------------------------------------


    /*public void stop()
    {
        isMovable = false;
    }

    public void play()
    {
        isMovable = true;
    }*/
}
