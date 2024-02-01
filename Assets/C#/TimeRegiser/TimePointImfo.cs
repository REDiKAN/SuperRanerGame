using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimePointImfo : MonoBehaviour
{
    [SerializeField] string namePoint;

    [SerializeField] Finish finish;
    float timePoint;
    
    float timePointOne;
    float timePoimtTwo;
    public Timer timer;

    public TypeTimePoint typeTimePoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (typeTimePoint == TypeTimePoint.trigger)
        {
            if (collision.gameObject.tag == "Player")
            {
                timePoint = timer.maxTime;
                finish.AddTimeTrigger(timePoint, namePoint);
                Destroy(gameObject);
            }
        }
        else if (typeTimePoint == TypeTimePoint.zone)
        {
            if (collision.gameObject.tag == "Player")
            {
                timePointOne = timer.maxTime;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (typeTimePoint == TypeTimePoint.zone)
        {
            if (collision.gameObject.tag == "Player")
            {
                timePoimtTwo = timer.maxTime;
                finish.AddTimeZone(timePointOne, timePoimtTwo, namePoint);
                Destroy(gameObject);
            }
        }
    }
}
public enum TypeTimePoint { trigger, zone, events }
