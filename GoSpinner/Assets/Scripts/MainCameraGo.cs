using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraGo : MonoBehaviour
{
    public float rotateSpeed = 1f;
    public float maxRotateSpeed = 1.0f;
    public float minSwipeDistX;
    public List<float> speedHistory;
    public Vector2 startPosition;
    public float currentAngle;
    private float currentAngleSpeed;


    // Start is called before the first frame update 
    void Start()
    {
        Application.targetFrameRate = 60;
        speedHistory = new List<float>();
    }

    // Update is called once per frame 
    void Update()
    {
       
        currentAngleSpeed = Mathf.Lerp(currentAngleSpeed, 0f, 5f * Time.deltaTime);
        currentAngle += currentAngleSpeed * Time.deltaTime;
      
        Vector3 p = transform.position;
        p.z += (-10.1f * Time.deltaTime);
        transform.position = p;
    }

  
      
    
}