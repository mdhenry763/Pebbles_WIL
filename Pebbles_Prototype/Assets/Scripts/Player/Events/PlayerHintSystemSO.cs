using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/Hint System", fileName = "New Player Hint System")]
public class PlayerHintSystemSO : ScriptableObject
{
    public HintData[] hintData;

    private int hintIndex;

    public string GetHint()
    {
        if (hintIndex > hintData.Length - 1)
        {
            return "";
        }
        var hint = hintData.FirstOrDefault(hint => hint.hintID == hintIndex);

        if (hint.hintDialogue == null)
        {
            Debug.LogError("Hint not found");
            return "";
        }

        hintIndex++;
        return hint.hintDialogue;
    }
}

[Serializable]
public struct HintData
{
    public int hintID;
    [Header("Hint Dialogue")] [TextArea] public string hintDialogue;
}