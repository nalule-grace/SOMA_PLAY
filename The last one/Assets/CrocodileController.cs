using UnityEngine;

public class CrocodileController : MonoBehaviour
{
    public float scaleAmount = 0.01f;
    public float scaleSpeed = 1f;

    private Vector3 startScale;

    void Start()
    {
        startScale = transform.localScale;
    }

    void Update()
    {
        float scaleY = Mathf.Sin(Time.time * scaleSpeed) * scaleAmount;
        transform.localScale = new Vector3(startScale.x, startScale.y + scaleY, startScale.z);
    }
}
