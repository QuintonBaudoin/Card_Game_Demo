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
    /// </summary>
    public class ResolutionInfo
    {
        /// <summary>
        /// Message of Resolution
        /// </summary>
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


        public FieldInfo(ref List<GameObject> gdudes, ref List<GameObject> bdudes, string msg)
        {
            GoodDudes = gdudes;
            BadDudes = bdudes;

            LastMessage = msg;

        }

    }


    /// <summary>
    /// Class for Handling Field combats
    /// </summary>
    public class FieldHandler : MonoBehaviour
    {

        static private FieldHandler _instance;
        /// <summary>
        /// instance of field handler from first exsiting one in hierarchy 
        /// </summary>
        public static FieldHandler instance
        {
            get
            {
                if (_instance == null)
                    _instance = FindObjectOfType<FieldHandler>();

                return _instance;
            }
        }
        /// <summary>
        /// Last Message sent threw the Resolution info
        /// </summary>
        string m_LastMessage = "No Resolutions Made";

        /// <summary>
        /// List of GoodDudes
        /// </summary>
        List<GameObject> GoodDudes = new List<GameObject>();
        /// <summary>
        /// List of BadDudes
        /// </summary>
        List<GameObject> BadDudes = new List<GameObject>();

        /// <summary>
        /// Adds to the Good Dudes
        /// </summary>
        /// <param name="gd">GameObject list to be added to list</param>
        public void AddGoodDudes(List<GameObject> gd)
        {
            foreach (GameObject g in gd)
                GoodDudes.Add(g);
        }
        /// <summary>
        /// Adds to the Bad Dudes
        /// </summary>
        /// <param name="gd">GameObject to be added to list</param>
        public void AddBadDudes(List<GameObject> bd)
        {
            foreach (GameObject b in bd)
                BadDudes.Add(b);
        }
        /// <summary>
        /// Adds to the Good Dudes
        /// </summary>
        /// <param name="gd">GameObject to be added to list</param>
        public void AddGoodDude(GameObject gd)
        {
            GoodDudes.Add(gd);
        }
        /// <summary>
        /// Adds to the Bad Dudes
        /// </summary>
        /// <param name="gd">GameObject to be added to list</param>
        public void AddBadDude(GameObject bd)
        {
            BadDudes.Add(bd);
        }
        /// <summary>
        /// Remove to the Good Dudes
        /// </summary>
        /// <param name="gd">GameObject to be added to list</param>
        public void RemoveGoodDude(GameObject gd)
        {
            if (GoodDudes.Contains(gd))
                GoodDudes.Remove(gd);

            else throw new System.Exception(gd + "is not in the GoodDudes");

        }
        /// <summary>
        /// Remove to the Bad Dudes
        /// </summary>
        /// <param name="gd">GameObject to be added to list</param>
        public void RemoveBadDude(GameObject bd)
        {
            if (GoodDudes.Contains(bd))
                BadDudes.Remove(bd);

            else throw new System.Exception(bd + "is not in the BadDudes");

        }
        /// <summary>
        /// Clears all the dudes
        /// </summary>
        public void ClearDudes()
        {
            BadDudes.Clear();
            GoodDudes.Clear();
        }
        /// <summary>
        /// Clears All Good Dudes
        /// </summary>
        public void ClearGoodDudes()
        {
            GoodDudes.Clear();
        }
        /// <summary>
        /// Clears all Bad Dudes
        /// </summary>
        public void ClearBadDudes()
        {
            BadDudes.Clear();
        }
        /// <summary>
        /// Resolves combat
        /// </summary>
        /// <returns>ResolutionInfo Containing who won</returns>
        public ResolutionInfo Resolve()
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

            return new ResolutionInfo("resolveMessage");
        }

        /// <summary>
        /// Returns info
        /// </summary>
        /// <returns>FIeldInfo</returns>
        public FieldInfo GatherFieldInfo()
        {
            return new FieldInfo(ref GoodDudes, ref BadDudes, m_LastMessage);
        }

    }
}