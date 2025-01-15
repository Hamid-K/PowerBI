using System;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000032 RID: 50
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public class ColumnInfo
	{
		// Token: 0x060001B3 RID: 435 RVA: 0x00005530 File Offset: 0x00003730
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

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x0000556D File Offset: 0x0000376D
		public ColumnID ColumnID
		{
			get
			{
				return this.columnID;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00005575 File Offset: 0x00003775
		public DBORDINAL Ordinal
		{
			get
			{
				return this.ordinal;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x0000557D File Offset: 0x0000377D
		public DBCOLUMNFLAGS Flags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x00005585 File Offset: 0x00003785
		public DBLENGTH ColumnSize
		{
			get
			{
				return this.columnSize;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x0000558D File Offset: 0x0000378D
		public DBTYPE Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00005595 File Offset: 0x00003795
		public byte Precision
		{
			get
			{
				return this.precision;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001BA RID: 442 RVA: 0x0000559D File Offset: 0x0000379D
		public byte Scale
		{
			get
			{
				return this.scale;
			}
		}

		// Token: 0x04000050 RID: 80
		private readonly ColumnID columnID;

		// Token: 0x04000051 RID: 81
		private readonly DBORDINAL ordinal;

		// Token: 0x04000052 RID: 82
		private readonly DBCOLUMNFLAGS flags;

		// Token: 0x04000053 RID: 83
		private readonly DBLENGTH columnSize;

		// Token: 0x04000054 RID: 84
		private readonly DBTYPE type;

		// Token: 0x04000055 RID: 85
		private readonly byte precision;

		// Token: 0x04000056 RID: 86
		private readonly byte scale;
	}
}
