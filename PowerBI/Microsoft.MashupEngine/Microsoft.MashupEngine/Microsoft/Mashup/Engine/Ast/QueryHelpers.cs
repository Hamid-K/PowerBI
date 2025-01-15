using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001B89 RID: 7049
	internal static class QueryHelpers
	{
		// Token: 0x0600B0A4 RID: 45220 RVA: 0x002433DC File Offset: 0x002415DC
		public static bool TryGetConstant(this IExpression node, out IValue constant)
		{
			IConstantExpression2 constantExpression = node as IConstantExpression2;
			if (constantExpression != null)
			{
				constant = constantExpression.Value;
				return true;
			}
			constant = null;
			return false;
		}
	}
}
