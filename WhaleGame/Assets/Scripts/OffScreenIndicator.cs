using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
[RequireComponent(typeof(RectTransform))]
public class OffScreenIndicator : MonoBehaviour {

    private RectTransform canvasTransform;
    [SerializeField]
    private Image[] indicator;
    private new Camera camera;

    // Use this for initialization
    void Awake() {
        // Find and assign the canvas compenent
        if (!(canvasTransform = GetComponent<RectTransform>()))
            Debug.LogError("OffScreenIndicator: Missing RectTransform component", this); // Throw an error if RectTransform(canvas transforms) not found
    }

    private void Start() {
        camera = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate() {
        SetIndicatorCount(OceanManager.allFishHomes.Count);
        if (OceanManager.allFishHomes.Count > 0) {
            float height = Mathf.Tan(camera.fieldOfView / 2 * Mathf.Deg2Rad) * Vector3.Distance(camera.transform.position, transform.position) * 2;
            float width = height / ((float)camera.pixelHeight / camera.pixelWidth);
            canvasTransform.sizeDelta = new Vector2(width, height);
            for (int i = 0; i < OceanManager.allFishHomes.Count; ++i) {
                if (OceanManager.allFishHomes[i] == null) // if target some how becomes null, ignore target
                    continue;
                
                float distance = Vector3.Distance(OceanManager.allFishHomes[i].transform.position, transform.position) / height * 2;

                // Get target object's angle from the center of the screen by projecting the objects position to a plane on the canvas
                float screenAngle = Vector3.SignedAngle(canvasTransform.transform.up, Vector3.ProjectOnPlane(OceanManager.allFishHomes[i].transform.position - canvasTransform.position, canvasTransform.forward), canvasTransform.forward);
                Vector2 elipticalPoint = new Vector2(-width*0.95f / 2 * Mathf.Sin(screenAngle * Mathf.Deg2Rad), height*0.95f / 2 * Mathf.Cos(screenAngle * Mathf.Deg2Rad));

                print(distance);
                float distanceScaled = Mathf.Max(0, -1 / (distance * 2 - 0.6f) + 1);
                distanceScaled = distanceScaled > 1 ? 0 : distanceScaled;

                indicator[i].transform.localRotation = Quaternion.Euler(0, 0, screenAngle);
                indicator[i].rectTransform.sizeDelta = new Vector2(4 * distanceScaled, 4 * distanceScaled);
                Color c = indicator[i].color;
                c.a = distanceScaled;
                indicator[i].color = c;
                indicator[i].rectTransform.anchoredPosition = Vector2.Lerp(Vector2.zero, elipticalPoint, distanceScaled);
            }
        }
    }

    private void SetIndicatorCount(int count) {
       for (int i = 0; i < indicator.Length; ++i) {
            indicator[i].gameObject.SetActive(i < count);
       }
    }
}
