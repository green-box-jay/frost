using UnityEngine;

public class cameraGo : MonoBehaviour
{
    [SerializeField] private Transform Player;
    [SerializeField] private float DistanceX = 10;
    [SerializeField] private float DistanceY = 10;
    [SerializeField] private float DistanceZ = 10;

    [SerializeField] private float SpeedChangeLoseCamera = 1;
    [SerializeField] private float MaxFOVLose = 80;
    [SerializeField] private TypeFollow RightFollow = TypeFollow.XYZ;

    private bool IsChangeLoseCamera = false;

    private enum TypeFollow
    {
        XYZ,
        Z
    }

    private void FixedUpdate()
    {
        if (IsChangeLoseCamera)
            ChangeCameraFOV();

        MainMove();
    }

    private void MainMove() {
        Vector3 posPlayer = Player.position;
        switch (RightFollow)
        {
            case TypeFollow.XYZ:
                transform.position = new Vector3(posPlayer.x - DistanceX, posPlayer.y + DistanceY, posPlayer.z - DistanceZ);
                break;
            case TypeFollow.Z:
                transform.position = new Vector3(transform.position.x, transform.position.y, posPlayer.z - DistanceZ);
                break;
        }
    }

    #region Main game lose
    public void SetLosedCamera()
    {
        IsChangeLoseCamera = true;
    }
    private void ChangeCameraFOV()
    {
        var FOV = GetComponent<Camera>().fieldOfView;

        FOV += SpeedChangeLoseCamera * Time.deltaTime;

        Mathf.Clamp(FOV, GetComponent<Camera>().fieldOfView, MaxFOVLose);

        GetComponent<Camera>().fieldOfView = FOV;
    }
    #endregion
}
