using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.SqlTranslator
{
	// Token: 0x0200200C RID: 8204
	internal static class ExpressionHelpers
	{
		// Token: 0x0600C7EA RID: 51178 RVA: 0x0027C754 File Offset: 0x0027A954
		public static IExpression NewExpressionError(this IEngine engine, string message, IExpression detail = null)
		{
			List<VariableInitializer> list = new List<VariableInitializer>(3);
			list.Add(new VariableInitializer(Identifier.New("Reason"), engine.ConstantExpression(engine.Text("Expression.Error"))));
			list.Add(new VariableInitializer(Identifier.New("Message"), engine.ConstantExpression(engine.Text(message))));
			if (detail != null)
			{
				list.Add(new VariableInitializer(Identifier.New("Detail"), detail));
			}
			return new ThrowExpressionSyntaxNode(new RecordExpressionSyntaxNode(list));
		}
	}
}
