using System;
using UnityEngine;
using UnityEngine.UI;

namespace Realtime.Demos.TerminalConsole.Views
{
    /// <summary>
    /// Command View Controller
    /// </summary>
    [AddComponentMenu("Realtime/Demos/Terminal/TerminalViewCommand")]
    public class TerminalViewCommand : MonoBehaviour
    {
        public TerminalCommand Model { get; set; }

        public Text Label;

        public Action Handler;

        public void OnClick()
        {
            if (Handler != null)
                Handler();
        }

    }
}