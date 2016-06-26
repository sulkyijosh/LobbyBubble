using UnityEngine;
using System.Collections;

public class ShootScript : MonoBehaviour {

	[SerializeField] GameObject activeBubble;
	[SerializeField] GameObject idleBubble;
	[SerializeField] GameObject[] randomBubble;

	[SerializeField] GameObject cannon;

	[SerializeField] GameObject activeBubbleLocation;
	[SerializeField] GameObject idleBubbleLocation;

	void Awake()
	{
		activeBubble = Instantiate(randomBubble[Random.Range(0, randomBubble.Length)], activeBubbleLocation.transform.position, transform.rotation) as GameObject;
		activeBubble.GetComponent<CircleCollider2D>().enabled = true;
		idleBubble = Instantiate(randomBubble[Random.Range(0, randomBubble.Length)], idleBubbleLocation.transform.position, transform.rotation) as GameObject;
	}

	void Update()
	{
		float maxSpeed = 7;

		if(Input.touchCount > 0)
		{
			Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3 (Input.GetTouch(0).position.x, Input.GetTouch(0).position.y , Camera.main.nearClipPlane + 5f));
			if(Input.GetTouch(0).phase == TouchPhase.Began)
			{
				if(pos.y >= 0)
				{
				cannon.transform.up = new Vector2 (pos.x, pos.y);
				}
			}
			if(Input.GetTouch(0).phase == TouchPhase.Moved)
			{
				if(pos.y >= 0)
				{
				cannon.transform.up = new Vector2 (pos.x, pos.y);
				}
				else
				{
					cannon.transform.up = Vector2.up;
				}
			}
			if(Input.GetTouch(0).phase == TouchPhase.Ended)
			{
				if(pos.y >= 0)
				{
				Shoot();
				cannon.transform.up = new Vector2 (pos.x, pos.y);
				}
				else
				{
					cannon.transform.up = Vector2.up;
				}
			}
		}

		if(activeBubble.GetComponent<Rigidbody2D>().velocity.magnitude >= maxSpeed)
		{
			activeBubble.GetComponent<Rigidbody2D>().velocity = activeBubble.GetComponent<Rigidbody2D>().velocity.normalized * maxSpeed;
		}
	}

	void Shoot() //actual launching of bubble
	{
		Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3 (Input.GetTouch(0).position.x, Input.GetTouch(0).position.y , Camera.main.nearClipPlane + 5f));
		if(pos.y >= activeBubble.transform.position.y)
		{
			Ray ray = new Ray (activeBubble.transform.position, pos);
			Vector3 direction = Vector3.ClampMagnitude(pos, 1f);
			float go = 100 * Time.deltaTime;
			activeBubble.GetComponent<Rigidbody2D>().AddForce(direction * 35000 / pos.y);
			StartCoroutine(CreateBubble());

		}

	}

	void BubbleCreation() //creates new bubble
	{
		activeBubble = idleBubble;
		iTween.MoveTo(activeBubble.gameObject, iTween.Hash("position", activeBubbleLocation.transform.position, "time", 0.7f, "islocal", true));
		idleBubble = Instantiate(randomBubble[Random.Range(0, randomBubble.Length)], idleBubbleLocation.transform.position, transform.rotation) as GameObject;
		MakeBubbleActive();
	}
	void MakeBubbleActive() //enables bubble
	{
		activeBubble.GetComponent<CircleCollider2D>().enabled = true;
	}

	private IEnumerator CreateBubble() // delays creation of bubble
	{
		yield return new WaitForSeconds(.5f);

		BubbleCreation();
	}

}
