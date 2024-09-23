using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    public Vector3 rotation = new Vector3(0, 90, 0);

    public void RotateObject()
    {
        transform.Rotate(rotation);
    }
}