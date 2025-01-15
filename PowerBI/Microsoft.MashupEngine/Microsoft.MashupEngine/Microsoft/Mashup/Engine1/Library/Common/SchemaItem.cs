using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001080 RID: 4224
	public struct SchemaItem : IEquatable<SchemaItem>, IComparable<SchemaItem>
	{
		// Token: 0x06006EA4 RID: 28324 RVA: 0x0017E13A File Offset: 0x0017C33A
		public SchemaItem(string schema, string item, string kind)
		{
			this.schema = schema ?? string.Empty;
			this.item = item ?? string.Empty;
			this.kind = kind;
		}

		// Token: 0x17001F34 RID: 7988
		// (get) Token: 0x06006EA5 RID: 28325 RVA: 0x0017E163 File Offset: 0x0017C363
		public string Schema
		{
			get
			{
				return this.schema;
			}
		}

		// Token: 0x17001F35 RID: 7989
		// (get) Token: 0x06006EA6 RID: 28326 RVA: 0x0017E16B File Offset: 0x0017C36B
		public string Item
		{
			get
			{
				return this.item;
			}
		}

		// Token: 0x17001F36 RID: 7990
		// (get) Token: 0x06006EA7 RID: 28327 RVA: 0x0017E173 File Offset: 0x0017C373
		public string Kind
		{
			get
			{
				return this.kind;
			}
		}

		// Token: 0x17001F37 RID: 7991
		// (get) Token: 0x06006EA8 RID: 28328 RVA: 0x0017E17B File Offset: 0x0017C37B
		public string Identifier
		{
			get
			{
				return DbEnvironment.CreateIdentifierName(this.schema, this.item);
			}
		}

		// Token: 0x06006EA9 RID: 28329 RVA: 0x0017E18E File Offset: 0x0017C38E
		public override int GetHashCode()
		{
			return this.schema.GetHashCode() ^ (this.item.GetHashCode() << 3);
		}

		// Token: 0x06006EAA RID: 28330 RVA: 0x0017E1AC File Offset: 0x0017C3AC
		public override bool Equals(object obj)
		{
			SchemaItem? schemaItem = obj as SchemaItem?;
			return schemaItem != null && this.Equals(schemaItem.Value);
		}

		// Token: 0x06006EAB RID: 28331 RVA: 0x0017E1DD File Offset: 0x0017C3DD
		public bool Equals(SchemaItem other)
		{
			return this.schema == other.schema && this.item == other.item;
		}

		// Token: 0x06006EAC RID: 28332 RVA: 0x0017E208 File Offset: 0x0017C408
		public override string ToString()
		{
			return string.Concat(new string[] { "[", this.schema, "/", this.item, "/", this.kind, "]" });
		}

		// Token: 0x06006EAD RID: 28333 RVA: 0x0017E25B File Offset: 0x0017C45B
		public int CompareTo(SchemaItem other)
		{
			return this.CompareTo(other, StringComparison.CurrentCulture);
		}

		// Token: 0x06006EAE RID: 28334 RVA: 0x0017E268 File Offset: 0x0017C468
		public int CompareTo(SchemaItem other, StringComparison comparison)
		{
			int num = string.Compare(this.schema, other.schema, comparison);
			if (num != 0)
			{
				return num;
			}
			return string.Compare(this.item, other.item, comparison);
		}

		// Token: 0x04003D62 RID: 15714
		public static readonly IComparer<SchemaItem> Comparer = new SchemaItem.SchemaItemComparer();

		// Token: 0x04003D63 RID: 15715
		private readonly string schema;

		// Token: 0x04003D64 RID: 15716
		private readonly string item;

		// Token: 0x04003D65 RID: 15717
		private readonly string kind;

		// Token: 0x02001081 RID: 4225
		private class SchemaItemComparer : IComparer<SchemaItem>
		{
			// Token: 0x06006EB0 RID: 28336 RVA: 0x0017E2AC File Offset: 0x0017C4AC
			public int Compare(SchemaItem x, SchemaItem y)
			{
				int num = x.CompareTo(y, StringComparison.CurrentCulture);
				if (num == 0)
				{
					num = x.CompareTo(y, StringComparison.Ordinal);
				}
				return num;
			}
		}
	}
}
