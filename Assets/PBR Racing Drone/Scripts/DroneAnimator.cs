using UnityEngine;
using System.Collections;

public class DroneAnimator : MonoBehaviour {
	Transform[] Props;
	public float propSpeed = 0;
	Transform trans;
	public float bobSpeed;
	public float bobHeight;
	public float wobble;
	public float wobbleSpeed;

	// Use this for initialization
	void Start () {
		trans = transform;
		int propCount = 0;
		Props = new Transform[4];
		foreach (Transform child in transform) {
			if (child.name.ToLower().Contains ("motor")) {
				Props [propCount] = child.GetChild(0);
				propCount++;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		foreach (Transform prop in Props) {
			prop.Rotate (0, 0, propSpeed * Time.deltaTime);
		}
	}
}
