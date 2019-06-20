using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class VirtualJoystic : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
 
{
    private Image bgImg;
    private Image joystickImg;
    public Vector3 InputDirection { set; get; }

    private void Start()
    {
    bgImg = GetComponent<Image>();
    joystickImg = transform.GetChild(0).GetComponent<Image>();
    InputDirection = Vector3.zero;
    }
    public virtual void OnDrag(PointerEventData eventData)
{
    Vector2 pos = Vector2.zero;
    if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImg.rectTransform, eventData.position, eventData.pressEventCamera, out pos))
    {
        pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
        pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);
        float x = (bgImg.rectTransform.pivot.x == 1) ? pos.x * 2 + 1 : pos.x * 2 - 1;
        float y = (bgImg.rectTransform.pivot.y == 1) ? pos.y * 2 + 1 : pos.y * 2 - 1;
        InputDirection = new Vector3(x, 0, y);
        InputDirection = (InputDirection.magnitude > 1) ? InputDirection.normalized : InputDirection;
        joystickImg.rectTransform.anchoredPosition = new Vector3(InputDirection.x * (bgImg.rectTransform.sizeDelta.x / 3), InputDirection.z * (bgImg.rectTransform.sizeDelta.y / 3));
    }
    Debug.Log("OnDrag");
    // throw new System.NotImplementedException();
}
public virtual void OnPointerDown(PointerEventData eventData)
{
    OnDrag(eventData);
    Debug.Log("OnPointerDown");
    // throw new System.NotImplementedException();
}
public virtual void OnPointerUp(PointerEventData eventData)
{
    InputDirection = Vector3.zero;
    joystickImg.rectTransform.anchoredPosition = Vector3.zero;
    Debug.Log("OnPointerUp");
    // throw new System.NotImplementedException();
}
public float Horizontal()
{
    if (InputDirection.x > 0)
        return InputDirection.x;//>>>>>>>>>>>>>>>>>>>>>> to move right coz it will return +ive value
    else
        return Input.GetAxis("Horizontal");
}
public float Horizontal2()
{
    if (InputDirection.x < 0) //<< to move left
    {
        return InputDirection.x;
    }
    else
        return Input.GetAxis("Horizontal");
}
public float Vertical()
{
    if (InputDirection.z > 0) //
    {
        return InputDirection.z;
    }
    else
    {
        return Input.GetAxis("Vertical");
    }
}
public float Vertical2()
{
    if (InputDirection.z < 0)   // for doawn key
    {
        return InputDirection.z;
    }
    else
    {
        return Input.GetAxis("Vertical");
    }
}
}