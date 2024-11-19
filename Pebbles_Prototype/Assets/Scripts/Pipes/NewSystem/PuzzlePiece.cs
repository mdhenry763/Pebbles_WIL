using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    public SFXManager sfxManager;
    
    public Vector3 rotation = new Vector3(0, 90, 0);

    public void RotateObject()
    {
        transform.Rotate(rotation);
        CrossSceneEvents.FirePipeRotated();

        if (sfxManager == null) return;

        sfxManager.PlayPuzzleClip();
    }
}