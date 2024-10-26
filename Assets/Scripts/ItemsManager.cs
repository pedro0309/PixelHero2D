using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class ItemsManager : MonoBehaviour
{
    public static ItemsManager instance; //Agregamos la instancia de esta Clase (ItemsManager)
    public int itemsAll,itemCSh, itemCSp, itemPH = 0;

    void Awake()
    {
        instance = this; //Apenas se crea esta instancia en el Awake. Con esto tenemos garantizado el inicio de la instancia desde que la fase Inicializacion (Initialization)
    }

    public void AddItemCoinSpin()
    {
        AddItems();
        itemCSp++;
        print("CoinSpin");
    }

    public void AddItemCoinShine()
    {
        AddItems();
        itemCSh++;
        print("CoinShine");
    }

    public void AddItemPickHeart()
    {
        AddItems();
        itemPH++;
        print("PickHeart");
    }

    public void AddItems()
    {
        itemsAll++;
    }
}
