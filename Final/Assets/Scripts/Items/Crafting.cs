using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Crafting : MonoBehaviour {
   [SerializeField] private InventorySlot[] inventorySlots;
   [SerializeField] private Item fireItem;
   [SerializeField] private Item iceItem;
   [SerializeField] private GameObject fireCraft;
   [SerializeField] private GameObject iceCraft;
   public TextMeshProUGUI firetext;
   public TextMeshProUGUI icetext;


   private void Start() {
      fireCraft.GetComponent<Button>().interactable = false;
      iceCraft.GetComponent<Button>().interactable = false;
   }

   void Update() {
      if (FPSShooter.spellLevelFire == 3) {
         fireCraft.GetComponentInChildren<TextMeshProUGUI>().SetText( "Fire Spell Max Upgrade");
      }
      if (FPSShooter.spellLevelIce == 3) {
         iceCraft.GetComponentInChildren<TextMeshProUGUI>().SetText("Ice Spell Max Upgrade");

      }
      if (CheckUpgradeFireSpell() && FPSShooter.spellLevelFire <= 2 ) {
         fireCraft.GetComponent<Button>().interactable = true;
      }

      if (CheckUpgradeIceSpell() && FPSShooter.spellLevelIce <= 2) {
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
      FPSShooter.spellLevelIce++;
      for (int i = 0; i < inventorySlots.Length; i++) {
         InventorySlot slot = inventorySlots[i];
         InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
         if (itemInSlot != null && itemInSlot.item == iceItem) {
            itemInSlot.count -= 10;
            itemInSlot.RefreshCount();
         }
      }
   }
}
