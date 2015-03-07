using UnityEngine;
using UnityEngine.UI;

namespace Realtime.Demos.TerminalConsole.Views
{
    /// <summary>
    /// Item View Controller
    /// </summary>
    [AddComponentMenu("Realtime/Demos/Terminal/TerminalViewItem")]
    public class TerminalViewItem : MonoBehaviour
    {
        public TerminalItem Model { get; set; }

        public Text Label;
    }
}