using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Core.UriParser.Metadata;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Core.UriParser.Syntactic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Parsers
{
	// Token: 0x020001DC RID: 476
	internal static class SelectPathSegmentTokenBinder
	{
		// Token: 0x06001176 RID: 4470 RVA: 0x0003E0F4 File Offset: 0x0003C2F4
		public static ODataPathSegment ConvertNonTypeTokenToSegment(PathSegmentToken tokenIn, IEdmModel model, IEdmStructuredType edmType, ODataUriResolver resolver = null)
		{
			if (resolver == null)
			{
				resolver = ODataUriResolver.Default;
			}
			ODataPathSegment odataPathSegment;
			if (SelectPathSegmentTokenBinder.TryBindAsDeclaredProperty(tokenIn, edmType, resolver, out odataPathSegment))
			{
				return odataPathSegment;
			}
			if (tokenIn.IsNamespaceOrContainerQualified())
			{
				if (SelectPathSegmentTokenBinder.TryBindAsOperation(tokenIn, model, edmType, out odataPathSegment))
				{
					return odataPathSegment;
				}
				if (!edmType.IsOpen)
				{
					return null;
				}
			}
			if (edmType.IsOpen)
			{
				return new OpenPropertySegment(tokenIn.Identifier);
			}
			throw new ODataException(Strings.MetadataBinder_PropertyNotDeclared(edmType.FullTypeName(), tokenIn.Identifier));
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x0003E17C File Offset: 0x0003C37C
		public static bool TryBindAsWildcard(PathSegmentToken tokenIn, IEdmModel model, out SelectItem item)
		{
			bool flag = tokenIn.IsNamespaceOrContainerQualified();
			bool flag2 = tokenIn.Identifier.EndsWith("*", 4);
			if (flag && flag2)
			{
				string namespaceName = tokenIn.Identifier.Substring(0, tokenIn.Identifier.Length - 2);
				if (Enumerable.Any<string>(model.DeclaredNamespaces, (string declaredNamespace) => declaredNamespace.Equals(namespaceName, 4)))
				{
					item = new NamespaceQualifiedWildcardSelectItem(namespaceName);
					return true;
				}
			}
			if (tokenIn.Identifier == "*")
			{
				item = new WildcardSelectItem();
				return true;
			}
			item = null;
			return false;
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x0003E238 File Offset: 0x0003C438
		[SuppressMessage("DataWeb.Usage", "AC0003:MethodCallNotAllowed", Justification = "Rule only applies to ODataLib Serialization code.")]
		[SuppressMessage("DataWeb.Usage", "AC0014:DoNotHandleProhibitedExceptionsRule", Justification = "ExceptionUtils.IsCatchableExceptionType is being used correctly")]
		internal static bool TryBindAsOperation(PathSegmentToken pathToken, IEdmModel model, IEdmStructuredType entityType, out ODataPathSegment segment)
		{
			List<IEdmOperation> list = new List<IEdmOperation>();
			try
			{
				int num = pathToken.Identifier.IndexOf("*", 4);
				if (num > -1)
				{
					string namespaceName = pathToken.Identifier.Substring(0, num - 1);
					list = Enumerable.ToList<IEdmOperation>(Enumerable.Where<IEdmOperation>(model.FindBoundOperations(entityType), (IEdmOperation o) => o.Namespace == namespaceName));
				}
				else
				{
					NonSystemToken nonSystemToken = pathToken as NonSystemToken;
					IList<string> list2 = new List<string>();
					if (nonSystemToken != null && nonSystemToken.NamedValues != null)
					{
						list2 = Enumerable.ToList<string>(Enumerable.Select<NamedValue, string>(nonSystemToken.NamedValues, (NamedValue s) => s.Name));
					}
					if (list2.Count > 0)
					{
						list = Enumerable.ToList<IEdmOperation>(model.FindBoundOperations(entityType).FilterByName(true, pathToken.Identifier).FilterOperationsByParameterNames(list2, false));
					}
					else
					{
						list = Enumerable.ToList<IEdmOperation>(model.FindBoundOperations(entityType).FilterByName(true, pathToken.Identifier));
					}
				}
			}
			catch (Exception ex)
			{
				if (!ExceptionUtils.IsCatchableExceptionType(ex))
				{
					throw;
				}
			}
			list = Enumerable.ToList<IEdmOperation>(list.EnsureOperationsBoundWithBindingParameter());
			if (list.Count > 1)
			{
				list = Enumerable.ToList<IEdmOperation>(list.FilterBoundOperationsWithSameTypeHierarchyToTypeClosestToBindingType(entityType));
			}
			if (list.Count <= 0)
			{
				segment = null;
				return false;
			}
			segment = new OperationSegment(list, null);
			return true;
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x0003E388 File Offset: 0x0003C588
		[SuppressMessage("DataWeb.Usage", "AC0003:MethodCallNotAllowed", Justification = "Rule only applies to ODataLib Serialization code.")]
		private static bool TryBindAsDeclaredProperty(PathSegmentToken tokenIn, IEdmStructuredType edmType, ODataUriResolver resolver, out ODataPathSegment segment)
		{
			IEdmProperty edmProperty = resolver.ResolveProperty(edmType, tokenIn.Identifier);
			if (edmProperty == null)
			{
				segment = null;
				return false;
			}
			if (edmProperty.PropertyKind == EdmPropertyKind.Structural)
			{
				segment = new PropertySegment((IEdmStructuralProperty)edmProperty);
				return true;
			}
			if (edmProperty.PropertyKind == EdmPropertyKind.Navigation)
			{
				segment = new NavigationPropertySegment((IEdmNavigationProperty)edmProperty, null);
				return true;
			}
			throw new ODataException(Strings.SelectExpandBinder_UnknownPropertyType(edmProperty.Name));
		}
	}
}
