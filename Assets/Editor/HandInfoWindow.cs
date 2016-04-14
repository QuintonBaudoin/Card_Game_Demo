using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using Character;






public class HandInfoWindow : EditorWindow
{

    private static List<Player> Players = new List<Player>();

    private static EditorWindow window;

    [MenuItem("QuintonTools/ViewHands")]
    public static void OpenWindow()
    {
        window = GetWindow(typeof(HandInfoWindow));




    }


    static List<Vector2> scrolls = new List<Vector2>();

    void InitWindowUI()
    {
        if (scrolls.Count < Players.Count)
            scrolls.Clear();
        else
            return;

        for (int i = 0; i < Players.Count; i++)
        {
            Vector2 scrollposition = Vector2.zero;

            scrollposition = GUILayout.BeginScrollView(scrollposition, EditorStyles.objectField);
            GUILayout.EndScrollView();
            scrolls.Add(scrollposition);
        }

    }

    void UpdateUI()
    {
        if (Players.Count <= 0)
        {
            window.Close();
            throw new System.Exception("Missing Players");

        }
        GUILayout.Label("Player Info", EditorStyles.largeLabel);

        for (int i = 0; i < Players.Count; i++)
        {
            //    float yOffset = i * 10;

            //    Rect areaSize = new Rect(x , y+ yOffset, width, height);
            scrolls[i] = GUILayout.BeginScrollView(scrolls[i], EditorStyles.objectField);
            GUILayout.Label(Players[i].name, EditorStyles.boldLabel);
            GUILayout.Label("Hand", EditorStyles.boldLabel);

           
            List<ICard> hand = Players[i].hand;
            MonoBehaviour.print(hand.Count);
            GUILayout.Label("Mystery",EditorStyles.boldLabel);
            foreach (ICard c in hand)
            {
                if(c.ToString() == "MysteryCard")
                {
                    MysteryCard m = c as MysteryCard;

                    GUILayout.Label(m.Name + " : " + m.Description + " | " + "Power: " + m.Power + " | " + "Reward: " + m.Reward, EditorStyles.label);

                }


            }
            GUILayout.Label("Treasure", EditorStyles.boldLabel);

            foreach (ICard c in hand)
            {
                
                if (c.ToString() == "TreasureCard")
                {
                    TreasureCard t = c as TreasureCard;

                    GUILayout.Label(t.Name + " : " + t.Description + " | " + "Power: " + t.Power + " | " + "Gold: " + t.Gold);
                }

            }

           
            GUILayout.Label("-------------------------------------------------------------------------", EditorStyles.boldLabel);

            List<GameObject> cards = Players[i].cards;

            GUILayout.Label("Mystery", EditorStyles.boldLabel);
            foreach (GameObject c in cards)
            {
                if (c.GetComponent<MysteryCardMono>() != null)
                {
                    MysteryCardMono m = c.GetComponent<MysteryCardMono>();

                    GUILayout.Label(m.Name + " : " + m.Description + " | " + "Power: " + m.Power + " | " + "Reward: " + m.Reward, EditorStyles.label);

                }


            }

            GUILayout.Label("Treasure", EditorStyles.boldLabel);
            foreach (GameObject c in cards)
            {

                if (c.GetComponent<TreasureCardMono>() != null)
                {
                    TreasureCardMono t = c.GetComponent<TreasureCardMono>();

                    GUILayout.Label(t.Name + " : " + t.Description + " | " + "Power: " + t.Power + " | " + "Gold: " + t.Gold);
                }
            }




            GUILayout.EndScrollView();
        }

    }

    void OnGUI()
    {
        Players.Clear();

        foreach (Player plr in FindObjectsOfType<Player>())
            Players.Add(plr);
        InitWindowUI();

        window = GetWindow(typeof(HandInfoWindow));


        UpdateUI();



    }
}
