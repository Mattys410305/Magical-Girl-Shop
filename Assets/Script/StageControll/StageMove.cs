using UnityEngine;
using System.Collections;

public class StageMove : MonoBehaviour {

    enum SpinState { Stop, Right, Left };

    public float fowardSpeed = 10.0f;
    public float spinSpeed = 1.4f;

    PlayerController player;
    Mileage mileage;

    bool isMovable = true;
    SpinState spinState = SpinState.Stop;
    int totalLine = 4;
    int curLine = 0;
    int targetLine = 0;

    Transform floorTransform;

	void Start () {
        floorTransform = gameObject.transform.GetChild(0);

        if (!floorTransform)
            Debug.Log("Floor not Found.");

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        if (!player)
            Debug.Log("Player not Found!");

        mileage = GameObject.FindObjectOfType<Mileage>();
        if (!mileage)
            Debug.Log("Mileage Data not Found!");
    }
	
	void Update () {
        moveFloor();
	}

    void moveFloor()
    {
        if(!isMovable)
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
        float keyDownAxisHorizontal = Input.GetAxisRaw("Horizontal");

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

    }


    //------------------------------------------

    public float getFowardSpeed()
    {
        return fowardSpeed;
    }

    public void setFowardSpeed(float speed)
    {
        fowardSpeed = speed;
        mileage.updateSpeed();
    }

    //------------------------------------------


    public void stop()
    {
        isMovable = false;
    }

    public void start()
    {
        isMovable = true;
    }
}
