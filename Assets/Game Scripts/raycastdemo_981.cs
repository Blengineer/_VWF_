using UnityEngine;


public class RaycastDemo : MonoBehaviour {
	public Transform target1, target2;
	
	
	void Update () {
		if (Input.GetMouseButton(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			
			if (Physics.Raycast(ray, out hit)) {
				if (hit.transform == target1) {
					Debug.Log("Hit target 1");
				} else if (hit.transform == target2) {
					Debug.Log("Hit target 2");
				}
			} else {
				Debug.Log("Hit nothing");
			}
		}
	}
}
