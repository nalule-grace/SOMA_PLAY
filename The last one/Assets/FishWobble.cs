using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishWobble : MonoBehaviour
{
    public float swaySpeed = 3f;
    public float swayAmount = 5f;

    private Quaternion initialRotation;

    void Start()
    {
        initialRotation = transform.rotation;
    }

    void Update()
    {
        float sway = Mathf.Sin(Time.time * swaySpeed) * swayAmount;
        transform.rotation = initialRotation * Quaternion.Euler(0f, sway, 0f);
    }
}
