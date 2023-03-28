using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBG : MonoBehaviour
{
    private float length;
    private float startPos;

    public GameObject cam;
    public float parallaxEffect;

    private Vector3 startingPosition;

    void Start() {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update() {
        float dist = cam.transform.position.x * parallaxEffect;
        float temp = cam.transform.position.x * (1 - parallaxEffect);

        transform.position = new Vector3(startPos + dist, startingPosition.y, transform.position.z);

        if(temp > startPos + length) {
            startPos += length;
        } else if (temp < startPos - length) {
            startPos -= length;
        }
    }
}