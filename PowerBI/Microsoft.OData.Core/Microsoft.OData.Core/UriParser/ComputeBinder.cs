using System;
using System.Collections.Generic;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000101 RID: 257
	internal sealed class ComputeBinder
	{
		// Token: 0x06000F0B RID: 3851 RVA: 0x0002578D File Offset: 0x0002398D
		public ComputeBinder(MetadataBinder.QueryTokenVisitor bindMethod)
		{
			this.bindMethod = bindMethod;
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x0002579C File Offset: 0x0002399C
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

		// Token: 0x06000F0D RID: 3853 RVA: 0x0002580C File Offset: 0x00023A0C
		private ComputeExpression BindComputeExpressionToken(ComputeExpressionToken token)
		{
			SingleValueNode singleValueNode = this.bindMethod(token.Expression) as SingleValueNode;
			return new ComputeExpression(singleValueNode, token.Alias, singleValueNode.TypeReference);
		}

		// Token: 0x04000764 RID: 1892
		private MetadataBinder.QueryTokenVisitor bindMethod;
	}
}
