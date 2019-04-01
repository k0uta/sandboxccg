using System;

namespace AutoCCG
{
    public class ActionStepModel
    {
        public Action run;

        public Action undo;

        public ActionStepModel(Action run, Action undo = null)
        {
            this.run = run;
            this.undo = undo;
        }
    }

}