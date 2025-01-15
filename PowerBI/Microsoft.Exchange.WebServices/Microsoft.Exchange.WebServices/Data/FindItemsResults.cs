using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002F6 RID: 758
	public sealed class FindItemsResults<TItem> : IEnumerable<TItem>, IEnumerable where TItem : Item
	{
		// Token: 0x06001AC8 RID: 6856 RVA: 0x00048380 File Offset: 0x00047380
		internal FindItemsResults()
		{
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06001AC9 RID: 6857 RVA: 0x0004839E File Offset: 0x0004739E
		// (set) Token: 0x06001ACA RID: 6858 RVA: 0x000483A6 File Offset: 0x000473A6
		public int TotalCount
		{
			get
			{
				return this.totalCount;
			}
			internal set
			{
				this.totalCount = value;
			}
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06001ACB RID: 6859 RVA: 0x000483AF File Offset: 0x000473AF
		// (set) Token: 0x06001ACC RID: 6860 RVA: 0x000483B7 File Offset: 0x000473B7
		public int? NextPageOffset
		{
			get
			{
				return this.nextPageOffset;
			}
			internal set
			{
				this.nextPageOffset = value;
			}
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06001ACD RID: 6861 RVA: 0x000483C0 File Offset: 0x000473C0
		// (set) Token: 0x06001ACE RID: 6862 RVA: 0x000483C8 File Offset: 0x000473C8
		public bool MoreAvailable
		{
			get
			{
				return this.moreAvailable;
			}
			internal set
			{
				this.moreAvailable = value;
			}
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06001ACF RID: 6863 RVA: 0x000483D1 File Offset: 0x000473D1
		public Collection<TItem> Items
		{
			get
			{
				return this.items;
			}
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06001AD0 RID: 6864 RVA: 0x000483D9 File Offset: 0x000473D9
		public Collection<HighlightTerm> HighlightTerms
		{
			get
			{
				return this.highlightTerms;
			}
		}

		// Token: 0x06001AD1 RID: 6865 RVA: 0x000483E1 File Offset: 0x000473E1
		public IEnumerator<TItem> GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x06001AD2 RID: 6866 RVA: 0x000483EE File Offset: 0x000473EE
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x0400142A RID: 5162
		private int totalCount;

		// Token: 0x0400142B RID: 5163
		private int? nextPageOffset;

		// Token: 0x0400142C RID: 5164
		private bool moreAvailable;

		// Token: 0x0400142D RID: 5165
		private Collection<TItem> items = new Collection<TItem>();

		// Token: 0x0400142E RID: 5166
		private Collection<HighlightTerm> highlightTerms = new Collection<HighlightTerm>();
	}
}
