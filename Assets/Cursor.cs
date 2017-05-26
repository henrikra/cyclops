using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR.WSA.Input;

public class Cursor : MonoBehaviour {
    private MeshRenderer meshRenderer;
    private bool isLaserAllowed = false;
    GestureRecognizer recognizer;

	// Use this for initialization
	void Start () {
        meshRenderer = this.gameObject.GetComponentInChildren<MeshRenderer>();
        GetComponent<Renderer>().material.color = new Color(255, 0, 0);
        InvokeRepeating("DrawCube", .5f, 0.02f);

        recognizer = new GestureRecognizer();
        recognizer.TappedEvent += EnableLaser;
        recognizer.StartCapturingGestures();
	}

    private void EnableLaser (InteractionSourceKind source, int tapCount, Ray headRay) {
        Debug.Log("nonniiiin");
        isLaserAllowed = true;
    }
	
	// Update is called once per frame
	void DrawCube () {
        if (isLaserAllowed) {
            var headPosition = Camera.main.transform.position;
            var gazeDirection = Camera.main.transform.forward;

            RaycastHit hitInfo;
            if (Physics.Raycast(headPosition, gazeDirection, out hitInfo)) {
                meshRenderer.enabled = true;

                this.gameObject.transform.position = hitInfo.point;
                this.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
                var newCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                newCube.transform.position = hitInfo.point;
                newCube.transform.localScale = new Vector3(.1f, .1f, .1f);
                newCube.GetComponent<Renderer>().material.color = new Color(255, 0, 0);
                newCube.AddComponent<DestroyCube>();
                foreach (var comp in newCube.GetComponents<Component>()) {
                    if (comp is Collider) {
                        Destroy(comp);
                    }
                }
            }
            else {
                meshRenderer.enabled = false;
            }
        }
	}
}
