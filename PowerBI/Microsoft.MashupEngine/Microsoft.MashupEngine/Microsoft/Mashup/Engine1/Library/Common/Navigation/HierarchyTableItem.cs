using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common.Navigation
{
	// Token: 0x02001173 RID: 4467
	public sealed class HierarchyTableItem : HierarchyItem, IEquatable<HierarchyTableItem>
	{
		// Token: 0x0600751C RID: 29980 RVA: 0x00191927 File Offset: 0x0018FB27
		public HierarchyTableItem(string catalog, string schema, string name, TableType type, Value description)
			: base(name, type, description)
		{
			this.catalog = catalog;
			this.schema = schema;
		}

		// Token: 0x17002088 RID: 8328
		// (get) Token: 0x0600751D RID: 29981 RVA: 0x00191942 File Offset: 0x0018FB42
		public string CatalogName
		{
			get
			{
				return this.catalog;
			}
		}

		// Token: 0x17002089 RID: 8329
		// (get) Token: 0x0600751E RID: 29982 RVA: 0x0019194A File Offset: 0x0018FB4A
		public string SchemaName
		{
			get
			{
				return this.schema;
			}
		}

		// Token: 0x1700208A RID: 8330
		// (get) Token: 0x0600751F RID: 29983 RVA: 0x00191952 File Offset: 0x0018FB52
		public HierarchyCatalogItem CatalogItem
		{
			get
			{
				return new HierarchyCatalogItem(this.CatalogName, Value.Null);
			}
		}

		// Token: 0x1700208B RID: 8331
		// (get) Token: 0x06007520 RID: 29984 RVA: 0x00191964 File Offset: 0x0018FB64
		public HierarchySchemaItem SchemaItem
		{
			get
			{
				return new HierarchySchemaItem(this.CatalogName, this.SchemaName, Value.Null);
			}
		}

		// Token: 0x06007521 RID: 29985 RVA: 0x0019197C File Offset: 0x0018FB7C
		public bool Equals(HierarchyTableItem other)
		{
			return string.Equals(base.Name, other.Name, StringComparison.Ordinal) && string.Equals(this.CatalogName, other.CatalogName, StringComparison.Ordinal) && string.Equals(this.SchemaName, other.SchemaName, StringComparison.Ordinal) && base.Kind == other.Kind;
		}

		// Token: 0x06007522 RID: 29986 RVA: 0x001919D5 File Offset: 0x0018FBD5
		public override bool Equals(object other)
		{
			return other != null && this.Equals(other as HierarchyTableItem);
		}

		// Token: 0x06007523 RID: 29987 RVA: 0x001919E8 File Offset: 0x0018FBE8
		public override int GetHashCode()
		{
			int num = ((this.CatalogName == null) ? 613 : this.CatalogName.GetHashCode());
			int num2 = ((this.SchemaName == null) ? 109 : this.SchemaName.GetHashCode());
			return num ^ (num2 << 7) ^ (base.GetHashCode() << 3);
		}

		// Token: 0x0400405C RID: 16476
		private readonly string catalog;

		// Token: 0x0400405D RID: 16477
		private readonly string schema;
	}
}
