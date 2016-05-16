using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class DroneController : MonoBehaviour {
    ///// CONFIGURABLES /////
    public GameObject Drone;
    public DroneAnimator Animator;

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

        Vector2 throttle = _device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis1);
        Debug.Log("Trigger state: (" + throttle.x + ", " + throttle.y + ")");
	}
}
