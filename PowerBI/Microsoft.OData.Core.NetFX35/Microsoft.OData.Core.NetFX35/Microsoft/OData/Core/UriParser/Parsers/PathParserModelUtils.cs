using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Core.UriParser.Parsers
{
	// Token: 0x0200020B RID: 523
	internal static class PathParserModelUtils
	{
		// Token: 0x0600131B RID: 4891 RVA: 0x00045A74 File Offset: 0x00043C74
		internal static bool IsOpenType(this IEdmType edmType)
		{
			IEdmStructuredType edmStructuredType = edmType as IEdmStructuredType;
			if (edmStructuredType != null)
			{
				return edmStructuredType.IsOpen;
			}
			IEdmCollectionType edmCollectionType = edmType as IEdmCollectionType;
			return edmCollectionType != null && edmCollectionType.ElementType.Definition.IsOpenType();
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x00045AB0 File Offset: 0x00043CB0
		internal static bool IsEntityOrEntityCollectionType(this IEdmType edmType)
		{
			IEdmEntityType edmEntityType;
			return edmType.IsEntityOrEntityCollectionType(out edmEntityType);
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x00045AC8 File Offset: 0x00043CC8
		internal static bool IsEntityOrEntityCollectionType(this IEdmType edmType, out IEdmEntityType entityType)
		{
			if (edmType.TypeKind == EdmTypeKind.Entity)
			{
				entityType = (IEdmEntityType)edmType;
				return true;
			}
			if (edmType.TypeKind != EdmTypeKind.Collection)
			{
				entityType = null;
				return false;
			}
			entityType = ((IEdmCollectionType)edmType).ElementType.Definition as IEdmEntityType;
			return entityType != null;
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x00045B24 File Offset: 0x00043D24
		internal static IEdmEntitySetBase GetTargetEntitySet(this IEdmOperationImport operationImport, IEdmEntitySetBase sourceEntitySet, IEdmModel model)
		{
			IEdmEntitySet edmEntitySet;
			if (operationImport.TryGetStaticEntitySet(out edmEntitySet))
			{
				return edmEntitySet;
			}
			if (sourceEntitySet == null)
			{
				return null;
			}
			if (operationImport.Operation.IsBound && Enumerable.Any<IEdmOperationParameter>(operationImport.Operation.Parameters))
			{
				IEdmOperationParameter edmOperationParameter;
				IEnumerable<IEdmNavigationProperty> enumerable;
				IEnumerable<EdmError> enumerable2;
				if (operationImport.TryGetRelativeEntitySetPath(model, out edmOperationParameter, out enumerable, out enumerable2))
				{
					IEdmEntitySetBase edmEntitySetBase = sourceEntitySet;
					foreach (IEdmNavigationProperty edmNavigationProperty in enumerable)
					{
						edmEntitySetBase = edmEntitySetBase.FindNavigationTarget(edmNavigationProperty) as IEdmEntitySetBase;
						if (edmEntitySetBase == null || edmEntitySetBase is IEdmUnknownEntitySet)
						{
							return edmEntitySetBase;
						}
					}
					return edmEntitySetBase;
				}
				if (Enumerable.Any<EdmError>(enumerable2, (EdmError e) => e.ErrorCode == EdmErrorCode.InvalidPathFirstPathParameterNotMatchingFirstParameterName))
				{
					throw ExceptionUtil.CreateSyntaxError();
				}
			}
			return null;
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x00045C18 File Offset: 0x00043E18
		internal static IEdmEntitySetBase GetTargetEntitySet(this IEdmOperation operation, IEdmNavigationSource source, IEdmModel model)
		{
			if (source == null)
			{
				return null;
			}
			if (operation.IsBound && Enumerable.Any<IEdmOperationParameter>(operation.Parameters))
			{
				IEdmOperationParameter edmOperationParameter;
				IEnumerable<IEdmNavigationProperty> enumerable;
				IEdmEntityType edmEntityType;
				IEnumerable<EdmError> enumerable2;
				if (operation.TryGetRelativeEntitySetPath(model, out edmOperationParameter, out enumerable, out edmEntityType, out enumerable2))
				{
					IEdmNavigationSource edmNavigationSource = source;
					foreach (IEdmNavigationProperty edmNavigationProperty in enumerable)
					{
						edmNavigationSource = edmNavigationSource.FindNavigationTarget(edmNavigationProperty);
						if (edmNavigationSource == null)
						{
							return null;
						}
					}
					return edmNavigationSource as IEdmEntitySetBase;
				}
				if (Enumerable.Any<EdmError>(enumerable2, (EdmError e) => e.ErrorCode == EdmErrorCode.InvalidPathFirstPathParameterNotMatchingFirstParameterName))
				{
					throw ExceptionUtil.CreateSyntaxError();
				}
			}
			return null;
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x00045CE0 File Offset: 0x00043EE0
		internal static IEdmType AsElementType(this IEdmType type)
		{
			IEdmCollectionType edmCollectionType = type as IEdmCollectionType;
			if (edmCollectionType == null)
			{
				return type;
			}
			return edmCollectionType.ElementType.Definition;
		}
	}
}
