using System;

namespace Microsoft.Mashup.Engine1.Library.ActiveDirectory
{
	// Token: 0x02000FCA RID: 4042
	internal class ActiveDirectoryColumnInfo
	{
		// Token: 0x06006A17 RID: 27159 RVA: 0x0016D720 File Offset: 0x0016B920
		private ActiveDirectoryColumnInfo(string[] attributeNames, ColumnType type, bool isPrimaryKey)
		{
			this.attributeNames = attributeNames;
			this.type = type;
			this.isPrimaryKey = isPrimaryKey;
		}

		// Token: 0x17001E75 RID: 7797
		// (get) Token: 0x06006A18 RID: 27160 RVA: 0x0016D73D File Offset: 0x0016B93D
		public string[] AttributeNames
		{
			get
			{
				return this.attributeNames;
			}
		}

		// Token: 0x17001E76 RID: 7798
		// (get) Token: 0x06006A19 RID: 27161 RVA: 0x0016D745 File Offset: 0x0016B945
		public ColumnType ColumnType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17001E77 RID: 7799
		// (get) Token: 0x06006A1A RID: 27162 RVA: 0x0016D74D File Offset: 0x0016B94D
		public bool IsPrimaryKey
		{
			get
			{
				return this.isPrimaryKey;
			}
		}

		// Token: 0x06006A1B RID: 27163 RVA: 0x0016D755 File Offset: 0x0016B955
		public static ActiveDirectoryColumnInfo CreateAttributeColumn(string attributeName, bool isPrimaryKey)
		{
			return new ActiveDirectoryColumnInfo(new string[] { attributeName }, ColumnType.Attribute, isPrimaryKey);
		}

		// Token: 0x06006A1C RID: 27164 RVA: 0x0016D768 File Offset: 0x0016B968
		public static ActiveDirectoryColumnInfo CreateAttributeGroup(string[] attributeNames)
		{
			return new ActiveDirectoryColumnInfo(attributeNames, ColumnType.AttributeGroup, false);
		}

		// Token: 0x04003AE5 RID: 15077
		private readonly string[] attributeNames;

		// Token: 0x04003AE6 RID: 15078
		private readonly ColumnType type;

		// Token: 0x04003AE7 RID: 15079
		private readonly bool isPrimaryKey;
	}
}
