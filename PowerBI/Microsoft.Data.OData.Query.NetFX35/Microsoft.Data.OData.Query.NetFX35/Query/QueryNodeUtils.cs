using System;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;
using Microsoft.Data.Experimental.OData.Metadata;
using Microsoft.Data.OData;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x0200003B RID: 59
	internal static class QueryNodeUtils
	{
		// Token: 0x06000162 RID: 354 RVA: 0x00008894 File Offset: 0x00006A94
		internal static CollectionQueryNode AsEntityCollectionNode(this QueryNode query)
		{
			CollectionQueryNode collectionQueryNode = query as CollectionQueryNode;
			if (collectionQueryNode != null && collectionQueryNode.ItemType != null && collectionQueryNode.ItemType.IsODataEntityTypeKind())
			{
				return collectionQueryNode;
			}
			return null;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x000088C4 File Offset: 0x00006AC4
		internal static IEdmPrimitiveTypeReference GetBinaryOperatorResultType(IEdmPrimitiveTypeReference typeReference, BinaryOperatorKind operatorKind)
		{
			switch (operatorKind)
			{
			case BinaryOperatorKind.Or:
			case BinaryOperatorKind.And:
			case BinaryOperatorKind.Equal:
			case BinaryOperatorKind.NotEqual:
			case BinaryOperatorKind.GreaterThan:
			case BinaryOperatorKind.GreaterThanOrEqual:
			case BinaryOperatorKind.LessThan:
			case BinaryOperatorKind.LessThanOrEqual:
				return EdmCoreModel.Instance.GetBoolean(typeReference.IsNullable);
			case BinaryOperatorKind.Add:
			case BinaryOperatorKind.Subtract:
			case BinaryOperatorKind.Multiply:
			case BinaryOperatorKind.Divide:
			case BinaryOperatorKind.Modulo:
				return typeReference;
			default:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.QueryNodeUtils_BinaryOperatorResultType_UnreachableCodepath));
			}
		}
	}
}
