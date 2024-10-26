using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIManager : MonoBehaviour
    
{
    private void OnGUI()
    {
        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.fontSize = 42;

        //All Items
        GUILayout.BeginHorizontal();
        GUILayout.Label("All Items: ", style);
        string msg1 = ItemsManager.instance.itemsAll.ToString();
        GUILayout.TextField(msg1, style);
        GUILayout.EndHorizontal();

        //PickHeart
        GUILayout.BeginHorizontal();
        GUILayout.Label("PickHeart: ", style);
        string msg2 = ItemsManager.instance.itemPH.ToString();
        GUILayout.TextField(msg2 + "/5", style);
        GUILayout.EndHorizontal();

        //CoinSpine
        GUILayout.BeginHorizontal();
        GUILayout.Label("CoinSpine: ", style);
        string msg3 = ItemsManager.instance.itemCSp.ToString();
        GUILayout.TextField(msg3 + "/6", style);
        GUILayout.EndHorizontal();

        //CoinShine
        GUILayout.BeginHorizontal();
        GUILayout.Label("CoinShine: ", style);
        string msg4 = ItemsManager.instance.itemCSh.ToString();
        GUILayout.TextField(msg4 + "/10", style);
        GUILayout.EndHorizontal();
    }
}
