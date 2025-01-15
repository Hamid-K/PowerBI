using System;
using System.Collections.Generic;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Query.Expressions
{
	// Token: 0x020000F2 RID: 242
	internal class SelectExpandIncludedProperty
	{
		// Token: 0x0600083C RID: 2108 RVA: 0x0002024C File Offset: 0x0001E44C
		public SelectExpandIncludedProperty(PropertySegment propertySegment)
			: this(propertySegment, null)
		{
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x00020256 File Offset: 0x0001E456
		public SelectExpandIncludedProperty(PropertySegment propertySegment, IEdmNavigationSource navigationSource)
		{
			if (propertySegment == null)
			{
				throw Error.ArgumentNull("propertySegment");
			}
			this._propertySegment = propertySegment;
			this._navigationSource = navigationSource;
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x0002027C File Offset: 0x0001E47C
		public PathSelectItem ToPathSelectItem()
		{
			if (this._subSelectItems == null)
			{
				return this._propertySelectItem;
			}
			bool flag = false;
			if (this._propertySelectItem != null && this._propertySelectItem.SelectAndExpand != null)
			{
				flag = this._propertySelectItem.SelectAndExpand.AllSelected;
				foreach (SelectItem selectItem in this._propertySelectItem.SelectAndExpand.SelectedItems)
				{
					this._subSelectItems.Add(selectItem);
				}
			}
			if (!flag)
			{
				flag = true;
				foreach (SelectItem selectItem2 in this._subSelectItems)
				{
					if (!(selectItem2 is ExpandedNavigationSelectItem) && !(selectItem2 is ExpandedReferenceSelectItem))
					{
						flag = false;
						break;
					}
				}
			}
			SelectExpandClause selectExpandClause = new SelectExpandClause(this._subSelectItems, flag);
			if (this._propertySelectItem == null && selectExpandClause == null)
			{
				return null;
			}
			if (this._propertySelectItem == null)
			{
				return new PathSelectItem(new ODataSelectPath(new ODataPathSegment[] { this._propertySegment }), this._navigationSource, selectExpandClause, null, null, null, null, null, null, null);
			}
			return new PathSelectItem(new ODataSelectPath(new ODataPathSegment[] { this._propertySegment }), this._navigationSource, selectExpandClause, this._propertySelectItem.FilterOption, this._propertySelectItem.OrderByOption, this._propertySelectItem.TopOption, this._propertySelectItem.SkipOption, this._propertySelectItem.CountOption, this._propertySelectItem.SearchOption, this._propertySelectItem.ComputeOption);
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x00020434 File Offset: 0x0001E634
		public void AddSubSelectItem(IList<ODataPathSegment> remainingSegments, PathSelectItem oldSelectItem)
		{
			if (remainingSegments == null)
			{
				this._propertySelectItem = oldSelectItem;
				return;
			}
			if (this._subSelectItems == null)
			{
				this._subSelectItems = new List<SelectItem>();
			}
			this._subSelectItems.Add(new PathSelectItem(new ODataSelectPath(remainingSegments), oldSelectItem.NavigationSource, oldSelectItem.SelectAndExpand, oldSelectItem.FilterOption, oldSelectItem.OrderByOption, oldSelectItem.TopOption, oldSelectItem.SkipOption, oldSelectItem.CountOption, oldSelectItem.SearchOption, oldSelectItem.ComputeOption));
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x000204AC File Offset: 0x0001E6AC
		public void AddSubExpandItem(IList<ODataPathSegment> remainingSegments, ExpandedReferenceSelectItem oldRefItem)
		{
			if (this._subSelectItems == null)
			{
				this._subSelectItems = new List<SelectItem>();
			}
			ODataExpandPath odataExpandPath = new ODataExpandPath(remainingSegments);
			ExpandedNavigationSelectItem expandedNavigationSelectItem = oldRefItem as ExpandedNavigationSelectItem;
			if (expandedNavigationSelectItem != null)
			{
				this._subSelectItems.Add(new ExpandedNavigationSelectItem(odataExpandPath, expandedNavigationSelectItem.NavigationSource, expandedNavigationSelectItem.SelectAndExpand, expandedNavigationSelectItem.FilterOption, expandedNavigationSelectItem.OrderByOption, expandedNavigationSelectItem.TopOption, expandedNavigationSelectItem.SkipOption, expandedNavigationSelectItem.CountOption, expandedNavigationSelectItem.SearchOption, expandedNavigationSelectItem.LevelsOption, expandedNavigationSelectItem.ComputeOption, expandedNavigationSelectItem.ApplyOption));
				return;
			}
			this._subSelectItems.Add(new ExpandedReferenceSelectItem(odataExpandPath, oldRefItem.NavigationSource, oldRefItem.FilterOption, oldRefItem.OrderByOption, oldRefItem.TopOption, oldRefItem.SkipOption, oldRefItem.CountOption, oldRefItem.SearchOption, oldRefItem.ComputeOption, oldRefItem.ApplyOption));
		}

		// Token: 0x0400026B RID: 619
		private PropertySegment _propertySegment;

		// Token: 0x0400026C RID: 620
		private IEdmNavigationSource _navigationSource;

		// Token: 0x0400026D RID: 621
		private PathSelectItem _propertySelectItem;

		// Token: 0x0400026E RID: 622
		private IList<SelectItem> _subSelectItems;
	}
}
