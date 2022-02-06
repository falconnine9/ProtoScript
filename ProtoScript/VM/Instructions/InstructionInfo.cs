using System;

namespace ProtoScript.VM.Instructions
{
    class InstructionInfo
    {
        public Constants.InstructionExecutor Runner;
        public Range AllowedParameters;

        public InstructionInfo(Constants.InstructionExecutor runner, Range allowed_params)
        {
            this.Runner = runner;
            this.AllowedParameters = allowed_params;
        }
    }
}
