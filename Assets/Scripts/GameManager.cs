using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Instance dari kelas GameManager
    public Card[] cards; // Array untuk menyimpan semua kartu dalam game

    public int score; // Skor pemain
    public int lives; // Jumlah nyawa pemain
    public int streak; // Jumlah pasangan kartu yang ditemukan secara berturut-turut

    public GameObject powerUpText; // Objek untuk menampilkan teks power up

    [SerializeField] private Card firstCard; // Kartu pertama yang diklik
    [SerializeField] private Card secondCard; // Kartu kedua yang diklik

    void Start()
    {
        instance = this;

        // Mengacak posisi kartu-kartu dengan menggunakan metode Fisher-Yates shuffle
        for (int i = 0; i < cards.Length; i++)
        {
            int randomIndex = Random.Range(i, cards.Length);
            Card temp = cards[i];
            cards[i] = cards[randomIndex];
            cards[randomIndex] = temp;
        }
    }

    void Update()
    {

    }

    // Fungsi untuk mengatur apa yang terjadi ketika sebuah kartu diklik
    public void CardClicked(Card card)
    {
        // Jika ini adalah kartu pertama yang diklik, simpan kartu tersebut ke dalam variabel firstCard
        if (firstCard == null)
        {
            firstCard = card;
        }
        // Jika ini adalah kartu kedua yang diklik, simpan kartu tersebut ke dalam variabel secondCard
        else if (secondCard == null)
        {
            secondCard = card;

            // Jika kedua kartu yang diklik jenisnya sama, tandai kedua kartu tersebut sebagai sudah cocok
            // dan tambahkan skor pemain sebanyak 1
            if (firstCard.frontImage == secondCard.frontImage)
            {
                firstCard.SetMatched();
                secondCard.SetMatched();

                StartCoroutine(RemoveCards());

                score++;
                // Tambahkan streak jika kartu yang ditemukan sama dengan streak sebelumnya
                if (streak == 2)
                {
                    streak++;

                    // Tampilkan teks power up dan aktifkan fitur power up
                    powerUpText.SetActive(true);
                    StartCoroutine(PowerUp());
                }
                else
                {
                    streak = 1;
                }
            }
            // Jika kedua kartu yang diklik jenisnya berbeda, kembalikan kedua kartu tersebut ke posisi semula
            // dan kurangi nyawa pemain sebanyak 1
            else
            {
                StartCoroutine(FlipCardsBack());

                lives--;
                streak = 0;
            }
        }
    }

    private void ResetCard()
    {
        // Bersihkan variabel firstCard dan secondCard agar siap menyimpan kartu baru jika diklik kembali
        firstCard = null;
        secondCard = null;
    }

    // Fungsi untuk menunggu selama 1 detik sebelum kembali menampilkan kartu yang jenisnya berbeda
    private IEnumerator FlipCardsBack()
    {
        yield return new WaitForSeconds(1);

        firstCard.Flip();
        secondCard.Flip();
        ResetCard();
    }

    private IEnumerator RemoveCards()
    {
        yield return new WaitForSeconds(1);

        firstCard.gameObject.SetActive(false);
        secondCard.gameObject.SetActive(false);
        firstCard = null;
        secondCard = null;
    }

    // Fungsi untuk mengaktifkan fitur power up selama 3 detik
    private IEnumerator PowerUp()
    {
        // Tandai semua kartu sebagai tertampil
        foreach (Card card in cards)
        {
            card.Flip();
        }

        yield return new WaitForSeconds(3);

        // Kembalikan semua kartu ke posisi semula
        foreach (Card card in cards)
        {
            if (!card.isMatched)
            {
                card.Flip();
            }
        }

        // Sembunyikan teks power up
        powerUpText.SetActive(false);
    }
}