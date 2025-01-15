using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000132 RID: 306
	internal sealed class SelectExpandClauseFinisher
	{
		// Token: 0x06001047 RID: 4167 RVA: 0x0002B118 File Offset: 0x00029318
		public static void AddExplicitNavPropLinksWhereNecessary(SelectExpandClause clause)
		{
			IEnumerable<SelectItem> selectedItems = clause.SelectedItems;
			bool flag = selectedItems.Any((SelectItem x) => x is PathSelectItem);
			IEnumerable<ODataSelectPath> enumerable = from item in selectedItems.OfType<PathSelectItem>()
				select item.SelectedPath;
			foreach (SelectItem selectItem in selectedItems.Where((SelectItem I) => I.GetType() == typeof(ExpandedNavigationSelectItem)))
			{
				ExpandedNavigationSelectItem expandedNavigationSelectItem = (ExpandedNavigationSelectItem)selectItem;
				SelectExpandClauseFinisher.AddExplicitNavPropLinksWhereNecessary(expandedNavigationSelectItem.SelectAndExpand);
			}
			using (IEnumerator<SelectItem> enumerator2 = selectedItems.Where((SelectItem I) => I.GetType() == typeof(ExpandedReferenceSelectItem)).GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					ExpandedReferenceSelectItem navigationSelect = (ExpandedReferenceSelectItem)enumerator2.Current;
					if (flag && !enumerable.Any((ODataSelectPath x) => x.Equals(navigationSelect.PathToNavigationProperty.ToSelectPath())))
					{
						clause.AddToSelectedItems(new PathSelectItem(navigationSelect.PathToNavigationProperty.ToSelectPath()));
					}
				}
			}
		}
	}
}
