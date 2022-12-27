using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public Sprite frontImage; // Gambar yang akan ditampilkan ketika kartu dibalik
    public Sprite backImage; // Gambar yang akan ditampilkan ketika kartu dibalik kembali
    public bool isMatched; // Status apakah kartu tersebut sudah cocok dengan kartu lain

    private SpriteRenderer spriteRenderer; // Komponen sprite renderer untuk menampilkan gambar
    private bool isFlipped; // Status apakah kartu tersebut sedang ditampilkan atau tidak

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = backImage;
    }

    void Update()
    {
        
    }

    // Fungsi untuk mengubah posisi kartu ketika diklik
    void OnMouseDown()
    {
        if (!isMatched)
        {
            Flip();

            GameManager.instance.CardClicked(this);
        }
    }

    // Fungsi untuk mengubah gambar kartu sesuai dengan status flip
    public void Flip()
    {
        if (isFlipped)
        {
            spriteRenderer.sprite = backImage;
            isFlipped = false;
        }
        else
        {
            spriteRenderer.sprite = frontImage;
            isFlipped = true;
        }
        Debug.Log("Flipped");
    }

    // Fungsi untuk menandai kartu sebagai sudah cocok dengan kartu lain
    public void SetMatched()
    {
        isMatched = true;
    }
}