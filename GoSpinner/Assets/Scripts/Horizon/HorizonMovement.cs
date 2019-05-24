using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class HorizonMovement : MonoBehaviour
{
    [Header("Movement parametrs")]
    [SerializeField] private float SpeedSwipe = 0.8f;
    [SerializeField] private float SpeedMove = 2f;

    [Header("Main parametrs")]
    [SerializeField] private GameObject PlayerObj;

    public float rotateSpeed = 1f;
    public float maxRotateSpeed = 1.0f;
    public float minSwipeDistX;
    public List<float> speedHistory;
    public Vector2 startPosition;
    public float currentAngle;
    private float currentAngleSpeed;
    public float CurrentSpeedMove = 10;

    // Game values
    private bool IsMoveNow = true;
    private bool CanAngele = true;

    Rigidbody rb;

    float last;

    private void Start()
    {
        rb = PlayerObj.GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        //CheckMovement();
        UpdateInput();
        CheckMoveMents();
    }

    private void CheckMovement()
    {
        if (IsMoveNow)
            PlayerObj.transform.localPosition = new Vector3(PlayerObj.transform.position.x, PlayerObj.transform.position.y, PlayerObj.transform.position.z + SpeedMove);
    }
    private void UpdateInput()
    {

        Vector3 moveVector = new Vector3(Input.mousePosition.x, 0f, 0f) - new Vector3(startPosition.x, 0f, 0f);
        float moveX = Mathf.Clamp(moveVector.magnitude, 0f, maxRotateSpeed);
        float screenWidth = ((float)Screen.width);
        float moveXPercent = moveX / screenWidth;
        float speed = (-Mathf.Sign(Input.mousePosition.x - startPosition.x) * moveXPercent) * rotateSpeed;
        if (Input.GetMouseButtonDown(0))
        {
            speedHistory.Clear();
            currentAngleSpeed = 0f;
            startPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            currentAngleSpeed = 0f;
            if (moveXPercent > minSwipeDistX)
            {
                speedHistory.Add(speed);
            }
            else
            {
                speedHistory.Add(0f);
            }
            if (speedHistory.Count > 4)
            {
                speedHistory.RemoveAt(0);
            }
            currentAngle += speed;
            startPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0) && (moveX > minSwipeDistX))
        {
            float speedX = 0f;
            for (int i = 0; i < speedHistory.Count; i++)
            {
                speedX += speedHistory[i];
            }
            currentAngleSpeed = 35f * speedX;
            startPosition = Input.mousePosition;
        }

        currentAngle = Mathf.Clamp(currentAngle, -2, 2);
    }
    private void CheckMoveMents()
    {
        Vector3 playerpos = PlayerObj.transform.position;
        Quaternion playerrot = PlayerObj.transform.rotation;

        currentAngleSpeed = Mathf.Lerp(currentAngleSpeed, 0f, 5f * Time.deltaTime);
        currentAngle += currentAngleSpeed * Time.deltaTime;

        rb.MovePosition(new Vector3(-currentAngle, playerpos.y, playerpos.z + SpeedMove));
        rb.MoveRotation(Quaternion.Euler(new Vector3(-90, playerrot.y - currentAngle * 100, playerrot.z)));
    }
}

