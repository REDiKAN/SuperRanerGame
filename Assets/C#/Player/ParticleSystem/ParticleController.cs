using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField] ParticleSystem movementParticle;
    [SerializeField] ParticleSystem fallParticle;
    [SerializeField] ParticleSystem touchParticle;

    [Range(0, 10), SerializeField] int occurAfterVelocity;
    [Range(0, 0.2f), SerializeField] float dustFormationPeriod;

    [SerializeField] Rigidbody2D playerRb;

    float count;

    public void PlayTouchParticle()
    {
        touchParticle.Play();
    }

    private void Update()
    {
        count += Time.deltaTime;

        if (playerRb.gameObject.GetComponent<PlayerMove>().onGraund == true && Mathf.Abs(playerRb.velocity.x) > occurAfterVelocity)
        {
            if (count > dustFormationPeriod)
            {
                movementParticle.Play();
                count = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            fallParticle.Play();
        }
    }
}
