using UnityEngine;
using System.Collections;

public class SunRotationController : MonoBehaviour
{
    public Transform sun;
    public float rotateSpeed = 2f;

    [Header("Manual X Rotation Values")]
    public float morningX = 20f;
    public float noonX = 90f;
    public float eveningX = 160f;
    [Header("Y Axis Movement")]
    public float yMoveRange = 60f;      // Kitna left-right ghoomega
    public float yMoveSpeed = 0.5f;
    private Coroutine yMoveRoutine;

    private Vector3 defaultPosition;
    private Quaternion defaultRotation;

    private Coroutine rotateRoutine;
    [Header("Sun Cycle Speed")]
    public float sunCycleSpeed = 0.1f;
    [Header("Sun Positions")]
    public Vector3 morningPos = new Vector3(-5f, 2f, 0f);
    public Vector3 noonPos = new Vector3(0f, 6f, 0f);
    public Vector3 eveningPos = new Vector3(5f, 2f, 0f);
    IEnumerator SmoothMoveSun(Vector3 targetPos, Vector3 targetRot)
    {
        Vector3 startPos = sun.position;
        Quaternion startRot = sun.rotation;
        Quaternion endRot = Quaternion.Euler(targetRot);

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * rotateSpeed;
            sun.position = Vector3.Lerp(startPos, targetPos, t);
            sun.rotation = Quaternion.Slerp(startRot, endRot, t);
            yield return null;
        }
    }
    private void Start()
    {
        defaultPosition = sun.position;
        defaultRotation = sun.rotation;
        // StartYMovement();
       // PerformSunCycle();
    }
    public void SetSunRotation(float xValue)
    {
        if (rotateRoutine != null)
            StopCoroutine(rotateRoutine);

        rotateRoutine = StartCoroutine(
     SmoothRotate(new Vector3(xValue, sun.eulerAngles.y, 0)));
    }

    IEnumerator SmoothRotate(Vector3 targetRot)
    {
        Quaternion startRot = sun.rotation;
        Quaternion endRot = Quaternion.Euler(targetRot);

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * rotateSpeed;
            sun.rotation = Quaternion.Slerp(startRot, endRot, t);
            yield return null;
        }
    }

    public void Morning()
    {
        StopAllCoroutines();
        StartCoroutine(
            SmoothMoveSun(
                morningPos,
                new Vector3(8.78f, -26.11f, 0f) // ✅ Correct rotation
            )
        );
    }

    public void Noon()
    {
        StopAllCoroutines();
        StartCoroutine(SmoothMoveSun(noonPos, new Vector3(noonX, 0, 0)));
    }

    public void Evening()
    {
        StartCoroutine(
            SmoothMoveSun(
                morningPos,
                new Vector3(8.78f, 58.11f, 0f) // ✅ Correct rotation
            )
        );
    }
    IEnumerator YAxisMovement()
    {
        float time = 0f;

        while (true)
        {
            time += Time.deltaTime * yMoveSpeed;

            float yRotation = Mathf.Lerp(
                -yMoveRange,
                yMoveRange,
                Mathf.PingPong(time, 1)
            );

            sun.rotation = Quaternion.Euler(
                sun.eulerAngles.x,
                yRotation,
                sun.eulerAngles.z
            );

            yield return null;
        }
    }
    public void StartYMovement()
    {
        if (yMoveRoutine == null)
            yMoveRoutine = StartCoroutine(YAxisMovement());
    }

    public void StopYMovement()
    {
        if (yMoveRoutine != null)
        {
            StopCoroutine(yMoveRoutine);
            yMoveRoutine = null;
        }
    }
    public void ResetSun()
    {
        StopAllCoroutines(); // Stop any movement
        yMoveRoutine = null;
        rotateRoutine = null;

        sun.position = defaultPosition;
        sun.rotation = defaultRotation;
    }
    public void SunRiseFromEastAndSetToWest()
    {
        StartCoroutine(EastToWestRoutine());
    }

    IEnumerator EastToWestRoutine()
    {
        Vector3 eastPos = new Vector3(sun.position.x, sun.position.y - 2f, sun.position.z - 5f);
        Quaternion eastRot = Quaternion.Euler(morningX, -yMoveRange, 0);

        Vector3 westPos = new Vector3(sun.position.x, sun.position.y - 2f, sun.position.z + 5f);
        Quaternion westRot = Quaternion.Euler(eveningX, yMoveRange, 0);

        float t = 0;

        // 🌅 East → 🌞 Noon (Slow Transition)
        while (t < 1)
        {
            t += Time.deltaTime * sunCycleSpeed;
            sun.position = Vector3.Lerp(eastPos, defaultPosition, t);
            sun.rotation = Quaternion.Slerp(eastRot, Quaternion.Euler(noonX, 0, 0), t);
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        t = 0;
        // 🌞 Noon → 🌇 West (Slow Sunset)
        while (t < 1)
        {
            t += Time.deltaTime * sunCycleSpeed;
            sun.position = Vector3.Lerp(defaultPosition, westPos, t);
            sun.rotation = Quaternion.Slerp(Quaternion.Euler(noonX, 0, 0), westRot, t);
            yield return null;
        }
    }
    public void StopCycleAndReset()
    {
        // Stop any ongoing coroutines (cycle, y movement, rotations)
        StopAllCoroutines();
        yMoveRoutine = null;
        rotateRoutine = null;

        // Smoothly restore default position & rotation
        StartCoroutine(SmoothResetToDefault());
    }
    IEnumerator SmoothResetToDefault()
    {
        Vector3 startPos = sun.position;
        Quaternion startRot = sun.rotation;

        float t = 0f;
        float resetSpeed = 1.5f; // adjust if needed

        while (t < 1)
        {
            t += Time.deltaTime * resetSpeed;
            sun.position = Vector3.Lerp(startPos, defaultPosition, t);
            sun.rotation = Quaternion.Slerp(startRot, defaultRotation, t);
            yield return null;
        }

        // Ensure perfect final snap
        sun.position = defaultPosition;
        sun.rotation = defaultRotation;
    }
    public void ResetToInitialTransform()
    {
        // agar koi coroutine chal rahi ho to band
        StopAllCoroutines();
        yMoveRoutine = null;
        rotateRoutine = null;

        // direct reset without smooth movement
        sun.position = defaultPosition;
        sun.rotation = defaultRotation;
    }
    public void PerformSunCycle()
    {
        SunRiseFromEastAndSetToWest();
    }
    public void ResetAndStop()
    {
        StopCycleAndReset();
    }

}
