using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class PickUpParent : MonoBehaviour {

    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device device;

    private HashSet<GameObject> grabbedChildren;

	void Awake () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        grabbedChildren = new HashSet<GameObject>();
	}
	
	void FixedUpdate () {
        device = SteamVR_Controller.Input((int)trackedObj.index);

        if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            foreach (GameObject formerlyGrabbed in grabbedChildren)
            {
                formerlyGrabbed.transform.SetParent(null, true);
                formerlyGrabbed.GetComponent<Rigidbody>().isKinematic = false;
            }

            grabbedChildren.Clear();
        }
	}

    void OnTriggerStay(Collider other)
    {
        Debug.Log("Trigger colliding with " + other.name);

        if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger) && ! grabbedChildren.Contains(other.gameObject))
        {
            other.transform.parent = transform;
            other.GetComponent<Rigidbody>().isKinematic = true;
            grabbedChildren.Add(other.gameObject);
        }
    }
}
