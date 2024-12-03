using UnityEngine;

public class Player : MonoBehaviour
{

    public Item itemA;
    public Item itemB;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.P))
        {
            Core.Instance.inventory.GetItem(itemA, 1);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            Core.Instance.inventory.GetItem(itemB, 1);
        }
    }
}
