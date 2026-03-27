using UnityEngine;
using TMPro;

public class GlobeRotation : MonoBehaviour
{
    [Header("Arrow Settings")]
    public GameObject rotationArrow;
    public float rotationSpeed = 120f;

    [SerializeField] bool animateArrow;
    public bool isEarthSpinComplete;
    public Transform sun;
    public Transform earth;
    public float dotThreshold = 0.0f;

    [SerializeField] Camera cam;
    [SerializeField] float oneDayTime;

    public TextMeshProUGUI dayStatusInfoText;

    [SerializeField] GameObject statusInfoTextParentGO;
    [SerializeField] string dayExperiences = "Click the part is day time view.";
    [SerializeField] string nightExperiences = "Click the part is night time view";

    [SerializeField] GameObject stage1_GO;
    [SerializeField] GameObject stage2_GO;

    [SerializeField] GameObject Day_NightArrowsGO;

    [SerializeField] AudioClip audioClipDay;
    [SerializeField] AudioClip audioClipNight;
    [SerializeField] AudioSource audioSource;

    void Start()
    {
        RotatingSpin();
        //stage1_GO.SetActive(true);
    }

    void Update()
    {

        earth.Rotate(-Vector3.up * rotationSpeed * Time.deltaTime, Space.World);

    }


   public void OnMouseDown()
    {
        if (!animateArrow)
            ShowAndAnimateArrow();
    }
    public void RotatingSpin()
    {
        if (!animateArrow)
           ShowAndAnimateArrow();
    }

    void ShowAndAnimateArrow()
    {
        animateArrow = true;
    }

    // Optional: stop animation
    public void StopArrow()
    {
        animateArrow = false;
    }
   

    public bool IsDaySide(Vector3 hitPoint)
    {
        Vector3 normal = (hitPoint - earth.position).normalized;
        Vector3 sunDir = (sun.position - earth.position).normalized;

        return Vector3.Dot(normal, sunDir) > dotThreshold;
    }

    void AuidoPlay(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

}
