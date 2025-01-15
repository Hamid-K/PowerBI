using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.OData
{
	// Token: 0x020000BD RID: 189
	internal static class EdmExtensionMethods
	{
		// Token: 0x06000766 RID: 1894 RVA: 0x00015268 File Offset: 0x00013468
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
					if (matchBindingPath.Invoke(edmNavigationPropertyBinding.Path))
					{
						return edmNavigationPropertyBinding.Target;
					}
				}
			}
			return new UnknownEntitySet(navigationSource, navigationProperty);
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x000152E4 File Offset: 0x000134E4
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
					if (matchBindingPath.Invoke(edmNavigationPropertyBinding.Path, parsedSegments))
					{
						bindingPath = edmNavigationPropertyBinding.Path;
						return edmNavigationPropertyBinding.Target;
					}
				}
			}
			return new UnknownEntitySet(navigationSource, navigationProperty);
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x00015370 File Offset: 0x00013570
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
