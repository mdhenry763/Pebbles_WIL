using System;

public static class CrossSceneEvents
{
    public static event Action onMiniGameFinished;

    public static void FireOnMiniGameFinished()
    {
        onMiniGameFinished?.Invoke();
    }
}