using System;

namespace Microsoft.OleDb
{
	// Token: 0x02001E82 RID: 7810
	public class ColumnInfo
	{
		// Token: 0x0600C0F7 RID: 49399 RVA: 0x0026CEE4 File Offset: 0x0026B0E4
		public ColumnInfo(ColumnID columnID, DBORDINAL ordinal, DBCOLUMNFLAGS flags, DBLENGTH columnSize, DBTYPE type, byte precision, byte scale)
		{
			this.columnID = columnID;
			this.ordinal = ordinal;
			this.flags = flags;
			this.columnSize = columnSize;
			this.type = type;
			this.precision = precision;
			this.scale = scale;
		}

		// Token: 0x17002F2A RID: 12074
		// (get) Token: 0x0600C0F8 RID: 49400 RVA: 0x0026CF21 File Offset: 0x0026B121
		public ColumnID ColumnID
		{
			get
			{
				return this.columnID;
			}
		}

		// Token: 0x17002F2B RID: 12075
		// (get) Token: 0x0600C0F9 RID: 49401 RVA: 0x0026CF29 File Offset: 0x0026B129
		public DBORDINAL Ordinal
		{
			get
			{
				return this.ordinal;
			}
		}

		// Token: 0x17002F2C RID: 12076
		// (get) Token: 0x0600C0FA RID: 49402 RVA: 0x0026CF31 File Offset: 0x0026B131
		public DBCOLUMNFLAGS Flags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x17002F2D RID: 12077
		// (get) Token: 0x0600C0FB RID: 49403 RVA: 0x0026CF39 File Offset: 0x0026B139
		public DBLENGTH ColumnSize
		{
			get
			{
				return this.columnSize;
			}
		}

		// Token: 0x17002F2E RID: 12078
		// (get) Token: 0x0600C0FC RID: 49404 RVA: 0x0026CF41 File Offset: 0x0026B141
		public DBTYPE Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17002F2F RID: 12079
		// (get) Token: 0x0600C0FD RID: 49405 RVA: 0x0026CF49 File Offset: 0x0026B149
		public byte Precision
		{
			get
			{
				return this.precision;
			}
		}

		// Token: 0x17002F30 RID: 12080
		// (get) Token: 0x0600C0FE RID: 49406 RVA: 0x0026CF51 File Offset: 0x0026B151
		public byte Scale
		{
			get
			{
				return this.scale;
			}
		}

		// Token: 0x0400615E RID: 24926
		private readonly ColumnID columnID;

		// Token: 0x0400615F RID: 24927
		private readonly DBORDINAL ordinal;

		// Token: 0x04006160 RID: 24928
		private readonly DBCOLUMNFLAGS flags;

		// Token: 0x04006161 RID: 24929
		private readonly DBLENGTH columnSize;

		// Token: 0x04006162 RID: 24930
		private readonly DBTYPE type;

		// Token: 0x04006163 RID: 24931
		private readonly byte precision;

		// Token: 0x04006164 RID: 24932
		private readonly byte scale;
	}
}
