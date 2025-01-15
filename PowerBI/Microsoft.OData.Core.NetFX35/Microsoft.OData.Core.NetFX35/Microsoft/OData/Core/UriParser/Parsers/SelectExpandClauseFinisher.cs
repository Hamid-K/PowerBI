using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Core.UriParser.Semantic;

namespace Microsoft.OData.Core.UriParser.Parsers
{
	// Token: 0x020001D8 RID: 472
	internal sealed class SelectExpandClauseFinisher
	{
		// Token: 0x0600116C RID: 4460 RVA: 0x0003DDC8 File Offset: 0x0003BFC8
		public static void AddExplicitNavPropLinksWhereNecessary(SelectExpandClause clause)
		{
			IEnumerable<SelectItem> selectedItems = clause.SelectedItems;
			bool flag = Enumerable.Any<SelectItem>(selectedItems, (SelectItem x) => x is PathSelectItem);
			IEnumerable<ODataSelectPath> enumerable = Enumerable.Select<PathSelectItem, ODataSelectPath>(Enumerable.OfType<PathSelectItem>(selectedItems), (PathSelectItem item) => item.SelectedPath);
			using (IEnumerator<SelectItem> enumerator = Enumerable.Where<SelectItem>(selectedItems, (SelectItem I) => I.GetType() == typeof(ExpandedNavigationSelectItem)).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ExpandedNavigationSelectItem navigationSelect2 = (ExpandedNavigationSelectItem)enumerator.Current;
					if (flag)
					{
						if (!Enumerable.Any<ODataSelectPath>(enumerable, (ODataSelectPath x) => x.Equals(navigationSelect2.PathToNavigationProperty.ToSelectPath())))
						{
							clause.AddToSelectedItems(new PathSelectItem(navigationSelect2.PathToNavigationProperty.ToSelectPath()));
						}
					}
					SelectExpandClauseFinisher.AddExplicitNavPropLinksWhereNecessary(navigationSelect2.SelectAndExpand);
				}
			}
			using (IEnumerator<SelectItem> enumerator2 = Enumerable.Where<SelectItem>(selectedItems, (SelectItem I) => I.GetType() == typeof(ExpandedReferenceSelectItem)).GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					ExpandedReferenceSelectItem navigationSelect = (ExpandedReferenceSelectItem)enumerator2.Current;
					if (flag)
					{
						if (!Enumerable.Any<ODataSelectPath>(enumerable, (ODataSelectPath x) => x.Equals(navigationSelect.PathToNavigationProperty.ToSelectPath())))
						{
							clause.AddToSelectedItems(new PathSelectItem(navigationSelect.PathToNavigationProperty.ToSelectPath()));
						}
					}
				}
			}
		}
	}
}
