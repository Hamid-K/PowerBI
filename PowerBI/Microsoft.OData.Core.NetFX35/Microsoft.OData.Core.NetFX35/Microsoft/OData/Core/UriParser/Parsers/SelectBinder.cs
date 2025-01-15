using System;
using System.Linq;
using Microsoft.OData.Core.UriParser.Metadata;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Core.UriParser.Syntactic;
using Microsoft.OData.Core.UriParser.Visitors;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Parsers
{
	// Token: 0x020001D6 RID: 470
	internal sealed class SelectBinder
	{
		// Token: 0x0600115A RID: 4442 RVA: 0x0003D6EC File Offset: 0x0003B8EC
		public SelectBinder(IEdmModel model, IEdmStructuredType edmType, int maxDepth, SelectExpandClause expandClauseToDecorate, ODataUriResolver resolver = null)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "tokenIn");
			ExceptionUtils.CheckArgumentNotNull<IEdmStructuredType>(edmType, "entityType");
			this.visitor = new SelectPropertyVisitor(model, edmType, maxDepth, expandClauseToDecorate, resolver);
		}

		// Token: 0x0600115B RID: 4443 RVA: 0x0003D71C File Offset: 0x0003B91C
		public SelectExpandClause Bind(SelectToken tokenIn)
		{
			if (tokenIn == null || !Enumerable.Any<PathSegmentToken>(tokenIn.Properties))
			{
				this.visitor.DecoratedExpandClause.SetAllSelected(true);
			}
			else
			{
				this.visitor.DecoratedExpandClause.SetAllSelected(false);
				foreach (PathSegmentToken pathSegmentToken in tokenIn.Properties)
				{
					pathSegmentToken.Accept(this.visitor);
				}
			}
			return this.visitor.DecoratedExpandClause;
		}

		// Token: 0x0400078C RID: 1932
		private readonly SelectPropertyVisitor visitor;
	}
}
