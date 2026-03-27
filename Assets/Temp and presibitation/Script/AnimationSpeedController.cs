using UnityEngine;
using UnityEngine.UI;

public class LegacyAnimationSpeedController : MonoBehaviour
{
    private Animation legacyAnimation;

    // The name of the animation clip to control
    public string clipName = "DefaultClipName";

    // The UI Slider object from the Hierarchy
    public Slider speedSlider;

    void Start()
    {
        // --- DEBUG 1: Component Check ---
        legacyAnimation = GetComponent<Animation>();
        if (legacyAnimation == null)
        {
            Debug.LogError("DEBUG: FAILED! Legacy Animation component not found on this object.", this);
            return;
        }
        Debug.Log("DEBUG: SUCCESS! Legacy Animation component found.", this);


        // --- DEBUG 2: Slider Check ---
        if (speedSlider == null)
        {
            Debug.LogError("DEBUG: FAILED! Slider component not assigned in the Inspector.", this);
            return;
        }
        Debug.Log("DEBUG: SUCCESS! Slider reference connected in the Inspector.", this);


        // --- DEBUG 3: Clip Name Check ---
        if (legacyAnimation.GetClip(clipName) == null)
        {
            Debug.LogError($"DEBUG: FAILED! Animation clip named '{clipName}' was NOT found on the Animation component. Check the name for typos.", this);
            return;
        }
        Debug.Log($"DEBUG: SUCCESS! Animation clip '{clipName}' found.", this);


        // --- DEBUG 4: Listener Setup ---
        speedSlider.onValueChanged.AddListener(AdjustAnimationSpeed);
        Debug.Log("DEBUG: SUCCESS! Slider value change listener set up.", this);

        // Set initial speed
        AdjustAnimationSpeed(speedSlider.value);
    }

    public void AdjustAnimationSpeed(float newSpeed)
    {
        // --- DEBUG 5: Function Call Check ---
        Debug.Log($"DEBUG: AdjustAnimationSpeed called with new speed: {newSpeed}", this);

        if (legacyAnimation != null)
        {
            AnimationState state = legacyAnimation[clipName];

            if (state != null)
            {
                // --- DEBUG 6: Speed Assignment Check ---
                state.speed = newSpeed;
                Debug.Log($"DEBUG: Clip '{clipName}' speed set to: {state.speed}", this);
            }
            else
            {
                Debug.LogError($"DEBUG: FAILED! Could not get AnimationState for clip '{clipName}'. Is the clip playing?", this);
            }
        }
    }
}