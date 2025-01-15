using System;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x0200061C RID: 1564
	internal static class OdbcNativeQueryExpression
	{
		// Token: 0x060030F9 RID: 12537 RVA: 0x00094E74 File Offset: 0x00093074
		public static IExpression New(IResource resource, string query)
		{
			RecordValue recordValue = RecordValue.New(QueryToExpressionVisitor.ResourceKeys, new Value[]
			{
				TextValue.NewOrNull(resource.Kind),
				TextValue.NewOrNull(resource.Path)
			});
			return new InvocationExpressionSyntaxNode2(new ConstantExpressionSyntaxNode(ResourceModule.Resource.Access), new ConstantExpressionSyntaxNode(recordValue), new ConstantExpressionSyntaxNode(TextValue.New(query)));
		}
	}
}
