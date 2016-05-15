using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class PickUpJointed : MonoBehaviour
{
    public GameObject Sphere;

    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device device;

    FixedJoint joint;

	void Awake () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
	}

    void FixedUpdate()
    {
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
        if (joint == null && device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {
            joint = other.gameObject.AddComponent<FixedJoint>();

            joint.connectedBody = gameObject.GetComponent<Rigidbody>();
        }
        else if (joint != null && device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            Rigidbody grabbed = joint.gameObject.GetComponent<Rigidbody>();
            Destroy(joint);
            joint = null;

            tossObject(grabbed);
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
