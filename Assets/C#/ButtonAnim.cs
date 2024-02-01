using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class ButtonAnim : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            gameObject.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 1f);
        }
    }
}
