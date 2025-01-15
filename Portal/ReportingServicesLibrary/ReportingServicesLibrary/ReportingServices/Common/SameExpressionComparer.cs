using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Modeling;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x02000359 RID: 857
	internal class SameExpressionComparer : IEqualityComparer<Expression>
	{
		// Token: 0x06001C4C RID: 7244 RVA: 0x00072968 File Offset: 0x00070B68
		public bool Equals(Expression x, Expression y)
		{
			return x.IsSameAs(y);
		}

		// Token: 0x06001C4D RID: 7245 RVA: 0x00072974 File Offset: 0x00070B74
		public int GetHashCode(Expression obj)
		{
			int num = obj.Path.Length.GetHashCode();
			FunctionNode nodeAsFunction = obj.NodeAsFunction;
			if (nodeAsFunction != null)
			{
				if (nodeAsFunction.FunctionName != FunctionName.Aggregate || nodeAsFunction.Arguments.Count != 1)
				{
					return num ^ nodeAsFunction.FunctionName.GetHashCode();
				}
				obj = nodeAsFunction.Arguments[0];
				num ^= nodeAsFunction.FunctionName.GetHashCode();
				num ^= obj.Path.Length.GetHashCode();
			}
			AttributeRefNode nodeAsAttributeRef = obj.NodeAsAttributeRef;
			if (nodeAsAttributeRef != null)
			{
				return num ^ nodeAsAttributeRef.Attribute.GetHashCode();
			}
			EntityRefNode nodeAsEntityRef = obj.NodeAsEntityRef;
			if (nodeAsEntityRef != null)
			{
				return nodeAsEntityRef.Entity.GetHashCode();
			}
			LiteralNode nodeAsLiteral = obj.NodeAsLiteral;
			if (nodeAsLiteral != null)
			{
				return num ^ nodeAsLiteral.Value.GetHashCode();
			}
			ParameterRefNode nodeAsParameterRef = obj.NodeAsParameterRef;
			if (nodeAsParameterRef != null)
			{
				return num ^ nodeAsParameterRef.Parameter.GetHashCode();
			}
			return num;
		}
	}
}
