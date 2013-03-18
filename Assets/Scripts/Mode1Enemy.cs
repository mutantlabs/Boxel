using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Mode1Enemy : MonoBehaviour
{
    public int scoreValue = 100;

    private Vector3 storedVelocity;

    void Start()
    {
        Invoke("AddOffscreenDestroy", 0.4f);
    }

    void OnCollisionEnter(Collision collision)
    {
        bool player = collision.gameObject.CompareTag("player"),
             enemy = collision.gameObject.CompareTag("enemy");
        if (player || enemy)
        {
            if (player)
            {
                Camera.main.GetComponent<CameraShake>().Shake();
                Mode1GameStateManager.Instance.ScoreSystem.RemoveScore(scoreValue * 2);
            }
            Destroy();
        }
    }

    void OnPauseGame()
    {
        storedVelocity = rigidbody.velocity;
        rigidbody.velocity = Vector3.zero;
    }

    void OnResumeGame()
    {
        rigidbody.velocity = storedVelocity;
    }

    #region helper functions

    void AddOffscreenDestroy()
    {
        if (PauseManager.Instance.IsPaused)
            Invoke("AddOffscreenDestroy", 0.4f);
        else
            gameObject.AddComponent<DestroyOffscreen>();
    }

    #endregion helper functions

    #region public functions

    public void Destroy()
    {
        PrefabManager.Instance.CreateInstance("Enemy Explosion", transform.position);
        Destroy(gameObject);
    }

    #endregion public functions
}