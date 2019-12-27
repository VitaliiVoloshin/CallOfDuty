using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using System;
namespace ShooterFeatures
{
    public enum Language
    {
        English,
        Russian
    }

    public class LanguageController: MonoBehaviour
    {
        public Action onSwitchLanguage;
        public static LanguageController instance;
        public Dictionary<string, string> words = new Dictionary<string, string>();
        private Language m_language;

        public Language language {
            get { return m_language; }

            set {
                switch (value) {
                    case Language.English:
                        ReloadDictionaryUsingPath("Data/English");
                        break;
                    case Language.Russian:
                        ReloadDictionaryUsingPath("Data/Russian");
                        break;

                }
                m_language = value;
            }
        }

        private void Awake()
        {
            instance = this;
            language = Language.English;
        }


        public void EnglishLanguageOnButtonClick()
        {
            language = Language.English;
            SetLanguageForAllActionSubscribers();
        }

        public void RussianLanguageOnButtonClick()
        {
            language = Language.Russian;
            SetLanguageForAllActionSubscribers();
        }

        void ReloadDictionaryUsingPath(string path)
        {
            ChangeCurrentLanguage(path);
        }

        void ChangeCurrentLanguage(string path)
        {
            var textAsset = Resources.Load<TextAsset>(path);

            if (textAsset != null) {
                words = new Dictionary<string, string>();
                JObject jObject = JObject.Parse(textAsset.text);
                words = jObject.ToObject<Dictionary<string, string>>();
            }
        }

        void SetLanguageForAllActionSubscribers()
        {
            if (onSwitchLanguage != null)
                onSwitchLanguage.Invoke();
        }
    }
}