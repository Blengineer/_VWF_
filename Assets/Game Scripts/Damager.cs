using UnityEngine;
using System.Collections;

public class Damager : MonoBehaviour {
	
	public float damage;
	public float hp;
	
	void OnCollisionEnter(Collision hit) {
		hit.transform.GetComponent<CartControl>().dealDamage(damage);
		// play animation before being destroyed
		Destroy(gameObject);
	}
	
	public void dealDamage(float amount) {
		hp -= amount;
		if(hp <= 0)
		{
			Destroy(gameObject);
		}
	}
}
