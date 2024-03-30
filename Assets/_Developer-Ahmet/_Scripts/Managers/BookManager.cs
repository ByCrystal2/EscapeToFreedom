using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BookManager : MonoBehaviour
{
    public static BookManager instance { get; private set; }
    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    public int TotalSecurityKeyIsTenInPlayerInventory()
    {
        List<Collectable> securityKeys = PlayerManager.instance.playerInventory.GetAllCollectedItems().Where(x => x.GetCollectType() == CollectType.Key).ToList();
        int count = securityKeys.Count;
        return count;
    }
}
