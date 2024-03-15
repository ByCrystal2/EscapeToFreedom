using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    List<ItemData> _currentItems = new List<ItemData>();
    public bool _isBusy { get; set; }
    public List<ItemData> GetCurrentItems()
    {
        return _currentItems;
    }
    public List<ItemData> GetDesiredItemTypeCurrentItems(ItemType itemType)
    {
        return _currentItems.Where(x=> x.ItemType == itemType).ToList();
    }
    public void AddItemInInventory(ItemData itemData)
    {
        _currentItems.Add(itemData);
    }
    public void AddRandomItemInInventory()
    {
        AddItemInInventory(ItemManager.instance.GetRandomItem());
    }
    public void RemoveItemInInventory(ItemData itemData)
    {
        _currentItems.Remove(itemData);
    }
    [CustomEditor(typeof(PlayerInventory))] // YourScript yerine eklemek istedi�iniz beti�i belirtin
    public class YourScriptEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            PlayerInventory script = (PlayerInventory)target;

            if (GUILayout.Button("AddItem"))
            {
                // Butona t�kland���nda yap�lacak i�lemler burada ger�ekle�tirilir
                script.AddRandomItemInInventory(); // �rne�in, belirli bir metodu �a��rabilirsiniz
            }
        }
    }
}
