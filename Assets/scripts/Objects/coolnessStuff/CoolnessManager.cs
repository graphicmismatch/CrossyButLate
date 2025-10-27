using UnityEngine;

public class CoolnessManager : MonoBehaviour
{
    public int coolnessValue;

    public void updateCoolnessValue(int delta) {
        coolnessValue += delta;
    }

    public void resetCoolness() { 
        coolnessValue = 0;
    }
}
