using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShooterFeatures
{
    public class KillList
    {
        public string Killer { get; set; }
        public string Victum { get; set; }
        public Sprite Weapon { get; set; }
    }

    public class BattleGrounObserver: MonoBehaviour
    {
        public Dictionary<int, KillList> killJournal = new Dictionary<int, KillList>();
        public PanelManager[] fields;
        public static BattleGrounObserver instance;

        private void Awake()
        {
            instance = this;
        }

        public void AddKill(KillList record)
        {
            killJournal.Add(killJournal.Count, record);
            MakeKillRecordOnEventBoard(record, fields);
            
        }

        void MakeKillRecordOnEventBoard(KillList record, PanelManager[] eventBoard) {

            if (killJournal.Count <= eventBoard.Length) {
                eventBoard[killJournal.Count - 1].gameObject.SetActive(true);
            }

            int i = eventBoard.Length - 1;
            while (i > 0) {
                eventBoard[i].FillPanelWithInfo(eventBoard[i - 1].GetPanelInfo());
                i--;
            }

            eventBoard[0].FillPanelWithInfo(record);
        }
    }
}