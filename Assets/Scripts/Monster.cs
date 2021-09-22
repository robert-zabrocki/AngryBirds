using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Monster : MonoBehaviour
{
    [SerializeField] Sprite _sprite;
    [SerializeField] ParticleSystem _particleSystem;

    private bool _hasDied;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (ShouldDieFromBird(collision)) StartCoroutine(Die());
    }

    IEnumerator Die()
    {
        _hasDied = true;

        GetComponent<SpriteRenderer>().sprite = _sprite;
        _particleSystem.Play();
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }

    private bool ShouldDieFromBird(Collision2D collision)
    {
        Bird bird = collision.gameObject.GetComponent<Bird>();

        if (_hasDied) return false;

        if (bird != null) return true;
        if (collision.contacts[0].normal.y < -0.5) return true;

        return false;
    }
}
