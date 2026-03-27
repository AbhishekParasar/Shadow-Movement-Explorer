using UnityEngine;

[CreateAssetMenu(fileName = "New Molecule", menuName = "Chemistry/Molecule")]
public class MoleculeData : ScriptableObject
{
    public string moleculeName;
    public float solidTemperature;
    public float liquidTemperature;
    public float gasTemperature;
}
