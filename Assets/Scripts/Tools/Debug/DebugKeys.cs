using UnityEngine;

public static class DebugKeys
{
#if UNITY_EDITOR
    public const bool EnableDebug =  true;
    #else
    public const bool EnableDebug =  false;
#endif
    
    
    public static readonly Color TitleColor = new Color(0.9607843f, 0.7568628f, 0.2235294f);
    public const string Title = "Antricode";
}