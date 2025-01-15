using System;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000125 RID: 293
	internal sealed class FilterBinder
	{
		// Token: 0x06000FDB RID: 4059 RVA: 0x00027FB1 File Offset: 0x000261B1
		internal FilterBinder(MetadataBinder.QueryTokenVisitor bindMethod, BindingState state)
		{
			this.bindMethod = bindMethod;
			this.state = state;
		}

		// Token: 0x06000FDC RID: 4060 RVA: 0x00027FC8 File Offset: 0x000261C8
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

		// Token: 0x040007A3 RID: 1955
		private readonly MetadataBinder.QueryTokenVisitor bindMethod;

		// Token: 0x040007A4 RID: 1956
		private readonly BindingState state;
	}
}
