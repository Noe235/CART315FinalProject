using UnityEngine;

public class ItemsToPickUp : MonoBehaviour {
  
   // easym get the collider, set the pick up range and then run the inventory manager line
   
   public float PickUpRadius = 1f;
   public Item item;

   private SphereCollider myCollider;
  

   private void Awake() {
      myCollider = GetComponent<SphereCollider>();
      myCollider.isTrigger = true;
      myCollider.radius = PickUpRadius;
   }


   private void OnTriggerEnter(Collider other) {
      if (other.tag == "Player") {
         bool canAdd = InventoryManager.instance.AddItem(item);
         Destroy(this.gameObject);
         GameMessageUI.Instance.Show("Added item to inventory!", 1f);
         Debug.Log("added" + item.name);
      }
     
   }
}
