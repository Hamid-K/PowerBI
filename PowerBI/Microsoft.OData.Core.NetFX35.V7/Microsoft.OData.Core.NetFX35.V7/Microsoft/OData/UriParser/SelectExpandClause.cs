using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000158 RID: 344
	public sealed class SelectExpandClause
	{
		// Token: 0x06000EEB RID: 3819 RVA: 0x0002AE2E File Offset: 0x0002902E
		public SelectExpandClause(IEnumerable<SelectItem> selectedItems, bool allSelected)
		{
			this.selectedItems = ((selectedItems != null) ? new ReadOnlyCollection<SelectItem>(Enumerable.ToList<SelectItem>(selectedItems)) : new ReadOnlyCollection<SelectItem>(new List<SelectItem>()));
			this.allSelected = new bool?(allSelected);
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000EEC RID: 3820 RVA: 0x0002AE62 File Offset: 0x00029062
		public IEnumerable<SelectItem> SelectedItems
		{
			get
			{
				return Enumerable.AsEnumerable<SelectItem>(this.selectedItems);
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000EED RID: 3821 RVA: 0x0002AE6F File Offset: 0x0002906F
		public bool AllSelected
		{
			get
			{
				return this.allSelected.Value;
			}
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x0002AE7C File Offset: 0x0002907C
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

		// Token: 0x06000EEF RID: 3823 RVA: 0x0002AF44 File Offset: 0x00029144
		internal void SetAllSelected(bool newValue)
		{
			this.allSelected = new bool?(newValue);
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x0002AF54 File Offset: 0x00029154
		private static bool IsStructuralOrNavigationPropertySelectionItem(SelectItem selectItem)
		{
			PathSelectItem pathSelectItem = selectItem as PathSelectItem;
			return pathSelectItem != null && (pathSelectItem.SelectedPath.LastSegment is NavigationPropertySegment || pathSelectItem.SelectedPath.LastSegment is PropertySegment);
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x0002AF94 File Offset: 0x00029194
		private static bool IsStructuralSelectionItem(SelectItem selectItem)
		{
			PathSelectItem pathSelectItem = selectItem as PathSelectItem;
			return pathSelectItem != null && pathSelectItem.SelectedPath.LastSegment is PropertySegment;
		}

		// Token: 0x04000795 RID: 1941
		private ReadOnlyCollection<SelectItem> selectedItems;

		// Token: 0x04000796 RID: 1942
		private bool? allSelected;
	}
}
