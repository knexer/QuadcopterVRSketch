using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class PickUpParent : MonoBehaviour {
    public GameObject Sphere;

    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device device;

	void Awake () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
	}
	
	void FixedUpdate () {
        device = SteamVR_Controller.Input((int)trackedObj.index);

        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Debug.Log("Resetting sphere position.");
            Sphere.transform.position = Vector3.zero;
            Sphere.GetComponent<Rigidbody>().velocity = Vector3.zero;
            Sphere.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
	}

    void OnTriggerStay(Collider other)
    {
        Debug.Log("Trigger colliding with " + other.name);

        if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {
            other.transform.parent = transform;
            other.GetComponent<Rigidbody>().isKinematic = true;
        }

        if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            other.transform.SetParent(null, true);
            other.GetComponent<Rigidbody>().isKinematic = false;

            tossObject(other.GetComponent<Rigidbody>());
        }
    }

    private void tossObject(Rigidbody other)
    {
        Transform origin = trackedObj.origin ? trackedObj.origin : trackedObj.transform.parent;

        if (origin != null)
        {
            other.velocity = origin.TransformVector(device.velocity);
            other.angularVelocity = origin.TransformVector(device.angularVelocity);// TODO is this too naive?
        }
        else
        {
            other.velocity = device.velocity;
            other.angularVelocity = device.angularVelocity;// TODO is this too naive?
        }
    }


}
