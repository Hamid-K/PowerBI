using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001A4 RID: 420
	internal static class PathParserModelUtils
	{
		// Token: 0x060010F2 RID: 4338 RVA: 0x0002F1D0 File Offset: 0x0002D3D0
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
			if (operationImport.Operation.IsBound && Enumerable.Any<IEdmOperationParameter>(operationImport.Operation.Parameters))
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
				if (Enumerable.Any<EdmError>(enumerable, (EdmError e) => e.ErrorCode == EdmErrorCode.InvalidPathFirstPathParameterNotMatchingFirstParameterName))
				{
					throw ExceptionUtil.CreateSyntaxError();
				}
			}
			return null;
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x0002F2C4 File Offset: 0x0002D4C4
		internal static IEdmEntitySetBase GetTargetEntitySet(this IEdmOperation operation, IEdmNavigationSource source, IEdmModel model)
		{
			if (source == null)
			{
				return null;
			}
			if (operation.IsBound && Enumerable.Any<IEdmOperationParameter>(operation.Parameters))
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
				if (Enumerable.Any<EdmError>(enumerable, (EdmError e) => e.ErrorCode == EdmErrorCode.InvalidPathFirstPathParameterNotMatchingFirstParameterName))
				{
					throw ExceptionUtil.CreateSyntaxError();
				}
			}
			return null;
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x0002F390 File Offset: 0x0002D590
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
