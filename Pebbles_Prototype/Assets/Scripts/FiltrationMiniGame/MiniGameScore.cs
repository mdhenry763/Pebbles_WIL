using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "MinigameData")]
public class MiniGameScore : ScriptableObject
{
    public int miniGameScore;
    public float timeScore;
    public float leakScore;
}