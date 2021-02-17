using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] Sprite[] hitSprites;

    Lvl lvl;
    GameStatus gameStatus;

    [SerializeField] int timesHit;

    private void Start()
    {
        if (gameObject.tag == "Breakable")
        {
            lvl = FindObjectOfType<Lvl>();
            lvl.CountBreakableBlocks();
        }

        gameStatus = FindObjectOfType<GameStatus>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.tag == "Breakable")
        {
            timesHit++;
            int maxHits = hitSprites.Length + 1;
            if (timesHit >= maxHits)
            {
                AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
                gameStatus.AddToScore();
                lvl.BlockDestroyed();
                TriggerSparklesVFX();
                Destroy(gameObject);
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = hitSprites[timesHit - 1];
            }
        }
    }

    private void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 2);
    }
}
