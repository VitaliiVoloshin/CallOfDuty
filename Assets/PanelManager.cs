using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShooterFeatures {
    public class PanelManager: MonoBehaviour
    {
        public Text killer;
        public Image weaponIcon;
        public Text victum;

        public void FillPanelWithInfo(KillList record) {
            killer.text = record.Killer;
            weaponIcon.sprite = record.Weapon;
            victum.text = record.Victum;
        }

        public KillList GetPanelInfo() {
            return new KillList { Killer = killer.text,Weapon = weaponIcon.sprite, Victum = victum.text};
        }
    }
}
