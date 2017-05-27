using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR.WSA.Input;

public class Cursor : MonoBehaviour {
    private bool isLaserAllowed = false;
    GestureRecognizer recognizer;

	// Use this for initialization
	void Start () {
        recognizer = new GestureRecognizer();
        recognizer.TappedEvent += ToggleLaser;
        recognizer.StartCapturingGestures();
	}

    private void ToggleLaser(InteractionSourceKind source, int tapCount, Ray headRay) {
        isLaserAllowed = !isLaserAllowed;
    }

    void Update() {
        if (isLaserAllowed) {
            var headPosition = Camera.main.transform.position;
            var gazeDirection = Camera.main.transform.forward;

            RaycastHit hitInfo;
            if (Physics.Raycast(headPosition, gazeDirection, out hitInfo)) {
                var newCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                newCube.transform.position = hitInfo.point;
                newCube.transform.rotation = Camera.main.transform.rotation;
                newCube.transform.localScale = new Vector3(.7f, .075f, .1f);
                newCube.GetComponent<Renderer>().material.color = new Color(255, 0, 0);
                newCube.AddComponent<DestroyCube>();
                Destroy(newCube.GetComponent<Collider>());
            }
        }
    }
}
