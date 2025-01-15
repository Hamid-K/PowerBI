using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001A4 RID: 420
	public sealed class SelectExpandClause
	{
		// Token: 0x06001410 RID: 5136 RVA: 0x0003AD76 File Offset: 0x00038F76
		public SelectExpandClause(IEnumerable<SelectItem> selectedItems, bool allSelected)
		{
			this.selectedItems = ((selectedItems != null) ? new ReadOnlyCollection<SelectItem>(selectedItems.ToList<SelectItem>()) : new ReadOnlyCollection<SelectItem>(new List<SelectItem>()));
			this.allSelected = new bool?(allSelected);
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06001411 RID: 5137 RVA: 0x0003ADAA File Offset: 0x00038FAA
		public IEnumerable<SelectItem> SelectedItems
		{
			get
			{
				return this.selectedItems.AsEnumerable<SelectItem>();
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06001412 RID: 5138 RVA: 0x0003ADB7 File Offset: 0x00038FB7
		public bool AllSelected
		{
			get
			{
				return this.allSelected.Value;
			}
		}

		// Token: 0x06001413 RID: 5139 RVA: 0x0003ADC4 File Offset: 0x00038FC4
		internal void AddToSelectedItems(SelectItem itemToAdd)
		{
			ExceptionUtils.CheckArgumentNotNull<SelectItem>(itemToAdd, "itemToAdd");
			if (this.selectedItems.Any((SelectItem x) => x is WildcardSelectItem) && SelectExpandClause.IsStructuralOrNavigationPropertySelectionItem(itemToAdd))
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

		// Token: 0x06001414 RID: 5140 RVA: 0x0003AE8C File Offset: 0x0003908C
		internal void SetAllSelected(bool newValue)
		{
			this.allSelected = new bool?(newValue);
		}

		// Token: 0x06001415 RID: 5141 RVA: 0x0003AE9C File Offset: 0x0003909C
		private static bool IsStructuralOrNavigationPropertySelectionItem(SelectItem selectItem)
		{
			PathSelectItem pathSelectItem = selectItem as PathSelectItem;
			return pathSelectItem != null && (pathSelectItem.SelectedPath.LastSegment is NavigationPropertySegment || pathSelectItem.SelectedPath.LastSegment is PropertySegment);
		}

		// Token: 0x06001416 RID: 5142 RVA: 0x0003AEDC File Offset: 0x000390DC
		private static bool IsStructuralSelectionItem(SelectItem selectItem)
		{
			PathSelectItem pathSelectItem = selectItem as PathSelectItem;
			return pathSelectItem != null && pathSelectItem.SelectedPath.LastSegment is PropertySegment;
		}

		// Token: 0x040008D9 RID: 2265
		private ReadOnlyCollection<SelectItem> selectedItems;

		// Token: 0x040008DA RID: 2266
		private bool? allSelected;
	}
}
