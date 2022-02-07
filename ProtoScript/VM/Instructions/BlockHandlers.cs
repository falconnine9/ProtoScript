﻿using System;

using ProtoScript.Errors;
using ProtoScript.Objects.Arguments;
using ProtoScript.Objects.Blocks;
using ProtoScript.Objects.Lines;
using ProtoScript.Objects.Types;

namespace ProtoScript.VM.Instructions
{
    class BlockHandlers
    {
        public static TypeBase HandleIf(ExecutableLine ln)
        {
            IfBlock info = Executor.Code.BlockIndex[Executor.ExecutionLine] as IfBlock;

            bool condition;
            try {
                condition = ((ln.Arguments[0] as TypeArgument).Value as BooleanType).Value;
            } catch (NullReferenceException) {
                throw new ArgumentError("If block takes (boolean) arguments", Executor.Code.Name, ln.OriginLine);
            }

            if (!condition)
                Executor.ExecutionLine = info.End;

            return null;
        }

        public static TypeBase HandleLoop(ExecutableLine ln)
        {
            LoopBlock info = Executor.Code.BlockIndex[Executor.ExecutionLine] as LoopBlock;

            if (info.ToBeSet) {
                int max_iters;
                try {
                    max_iters = (int)((ln.Arguments[0] as TypeArgument).Value as NumberType).Value;
                }
                catch (NullReferenceException) {
                    throw new ArgumentError("If block takes (number) arguments", Executor.Code.Name, ln.OriginLine);
                }

                info.IterationVariable = ln.ReturnVariable;
                info.MaxLoops = max_iters;
                info.ToBeSet = false;
            }

            if (info.MaxLoops == 0)
                Executor.ExecutionLine = info.End;

            return new NumberType(0);
        }

        public static TypeBase HandleBlockEnd(ExecutableLine ln)
        {
            foreach (BlockBase block in Executor.Code.BlockIndex.Values) {
                if (block.End != Executor.ExecutionLine)
                    continue;
                if (block.RealType != typeof(LoopBlock))
                    return null;

                LoopBlock real = block as LoopBlock;

                try {
                    NumberType iter_var = Executor.Variables[real.IterationVariable] as NumberType;
                    if (iter_var.Value < real.MaxLoops - 1) {
                        Executor.ExecutionLine = real.Condition;
                        iter_var.Value++;
                    }
                    break;
                } catch (NullReferenceException) {
                    throw new InvalidVariableError("Iteration variable set to non number type", Executor.Code.Name, ln.OriginLine);
                }
            }

            return null;
        }
    }
}