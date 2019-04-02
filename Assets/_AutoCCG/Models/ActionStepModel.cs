using System;
using System.Collections;

namespace AutoCCG
{
    public class ActionStepModel
    {
        public Action run;

        public Action undo;

        public IEnumerator asyncRun;

        public ActionStepModel(Action run, Action undo = null)
        {
            this.run = run;
            this.undo = undo;
        }

        public ActionStepModel(IEnumerator runCoroutine)
        {
            this.asyncRun = runCoroutine;
        }

        public IEnumerator RunCoroutine()
        {
            if (this.asyncRun != null)
            {
                yield return this.asyncRun;
            }
            else
            {
                run();
                yield return null;
            }
        }
    }

}