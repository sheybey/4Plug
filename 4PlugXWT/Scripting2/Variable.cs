﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FPlug.Scripting2
{
    public class Variable
    {
        public VariableType Type { get; private set; }
        public object Object { get; private set; }

        public Variable(string name)
        {
            Type = VariableType.Null;
            Object = null;
        }

        public void SetValue(string value)
        {
            Object = value;
            Type = VariableType.String;
        }

        public void SetValue(int value)
        {
            Object = value;
            Type = VariableType.Int;
        }

        public int GetInt()
        {
            switch (Type)
            {
                case VariableType.Int:
                    return (int)Object;
                case VariableType.String:
                    {
                        int val;
                        if (int.TryParse((string)Object, out val))
                        {
                            return val;
                        }
                        else
                        {
                            throw new ScriptException($"The variable \"_unknown\" ({(string)Object}) can not be converted into an integer.");
                        }
                    }
                case VariableType.Null:
                    ScriptException.ThrowNullVarException();
                    break;
                default:
                    ScriptException.ThrowTypeIncorrectException(VariableType.Int);
                    break;
            }
            return 0;
        }

        public string GetString()
        {
            switch (Type)
            {
                case VariableType.String:
                    return (string)Object;
                default:
                    return Object.ToString();
            }
        }

        public override string ToString()
        {
            return $"{Type}:{Object}";
        }
    }

    [Flags]
    public enum VariableType
    {
        Null = 1,
        String = 2,
        Int = 4,
    }
}
