using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class EffectTrigger : MonoBehaviour
{
    [SerializeField] ItemEffects itemEffects;
    private GameManager GM;
    private void Start()
    {
        GM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (itemEffects.effectType == effectsType.upSpeed)
            {
                StartCoroutine(ActivEffectSpeed(collision.GetComponent<PlayerMove>()));
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }

    IEnumerator ActivEffectSpeed(PlayerMove player)
    {
        float contener = player.speed;
        player.speed += itemEffects.forceEffect;
        Debug.Log($"Ёффект {itemEffects.nameEffect} был подобран");
        StartCoroutine(StavnUIPanel());

        yield return new WaitForSeconds(itemEffects.time);

        player.speed = contener;
        StopCoroutine(ActivEffectSpeed(player));
    }
    IEnumerator StavnUIPanel()
    {
        GameObject fullAmount = Instantiate(GM.iconEffect, GM.effectsPanel.transform.parent);
        fullAmount.transform.parent = GM.effectsPanel.transform;
        DOTween.Sequence()
            .Append(fullAmount.transform.GetChild(0).GetComponent<Image>().DOFillAmount(1f, itemEffects.time));
        yield return new WaitForSeconds(itemEffects.time);
        Destroy(fullAmount);
    }
}
