using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002FC RID: 764
	public sealed class ItemGroup<TItem> where TItem : Item
	{
		// Token: 0x06001B16 RID: 6934 RVA: 0x00048956 File Offset: 0x00047956
		internal ItemGroup(string groupIndex, IList<TItem> items)
		{
			EwsUtilities.Assert(items != null, "ItemGroup.ctor", "items is null");
			this.GroupIndex = groupIndex;
			this.Items = new Collection<TItem>(items);
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x06001B17 RID: 6935 RVA: 0x00048987 File Offset: 0x00047987
		// (set) Token: 0x06001B18 RID: 6936 RVA: 0x0004898F File Offset: 0x0004798F
		public string GroupIndex { get; private set; }

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x06001B19 RID: 6937 RVA: 0x00048998 File Offset: 0x00047998
		// (set) Token: 0x06001B1A RID: 6938 RVA: 0x000489A0 File Offset: 0x000479A0
		public Collection<TItem> Items { get; private set; }
	}
}
