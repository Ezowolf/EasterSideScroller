using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BasicController : MonoBehaviour {
	private Vector2 movement;

	public AudioClip moneySound;
	public AudioClip jumpSound;
	
    public float walkSpeed = 2f;
	public float jumpForce = 200f;
	
	private bool m_FacingRight = true; 
	private bool is_Grounded = true;
	private bool is_Walking = false;

	public int totalCoins;

	public Text coinText;
	public Text gameOverText;

	private Rigidbody2D m_Rigidbody2D;
	private Animator animator;
	private AudioSource source;
	private ParallaxController _parallaxController;


	void Start () {
		GetTheComponents();
		gameOverText.text = "";
		totalCoins = 0;
		coinText.text = "Coins: 0";
	}

	void Update () {
		ButtonBasedMovement();
		WalkChecker();
		IdlerChecker();
	}

	void FixedUpdate(){
		VelocityChecker();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Coin")
		{
			source.PlayOneShot(moneySound);
			totalCoins = totalCoins + 1;
			Destroy(other.gameObject);
			coinText.text = "Coins: "+totalCoins;
		}
		
		if(other.gameObject.tag == "End")
		{
			source.PlayOneShot(moneySound);
			Destroy(other.gameObject);
			gameOverText.text = "End of test level. Congrats, you saved easter! :D Press R to Restart.";
		}
	}
	private void GetTheComponents()
	{
		animator = GetComponent<Animator>();
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		_parallaxController = GetComponent<ParallaxController> ();
		source = GetComponent<AudioSource>();

	}

	private void ButtonBasedMovement()
	{
		if (Input.GetKey("a"))
		{
			if(is_Grounded == true)
			{
			animator.SetInteger("state",1);
			}
			if(m_FacingRight == true)
			{
				Flip();
			}
			transform.Translate(-Vector3.right * (walkSpeed+totalCoins/33) * Time.deltaTime);
			is_Walking = true;
			_parallaxController.Scroll (1* new Vector2(1f,0f));
		}

		if (Input.GetKey("d"))
		{
			if(is_Grounded == true)
			{
			animator.SetInteger("state",1);
			}
			if(m_FacingRight == false)
			{
				Flip();
			}
			transform.Translate(Vector3.right * (walkSpeed+totalCoins/33) * Time.deltaTime);
			is_Walking = true;
			_parallaxController.Scroll (1*new Vector2(-1f,0f));
		}
		if (is_Grounded == true && Input.GetKeyDown("space"))
		{
			source.PlayOneShot(jumpSound);
			m_Rigidbody2D.AddForce(new Vector2(0f, jumpForce+totalCoins*2));
		}
		if (Input.GetKeyUp("r"))
		{
			Application.LoadLevel (Application.loadedLevelName);
		}

	}
	private void IdlerChecker()
	{
		if( is_Walking == false && is_Grounded == true &&!Input.GetKey("a") &&! Input.GetKey("d") )
		{
			animator.SetInteger("state",0);
		}
	}
	
	private void WalkChecker()
	{
		if (Input.GetKeyUp("a") && is_Grounded == true)
		{
			is_Walking = false;
		}
		
		if (Input.GetKeyUp("d" )&& is_Grounded == true)
		{
			is_Walking = false;
		}
	}
	
	private void VelocityChecker()
	{
		if(m_Rigidbody2D.velocity.y == 0 && is_Grounded == false)
		{
			is_Walking = false;
			is_Grounded = true;
		}
		if(m_Rigidbody2D.velocity.y > 0)
		{
			is_Grounded = false;
			animator.SetInteger("state",2);
		}
		if(m_Rigidbody2D.velocity.y < 0)
		{
			is_Grounded = false;
			animator.SetInteger("state",3);
		}
	}

	private void Flip()
	{
		m_FacingRight = !m_FacingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void OnDestroy()
	{
		gameOverText.text = "GAME OVER :( ";
		Application.LoadLevel (Application.loadedLevelName);
	}
}
