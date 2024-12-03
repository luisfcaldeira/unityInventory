using UnityEngine;

public class Core : MonoBehaviour
{
    public static Core Instance;
    public Inventory inventory;
     
    private void Awake()
    {
        Instance = this;
    }
}
