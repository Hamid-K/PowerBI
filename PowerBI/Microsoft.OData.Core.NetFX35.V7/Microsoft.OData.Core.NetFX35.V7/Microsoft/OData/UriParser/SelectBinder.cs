using System;
using System.Linq;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020000F3 RID: 243
	internal sealed class SelectBinder
	{
		// Token: 0x06000BCE RID: 3022 RVA: 0x0001E7E9 File Offset: 0x0001C9E9
		public SelectBinder(IEdmModel model, IEdmStructuredType edmType, int maxDepth, SelectExpandClause expandClauseToDecorate, ODataUriResolver resolver)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "tokenIn");
			ExceptionUtils.CheckArgumentNotNull<IEdmStructuredType>(edmType, "entityType");
			ExceptionUtils.CheckArgumentNotNull<ODataUriResolver>(resolver, "resolver");
			this.visitor = new SelectPropertyVisitor(model, edmType, maxDepth, expandClauseToDecorate, resolver);
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x0001E828 File Offset: 0x0001CA28
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

		// Token: 0x04000699 RID: 1689
		private readonly SelectPropertyVisitor visitor;
	}
}
