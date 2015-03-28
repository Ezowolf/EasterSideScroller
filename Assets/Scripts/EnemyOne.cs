using UnityEngine;
using System.Collections;
public class EnemyOne : MonoBehaviour {
	public float moveSpeed = 1.0f;
	public Vector2 moveAmount;
	private float moveDirection = 1.0f;
	public GameObject playerObj;
	private Rigidbody2D p_rigibody;	
	private AudioSource source;	
	public AudioClip plopSound;
	public GameObject coin;
	public GameObject splat;


	void Start()
	{
		source = GetComponent<AudioSource>();
	}

	void FixedUpdate()
	{
		moveAmount.x = moveDirection * moveSpeed * Time.deltaTime;
		transform.Translate(moveAmount);
	}
	
	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.name != "Player")
		{
		Flip();
		}
		else
		{
			source.PlayOneShot(plopSound);
			p_rigibody = playerObj.GetComponent<Rigidbody2D>();
		if(p_rigibody.velocity.y<0)
			{
				Instantiate(splat, transform.position, transform.rotation);
				Instantiate(coin, transform.position, transform.rotation);
				AudioSource.PlayClipAtPoint(plopSound, transform.position);
				p_rigibody.AddForce(new Vector2(0f, 500f));
				Destroy(this.gameObject);
			}
			else
			{
				Destroy(other.gameObject);
			}
		}
		
	}
	private void Flip()
	{
		moveDirection *= -1;
		Vector3 enemyScale = transform.localScale;
		enemyScale.x *= -1;
		transform.localScale = enemyScale;
	}
}