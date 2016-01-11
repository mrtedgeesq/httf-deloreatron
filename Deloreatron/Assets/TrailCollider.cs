using UnityEngine;
using System.Collections;

public class TrailCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter (Collision col)
    {
        Debug.LogWarning("Collision with " + col.gameObject.name);
    }
}
