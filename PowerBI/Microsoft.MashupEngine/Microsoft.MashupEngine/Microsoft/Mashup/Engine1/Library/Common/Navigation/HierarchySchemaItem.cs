using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common.Navigation
{
	// Token: 0x02001172 RID: 4466
	public sealed class HierarchySchemaItem : HierarchyItem, IEquatable<HierarchySchemaItem>
	{
		// Token: 0x06007516 RID: 29974 RVA: 0x00191895 File Offset: 0x0018FA95
		public HierarchySchemaItem(string catalog, string name, Value description)
			: base(name, HierarchyItem.SchemaKindValue, description)
		{
			this.catalog = catalog;
		}

		// Token: 0x17002086 RID: 8326
		// (get) Token: 0x06007517 RID: 29975 RVA: 0x001918AB File Offset: 0x0018FAAB
		public string CatalogName
		{
			get
			{
				return this.catalog;
			}
		}

		// Token: 0x17002087 RID: 8327
		// (get) Token: 0x06007518 RID: 29976 RVA: 0x001918B3 File Offset: 0x0018FAB3
		public HierarchyCatalogItem CatalogItem
		{
			get
			{
				return new HierarchyCatalogItem(this.CatalogName, Value.Null);
			}
		}

		// Token: 0x06007519 RID: 29977 RVA: 0x001918C5 File Offset: 0x0018FAC5
		public bool Equals(HierarchySchemaItem other)
		{
			return string.Equals(base.Name, other.Name, StringComparison.Ordinal) && string.Equals(this.CatalogName, other.CatalogName, StringComparison.Ordinal);
		}

		// Token: 0x0600751A RID: 29978 RVA: 0x001918EF File Offset: 0x0018FAEF
		public override bool Equals(object other)
		{
			return other != null && this.Equals(other as HierarchySchemaItem);
		}

		// Token: 0x0600751B RID: 29979 RVA: 0x00191902 File Offset: 0x0018FB02
		public override int GetHashCode()
		{
			return ((this.CatalogName == null) ? 211 : this.CatalogName.GetHashCode()) ^ (base.GetHashCode() << 3);
		}

		// Token: 0x0400405B RID: 16475
		private readonly string catalog;
	}
}
