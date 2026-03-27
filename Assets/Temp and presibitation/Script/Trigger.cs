using UnityEngine;
using System.Collections.Generic;

public class TriggerGate : MonoBehaviour
{
    public string targetTag = "Player";

    public List<GameObject> objectsToHide;

    public List<GameObject> objectsToShow;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("1");
        if (other.CompareTag(targetTag))
        {
            Debug.Log("2");
            SetVisibility(objectsToHide, false);

            SetVisibility(objectsToShow, true);
        }
    }

    private void SetVisibility(List<GameObject> objectList, bool isActive)
    {
        foreach (GameObject obj in objectList)
        {
            if (obj != null)
            {
                obj.SetActive(isActive);
            }
        }
    }
}