using UnityEngine;

public class InventoryManager : MonoBehaviour
{
   public static InventoryManager instance;
   
   public int maxStackedItems = 12;
   public InventorySlot[] inventorySlots;
   public GameObject inventoryItemPrefab;

   private void Awake() {
      instance = this;
   }
   public bool AddItem(Item item) {
      
      
      //check if any slot has the same type of item with count lower than max stack
      for (int i = 0; i < inventorySlots.Length; i++) {
         InventorySlot slot = inventorySlots[i];
         InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
         if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < maxStackedItems && itemInSlot.item.stackable == true) {
            itemInSlot.count++;
            itemInSlot.RefreshCount();
            return true;
         }
      }

      //for nay empty slots
      for (int i = 0; i < inventorySlots.Length; i++) {
         InventorySlot slot = inventorySlots[i];
         InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
         if (itemInSlot == null) {
            SpawnNewItem(item,slot);
            return true;
         }
      }
      return false;
   }

   public void SpawnNewItem(Item item, InventorySlot slot) {
      GameObject newItemGo = Instantiate(inventoryItemPrefab,slot.transform);
      InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
      inventoryItem.InitializeItem(item);
   }
   
}
