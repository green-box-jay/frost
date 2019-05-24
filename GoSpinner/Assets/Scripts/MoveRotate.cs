using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRotate : MonoBehaviour
{
    public float rotateSpeed = 1f;
    public float maxRotateSpeed = 1.0f;
    public float minSwipeDistX;
    public List<float> speedHistory;
    public Vector2 startPosition;
    public float currentAngle;
    private float currentAngleSpeed;
    public float CurrentSpeedMove = 10;


    // Start is called before the first frame update 
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame 
    void FixedUpdate()
    {
        UpdateInput();
        currentAngleSpeed = Mathf.Lerp(currentAngleSpeed, 0f, 5f * Time.deltaTime);
        currentAngle += currentAngleSpeed * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(new Vector3(0f, 90f, currentAngle));
        transform.Translate(0, 0, CurrentSpeedMove);
    }

    private void UpdateInput()
    {
#if UNITY_EDITOR
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
#endif

#if !UNITY_EDITOR
        if (Input.touchCount > 0)
        {
            Vector3 moveVector = new Vector3(Input.GetTouch(0).position.x, 0f, 0f) - new Vector3(startPosition.x, 0f, 0f);
            float moveX = Mathf.Clamp(moveVector.magnitude, 0f, maxRotateSpeed);
            float screenWidth = ((float)Screen.width);
            float moveXPercent = moveX / screenWidth;
            float speed = (-Mathf.Sign(Input.GetTouch(0).position.x - startPosition.x) * moveXPercent) * rotateSpeed * 1.4f;
            if (Input.GetMouseButtonDown(0))
            {
                speedHistory.Clear();
                currentAngleSpeed = 0f;
                startPosition = Input.GetTouch(0).position;
            }
            else if (Input.touchCount > 0)
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
                startPosition = Input.GetTouch(0).position;
            }
            else if (Input.touchCount > 0 && (moveX > minSwipeDistX))
            {
                float speedX = 0f;
                for (int i = 0; i < speedHistory.Count; i++)
                {
                    speedX += speedHistory[i];
                }
                currentAngleSpeed = 35f * speedX;
                startPosition = Input.GetTouch(0).position;
            }
        }
#endif

    }
}