using UnityEngine;

public class Stages : MonoBehaviour
{

    public GameObject currentStageGO;
    public GameObject nextStageGO;


    public void NextButton()
    {
        currentStageGO.SetActive(false);
        if (nextStageGO != null)
        {
            nextStageGO.SetActive(true);
        }
    }
}
