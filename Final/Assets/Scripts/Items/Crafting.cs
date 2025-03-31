using System;
using UnityEngine;
using UnityEngine.UI;

public class Crafting : MonoBehaviour {
   [SerializeField] private InventorySlot[] inventorySlots;
   [SerializeField] private Item fireItem;
   [SerializeField] private Item iceItem;
   [SerializeField] private GameObject fireCraft;
   [SerializeField] private GameObject iceCraft;


   private void Start() {
      fireCraft.GetComponent<Button>().interactable = false;
      iceCraft.GetComponent<Button>().interactable = false;
   }

   void Update() {
      if (CheckUpgradeFireSpell()) {
         fireCraft.GetComponent<Button>().interactable = true;
      }

      if (CheckUpgradeIceSpell()) {
         iceCraft.GetComponent<Button>().interactable = true;
      }
   }

   public bool CheckUpgradeFireSpell() {
      for (int i = 0; i < inventorySlots.Length; i++) {
         InventorySlot slot = inventorySlots[i];
         InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
         if (itemInSlot != null && itemInSlot.item == fireItem && itemInSlot.count >= 10) {
            return true;
         }
      }
      return false;
   }

   public void UpgradeFireSpell() {
      fireCraft.GetComponent<Button>().interactable = false;
      FPSShooter.spellLevelFire++;
      for (int i = 0; i < inventorySlots.Length; i++) {
         InventorySlot slot = inventorySlots[i];
         InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
         if (itemInSlot != null && itemInSlot.item == fireItem) {
            itemInSlot.count -= 10;
            itemInSlot.RefreshCount();
         }
      }
      Debug.Log(FPSShooter.spellLevelFire);
   }
   
   public bool CheckUpgradeIceSpell() {
      for (int i = 0; i < inventorySlots.Length; i++) {
         InventorySlot slot = inventorySlots[i];
         InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
         if (itemInSlot != null && itemInSlot.item == iceItem && itemInSlot.count >= 10) {
            return true;
         }
      }
      return false;
   }

   public void UpgradeIceSpell() {
      iceCraft.GetComponent<Button>().interactable = false;
      FPSShooter.spellLevelFire++;
      for (int i = 0; i < inventorySlots.Length; i++) {
         InventorySlot slot = inventorySlots[i];
         InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
         if (itemInSlot != null && itemInSlot.item == iceItem) {
            itemInSlot.count -= 10;
            itemInSlot.RefreshCount();
         }
      }
      Debug.Log(FPSShooter.spellLevelIce);
   }
}
