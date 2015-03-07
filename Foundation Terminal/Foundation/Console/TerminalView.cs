// -------------------------------------
//  Domain		: IBT / Realtime.co
//  Author		: Nicholas Ventimiglia
//  Product		: Messaging and Storage
//  Published	: 2014
//  -------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Realtime.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Realtime.Demos.TerminalConsole.Views
{
    /// <summary>
    /// renders the Terminal using new 4.6 uGUI
    /// </summary>
    [AddComponentMenu("Realtime/Demos/Terminal/TerminalView")]
    public class TerminalView : MonoBehaviour
    {
        /// <summary>
        /// Option
        /// </summary>
        public bool DoDontDestoryOnLoad = true;

        public GameObject DisplayRoot;
        public InputField TextInput;

        public TerminalViewCommand CommandPrototype;
        public TerminalViewItem ItemPrototype;

        public Transform CommandLayout;
        public Transform ItemLayout;
        public Scrollbar CommandScrollBar;
        public Scrollbar ItemScrollBar;
        [HideInInspector]
        public List<TerminalViewCommand> CommandItems = new List<TerminalViewCommand>();
        [HideInInspector]
        public List<TerminalViewItem> TextItems = new List<TerminalViewItem>();

        public bool IsVisible
        {
            get
            {
                return DisplayRoot.activeSelf;

            }
            set
            {
                DisplayRoot.SetActive(value);
            }
        }

        public KeyCode VisiblityKey = KeyCode.BackQuote;

        public Color LogColor = Color.white;
        public Color WarningColor = Color.yellow;
        public Color ErrorColor = Color.red;

        public Color SuccessColor = Color.green;
        public Color InputColor = Color.cyan;
        public Color ImportantColor = Color.yellow;

        void Awake()
        {
            // Display
            Terminal.Instance.LogColor = LogColor;
            Terminal.Instance.WarningColor = WarningColor;
            Terminal.Instance.ErrorColor = ErrorColor;
            Terminal.Instance.SuccessColor = SuccessColor;
            Terminal.Instance.InputColor = InputColor;
            Terminal.Instance.ImportantColor = ImportantColor;

            //Hide prototypes
            CommandPrototype.gameObject.SetActive(false);
            ItemPrototype.gameObject.SetActive(false);

            //wire
            Terminal.Instance.Items.OnAdd += Items_OnAdd;
            Terminal.Instance.Items.OnClear += Items_OnClear;
            Terminal.Instance.Items.OnRemove += Items_OnRemove;

            Terminal.Instance.Commands.OnAdd += Commands_OnAdd;
            Terminal.Instance.Commands.OnClear += Commands_OnClear;
            Terminal.Instance.Commands.OnRemove += Commands_OnRemove;

            //add items preadded
            foreach (var item in Terminal.Instance.Items)
            {
                Items_OnAdd(item);
            }

            foreach (var item in Terminal.Instance.Commands)
            {
                Commands_OnAdd(item);
            }

            Application.RegisterLogCallback(HandlerLog);

            if (DoDontDestoryOnLoad)
                DontDestroyOnLoad(gameObject);

            Debug.Log("Console Ready");
        }

        void OnDestroy()
        {
            //remove handlers
            Terminal.Instance.Items.OnAdd -= Items_OnAdd;
            Terminal.Instance.Items.OnClear -= Items_OnClear;
            Terminal.Instance.Items.OnRemove -= Items_OnRemove;

            Terminal.Instance.Commands.OnAdd -= Commands_OnAdd;
            Terminal.Instance.Commands.OnClear -= Commands_OnClear;
            Terminal.Instance.Commands.OnRemove -= Commands_OnRemove;
        }

        private void HandlerLog(string condition, string stackTrace, LogType type)
        {
            switch (type)
            {
                case LogType.Error:
                case LogType.Exception:
                    Terminal.LogError(condition);
                    break;
                case LogType.Warning:
                    Terminal.LogWarning(condition);
                    break;
                case LogType.Log:
                case LogType.Assert:
                    Terminal.Log(condition);
                    break;
            }
        }

        void Commands_OnRemove(TerminalCommand obj)
        {
            var item = CommandItems.FirstOrDefault(o => o.Model.Equals(obj));
            if (item != null)
            {
                CommandItems.Remove(item);
                Destroy(item.gameObject);
            }
        }

        void Commands_OnClear()
        {
            foreach (var item in CommandItems)
            {
                Destroy(item.gameObject);
            }
            CommandItems.Clear();
        }

        void Commands_OnAdd(TerminalCommand obj)
        {
            //inst
            var instance = (GameObject)Instantiate(CommandPrototype.gameObject);
            var script = instance.GetComponent<TerminalViewCommand>();
            script.Label.text = obj.Label;
            script.Handler = obj.Method;
            script.Model = obj;

            //parent
            instance.transform.SetParent(CommandLayout.transform);
            instance.SetActive(true);

            CommandItems.Add(script);

            Invoke("ResetCommandScroll", .01f);
        }

        void ResetCommandScroll()
        {
            CommandScrollBar.value = 1;
        }

        void Items_OnRemove(TerminalItem obj)
        {
            var item = TextItems.FirstOrDefault(o => o.Model.Equals(obj));
            if (item != null)
            {
                TextItems.Remove(item);
                Destroy(item.gameObject);
            }
        }

        void Items_OnClear()
        {
            foreach (var item in TextItems)
            {
                Destroy(item.gameObject);
            }
            TextItems.Clear();
        }

        void Items_OnAdd(TerminalItem obj)
        {
            StartCoroutine(AddItemAsync(obj));
        }

        IEnumerator AddItemAsync(TerminalItem obj)
        {

            //inst
            var instance = (GameObject)Instantiate(ItemPrototype.gameObject);
            var script = instance.GetComponent<TerminalViewItem>();
            script.Label.text = obj.Text;
            script.Label.color = obj.Color;
            script.Model = obj;

            //parent
            instance.transform.SetParent(ItemLayout.transform);
            instance.SetActive(true);

            TextItems.Add(script);

            yield return 1;

            ItemScrollBar.value = 0;

            yield return 1;

            ItemScrollBar.value = 0;
        }


        void Update()
        {
            if (Input.GetKeyUp(VisiblityKey))
            {
                IsVisible = !IsVisible;
            }

        }

        public void DoSend()
        {
            var text = TextInput.text.Replace(Environment.NewLine, "");

            if (string.IsNullOrEmpty(text))
                return;

            Terminal.Submit(text);

            TextInput.text = string.Empty;
        }

        public void DoClear()
        {
            Terminal.Clear();
        }
    }
}