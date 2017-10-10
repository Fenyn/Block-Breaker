using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {

    
    public Sprite[] hitSprites;
    public static int breakableCount = 0;
    public AudioClip crack;
    public GameObject smoke;

    private int maxHits;
    private int timesHit;
    private LevelManager levelManager;
    private bool isBreakable;
    



	// Use this for initialization
	void Start () {
        timesHit = 0;
        isBreakable = (this.tag == "Breakable");
        levelManager = GameObject.FindObjectOfType<LevelManager>();
        GameObject smokePuff = Instantiate(smoke, transform.position, Quaternion.identity) as GameObject;
        smokePuff.GetComponent<ParticleSystem>().startColor = gameObject.GetComponent<SpriteRenderer>().color;
        if (isBreakable)
        {
            breakableCount++;
        }

    }
	
	// Update is called once per frame
	void Update () {
        
	}

    void OnCollisionEnter2D (Collision2D collision)
    {
        timesHit++;
        maxHits = hitSprites.Length + 1;
        // SimulateWin();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (isBreakable)
        {
            HandleHits();
        }
    }

    void HandleHits()
    {
        //AudioSource.PlayClipAtPoint(crack, transform.position);
        if (timesHit >= maxHits)
        {
            
            breakableCount--;
            levelManager.BrickDestroyed();
            Destroy(gameObject);
        }
        else
        {
            LoadSprites();
        }
        Debug.Log(breakableCount);
    }

    void LoadSprites()
    {
        int spriteIndex = timesHit - 1;
        if (hitSprites[spriteIndex])
        {
            this.GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        } else
        {
            Debug.LogError("Sprite load failed for index: " + spriteIndex);
        }
    }

    // TODO Remove this method once we can actually win
    void SimulateWin()
    {
        levelManager.LoadNextLevel();
    }

    
}
