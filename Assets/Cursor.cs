using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR.WSA.Input;

public class Cursor : MonoBehaviour {
    private bool isLaserAllowed = false;
    GestureRecognizer recognizer;
    private GameObject laserBeam;

	// Use this for initialization
	void Start () {
        InvokeRepeating("DrawCube", .5f, .1f);
        recognizer = new GestureRecognizer();
        recognizer.TappedEvent += ToggleLaser;
        recognizer.StartCapturingGestures();
	}

    private void ToggleLaser(InteractionSourceKind source, int tapCount, Ray headRay) {
        Debug.Log("nonniiiin");
        isLaserAllowed = !isLaserAllowed;
    }

    void Update() {
        if (isLaserAllowed) {
            if (laserBeam == null) {
                laserBeam = GameObject.CreatePrimitive(PrimitiveType.Cube);
                laserBeam.transform.localScale = new Vector3(1f, .5f, 5);
            }
            var headPosition = Camera.main.transform.position;
            var gazeDirection = Camera.main.transform.forward;

            RaycastHit hitInfo;
            if (Physics.Raycast(headPosition, gazeDirection, out hitInfo)) {
                var bitInFrontOfHead = new Vector3(headPosition.x, headPosition.y, headPosition.z + .9f);
                var between = hitInfo.point - bitInFrontOfHead;
                laserBeam.transform.localScale = new Vector3(.7f, .075f, between.magnitude);
                laserBeam.transform.position = between / 2 + bitInFrontOfHead;
                laserBeam.transform.LookAt(hitInfo.point);
                laserBeam.GetComponent<Renderer>().material = new Material(Shader.Find("Transparent/Diffuse"));
                laserBeam.GetComponent<Renderer>().material.color = new Color(255, 0, 0, .3f);
                Destroy(laserBeam.GetComponent<Collider>());
            }
            else {
            }
        }
        else {
            laserBeam = null;
        }
    }

    // Update is called once per frame
    void DrawCube () {
        if (isLaserAllowed) {
            var headPosition = Camera.main.transform.position;
            var gazeDirection = Camera.main.transform.forward;

            RaycastHit hitInfo;
            if (Physics.Raycast(headPosition, gazeDirection, out hitInfo)) {
                var newCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                newCube.transform.position = hitInfo.point;
                newCube.transform.localScale = new Vector3(.1f, .1f, .1f);
                newCube.GetComponent<Renderer>().material.color = new Color(255, 0, 0);
                newCube.AddComponent<DestroyCube>();
                Destroy(newCube.GetComponent<Collider>());
            }
            else {
            }
        }
	}
}
