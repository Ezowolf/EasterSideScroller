using UnityEngine;
using System.Collections;

public class ParallaxLayer : MonoBehaviour 
{
	public float scrollSpeed;

	public void Scroll(Vector2 scrollDirection)
	{
		transform.Translate(scrollDirection * scrollSpeed * Time.deltaTime);
	}
}
