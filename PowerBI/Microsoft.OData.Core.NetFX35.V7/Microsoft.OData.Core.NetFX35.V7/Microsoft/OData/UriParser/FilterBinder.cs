using System;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020000E7 RID: 231
	internal sealed class FilterBinder
	{
		// Token: 0x06000B82 RID: 2946 RVA: 0x0001C859 File Offset: 0x0001AA59
		internal FilterBinder(MetadataBinder.QueryTokenVisitor bindMethod, BindingState state)
		{
			this.bindMethod = bindMethod;
			this.state = state;
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x0001C870 File Offset: 0x0001AA70
		internal FilterClause BindFilter(QueryToken filter)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryToken>(filter, "filter");
			QueryNode queryNode = this.bindMethod(filter);
			SingleValueNode singleValueNode = queryNode as SingleValueNode;
			if (singleValueNode == null || (singleValueNode.TypeReference != null && !singleValueNode.TypeReference.IsODataPrimitiveTypeKind()))
			{
				throw new ODataException(Strings.MetadataBinder_FilterExpressionNotSingleValue);
			}
			IEdmTypeReference typeReference = singleValueNode.TypeReference;
			if (typeReference != null)
			{
				IEdmPrimitiveTypeReference edmPrimitiveTypeReference = typeReference.AsPrimitiveOrNull();
				if (edmPrimitiveTypeReference == null || edmPrimitiveTypeReference.PrimitiveKind() != EdmPrimitiveTypeKind.Boolean)
				{
					throw new ODataException(Strings.MetadataBinder_FilterExpressionNotSingleValue);
				}
			}
			return new FilterClause(singleValueNode, this.state.ImplicitRangeVariable);
		}

		// Token: 0x04000690 RID: 1680
		private readonly MetadataBinder.QueryTokenVisitor bindMethod;

		// Token: 0x04000691 RID: 1681
		private readonly BindingState state;
	}
}
