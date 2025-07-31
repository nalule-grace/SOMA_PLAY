using UnityEngine;

public class PlantFaceLight : MonoBehaviour
{
    public float swaySpeed = 1f;
    public float swayAmount = 5f;

    private Quaternion startRotation;

    void Start()
    {
        startRotation = transform.rotation;
    }

    void Update()
    {
        float sway = Mathf.Sin(Time.time * swaySpeed) * swayAmount;
        transform.rotation = startRotation * Quaternion.Euler(0f, sway, 0f);
    }
}
