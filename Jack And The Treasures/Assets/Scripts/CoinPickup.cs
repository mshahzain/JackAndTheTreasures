using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour {
    [SerializeField] AudioClip CoinPickUpSFX;
    [SerializeField] int PointsForCoinPickUp=10;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<GameSession>().AddToScore(PointsForCoinPickUp);
        
        AudioSource.PlayClipAtPoint(CoinPickUpSFX, Camera.main.transform.position);
        Destroy(gameObject);
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
