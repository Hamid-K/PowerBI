using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Core.UriParser.Syntactic;

namespace Microsoft.OData.Core.UriParser.Parsers
{
	// Token: 0x020001D2 RID: 466
	internal sealed class OrderByBinder
	{
		// Token: 0x0600114F RID: 4431 RVA: 0x0003D37C File Offset: 0x0003B57C
		internal OrderByBinder(MetadataBinder.QueryTokenVisitor bindMethod)
		{
			ExceptionUtils.CheckArgumentNotNull<MetadataBinder.QueryTokenVisitor>(bindMethod, "bindMethod");
			this.bindMethod = bindMethod;
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x0003D398 File Offset: 0x0003B598
		internal OrderByClause BindOrderBy(BindingState state, IEnumerable<OrderByToken> orderByTokens)
		{
			ExceptionUtils.CheckArgumentNotNull<BindingState>(state, "state");
			ExceptionUtils.CheckArgumentNotNull<IEnumerable<OrderByToken>>(orderByTokens, "orderByTokens");
			OrderByClause orderByClause = null;
			foreach (OrderByToken orderByToken in Enumerable.Reverse<OrderByToken>(orderByTokens))
			{
				orderByClause = this.ProcessSingleOrderBy(state, orderByClause, orderByToken);
			}
			return orderByClause;
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x0003D404 File Offset: 0x0003B604
		private OrderByClause ProcessSingleOrderBy(BindingState state, OrderByClause thenBy, OrderByToken orderByToken)
		{
			ExceptionUtils.CheckArgumentNotNull<BindingState>(state, "state");
			ExceptionUtils.CheckArgumentNotNull<OrderByToken>(orderByToken, "orderByToken");
			QueryNode queryNode = this.bindMethod(orderByToken.Expression);
			SingleValueNode singleValueNode = queryNode as SingleValueNode;
			if (singleValueNode == null || (singleValueNode.TypeReference != null && !singleValueNode.TypeReference.IsODataPrimitiveTypeKind() && !singleValueNode.TypeReference.IsODataEnumTypeKind() && !singleValueNode.TypeReference.IsODataTypeDefinitionTypeKind()))
			{
				throw new ODataException(Strings.MetadataBinder_OrderByExpressionNotSingleValue);
			}
			return new OrderByClause(thenBy, singleValueNode, orderByToken.Direction, state.ImplicitRangeVariable);
		}

		// Token: 0x04000789 RID: 1929
		private readonly MetadataBinder.QueryTokenVisitor bindMethod;
	}
}
