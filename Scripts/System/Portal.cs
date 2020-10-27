using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {
    public float x, y;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Telepotation(x, y, collision);
    }
    private void Telepotation(float x, float y, Collider2D collision)
    {
        Vector2 destination;
        destination = collision.transform.position;
        destination.x = x;
        destination.y = y;
        collision.transform.position = destination;

    }
}
