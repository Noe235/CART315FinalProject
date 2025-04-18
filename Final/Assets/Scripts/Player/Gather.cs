using UnityEngine;

public class Gather : MonoBehaviour
{
    private Item item;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void onTriggerEnter(Collider other) {
        if (other.CompareTag("Player") && other.tag == "Item") {
           bool canAdd = InventoryManager.instance.AddItem(item);
           
        }
    }
}
