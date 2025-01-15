using System;
using System.Data;
using System.Text;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x0200010E RID: 270
	internal class SqlMetaDataPriv
	{
		// Token: 0x060015A8 RID: 5544 RVA: 0x0005EA0E File Offset: 0x0005CC0E
		internal SqlMetaDataPriv()
		{
		}

		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x060015A9 RID: 5545 RVA: 0x0005EA2C File Offset: 0x0005CC2C
		// (set) Token: 0x060015AA RID: 5546 RVA: 0x0005EA35 File Offset: 0x0005CC35
		public bool IsNullable
		{
			get
			{
				return this.HasFlag(SqlMetaDataPriv.SqlMetaDataPrivFlags.IsNullable);
			}
			set
			{
				this.Set(SqlMetaDataPriv.SqlMetaDataPrivFlags.IsNullable, value);
			}
		}

		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x060015AB RID: 5547 RVA: 0x0005EA3F File Offset: 0x0005CC3F
		// (set) Token: 0x060015AC RID: 5548 RVA: 0x0005EA48 File Offset: 0x0005CC48
		public bool IsMultiValued
		{
			get
			{
				return this.HasFlag(SqlMetaDataPriv.SqlMetaDataPrivFlags.IsMultiValued);
			}
			set
			{
				this.Set(SqlMetaDataPriv.SqlMetaDataPrivFlags.IsMultiValued, value);
			}
		}

		// Token: 0x060015AD RID: 5549 RVA: 0x0005EA52 File Offset: 0x0005CC52
		private bool HasFlag(SqlMetaDataPriv.SqlMetaDataPrivFlags flag)
		{
			return (this.flags & flag) > SqlMetaDataPriv.SqlMetaDataPrivFlags.None;
		}

		// Token: 0x060015AE RID: 5550 RVA: 0x0005EA5F File Offset: 0x0005CC5F
		private void Set(SqlMetaDataPriv.SqlMetaDataPrivFlags flag, bool value)
		{
			this.flags = (value ? (this.flags | flag) : (this.flags & ~flag));
		}

		// Token: 0x060015AF RID: 5551 RVA: 0x0005EA80 File Offset: 0x0005CC80
		internal virtual void CopyFrom(SqlMetaDataPriv original)
		{
			this.type = original.type;
			this.tdsType = original.tdsType;
			this.precision = original.precision;
			this.scale = original.scale;
			this.length = original.length;
			this.collation = original.collation;
			this.codePage = original.codePage;
			this.encoding = original.encoding;
			this.metaType = original.metaType;
			this.flags = original.flags;
			if (original.udt != null)
			{
				this.udt = new SqlMetaDataUdt();
				this.udt.CopyFrom(original.udt);
			}
			if (original.xmlSchemaCollection != null)
			{
				this.xmlSchemaCollection = new SqlMetaDataXmlSchemaCollection();
				this.xmlSchemaCollection.CopyFrom(original.xmlSchemaCollection);
			}
		}

		// Token: 0x060015B0 RID: 5552 RVA: 0x0005EB4D File Offset: 0x0005CD4D
		internal bool IsAlgorithmInitialized()
		{
			return this.cipherMD != null && this.cipherMD.IsAlgorithmInitialized();
		}

		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x060015B1 RID: 5553 RVA: 0x0005EB64 File Offset: 0x0005CD64
		internal byte NormalizationRuleVersion
		{
			get
			{
				if (this.cipherMD != null)
				{
					return this.cipherMD.NormalizationRuleVersion;
				}
				return 0;
			}
		}

		// Token: 0x04000897 RID: 2199
		internal SqlDbType type;

		// Token: 0x04000898 RID: 2200
		internal byte tdsType;

		// Token: 0x04000899 RID: 2201
		internal byte precision = byte.MaxValue;

		// Token: 0x0400089A RID: 2202
		internal byte scale = byte.MaxValue;

		// Token: 0x0400089B RID: 2203
		private SqlMetaDataPriv.SqlMetaDataPrivFlags flags;

		// Token: 0x0400089C RID: 2204
		internal int length;

		// Token: 0x0400089D RID: 2205
		internal SqlCollation collation;

		// Token: 0x0400089E RID: 2206
		internal int codePage;

		// Token: 0x0400089F RID: 2207
		internal Encoding encoding;

		// Token: 0x040008A0 RID: 2208
		internal MetaType metaType;

		// Token: 0x040008A1 RID: 2209
		public SqlMetaDataUdt udt;

		// Token: 0x040008A2 RID: 2210
		public SqlMetaDataXmlSchemaCollection xmlSchemaCollection;

		// Token: 0x040008A3 RID: 2211
		internal bool isEncrypted;

		// Token: 0x040008A4 RID: 2212
		internal SqlMetaDataPriv baseTI;

		// Token: 0x040008A5 RID: 2213
		internal SqlCipherMetadata cipherMD;

		// Token: 0x02000265 RID: 613
		[Flags]
		private enum SqlMetaDataPrivFlags : byte
		{
			// Token: 0x040016F4 RID: 5876
			None = 0,
			// Token: 0x040016F5 RID: 5877
			IsNullable = 2,
			// Token: 0x040016F6 RID: 5878
			IsMultiValued = 4
		}
	}
}
