using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MenuRotate : MonoBehaviour
{
    public float secondsRotating;
    public Vector3 rotation;

    void Start()
    {
        StartCoroutine(MenuRotateCo());
    }

    private IEnumerator MenuRotateCo()
    {
        transform.DORotate(rotation, secondsRotating).SetEase(Ease.InOutQuad);
        yield return new WaitForSeconds(secondsRotating);
        transform.DORotate(-rotation, secondsRotating).SetEase(Ease.InOutQuad);
        yield return new WaitForSeconds(secondsRotating);
        StartCoroutine(MenuRotateCo());
    }
}