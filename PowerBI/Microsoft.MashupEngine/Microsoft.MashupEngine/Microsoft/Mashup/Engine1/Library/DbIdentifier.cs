using System;
using System.Globalization;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library
{
	// Token: 0x0200023C RID: 572
	internal class DbIdentifier : IComparable<DbIdentifier>
	{
		// Token: 0x060018F1 RID: 6385 RVA: 0x00030FDB File Offset: 0x0002F1DB
		public DbIdentifier(string catalog, string schema, string name)
		{
			this.catalog = catalog;
			this.schema = schema;
			this.name = name;
		}

		// Token: 0x17000C9E RID: 3230
		// (get) Token: 0x060018F2 RID: 6386 RVA: 0x00030FF8 File Offset: 0x0002F1F8
		public string Catalog
		{
			get
			{
				return this.catalog;
			}
		}

		// Token: 0x17000C9F RID: 3231
		// (get) Token: 0x060018F3 RID: 6387 RVA: 0x00031000 File Offset: 0x0002F200
		public string Schema
		{
			get
			{
				return this.schema;
			}
		}

		// Token: 0x17000CA0 RID: 3232
		// (get) Token: 0x060018F4 RID: 6388 RVA: 0x00031008 File Offset: 0x0002F208
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x060018F5 RID: 6389 RVA: 0x00031010 File Offset: 0x0002F210
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DbIdentifier);
		}

		// Token: 0x060018F6 RID: 6390 RVA: 0x0003101E File Offset: 0x0002F21E
		public bool Equals(DbIdentifier tableIdentifier)
		{
			return this.Catalog == tableIdentifier.Catalog && this.Schema == tableIdentifier.Schema && this.Name == tableIdentifier.Name;
		}

		// Token: 0x060018F7 RID: 6391 RVA: 0x0003105C File Offset: 0x0002F25C
		public override int GetHashCode()
		{
			HashBuilder hashBuilder = default(HashBuilder);
			hashBuilder.Add(DbIdentifier.NullableStringHashcode(this.catalog));
			hashBuilder.Add(DbIdentifier.NullableStringHashcode(this.schema));
			hashBuilder.Add(DbIdentifier.NullableStringHashcode(this.name));
			return hashBuilder.ToHash();
		}

		// Token: 0x060018F8 RID: 6392 RVA: 0x000310AE File Offset: 0x0002F2AE
		private static int NullableStringHashcode(string value)
		{
			if (value == null)
			{
				return 342;
			}
			return value.GetHashCode();
		}

		// Token: 0x060018F9 RID: 6393 RVA: 0x000310C0 File Offset: 0x0002F2C0
		public int CompareTo(DbIdentifier other)
		{
			int num = string.CompareOrdinal(this.catalog, other.catalog);
			if (num == 0)
			{
				num = string.CompareOrdinal(this.schema, other.schema);
				if (num == 0)
				{
					num = string.CompareOrdinal(this.name, other.name);
				}
			}
			return num;
		}

		// Token: 0x060018FA RID: 6394 RVA: 0x0003110A File Offset: 0x0002F30A
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}.{1}.{2}", this.catalog ?? "<null>", this.schema ?? "<null>", this.name ?? "<null>");
		}

		// Token: 0x040006A3 RID: 1699
		private readonly string catalog;

		// Token: 0x040006A4 RID: 1700
		private readonly string schema;

		// Token: 0x040006A5 RID: 1701
		private readonly string name;
	}
}
