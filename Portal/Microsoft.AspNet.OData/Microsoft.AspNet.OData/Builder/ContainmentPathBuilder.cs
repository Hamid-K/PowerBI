using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.OData.Routing;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x0200010D RID: 269
	internal class ContainmentPathBuilder
	{
		// Token: 0x06000925 RID: 2341 RVA: 0x000266B4 File Offset: 0x000248B4
		public Microsoft.AspNet.OData.Routing.ODataPath TryComputeCanonicalContainingPath(Microsoft.AspNet.OData.Routing.ODataPath path)
		{
			this._segments = path.Segments.ToList<ODataPathSegment>();
			this.RemoveAllTypeCasts();
			this.RemovePathSegmentsAfterTheLastNavigationProperty();
			this.RemoveRedundantContainingPathSegments();
			this.AddTypeCastsIfNecessary();
			if (this._segments.Count > 0)
			{
				this._segments.RemoveAt(this._segments.Count - 1);
			}
			return new Microsoft.AspNet.OData.Routing.ODataPath(this._segments);
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x0002671C File Offset: 0x0002491C
		private void RemovePathSegmentsAfterTheLastNavigationProperty()
		{
			ODataPathSegment odataPathSegment = this._segments.OfType<NavigationPropertySegment>().LastOrDefault<NavigationPropertySegment>();
			List<ODataPathSegment> list = new List<ODataPathSegment>();
			foreach (ODataPathSegment odataPathSegment2 in this._segments)
			{
				list.Add(odataPathSegment2);
				if (odataPathSegment2 == odataPathSegment)
				{
					break;
				}
			}
			this._segments = list;
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x00026794 File Offset: 0x00024994
		private void RemoveRedundantContainingPathSegments()
		{
			this._segments.Reverse();
			NavigationPropertySegment navigationPropertySegment = null;
			List<ODataPathSegment> list = new List<ODataPathSegment>();
			foreach (ODataPathSegment odataPathSegment in this._segments)
			{
				navigationPropertySegment = odataPathSegment as NavigationPropertySegment;
				if (navigationPropertySegment != null)
				{
					EdmNavigationSourceKind edmNavigationSourceKind = navigationPropertySegment.NavigationSource.NavigationSourceKind();
					if (navigationPropertySegment.NavigationProperty.TargetMultiplicity() == EdmMultiplicity.Many && edmNavigationSourceKind == EdmNavigationSourceKind.EntitySet)
					{
						break;
					}
					if (edmNavigationSourceKind == EdmNavigationSourceKind.Singleton)
					{
						break;
					}
				}
				list.Insert(0, odataPathSegment);
			}
			if (navigationPropertySegment != null)
			{
				IEdmNavigationSource navigationSource = navigationPropertySegment.NavigationSource;
				if (navigationSource.NavigationSourceKind() == EdmNavigationSourceKind.Singleton)
				{
					SingletonSegment singletonSegment = new SingletonSegment((IEdmSingleton)navigationSource);
					list.Insert(0, singletonSegment);
				}
				else
				{
					EntitySetSegment entitySetSegment = new EntitySetSegment((IEdmEntitySet)navigationSource);
					list.Insert(0, entitySetSegment);
				}
			}
			this._segments = list;
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x00026878 File Offset: 0x00024A78
		private void RemoveAllTypeCasts()
		{
			List<ODataPathSegment> list = new List<ODataPathSegment>();
			foreach (ODataPathSegment odataPathSegment in this._segments)
			{
				if (!(odataPathSegment is TypeSegment))
				{
					list.Add(odataPathSegment);
				}
			}
			this._segments = list;
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x000268E0 File Offset: 0x00024AE0
		private void AddTypeCastsIfNecessary()
		{
			IEdmEntityType edmEntityType = null;
			List<ODataPathSegment> list = new List<ODataPathSegment>();
			foreach (ODataPathSegment odataPathSegment in this._segments)
			{
				NavigationPropertySegment navigationPropertySegment = odataPathSegment as NavigationPropertySegment;
				if (navigationPropertySegment != null && edmEntityType != null && edmEntityType.FindProperty(navigationPropertySegment.NavigationProperty.Name) == null)
				{
					TypeSegment typeSegment = new TypeSegment(navigationPropertySegment.NavigationProperty.DeclaringType, null);
					list.Add(typeSegment);
				}
				list.Add(odataPathSegment);
				IEdmEntityType targetEntityType = ContainmentPathBuilder.GetTargetEntityType(odataPathSegment);
				if (targetEntityType != null)
				{
					edmEntityType = targetEntityType;
				}
			}
			this._segments = list;
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x00026990 File Offset: 0x00024B90
		private static IEdmEntityType GetTargetEntityType(ODataPathSegment segment)
		{
			EntitySetSegment entitySetSegment = segment as EntitySetSegment;
			if (entitySetSegment != null)
			{
				return entitySetSegment.EntitySet.EntityType();
			}
			SingletonSegment singletonSegment = segment as SingletonSegment;
			if (singletonSegment != null)
			{
				return singletonSegment.Singleton.EntityType();
			}
			NavigationPropertySegment navigationPropertySegment = segment as NavigationPropertySegment;
			if (navigationPropertySegment != null)
			{
				return navigationPropertySegment.NavigationSource.EntityType();
			}
			return null;
		}

		// Token: 0x040002F3 RID: 755
		private List<ODataPathSegment> _segments;
	}
}
