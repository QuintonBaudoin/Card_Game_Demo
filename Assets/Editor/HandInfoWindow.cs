using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using Character;






public class HandInfoWindow : EditorWindow
{
    /// <summary>
    /// List of all the players (Updated each OnGui)
    /// </summary>
    private static List<Player> Players = new List<Player>();
   
    /// <summary>
    /// Initiates the Editor Window
    /// </summary>
    [MenuItem("QuintonTools/ViewHands")]
    public static void OpenWindow()
    {
        GetWindow(typeof(HandInfoWindow));
        
    }

    /// <summary>
    /// used to keep track of the previous position of each ScrollView's in the window
    /// </summary>
    static List<Vector2> scrolls = new List<Vector2>();

    /// <summary>
    /// Initializes the UI of the window
    /// </summary>
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

    /// <summary>
    /// Updates UI
    /// </summary>
    void UpdateUI()
    {
        GUILayout.Label("Player Info", EditorStyles.largeLabel);
        if (Players.Count <= 0)
        {
            GUILayout.Label("No Players found", EditorStyles.largeLabel);
            return;

        }


        for (int i = 0; i < Players.Count; i++)
        {
            //    float yOffset = i * 10;

            //    Rect areaSize = new Rect(x , y+ yOffset, width, height);
            scrolls[i] = GUILayout.BeginScrollView(scrolls[i], EditorStyles.objectField);
            GUILayout.Label(Players[i].name, EditorStyles.boldLabel);
            GUILayout.Label("Hand", EditorStyles.boldLabel);


            List<ICard> hand = Players[i].hand;

            GUILayout.Label("Mystery", EditorStyles.boldLabel);
            foreach (ICard c in hand)
            {
                MysteryCard m = c as MysteryCard;
                if (m != null)
                    GUILayout.Label(m.Name + " : " + m.Description + " | " + "Power: " + m.Power + " | " + "Reward: " + m.Reward, EditorStyles.label);
            }
            GUILayout.Label("Treasure", EditorStyles.boldLabel);

            foreach (ICard c in hand)
            {
                TreasureCard t = c as TreasureCard;
                if (t != null)
                    GUILayout.Label(t.Name + " : " + t.Description + " | " + "Power: " + t.Power + " | " + "Gold: " + t.Gold);
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
    /// <summary>
    /// Unity's Update for the window and its GUI
    /// </summary>
    void OnGUI()
    {
        Players.Clear();

        foreach (Player plr in FindObjectsOfType<Player>())
            Players.Add(plr);
        InitWindowUI();

       // window = GetWindow(typeof(HandInfoWindow));


        UpdateUI();



    }
}
