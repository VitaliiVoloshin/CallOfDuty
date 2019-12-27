using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace ShooterFeatures
{
    public class FractionController: MonoBehaviour
    {
        public static FractionController instance;

        public List<GameObject> greenTeam;
        public List<GameObject> redTeam;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            GetTeams(UnitHolder.instance);
        }

        void GetTeams(UnitHolder unitHolder)
        {
            foreach (GameObject unit in unitHolder.units) {
                if (unit.GetComponent<PlayerController>()) {
                    unit.GetComponent<ActorController>().fraction = Fraction.GreenTeam;
                    greenTeam.Add(unit);
                } else {
                    unit.GetComponent<ActorController>().fraction = Fraction.RedTeam;
                    redTeam.Add(unit);
                }
            }
        }
    }
}