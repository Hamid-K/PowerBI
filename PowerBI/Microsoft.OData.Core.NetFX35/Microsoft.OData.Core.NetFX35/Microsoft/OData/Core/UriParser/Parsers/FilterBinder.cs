using System;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Core.UriParser.Syntactic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Parsers
{
	// Token: 0x020001C9 RID: 457
	internal sealed class FilterBinder
	{
		// Token: 0x06001107 RID: 4359 RVA: 0x0003B815 File Offset: 0x00039A15
		internal FilterBinder(MetadataBinder.QueryTokenVisitor bindMethod, BindingState state)
		{
			this.bindMethod = bindMethod;
			this.state = state;
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x0003B82C File Offset: 0x00039A2C
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

		// Token: 0x04000780 RID: 1920
		private readonly MetadataBinder.QueryTokenVisitor bindMethod;

		// Token: 0x04000781 RID: 1921
		private readonly BindingState state;
	}
}
