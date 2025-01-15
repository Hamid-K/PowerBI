using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200011A RID: 282
	internal static class PathParserModelUtils
	{
		// Token: 0x06000F91 RID: 3985 RVA: 0x000269CC File Offset: 0x00024BCC
		internal static IEdmEntitySetBase GetTargetEntitySet(this IEdmOperationImport operationImport, IEdmEntitySetBase sourceEntitySet, IEdmModel model)
		{
			IEdmEntitySetBase edmEntitySetBase;
			if (operationImport.TryGetStaticEntitySet(model, out edmEntitySetBase))
			{
				return edmEntitySetBase;
			}
			if (sourceEntitySet == null)
			{
				return null;
			}
			if (operationImport.Operation.IsBound && operationImport.Operation.Parameters.Any<IEdmOperationParameter>())
			{
				IEdmOperationParameter edmOperationParameter;
				Dictionary<IEdmNavigationProperty, IEdmPathExpression> dictionary;
				IEnumerable<EdmError> enumerable;
				if (operationImport.TryGetRelativeEntitySetPath(model, out edmOperationParameter, out dictionary, out enumerable))
				{
					IEdmEntitySetBase edmEntitySetBase2 = sourceEntitySet;
					foreach (KeyValuePair<IEdmNavigationProperty, IEdmPathExpression> keyValuePair in dictionary)
					{
						edmEntitySetBase2 = edmEntitySetBase2.FindNavigationTarget(keyValuePair.Key, keyValuePair.Value) as IEdmEntitySetBase;
						if (edmEntitySetBase2 is IEdmUnknownEntitySet)
						{
							return edmEntitySetBase2;
						}
					}
					return edmEntitySetBase2;
				}
				if (enumerable.Any((EdmError e) => e.ErrorCode == EdmErrorCode.InvalidPathFirstPathParameterNotMatchingFirstParameterName))
				{
					throw ExceptionUtil.CreateSyntaxError();
				}
			}
			return null;
		}

		// Token: 0x06000F92 RID: 3986 RVA: 0x00026AC0 File Offset: 0x00024CC0
		internal static IEdmEntitySetBase GetTargetEntitySet(this IEdmOperation operation, IEdmNavigationSource source, IEdmModel model)
		{
			if (source == null)
			{
				return null;
			}
			if (operation.IsBound && operation.Parameters.Any<IEdmOperationParameter>())
			{
				IEdmOperationParameter edmOperationParameter;
				Dictionary<IEdmNavigationProperty, IEdmPathExpression> dictionary;
				IEdmEntityType edmEntityType;
				IEnumerable<EdmError> enumerable;
				if (operation.TryGetRelativeEntitySetPath(model, out edmOperationParameter, out dictionary, out edmEntityType, out enumerable))
				{
					IEdmNavigationSource edmNavigationSource = source;
					foreach (KeyValuePair<IEdmNavigationProperty, IEdmPathExpression> keyValuePair in dictionary)
					{
						edmNavigationSource = edmNavigationSource.FindNavigationTarget(keyValuePair.Key, keyValuePair.Value);
					}
					return edmNavigationSource as IEdmEntitySetBase;
				}
				if (enumerable.Any((EdmError e) => e.ErrorCode == EdmErrorCode.InvalidPathFirstPathParameterNotMatchingFirstParameterName))
				{
					throw ExceptionUtil.CreateSyntaxError();
				}
			}
			return null;
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x00026B8C File Offset: 0x00024D8C
		internal static RequestTargetKind GetTargetKindFromType(this IEdmType type)
		{
			switch (type.TypeKind)
			{
			case EdmTypeKind.Entity:
			case EdmTypeKind.Complex:
				return RequestTargetKind.Resource;
			case EdmTypeKind.Collection:
				if (type.IsStructuredCollectionType())
				{
					return RequestTargetKind.Resource;
				}
				return RequestTargetKind.Collection;
			case EdmTypeKind.Enum:
				return RequestTargetKind.Enum;
			case EdmTypeKind.TypeDefinition:
				return RequestTargetKind.Primitive;
			}
			return RequestTargetKind.Primitive;
		}
	}
}
