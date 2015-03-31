using UnityEngine;
using System.Collections;

public class Controled_Push : MonoBehaviour {

	private Collider sphere;
	private bool isInside = false;
	//public GameObject bubbles;
	//
	//void OnStart(){
	//	bubbles = GameObject.FindGameObjectWithTag("Bubbles");
	//}
	//
	//void Update(){
	//	if (Input.GetKey (KeyCode.Space)) {
	//					if (bubbles != null)		
	//			bubbles.particleSystem.Emit(10);
	//			} else
	//		if (bubbles != null)		
	//			bubbles.particleSystem.Emit(0);
	//}
	void Update(){
		if (isInside)
			if(sphere != null)
				if (Input.GetKey (KeyCode.Space))
					sphere.GetComponent<Rigidbody>().AddForce (Vector3.up * Time.deltaTime * 1000);
		
	}
	void OnTriggerEnter(Collider other){
		sphere = other;
		isInside = true;
		
	}
	void OnTriggerExit(Collider other){
		sphere = null;
		isInside = false;
	}
}