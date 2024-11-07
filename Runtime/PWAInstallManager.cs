using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Scripting;

namespace PWAInstallManager
{
    public class PWAInstallManager : MonoBehaviour
    {
        public UnityEvent onInstallPromptAvailableChanged
        {
            get => this._onInstallPromptAvailableChanged;
            set => this._onInstallPromptAvailableChanged = value;
        }
    
        public bool isInstallPromptAvailable { get; private set; } = false;
    
        [SerializeField]
        private UnityEvent _onInstallPromptAvailableChanged = new UnityEvent();

        public void ShowInstallPrompt()
        {
#if !UNITY_EDITOR && UNITY_WEBGL
            Application.ExternalEval("if (window.deferredPWAInstallPrompt) { window.deferredPWAInstallPrompt.prompt(); }");
#endif
        }

        [Preserve]
        private void OnPWAInstallAvailable()
        {
            Debug.Log("PWA Install Available");
            this.isInstallPromptAvailable = true;
            this._onInstallPromptAvailableChanged.Invoke();
        }

        [Preserve]
        private void OnPWAInstalled()
        {
            Debug.Log("PWA Installed");
            this.isInstallPromptAvailable = false;
            this._onInstallPromptAvailableChanged.Invoke();
        }
    }
}
