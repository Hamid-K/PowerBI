using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020000EF RID: 239
	internal sealed class OrderByBinder
	{
		// Token: 0x06000BC3 RID: 3011 RVA: 0x0001E48C File Offset: 0x0001C68C
		internal OrderByBinder(MetadataBinder.QueryTokenVisitor bindMethod)
		{
			ExceptionUtils.CheckArgumentNotNull<MetadataBinder.QueryTokenVisitor>(bindMethod, "bindMethod");
			this.bindMethod = bindMethod;
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x0001E4A8 File Offset: 0x0001C6A8
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

		// Token: 0x06000BC5 RID: 3013 RVA: 0x0001E514 File Offset: 0x0001C714
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

		// Token: 0x04000696 RID: 1686
		private readonly MetadataBinder.QueryTokenVisitor bindMethod;
	}
}
