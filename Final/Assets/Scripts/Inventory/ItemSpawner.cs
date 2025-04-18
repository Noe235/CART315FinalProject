using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public InventoryManager inventoryManager;

    public Item[] itemsToPickUp;

    public void PickUpItem(int id) {
        
        bool result = inventoryManager.AddItem(itemsToPickUp[id]);
        if (result == true) {
            Debug.Log("Picked up " + itemsToPickUp[id].name);
        }
        else {
            Debug.Log("Failed to add " + itemsToPickUp[id].name);
        }
    }
}
