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
    /// exit app command
    /// </summary>
    [AddComponentMenu("Realtime/Demos/Terminal/Quit")]
    public class QuitCommand : MonoBehaviour
    {
        protected void Awake()
        {


#if UNITY_EDITOR
             Terminal.Add(new TerminalCommand
            {
                Label = "Quit",
                Method = () =>
                {
                    UnityEditor.EditorApplication.isPlaying = false;
                }
            });
#elif UNITY_STANDALONE || UNITY_ANDROID
             Terminal.Add(new TerminalCommand
            {
                Label = "Quit",
                Method = () => Application.Quit()
            });
#endif

        }
    }
}