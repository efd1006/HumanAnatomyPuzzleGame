using UnityEngine;

public class SimpleDragA : MonoBehaviour {

    public GameObject black, colored;

    public LayerMask LayerMask = UnityEngine.Physics.DefaultRaycastLayers;

    private Lean.LeanFinger draggingFinger;
	// Use this for initialization
	void Start ()
    {
		
	}

    protected virtual void OnEnable()
    {
        // Hook into the OnFingerDown event
		Lean.LeanTouch.OnFingerDown += OnFingerDown;

        // Hook into the OnFingerUp event
		Lean.LeanTouch.OnFingerUp += OnFingerUp;
    }

    protected virtual void OnDisable()
    {
		Lean.LeanTouch.OnFingerDown += OnFingerDown;
		Lean.LeanTouch.OnFingerUp += OnFingerUp;
    }
	
    protected virtual void LateUpdate()
    {
        if(draggingFinger != null)
        {
            Lean.LeanTouch.MoveObject(transform, draggingFinger.DeltaScreenPosition);
        }
    }

	// Update is called once per frame
	void Update ()
    {
        float distance = Vector3.Distance(black.transform.position, colored.transform.position);

        if(distance < 0.5)
        {
            draggingFinger = null;

            colored.transform.position = black.transform.position;
			black.SetActive (false);
        }
	}

	public void OnFingerDown(Lean.LeanFinger finger)
	{
		// Raycast information
		var ray = finger.GetRay();
		var hit = default(RaycastHit);

		// Was this finger pressed down on a collider?
		if (Physics.Raycast(ray, out hit, float.PositiveInfinity, LayerMask) == true)
		{
			// Was that collider this one?
			if (hit.collider.gameObject == gameObject) {
				// Set the current finger to this one
				draggingFinger = finger;
			}
		}
	}

	public void OnFingerUp(Lean.LeanFinger finger)
	{
		// Was the current finger lifted from the screen?
		if (finger == draggingFinger)
		{
			// Unset the current finger
			draggingFinger = null;
		}
	}
}
