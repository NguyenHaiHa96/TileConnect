using UnityEngine;

public static class Logger 
{
    public const string PREFIX = "";

    public static void LogMsg(object msg)
    {
#if UNITY_EDITOR
        Debug.Log($"{PREFIX}{msg}");
#endif
    }

    public static void LogMsg(this object obj, object msg)
    {
#if UNITY_EDITOR
        Debug.Log($"[{PREFIX} {obj}]: {msg}");
#endif
    }

    public static void LogWarning(this object obj, object msg)
    {
#if UNITY_EDITOR
        Debug.LogWarning($"[{PREFIX} {obj}]: {msg}");
#endif
    }

    public static void DebugLogMsg(this Debug obj, object msg)
    {
#if UNITY_EDITOR
        Debug.Log($"[{PREFIX} {obj.ToString()}]: {msg}");
#endif
    }
}
