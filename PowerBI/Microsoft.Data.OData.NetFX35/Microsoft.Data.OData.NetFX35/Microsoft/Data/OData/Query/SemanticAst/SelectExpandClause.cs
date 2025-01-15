using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Data.OData.Query.SemanticAst
{
	// Token: 0x02000077 RID: 119
	public sealed class SelectExpandClause
	{
		// Token: 0x060002C8 RID: 712 RVA: 0x0000A8D6 File Offset: 0x00008AD6
		public SelectExpandClause(ICollection<SelectItem> selectedItems, bool allSelected)
		{
			this.usedInternalLegacyConsturctor = false;
			this.selectedItems = selectedItems;
			this.allSelected = new bool?(allSelected);
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000A8F8 File Offset: 0x00008AF8
		internal SelectExpandClause(Selection selection, Expansion expansion)
		{
			this.usedInternalLegacyConsturctor = true;
			this.selection = selection;
			this.expansion = expansion ?? new Expansion(new List<ExpandedNavigationSelectItem>());
			this.selectedItems = null;
			this.allSelected = default(bool?);
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060002CA RID: 714 RVA: 0x0000A936 File Offset: 0x00008B36
		public IEnumerable<SelectItem> SelectedItems
		{
			get
			{
				return this.selectedItems;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060002CB RID: 715 RVA: 0x0000A93E File Offset: 0x00008B3E
		public bool AllSelected
		{
			get
			{
				if (this.usedInternalLegacyConsturctor)
				{
					return this.Selection is AllSelection;
				}
				return this.allSelected.Value;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060002CC RID: 716 RVA: 0x0000A962 File Offset: 0x00008B62
		internal Selection Selection
		{
			get
			{
				return this.selection;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060002CD RID: 717 RVA: 0x0000A96A File Offset: 0x00008B6A
		internal Expansion Expansion
		{
			get
			{
				return this.expansion;
			}
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000A980 File Offset: 0x00008B80
		internal void AddSelectItem(SelectItem itemToAdd)
		{
			ExceptionUtils.CheckArgumentNotNull<SelectItem>(itemToAdd, "itemToAdd");
			if (this.selection is AllSelection)
			{
				return;
			}
			if (this.selection is ExpansionsOnly || this.selection is UnknownSelection)
			{
				List<SelectItem> list = new List<SelectItem>();
				list.Add(itemToAdd);
				this.selection = new PartialSelection(list);
				return;
			}
			List<SelectItem> list2 = Enumerable.ToList<SelectItem>(((PartialSelection)this.selection).SelectedItems);
			if (itemToAdd is WildcardSelectItem)
			{
				IEnumerable<SelectItem> enumerable = Enumerable.ToArray<SelectItem>(Enumerable.Where<SelectItem>(list2, new Func<SelectItem, bool>(UriUtils.IsStructuralOrNavigationPropertySelectionItem)));
				using (IEnumerator<SelectItem> enumerator = enumerable.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						SelectItem selectItem = enumerator.Current;
						list2.Remove(selectItem);
					}
					goto IL_00E2;
				}
			}
			if (UriUtils.IsStructuralOrNavigationPropertySelectionItem(itemToAdd))
			{
				if (Enumerable.Any<SelectItem>(list2, (SelectItem item) => item is WildcardSelectItem))
				{
					return;
				}
			}
			IL_00E2:
			list2.Add(itemToAdd);
			this.selection = new PartialSelection(list2);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000AA94 File Offset: 0x00008C94
		internal void SetAllSelectionRecursively()
		{
			this.selection = AllSelection.Instance;
			foreach (ExpandedNavigationSelectItem expandedNavigationSelectItem in this.expansion.ExpandItems)
			{
				expandedNavigationSelectItem.SelectAndExpand.SetAllSelectionRecursively();
			}
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000AAF8 File Offset: 0x00008CF8
		internal void InitializeEmptySelection()
		{
			if (this.selection is UnknownSelection)
			{
				this.selection = ExpansionsOnly.Instance;
			}
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000AB14 File Offset: 0x00008D14
		internal void ComputeFinalSelectedItems()
		{
			if (this.selectedItems != null)
			{
				throw new InvalidOperationException("This should only be called once at the end of processing.");
			}
			this.selectedItems = new List<SelectItem>();
			PartialSelection partialSelection = this.Selection as PartialSelection;
			if (partialSelection != null)
			{
				foreach (SelectItem selectItem in partialSelection.SelectedItems)
				{
					this.selectedItems.Add(selectItem);
				}
			}
			foreach (ExpandedNavigationSelectItem expandedNavigationSelectItem in this.Expansion.ExpandItems)
			{
				if (expandedNavigationSelectItem.SelectAndExpand != null)
				{
					expandedNavigationSelectItem.SelectAndExpand.ComputeFinalSelectedItems();
				}
				this.selectedItems.Add(expandedNavigationSelectItem);
			}
		}

		// Token: 0x040000C1 RID: 193
		private readonly Expansion expansion;

		// Token: 0x040000C2 RID: 194
		private readonly bool usedInternalLegacyConsturctor;

		// Token: 0x040000C3 RID: 195
		private Selection selection;

		// Token: 0x040000C4 RID: 196
		private ICollection<SelectItem> selectedItems;

		// Token: 0x040000C5 RID: 197
		private bool? allSelected;
	}
}
