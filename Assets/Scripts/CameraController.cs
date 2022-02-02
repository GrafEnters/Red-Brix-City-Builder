using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float dragSensity = 10; // скорость передвижения камеры
    [Range(0, 1)]
    public float lerpSpeed = 0.5f; // плавность и ускорение камеры
    [Min(1)]
    public float minHeight; // минимальная высота приближение камеры
    public float maxHeight; // максимальная высота отдаления камеры
    public float cameraBoundsSize; //расстояние, на котором камера может отлететь от края острова

    Vector3 startCameraPostion, endCameraPosition;
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

    //Приближает камеру в место, куда смотрит курсор игрока
    //Отдаляет камеру противоположно направлению камеры
    //Скорость приближения зависит от высоты камеры - чем выше, тем быстрее
    public void Scrolling(Vector2 scrolldelta)
    {
        Vector3 direction;
        if (scrolldelta.y > 0)
            direction = Camera.main.ScreenPointToRay(Input.mousePosition).direction;
        else
            direction = Camera.main.transform.forward;
        Vector3 delta = direction * scrolldelta.y * 1;
        float speedMultiplier = Mathf.Sqrt(transform.position.y);
        Vector3 targetPosition = transform.position + delta * speedMultiplier;

        MoveCamera(targetPosition);
    }

    //Перетаскивает камеру
    //Скорость перетаскивания зависит от высоты камеры - чем выше, тем быстрее
    void Dragging()
    {
        Vector3 pointerDelta = Input.mousePosition - startPointerPos;
        Vector3 cameraDelta = new Vector3(pointerDelta.x, 0, pointerDelta.y) * -1;
        Quaternion cameraYRotation = transform.rotation;

        float speedMultiplier = Mathf.Sqrt(transform.position.y);
        cameraDelta = Quaternion.Euler(0, cameraYRotation.eulerAngles.y, 0) * cameraDelta * speedMultiplier; //Сдвиг камеры, учитывая её поворот и угол наклона
        endCameraPosition = startCameraPostion + cameraDelta * 0.001f * dragSensity;
        MoveCamera(endCameraPosition);
    }

    //Передвигает камеру в направлении точки target
    //Камера не может выйти за пределы определённой области
    void MoveCamera(Vector3 target)
    {
        Vector3 islandSize = GridManager.instance.IslandSize();
        Vector3 minBounds = -Vector3.one * 1.5f * cameraBoundsSize;
        Vector3 maxBounds = islandSize - Vector3.one * cameraBoundsSize;
        minBounds.y = minHeight;
        maxBounds.y = maxHeight;
        target = ClampVector(target, minBounds, maxBounds);
        transform.position = Vector3.Lerp(transform.position, target, lerpSpeed);
    }


    Vector3 ClampVector(Vector3 target, Vector3 minBounds, Vector3 maxBounds)
    {
        target.x = Mathf.Clamp(target.x, minBounds.x, maxBounds.x);
        target.y = Mathf.Clamp(target.y, minBounds.y, maxBounds.y);
        target.z = Mathf.Clamp(target.z, minBounds.z, maxBounds.z);
        return target;
    }

    public void EndDrag()
    {
        isDragging = false;
    }
}
