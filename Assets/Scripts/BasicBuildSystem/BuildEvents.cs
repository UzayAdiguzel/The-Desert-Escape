using System;

namespace BasicBuildSystem
{
    public static class BuildEvents
    {
        public static Action<float> OnAddExp;
        public static Action<int> OnChangeBuildLevel;

        public static void AddExp(float exp)
        {
            OnAddExp?.Invoke(exp);
        }
        
        public static void ChangeBuildLevel(int level)
        {
            OnChangeBuildLevel?.Invoke(level);
        }
    }
}
