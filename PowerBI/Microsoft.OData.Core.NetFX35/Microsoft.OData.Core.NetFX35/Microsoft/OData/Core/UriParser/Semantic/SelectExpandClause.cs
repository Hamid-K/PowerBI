using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x0200025D RID: 605
	public sealed class SelectExpandClause
	{
		// Token: 0x06001551 RID: 5457 RVA: 0x0004B0F3 File Offset: 0x000492F3
		public SelectExpandClause(IEnumerable<SelectItem> selectedItems, bool allSelected)
		{
			this.selectedItems = ((selectedItems != null) ? new ReadOnlyCollection<SelectItem>(Enumerable.ToList<SelectItem>(selectedItems)) : new ReadOnlyCollection<SelectItem>(new List<SelectItem>()));
			this.allSelected = new bool?(allSelected);
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06001552 RID: 5458 RVA: 0x0004B127 File Offset: 0x00049327
		public IEnumerable<SelectItem> SelectedItems
		{
			get
			{
				return Enumerable.AsEnumerable<SelectItem>(this.selectedItems);
			}
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06001553 RID: 5459 RVA: 0x0004B134 File Offset: 0x00049334
		public bool AllSelected
		{
			get
			{
				return this.allSelected.Value;
			}
		}

		// Token: 0x06001554 RID: 5460 RVA: 0x0004B14C File Offset: 0x0004934C
		internal void AddToSelectedItems(SelectItem itemToAdd)
		{
			ExceptionUtils.CheckArgumentNotNull<SelectItem>(itemToAdd, "itemToAdd");
			if (Enumerable.Any<SelectItem>(this.selectedItems, (SelectItem x) => x is WildcardSelectItem) && SelectExpandClause.IsStructuralOrNavigationPropertySelectionItem(itemToAdd))
			{
				return;
			}
			bool flag = itemToAdd is WildcardSelectItem;
			List<SelectItem> list = new List<SelectItem>();
			foreach (SelectItem selectItem in this.selectedItems)
			{
				if (flag)
				{
					if (!SelectExpandClause.IsStructuralSelectionItem(selectItem))
					{
						list.Add(selectItem);
					}
				}
				else
				{
					list.Add(selectItem);
				}
			}
			list.Add(itemToAdd);
			this.selectedItems = new ReadOnlyCollection<SelectItem>(list);
		}

		// Token: 0x06001555 RID: 5461 RVA: 0x0004B210 File Offset: 0x00049410
		internal void SetAllSelected(bool newValue)
		{
			this.allSelected = new bool?(newValue);
		}

		// Token: 0x06001556 RID: 5462 RVA: 0x0004B220 File Offset: 0x00049420
		private static bool IsStructuralOrNavigationPropertySelectionItem(SelectItem selectItem)
		{
			PathSelectItem pathSelectItem = selectItem as PathSelectItem;
			return pathSelectItem != null && (pathSelectItem.SelectedPath.LastSegment is NavigationPropertySegment || pathSelectItem.SelectedPath.LastSegment is PropertySegment);
		}

		// Token: 0x06001557 RID: 5463 RVA: 0x0004B260 File Offset: 0x00049460
		private static bool IsStructuralSelectionItem(SelectItem selectItem)
		{
			PathSelectItem pathSelectItem = selectItem as PathSelectItem;
			return pathSelectItem != null && pathSelectItem.SelectedPath.LastSegment is PropertySegment;
		}

		// Token: 0x040008E1 RID: 2273
		private ReadOnlyCollection<SelectItem> selectedItems;

		// Token: 0x040008E2 RID: 2274
		private bool? allSelected;
	}
}
