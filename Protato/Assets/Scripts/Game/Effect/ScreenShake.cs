using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public float shakeDuration = 0.5f;    // Duration of the shake effect
    public float shakeIntensity = 0.1f;   // Intensity of the shake effect

    private float currentShakeDuration;   // Current duration of the shake effect
    private float currentShakeIntensity;  // Current intensity of the shake effect

    private Vector3 originalPosition;     // Original position of the camera or object

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void Update()
    {
        if (currentShakeDuration > 0)
        {
            // Generate a random offset in X and Y directions based on shake intensity
            Vector3 randomOffset = Random.insideUnitSphere * currentShakeIntensity;
            randomOffset.z = 0; // Set the Z offset to 0 for 2D shaking

            // Apply the offset to the camera or object position
            transform.position = originalPosition + randomOffset;

            currentShakeDuration -= Time.deltaTime;
        }
        else
        {
            // Shake effect is over, reset the position to the original
            transform.position = originalPosition;
        }
    }

    public void Shake()
    {
        currentShakeDuration = shakeDuration;
        currentShakeIntensity = shakeIntensity;
    }
}
