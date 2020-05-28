using GameSettings;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VirtualJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler {

    public Image ImgBg;
    public Image ImgJoystick;
    float elapsedTime = 0;

    private Vector3 _inputVector;
    public Vector3 InputVector {
        get {
            return _inputVector;
        }
    }


    public void OnPointerDown(PointerEventData e) {
        OnDrag(e);
    }

    public void OnDrag(PointerEventData e) {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(ImgBg.rectTransform,
                                                                    e.position,
                                                                    e.pressEventCamera,
                                                                    out pos)) {

            pos.x = (pos.x / ImgBg.rectTransform.sizeDelta.x);
            pos.y = (pos.y / ImgBg.rectTransform.sizeDelta.y);

            _inputVector = new Vector3(pos.x * 1 , 0, pos.y * 1);
            _inputVector = (_inputVector.magnitude > 1.0f) ? _inputVector.normalized : _inputVector;

            ImgJoystick.rectTransform.anchoredPosition = new Vector3(_inputVector.x * (ImgBg.rectTransform.sizeDelta.x * .4f),
                                                                     _inputVector.z * (ImgBg.rectTransform.sizeDelta.y * .4f));
        }

    }   

    public void OnPointerUp(PointerEventData e) {
        _inputVector = Vector3.zero;
        ImgJoystick.rectTransform.anchoredPosition = Vector3.zero;
    }


    public float Horizontal() {
        print("HORIZ: " + _inputVector.x);
        if (_inputVector.x != 0) {
            return _inputVector.x;
        }

       
        return Input.GetAxis("Horizontal");
    }

    public float Vertical() {
        print("HORIZ: " + _inputVector.z);
        if (_inputVector.z != 0) {
            return _inputVector.z;
        }

        return Input.GetAxis("Vertical");
    }


    void Update()
    {
        float outX = _inputVector.x;
        float outY = _inputVector.z;

        print("%d Data: " + outX + "<X Y>" + outY);
        if (elapsedTime > 1 - Mathf.Abs(outX))
        {
            if (outX > 0)
                GameData.inX = 1;

            if (outX < 0)
                GameData.inX = -1;

            elapsedTime = 0;
        }
        else
        {
            elapsedTime += Time.deltaTime;
        }

        if (elapsedTime > 1 - Mathf.Abs(outY))
        {
            if (outY > 0)
                GameData.inY = 1;

            if (outY < 0)
                GameData.inY = -1;

            elapsedTime = 0;
        }
        else
        {
            elapsedTime += Time.deltaTime;
        }
        
    }
}