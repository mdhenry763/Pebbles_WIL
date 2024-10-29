using System;

public static class CrossSceneEvents
{
    public static event Action onMiniGameFinished;

    public static event Action OnConnectionChange;
    public static event Action OnPipeRotated;

    public static void FirePipeRotated()
    {
        OnPipeRotated?.Invoke();
    }

    public static void FireOnConnectionChange()
    {
        OnConnectionChange?.Invoke();
    }

    public static void FireOnMiniGameFinished()
    {
        onMiniGameFinished?.Invoke();
    }
}