// -------------------------------------
//  Domain		: IBT / Realtime.co
//  Author		: Nicholas Ventimiglia
//  Product		: Messaging and Storage
//  Published	: 2014
//  -------------------------------------

using UnityEngine;

namespace Realtime.Demos.TerminalConsole.Commands
{
    /// <summary>
    /// Extends the console with 'about me' command
    /// </summary>
    [AddComponentMenu("Realtime/Demos/Terminal/Clear")]
    public class ClearCommand : MonoBehaviour
    {
        protected void Awake()
        {
            Terminal.Add(new TerminalCommand
            {
                Label = "Clear",
                Method = () => Terminal.Clear()
            });
        }

    }
}
