using UnityEngine;
using UnityEngine.EventSystems;

public class DragSystem : MonoBehaviour, IBeginDragHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
{

    [Header("Movement parametrs")]
    public float speedRotate = 75f;

    [SerializeField] GameObject PlayerObj;

    // Other hide parametrs
    Transform PlayerTransform;
    float SpeedToRotate;

    bool IsRotateNow = false;

    void PlayerRotate()
    {
        PlayerTransform.Rotate(new Vector3(0, 0, speedRotate));
    }


    private void Start()
    {
        PlayerTransform = PlayerObj.transform;

        SpeedToRotate = speedRotate;
    }
    private void FixedUpdate()
    {
        if (IsRotateNow)
            PlayerRotate();
    }

    // Some voids to point drags and swipes
    #region
    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        if ((Mathf.Abs(eventData.delta.x)) > (Mathf.Abs(eventData.delta.y)))
        {
            if (eventData.delta.x > 0)
            {
                // Swipe right
                speedRotate = -SpeedToRotate;
            }
            else
            {
                speedRotate = SpeedToRotate;
                // Swipe left
            }
            IsRotateNow = true;
        }
    }
    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }
    public virtual void OnDrag(PointerEventData eventData)
    {
        OnBeginDrag(eventData);
    }
    public virtual void OnPointerUp(PointerEventData eventData)
    {
        IsRotateNow = false;
    }
    #endregion
}
