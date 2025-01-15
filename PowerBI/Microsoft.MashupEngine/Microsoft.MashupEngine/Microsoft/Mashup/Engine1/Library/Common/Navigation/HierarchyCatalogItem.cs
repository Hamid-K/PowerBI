using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common.Navigation
{
	// Token: 0x02001170 RID: 4464
	public sealed class HierarchyCatalogItem : HierarchyItem, IEquatable<HierarchyCatalogItem>
	{
		// Token: 0x06007508 RID: 29960 RVA: 0x00191723 File Offset: 0x0018F923
		public HierarchyCatalogItem(string name, Value description)
			: base(name, HierarchyItem.DatabaseKindValue, description)
		{
		}

		// Token: 0x06007509 RID: 29961 RVA: 0x00191732 File Offset: 0x0018F932
		public bool Equals(HierarchyCatalogItem other)
		{
			return string.Equals(base.Name, other.Name, StringComparison.Ordinal);
		}

		// Token: 0x0600750A RID: 29962 RVA: 0x00191746 File Offset: 0x0018F946
		public override bool Equals(object other)
		{
			return other != null && this.Equals(other as HierarchyCatalogItem);
		}

		// Token: 0x0600750B RID: 29963 RVA: 0x00191759 File Offset: 0x0018F959
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
