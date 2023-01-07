using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private MoverAgent mover;

    private SpriteRenderer spriteRenderer;
    private Color defaultBackgroundColor = Color.black;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void swapColor(Color c)
    {
        spriteRenderer.color = c;
    }

    IEnumerator swapColor(Color c, float time)
    {
        spriteRenderer.color = c;
        yield return new WaitForSeconds(time);
        spriteRenderer.color = defaultBackgroundColor;
    }
}
