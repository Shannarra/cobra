 using System;

namespace CobraLib.CodeDom.Binding
{
    internal class BoundVariableExpression : BoundExpression
    {
        public string Name { get; }
        public override Type Type { get; }
        public override BoundNodeKind Kind => BoundNodeKind.VariableExpression;


        public BoundVariableExpression(string name, Type type)
        {
            Name = name;
            Type = type;
        }

    }
}        