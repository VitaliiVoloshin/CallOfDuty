using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShooterFeatures
{
    [RequireComponent(typeof(Text))]
    public class SetLocalizedText: MonoBehaviour
    {
        [SerializeField] private string keyWord;

        private LanguageController m_languageController;
        private Text m_text;

        private void Start()
        {
            m_languageController = LanguageController.instance;
            m_text = GetComponent<Text>();
            OnSwitchLanguage();
            SubscribeToLanguageSwitch();
        }

        void OnSwitchLanguage() {
            if (keyWord != null)
                m_text.text = LanguageController.instance.words[keyWord];
        }

        private void OnDestroy()
        {
            UnsubscribeFromLanguageSwitch();
        }

        void SubscribeToLanguageSwitch() {
            if (m_languageController)
                m_languageController.onSwitchLanguage += OnSwitchLanguage;
        }

        void UnsubscribeFromLanguageSwitch() {
            if (m_languageController)
                m_languageController.onSwitchLanguage -= OnSwitchLanguage;
        }
    }
}
