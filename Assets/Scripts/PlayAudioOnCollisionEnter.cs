using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlappyBird.Gameplay.Bird;

public class PlayAudioOnCollisionEnter : MonoBehaviour
{
    public AudioSource source;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            var birdController = collision.gameObject.GetComponent<BirdController>();
            if (birdController != null && !birdController.IsDead)
                source.Play();
        }
    }
}
