using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

    public Text itemName;
    public GameObject InfoPopUp;
    GameObject info;
    Item item;

    public void AddItem(Item newItem)
    {
   
            item = newItem;
            itemName.text = newItem.Name;
        

    }



    public void ClearSlot()
    {
        Destroy(gameObject);
    }

    public Item GetItem()
    {
        return item;
    }

    public void UseItem()
    {
        InventoryController inventory = InventoryController.instance;
        if (inventory.GetSellMode())
        {
            inventory.GetCoin((item.Value)/2);
            Destroy(gameObject);
        }
        else
        {
            item.UseItem(item, PlayerWeaponController.instance);

        }
    }
    public void ChooseItem()
    {
        item.ChooseItem(item, InventoryController.instance);
    }
    public void InformationHover()
    {

        info = Instantiate(InfoPopUp, Vector3.zero,InfoPopUp.transform.rotation, GameObject.FindGameObjectWithTag("UI").transform);
        Vector3 location = Input.mousePosition;


        info.transform.position = location;
        info.GetComponent<InfoPopUp>().SetInfo(item.Name,item.Stats[0].GetCalculatedValue().ToString(),item.Stats[1].GetCalculatedValue().ToString(), item.Stats[2].GetCalculatedValue().ToString(), item.Stats[3].GetCalculatedValue().ToString(),item.AOE.Length.ToString(),( item.OnHitEffect.Name.Length >0 ? item.OnHitEffect.Name : "No Effect"));
    }
    public void BuyItem()
    {
        if (InventoryController.instance.ReturnCoin() > item.Value)
        {
            item.ChooseItem(item, InventoryController.instance);
            InventoryController.instance.LoseCoin(item.Value);
            Destroy(gameObject);
        }
    }
    public void InformationExit()
    {
        Destroy(info);
    }

}
