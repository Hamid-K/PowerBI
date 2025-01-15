using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Core.Common.CommandTrees.Internal
{
	// Token: 0x020006EF RID: 1775
	internal sealed class ParameterRetriever : BasicCommandTreeVisitor
	{
		// Token: 0x0600528F RID: 21135 RVA: 0x001288FF File Offset: 0x00126AFF
		private ParameterRetriever()
		{
		}

		// Token: 0x06005290 RID: 21136 RVA: 0x00128912 File Offset: 0x00126B12
		internal static ReadOnlyCollection<DbParameterReferenceExpression> GetParameters(DbCommandTree tree)
		{
			ParameterRetriever parameterRetriever = new ParameterRetriever();
			parameterRetriever.VisitCommandTree(tree);
			return new ReadOnlyCollection<DbParameterReferenceExpression>(parameterRetriever.paramMappings.Values.ToList<DbParameterReferenceExpression>());
		}

		// Token: 0x06005291 RID: 21137 RVA: 0x00128934 File Offset: 0x00126B34
		public override void Visit(DbParameterReferenceExpression expression)
		{
			Check.NotNull<DbParameterReferenceExpression>(expression, "expression");
			this.paramMappings[expression.ParameterName] = expression;
		}

		// Token: 0x04001DDC RID: 7644
		private readonly Dictionary<string, DbParameterReferenceExpression> paramMappings = new Dictionary<string, DbParameterReferenceExpression>();
	}
}
