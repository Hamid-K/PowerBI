using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Vocabularies;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000135 RID: 309
	internal static class SelectPathSegmentTokenBinder
	{
		// Token: 0x0600104C RID: 4172 RVA: 0x0002B3A0 File Offset: 0x000295A0
		public static ODataPathSegment ConvertNonTypeTokenToSegment(PathSegmentToken tokenIn, IEdmModel model, IEdmStructuredType edmType, ODataUriResolver resolver, BindingState state = null)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataUriResolver>(resolver, "resolver");
			if (UriParserHelper.IsAnnotation(tokenIn.Identifier))
			{
				ODataPathSegment odataPathSegment;
				if (SelectPathSegmentTokenBinder.TryBindAsDeclaredTerm(tokenIn, model, resolver, out odataPathSegment))
				{
					return odataPathSegment;
				}
				string text = tokenIn.Identifier.Remove(0, 1);
				int num = text.LastIndexOf(".", StringComparison.Ordinal);
				string text2 = text.Substring(0, num);
				string text3 = text.Substring((num == 0) ? 0 : (num + 1));
				if (string.Compare(text2, "odata", StringComparison.OrdinalIgnoreCase) == 0)
				{
					throw new ODataException(Strings.UriSelectParser_TermIsNotValid(tokenIn.Identifier));
				}
				return new AnnotationSegment(new EdmTerm(text2, text3, EdmCoreModel.Instance.GetUntyped()));
			}
			else
			{
				EndPathToken endPathToken = new EndPathToken(tokenIn.Identifier, null);
				if (state != null && state.IsCollapsed)
				{
					bool? flag;
					if (state == null)
					{
						flag = null;
					}
					else
					{
						HashSet<EndPathToken> aggregatedPropertyNames = state.AggregatedPropertyNames;
						flag = ((aggregatedPropertyNames != null) ? new bool?(aggregatedPropertyNames.Contains(endPathToken)) : null);
					}
					if (!(flag ?? false))
					{
						throw new ODataException(Strings.ApplyBinder_GroupByPropertyNotPropertyAccessValue(tokenIn.Identifier));
					}
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
				if (!edmType.IsOpen)
				{
					bool? flag2;
					if (state == null)
					{
						flag2 = null;
					}
					else
					{
						HashSet<EndPathToken> aggregatedPropertyNames2 = state.AggregatedPropertyNames;
						flag2 = ((aggregatedPropertyNames2 != null) ? new bool?(aggregatedPropertyNames2.Contains(endPathToken)) : null);
					}
					if (!(flag2 ?? false))
					{
						throw new ODataException(Strings.MetadataBinder_PropertyNotDeclared(edmType.FullTypeName(), tokenIn.Identifier));
					}
				}
				return new DynamicPathSegment(tokenIn.Identifier);
			}
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x0002B558 File Offset: 0x00029758
		public static bool TryBindAsWildcard(PathSegmentToken tokenIn, IEdmModel model, out SelectItem item)
		{
			bool flag = tokenIn.IsNamespaceOrContainerQualified();
			bool flag2 = tokenIn.Identifier.EndsWith("*", StringComparison.Ordinal);
			if (flag && flag2)
			{
				string namespaceName = tokenIn.Identifier.Substring(0, tokenIn.Identifier.Length - 2);
				if (model.DeclaredNamespaces.Any((string declaredNamespace) => declaredNamespace.Equals(namespaceName, StringComparison.Ordinal)))
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

		// Token: 0x0600104E RID: 4174 RVA: 0x0002B5F0 File Offset: 0x000297F0
		internal static bool TryBindAsOperation(PathSegmentToken pathToken, IEdmModel model, IEdmStructuredType entityType, out ODataPathSegment segment)
		{
			IEnumerable<IEdmOperation> enumerable = Enumerable.Empty<IEdmOperation>();
			IList<string> list = new List<string>();
			try
			{
				int num = pathToken.Identifier.IndexOf("*", StringComparison.Ordinal);
				if (num > -1)
				{
					string namespaceName = pathToken.Identifier.Substring(0, num - 1);
					enumerable = from o in model.FindBoundOperations(entityType)
						where o.Namespace == namespaceName
						select o;
				}
				else
				{
					NonSystemToken nonSystemToken = pathToken as NonSystemToken;
					if (nonSystemToken != null && nonSystemToken.NamedValues != null)
					{
						list = nonSystemToken.NamedValues.Select((NamedValue s) => s.Name).ToList<string>();
					}
					if (list.Count > 0)
					{
						enumerable = model.FindBoundOperations(entityType).FilterByName(true, pathToken.Identifier).FilterOperationsByParameterNames(list, false);
					}
					else
					{
						enumerable = model.FindBoundOperations(entityType).FilterByName(true, pathToken.Identifier);
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
			if (enumerable.Count<IEdmOperation>() > 1)
			{
				enumerable = enumerable.FilterBoundOperationsWithSameTypeHierarchyToTypeClosestToBindingType(entityType);
			}
			if (enumerable.Count<IEdmOperation>() > 1 && list.Count > 0)
			{
				enumerable = enumerable.FindBestOverloadBasedOnParameters(list, false);
			}
			if (!enumerable.HasAny<IEdmOperation>())
			{
				segment = null;
				return false;
			}
			enumerable.EnsureOperationsBoundWithBindingParameter();
			segment = new OperationSegment(enumerable, null);
			return true;
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x0002B744 File Offset: 0x00029944
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

		// Token: 0x06001050 RID: 4176 RVA: 0x0002B7A8 File Offset: 0x000299A8
		private static bool TryBindAsDeclaredTerm(PathSegmentToken tokenIn, IEdmModel model, ODataUriResolver resolver, out ODataPathSegment segment)
		{
			if (!UriParserHelper.IsAnnotation(tokenIn.Identifier))
			{
				segment = null;
				return false;
			}
			IEdmTerm edmTerm = resolver.ResolveTerm(model, tokenIn.Identifier.Remove(0, 1));
			if (edmTerm == null)
			{
				segment = null;
				return false;
			}
			segment = new AnnotationSegment(edmTerm);
			return true;
		}
	}
}
