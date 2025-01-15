using System;
using System.Linq;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Query.SemanticAst;

namespace Microsoft.Data.OData.Query.SyntacticAst
{
	// Token: 0x0200008F RID: 143
	internal sealed class SelectBinder
	{
		// Token: 0x0600034F RID: 847 RVA: 0x0000B78F File Offset: 0x0000998F
		public SelectBinder(IEdmModel model, IEdmEntityType entityType, int maxDepth, SelectExpandClause expandClauseToDecorate)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "tokenIn");
			ExceptionUtils.CheckArgumentNotNull<IEdmEntityType>(entityType, "entityType");
			this.visitor = new SelectPropertyVisitor(model, entityType, maxDepth, expandClauseToDecorate);
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000B7C0 File Offset: 0x000099C0
		public SelectExpandClause Bind(SelectToken tokenIn)
		{
			if (tokenIn == null || !Enumerable.Any<PathSegmentToken>(tokenIn.Properties))
			{
				this.visitor.DecoratedExpandClause.SetAllSelectionRecursively();
			}
			else
			{
				foreach (PathSegmentToken pathSegmentToken in tokenIn.Properties)
				{
					pathSegmentToken.Accept(this.visitor);
				}
			}
			return this.visitor.DecoratedExpandClause;
		}

		// Token: 0x040000FD RID: 253
		private readonly SelectPropertyVisitor visitor;
	}
}
