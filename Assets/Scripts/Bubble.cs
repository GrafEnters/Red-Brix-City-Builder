using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Bubble : MonoBehaviour
{
    public SpriteRenderer Icon;
    public TextMeshPro Text;
    public bool isRotatingTowardsCamera = true;

    UnityAction onClicked; // событие, которое запустится при нажатии на кнопку
    bool canBeClicked;

    public void SetValues(Sprite icon, string text = "")
    {
        Icon.gameObject.SetActive(text == "");
        Icon.sprite = icon;
        Text.text = text;
    }

    public void SetClickedAction(UnityAction onClickedAction)
    {
        onClicked = onClickedAction;
    }

    private void OnMouseDown()
    {
        if (onClicked != null && canBeClicked)
            onClicked.Invoke();
    }

    //Поворачиватся всегда лицом в сторону камеры
    private void Update()
    {
        if(isRotatingTowardsCamera)
            transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
    }

    public void StartTimer(int seconds, UnityAction onEndTimer)
    {
        StartCoroutine(TimerCoroutine(seconds, onEndTimer));
    }

    //Таймер обратного отсчёта, по окончании которого запускается событие onEndTimer
    //Во время работы таймера кнопка не интерактивна
    IEnumerator TimerCoroutine(int seconds, UnityAction onEndTimer)
    {
        canBeClicked = false;
        int time = seconds;
        Icon.gameObject.SetActive(false);
        Text.text = TimeStringConverter.GetTimeString(time);
        while (time > 0)
        {
            yield return new WaitForSecondsRealtime(1);
            time -= 1;
            Text.text = TimeStringConverter.GetTimeString(time);
        }
        onEndTimer.Invoke();
        canBeClicked = true;
    }
}
