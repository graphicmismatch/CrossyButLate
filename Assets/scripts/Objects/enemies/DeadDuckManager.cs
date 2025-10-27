using UnityEngine;

public class DeadDuckManager : MonoBehaviour
{

    public RectTransform deadDuckBoundl;
    public RectTransform deadDuckBoundr;
    public DeadDuck deadDuck;
    public static DeadDuckManager instance;
    public Vector2 bounds;
    private void Start()
    {
        instance = this;
        bounds = new Vector2(deadDuckBoundl.position.x,deadDuckBoundr.position.x);
    }
    public static void duckKilled()
    {
        Instantiate(instance.deadDuck, instance.transform).init(instance.bounds);
    }
}
