using UnityEngine;

[DefaultExecutionOrder(-1)]
public class ObjectParent : MonoBehaviour
{
    public static ObjectParent Instance;
    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
}