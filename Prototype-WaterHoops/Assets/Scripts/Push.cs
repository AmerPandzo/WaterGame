using UnityEngine;
using System.Collections;

public class Push : MonoBehaviour {

	void OnTriggerStay(Collider other){
			other.rigidbody.AddForce (Vector3.up * Time.deltaTime * 1000);
		
	}
}
