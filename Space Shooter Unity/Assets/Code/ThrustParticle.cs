using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrustParticle : MonoBehaviour
{
    private float _fadeSpeed;
    private float _scale;
    public SpriteRenderer sprite;

    private void Start()
    {
        _fadeSpeed = Random.Range(0.01f, 0.15f);
        _scale = Random.Range(0.1f, 0.3f);
        transform.localScale = new Vector3(_scale, _scale, 1);
    }

    void Update()
    {
        if (sprite.color.a > 0)
        {
            float newAlpha = sprite.color.a - _fadeSpeed;
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, newAlpha);
            transform.Translate((Vector3.down * 5) * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
