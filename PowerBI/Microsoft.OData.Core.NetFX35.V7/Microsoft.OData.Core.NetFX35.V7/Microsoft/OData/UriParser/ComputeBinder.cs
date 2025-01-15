using System;
using System.Collections.Generic;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020000E2 RID: 226
	internal sealed class ComputeBinder
	{
		// Token: 0x06000B68 RID: 2920 RVA: 0x0001BE61 File Offset: 0x0001A061
		public ComputeBinder(MetadataBinder.QueryTokenVisitor bindMethod)
		{
			this.bindMethod = bindMethod;
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x0001BE70 File Offset: 0x0001A070
		public ComputeClause BindCompute(ComputeToken token)
		{
			ExceptionUtils.CheckArgumentNotNull<ComputeToken>(token, "token");
			List<ComputeExpression> list = new List<ComputeExpression>();
			foreach (ComputeExpressionToken computeExpressionToken in token.Expressions)
			{
				ComputeExpression computeExpression = this.BindComputeExpressionToken(computeExpressionToken);
				list.Add(computeExpression);
			}
			return new ComputeClause(list);
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x0001BEE0 File Offset: 0x0001A0E0
		private ComputeExpression BindComputeExpressionToken(ComputeExpressionToken token)
		{
			SingleValueNode singleValueNode = this.bindMethod(token.Expression) as SingleValueNode;
			return new ComputeExpression(singleValueNode, token.Alias, singleValueNode.TypeReference);
		}

		// Token: 0x0400068E RID: 1678
		private MetadataBinder.QueryTokenVisitor bindMethod;
	}
}
