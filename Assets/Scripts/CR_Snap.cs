using UnityEngine;
using System.Collections;

public class CR_Snap : MonoBehaviour {

	void OnTriggerEnter(Collider e)
	{
		if (e.gameObject.GetComponent<CR_Drag>() != null)
		{
			e.gameObject.GetComponent<CR_Drag>().SnapToMe(this);
		}
	}
}
