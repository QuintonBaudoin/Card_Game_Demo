using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using System;

namespace Quinton
{
    /// <summary>
    /// Information of Resolution:
    /// 
    /// Messages:
    /// 
    /// GoodDudesWin -> If GoodDudes have higher power
    /// BadDudesWin  -> If BadDudes have higher power or If both have same power

    /// 
    /// </summary>
    public class ResolutionInfo
    {

        public string Message;

        public ResolutionInfo(string msg)
        {
            Message = msg;

        }

    }


    public class FieldInfo
    {
        public List<GameObject> GoodDudes;
        public List<GameObject> BadDudes;

        string LastMessage;


        public FieldInfo(ref List<GameObject> gdudes,ref List<GameObject> bdudes, string msg)
        {
            GoodDudes = gdudes;
            BadDudes = bdudes;

            LastMessage = msg;

        }

    }


    /// <summary>
    /// Field Event. Structured as Singleton
    /// </summary>
    public class FieldEvent : UnityEvent<ResolutionInfo>
    {

        static private FieldEvent _instance;

        static public FieldEvent instance
        {
            get
            {
                if (_instance == null)
                    _instance = new FieldEvent();

                return _instance;
            }
        }


        private FieldEvent()
        { }

    }

    /// <summary>
    /// Handles "Combat" between GoodDudes and BadDudes 
    /// (Both are List<GameObject>
    /// </summary>
    /// <body>
    /// When Resolve() method is called the lists of GoodDudes 
    /// and BadDudes will be compared
    /// The group with the highest "Power" will "win".
    /// If resalting in tie the BadDudes will "win"
    /// 
    /// To add to a list use the Add----Dudes to pass in a list
    /// and Add----Dude to pass in a single GameObjects.
    /// 
    /// Note: Will only compare those that are of the 
    /// MysteryCardMono type.
    /// 
    /// To collect information of the field use GatherFieldInfo() 
    /// this will return a FieldInfo object that contains
    /// the GoodDudes and BadDudes. As well ass the last message 
    /// sent threw the event system
    /// </body> 
    public class FieldHandler : MonoBehaviour
    {

        static private FieldHandler _instance;
             
        public static FieldHandler instance
        {
            get
            {
                if (_instance == null)
                    _instance = FindObjectOfType<FieldHandler>();

                return _instance;
            }
        }

        string m_LastMessage = "No Resolutions Made";

        public FieldEvent fieldEvent = FieldEvent.instance;

        List<GameObject> GoodDudes = new List<GameObject>();
        List<GameObject> BadDudes = new List<GameObject>();

        public void AddGoodDudes(List<GameObject> gd)
        {
            foreach (GameObject g in gd)
                GoodDudes.Add(g);
        }
        public void AddBadDudes(List<GameObject> bd)
        {
            foreach (GameObject b in bd)
                BadDudes.Add(b);
        }

        public void AddGoodDude(GameObject gd)
        {
            GoodDudes.Add(gd);
        }
        public void AddBadDude(GameObject bd)
        {
            BadDudes.Add(bd);
        }

        public void ClearDudes()
        {
            BadDudes.Clear();
            GoodDudes.Clear();
        }
        public void Resolve()
        {
            int GoodDudesPower = 0;
            int BadDudesPower = 0;
            string resolveMessage;


            if (GoodDudes.Count <= 0 || BadDudes.Count <= 0)
                throw new Exception("Missing Cards: " + "GoodDudesCount: " + GoodDudes.Count + " BadDudesCount: " + BadDudes.Count);
               

            foreach (GameObject go in GoodDudes)
                if (go.GetComponent<MysteryCardMono>() != null)
                    GoodDudesPower += go.GetComponent<MysteryCardMono>().Power;
            foreach (GameObject go in BadDudes)
                if (go.GetComponent<MysteryCardMono>() != null)
                    BadDudesPower += go.GetComponent<MysteryCardMono>().Power;



            

            if (GoodDudesPower > BadDudesPower)
                resolveMessage = "GoodDudesWin";
            else
                resolveMessage = "BadDudesWin";
            m_LastMessage = resolveMessage;
            fieldEvent.Invoke(new ResolutionInfo(resolveMessage));

        }

        public FieldInfo GatherFieldInfo()
        {
            return new FieldInfo(ref GoodDudes, ref BadDudes, m_LastMessage);
        }

    }
}