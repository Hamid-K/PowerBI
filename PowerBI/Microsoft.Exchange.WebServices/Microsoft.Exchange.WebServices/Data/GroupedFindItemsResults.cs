using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002FA RID: 762
	public sealed class GroupedFindItemsResults<TItem> : IEnumerable<ItemGroup<TItem>>, IEnumerable where TItem : Item
	{
		// Token: 0x06001AFE RID: 6910 RVA: 0x00048796 File Offset: 0x00047796
		internal GroupedFindItemsResults()
		{
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06001AFF RID: 6911 RVA: 0x000487A9 File Offset: 0x000477A9
		// (set) Token: 0x06001B00 RID: 6912 RVA: 0x000487B1 File Offset: 0x000477B1
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

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06001B01 RID: 6913 RVA: 0x000487BA File Offset: 0x000477BA
		// (set) Token: 0x06001B02 RID: 6914 RVA: 0x000487C2 File Offset: 0x000477C2
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

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06001B03 RID: 6915 RVA: 0x000487CB File Offset: 0x000477CB
		// (set) Token: 0x06001B04 RID: 6916 RVA: 0x000487D3 File Offset: 0x000477D3
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

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06001B05 RID: 6917 RVA: 0x000487DC File Offset: 0x000477DC
		public Collection<ItemGroup<TItem>> ItemGroups
		{
			get
			{
				return this.itemGroups;
			}
		}

		// Token: 0x06001B06 RID: 6918 RVA: 0x000487E4 File Offset: 0x000477E4
		public IEnumerator<ItemGroup<TItem>> GetEnumerator()
		{
			return this.itemGroups.GetEnumerator();
		}

		// Token: 0x06001B07 RID: 6919 RVA: 0x000487F1 File Offset: 0x000477F1
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.itemGroups.GetEnumerator();
		}

		// Token: 0x04001436 RID: 5174
		private int totalCount;

		// Token: 0x04001437 RID: 5175
		private int? nextPageOffset;

		// Token: 0x04001438 RID: 5176
		private bool moreAvailable;

		// Token: 0x04001439 RID: 5177
		private Collection<ItemGroup<TItem>> itemGroups = new Collection<ItemGroup<TItem>>();
	}
}
