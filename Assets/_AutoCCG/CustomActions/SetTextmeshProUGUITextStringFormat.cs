using System;
using TMPro;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("AutoCCG")]
    public class SetTextmeshProUGUITextStringFormat : FsmStateAction
    {
        [RequiredField]
        [Tooltip("E.g. Hello {0} and {1}\nWith 2 variables that replace {0} and {1}\nSee C# string.Format docs.")]
        public FsmString format;

        [Tooltip("Variables to use for each formatting item.")]
        public FsmVar[] variables;

        [Tooltip("Repeat every frame. This is useful if the variables are changing.")]
        public FsmBool everyFrame;

        private object[] objectArray;

        [RequiredField]
        [CheckForComponent(typeof(TextMeshProUGUI))]
        [Tooltip("Textmesh Pro component is required.")]
        public FsmOwnerDefault gameObject;

        TextMeshProUGUI meshproScript;

        public override void Reset()
        {
            format = null;
            variables = null;
            everyFrame = false;
            gameObject = null;
        }

        // Code that runs on entering the state.
        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            objectArray = new object[variables.Length];

            meshproScript = go.GetComponent<TextMeshProUGUI>();

            if (!everyFrame.Value)
            {
                DoMeshChange();
                Finish();
            }
        }

        public override void OnUpdate()
        {
            if (everyFrame.Value)
            {
                DoMeshChange();
            }
        }

        void DoMeshChange()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }

            meshproScript.text = GetFormattedString();
        }

        void DoFormatString()
        {
            var formattedString = GetFormattedString();

        }

        string GetFormattedString()
        {
            for (var i = 0; i < variables.Length; i++)
            {
                variables[i].UpdateValue();
                objectArray[i] = variables[i].GetValue();
            }

            string result = "";

            try
            {
                result = string.Format(format.Value.Replace("[nl]", Environment.NewLine), objectArray);
            }
            catch (System.FormatException e)
            {
                LogError(e.Message);
                Finish();
            }

            return result;
        }
    }

}
