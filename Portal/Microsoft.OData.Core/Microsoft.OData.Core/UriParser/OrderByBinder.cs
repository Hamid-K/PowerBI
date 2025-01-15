using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200012D RID: 301
	internal sealed class OrderByBinder
	{
		// Token: 0x06001020 RID: 4128 RVA: 0x00029F20 File Offset: 0x00028120
		internal OrderByBinder(MetadataBinder.QueryTokenVisitor bindMethod)
		{
			ExceptionUtils.CheckArgumentNotNull<MetadataBinder.QueryTokenVisitor>(bindMethod, "bindMethod");
			this.bindMethod = bindMethod;
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x00029F3C File Offset: 0x0002813C
		internal OrderByClause BindOrderBy(BindingState state, IEnumerable<OrderByToken> orderByTokens)
		{
			ExceptionUtils.CheckArgumentNotNull<BindingState>(state, "state");
			ExceptionUtils.CheckArgumentNotNull<IEnumerable<OrderByToken>>(orderByTokens, "orderByTokens");
			OrderByClause orderByClause = null;
			foreach (OrderByToken orderByToken in orderByTokens.Reverse<OrderByToken>())
			{
				orderByClause = this.ProcessSingleOrderBy(state, orderByClause, orderByToken);
			}
			return orderByClause;
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x00029FA8 File Offset: 0x000281A8
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

		// Token: 0x040007A9 RID: 1961
		private readonly MetadataBinder.QueryTokenVisitor bindMethod;
	}
}
