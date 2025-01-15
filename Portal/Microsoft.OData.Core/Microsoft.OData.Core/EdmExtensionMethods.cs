using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.OData
{
	// Token: 0x02000007 RID: 7
	internal static class EdmExtensionMethods
	{
		// Token: 0x0600002B RID: 43 RVA: 0x00002710 File Offset: 0x00000910
		public static IEdmNavigationSource FindNavigationTarget(this IEdmNavigationSource navigationSource, IEdmNavigationProperty navigationProperty, Func<IEdmPathExpression, bool> matchBindingPath)
		{
			if (navigationProperty.ContainsTarget)
			{
				return navigationSource.FindNavigationTarget(navigationProperty);
			}
			IEnumerable<IEdmNavigationPropertyBinding> enumerable = navigationSource.FindNavigationPropertyBindings(navigationProperty);
			if (enumerable != null)
			{
				foreach (IEdmNavigationPropertyBinding edmNavigationPropertyBinding in enumerable)
				{
					if (matchBindingPath(edmNavigationPropertyBinding.Path))
					{
						return edmNavigationPropertyBinding.Target;
					}
				}
			}
			return new UnknownEntitySet(navigationSource, navigationProperty);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000278C File Offset: 0x0000098C
		public static IEdmNavigationSource FindNavigationTarget(this IEdmNavigationSource navigationSource, IEdmNavigationProperty navigationProperty, Func<IEdmPathExpression, List<ODataPathSegment>, bool> matchBindingPath, List<ODataPathSegment> parsedSegments, out IEdmPathExpression bindingPath)
		{
			bindingPath = null;
			if (navigationProperty.ContainsTarget)
			{
				return navigationSource.FindNavigationTarget(navigationProperty);
			}
			IEnumerable<IEdmNavigationPropertyBinding> enumerable = navigationSource.FindNavigationPropertyBindings(navigationProperty);
			if (enumerable != null)
			{
				foreach (IEdmNavigationPropertyBinding edmNavigationPropertyBinding in enumerable)
				{
					if (matchBindingPath(edmNavigationPropertyBinding.Path, parsedSegments))
					{
						bindingPath = edmNavigationPropertyBinding.Path;
						return edmNavigationPropertyBinding.Target;
					}
				}
			}
			return new UnknownEntitySet(navigationSource, navigationProperty);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002818 File Offset: 0x00000A18
		public static bool HasKey(IEdmNavigationSource currentNavigationSource, IEdmStructuredType currentResourceType)
		{
			if (currentResourceType is IEdmComplexType)
			{
				return false;
			}
			if (currentNavigationSource is IEdmEntitySet)
			{
				return true;
			}
			IEdmContainedEntitySet edmContainedEntitySet = currentNavigationSource as IEdmContainedEntitySet;
			return edmContainedEntitySet != null && edmContainedEntitySet.NavigationProperty.Type.TypeKind() == EdmTypeKind.Collection;
		}
	}
}
