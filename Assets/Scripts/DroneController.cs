using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class DroneController : MonoBehaviour {
    ///// HOOKUP CONFIGURABLES /////
    public GameObject Drone;
    public DroneAnimator Animator;

    ///// BEHAVIOR CONFIGURABLES /////
    public float MaxForce;

    ///// INFERRED CONFIGURATION /////
    private SteamVR_Controller.Device _device;
    private SteamVR_TrackedObject _controller;

    ///// STATE /////


	// Use this for initialization
    void Awake()
    {
        _controller= GetComponent<SteamVR_TrackedObject>();
	}
	
	// Update is called once per frame
    void FixedUpdate()
    {
        _device = SteamVR_Controller.Input((int)_controller.index);

        handleThrottle();
	}

    void handleThrottle()
    {
        // get trigger degree of activation
        float throttle = _device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis1).x;

        // compute force direction + magnitude
        Vector3 localSpaceForce = new Vector3(0, 0, throttle * MaxForce);
        Vector3 worldSpaceForce = Drone.transform.TransformDirection(localSpaceForce);

        // apply to drone
        Drone.GetComponent<Rigidbody>().AddForce(worldSpaceForce);
    }
}
