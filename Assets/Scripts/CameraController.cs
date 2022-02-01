using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float dragSensity = 10; // скорость передвижения камеры
    [Range(0,1)]
    public float lerpSpeed = 0.5f; // плавность и ускорение камеры

    Vector3 startCameraPostion,endCameraPosition;
    Vector3 startPointerPos;
    bool isDragging;

    private void Update()
    {
        if (isDragging)        
            Dragging();
        
        if (Input.mouseScrollDelta.magnitude > 0)
            Scrolling(Input.mouseScrollDelta);
    }

    
    public void StartDrag()
    {
        isDragging = true;
        startPointerPos = Input.mousePosition;
        startCameraPostion = transform.position;
    }

    public void Scrolling(Vector2 scrolldelta)
    {
        //приближение и отдаление камеры
        Debug.Log("Not Implemented");
    }


    void Dragging()
    {
        Vector3 pointerDelta = Input.mousePosition - startPointerPos;
        Vector3 cameraDelta = new Vector3(pointerDelta.x, 0, pointerDelta.y) * -1;
        Quaternion cameraYRotation = transform.rotation;

        
        cameraDelta = Quaternion.Euler(0, cameraYRotation.eulerAngles.y, 0) * cameraDelta; //Сдвиг камеры, учитывая её поворот и угол наклона
        endCameraPosition = startCameraPostion + cameraDelta * dragSensity;
        transform.position = Vector3.Lerp(transform.position, endCameraPosition, lerpSpeed);
    }

    public void EndDrag()
    {
        isDragging = false;
    }
}
