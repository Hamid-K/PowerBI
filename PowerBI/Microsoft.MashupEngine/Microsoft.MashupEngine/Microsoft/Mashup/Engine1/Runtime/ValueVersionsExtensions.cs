using System;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Ast;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020016C0 RID: 5824
	internal static class ValueVersionsExtensions
	{
		// Token: 0x0600943C RID: 37948 RVA: 0x001E96EC File Offset: 0x001E78EC
		public static IExpression AccessVersion(this IExpression expression, string identity)
		{
			return new RequiredFieldAccessExpressionSyntaxNode(new RequiredElementAccessExpressionSyntaxNode(new InvocationExpressionSyntaxNode1(new ConstantExpressionSyntaxNode(Library._Value.Versions), expression), new RecordExpressionSyntaxNode(new VariableInitializer[]
			{
				new VariableInitializer(Identifier.New("Version"), new ConstantExpressionSyntaxNode(TextValue.New(identity)))
			})), Identifier.New("Data"));
		}
	}
}
