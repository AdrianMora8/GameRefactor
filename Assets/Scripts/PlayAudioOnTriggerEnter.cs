using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlappyBird.Gameplay.Bird;

public class PlayAudioOnTriggerEnter : MonoBehaviour
{
    public AudioSource source;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var birdController = collision.gameObject.GetComponent<BirdController>();
            if (birdController != null && !birdController.IsDead)
                source.Play();
        }
    }
}
