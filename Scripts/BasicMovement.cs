using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour {

    public Animator animator;
	
	// Update is called once per frame
	void Update () {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Verical", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);


        transform.position = transform.position + 2 * movement * Time.deltaTime;
	}
}
