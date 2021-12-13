using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

	public CharacterController2D controller;

	public float runSpeed = 40f;

	float horizontalMove = 0f;
	bool jump = false;
	bool crouch = false;
    public Animator anima;
	

	[SerializeField] private int cherries = 0;
	[SerializeField] private Text CherryText;

	// Update is called once per frame
	void Update () {

		// Moving
		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        anima.SetFloat("Speed", Mathf.Abs(horizontalMove));

		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
            anima.SetBool("IsJumping", true);
		}

		if (Input.GetButtonDown("Crouch"))
		{
			crouch = true;
		} else if (Input.GetButtonUp("Crouch"))
		{
			crouch = false;
		}

	}

	// Landing
    public void OnLanding(){
        anima.SetBool("IsJumping", false);
    }

	// Manyurup wkwkw
    public void OnCrouching(bool isCrouching)
    {
        anima.SetBool("IsCrouching", isCrouching);
    }

	// Hitung Total Cherry
	public void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Collect" ){
			Destroy(collision.gameObject);
			cherries += 1;
			CherryText.text = cherries.ToString();
		}
	}

	// Musuh
	private void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag == "enemy" && jump == false ){
			Destroy(other.gameObject);
		}
	}

	void FixedUpdate ()
	{
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
		jump = false;
	}
}
