using System;
using System.Data;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x0200010A RID: 266
	internal sealed class _SqlMetaData : SqlMetaDataPriv, ICloneable
	{
		// Token: 0x06001575 RID: 5493 RVA: 0x0005E478 File Offset: 0x0005C678
		internal _SqlMetaData(int ordinal)
		{
			this.ordinal = ordinal;
		}

		// Token: 0x06001576 RID: 5494 RVA: 0x0005E487 File Offset: 0x0005C687
		private bool HasFlag(_SqlMetaData._SqlMetadataFlags flag)
		{
			return (this.flags & flag) > _SqlMetaData._SqlMetadataFlags.None;
		}

		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x06001577 RID: 5495 RVA: 0x0005E494 File Offset: 0x0005C694
		internal string serverName
		{
			get
			{
				return this.multiPartTableName.ServerName;
			}
		}

		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x06001578 RID: 5496 RVA: 0x0005E4A1 File Offset: 0x0005C6A1
		internal string catalogName
		{
			get
			{
				return this.multiPartTableName.CatalogName;
			}
		}

		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x06001579 RID: 5497 RVA: 0x0005E4AE File Offset: 0x0005C6AE
		internal string schemaName
		{
			get
			{
				return this.multiPartTableName.SchemaName;
			}
		}

		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x0600157A RID: 5498 RVA: 0x0005E4BB File Offset: 0x0005C6BB
		internal string tableName
		{
			get
			{
				return this.multiPartTableName.TableName;
			}
		}

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x0600157B RID: 5499 RVA: 0x0005E4C8 File Offset: 0x0005C6C8
		// (set) Token: 0x0600157C RID: 5500 RVA: 0x0005E4D3 File Offset: 0x0005C6D3
		public byte Updatability
		{
			get
			{
				return (byte)(this.flags & _SqlMetaData._SqlMetadataFlags.IsUpdatableMask);
			}
			set
			{
				this.flags = (_SqlMetaData._SqlMetadataFlags)(value & 3) | (this.flags & ~(_SqlMetaData._SqlMetadataFlags.Updatable | _SqlMetaData._SqlMetadataFlags.UpdateableUnknown));
			}
		}

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x0600157D RID: 5501 RVA: 0x0005E4E8 File Offset: 0x0005C6E8
		public bool IsReadOnly
		{
			get
			{
				return !this.HasFlag(_SqlMetaData._SqlMetadataFlags.IsUpdatableMask);
			}
		}

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x0600157E RID: 5502 RVA: 0x0005E4F4 File Offset: 0x0005C6F4
		// (set) Token: 0x0600157F RID: 5503 RVA: 0x0005E4FD File Offset: 0x0005C6FD
		public bool IsDifferentName
		{
			get
			{
				return this.HasFlag(_SqlMetaData._SqlMetadataFlags.IsDifferentName);
			}
			set
			{
				this.Set(_SqlMetaData._SqlMetadataFlags.IsDifferentName, value);
			}
		}

		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x06001580 RID: 5504 RVA: 0x0005E507 File Offset: 0x0005C707
		// (set) Token: 0x06001581 RID: 5505 RVA: 0x0005E510 File Offset: 0x0005C710
		public bool IsKey
		{
			get
			{
				return this.HasFlag(_SqlMetaData._SqlMetadataFlags.IsKey);
			}
			set
			{
				this.Set(_SqlMetaData._SqlMetadataFlags.IsKey, value);
			}
		}

		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x06001582 RID: 5506 RVA: 0x0005E51A File Offset: 0x0005C71A
		// (set) Token: 0x06001583 RID: 5507 RVA: 0x0005E524 File Offset: 0x0005C724
		public bool IsHidden
		{
			get
			{
				return this.HasFlag(_SqlMetaData._SqlMetadataFlags.IsHidden);
			}
			set
			{
				this.Set(_SqlMetaData._SqlMetadataFlags.IsHidden, value);
			}
		}

		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x06001584 RID: 5508 RVA: 0x0005E52F File Offset: 0x0005C72F
		// (set) Token: 0x06001585 RID: 5509 RVA: 0x0005E539 File Offset: 0x0005C739
		public bool IsExpression
		{
			get
			{
				return this.HasFlag(_SqlMetaData._SqlMetadataFlags.IsExpression);
			}
			set
			{
				this.Set(_SqlMetaData._SqlMetadataFlags.IsExpression, value);
			}
		}

		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x06001586 RID: 5510 RVA: 0x0005E544 File Offset: 0x0005C744
		// (set) Token: 0x06001587 RID: 5511 RVA: 0x0005E54E File Offset: 0x0005C74E
		public bool IsIdentity
		{
			get
			{
				return this.HasFlag(_SqlMetaData._SqlMetadataFlags.IsIdentity);
			}
			set
			{
				this.Set(_SqlMetaData._SqlMetadataFlags.IsIdentity, value);
			}
		}

		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x06001588 RID: 5512 RVA: 0x0005E559 File Offset: 0x0005C759
		// (set) Token: 0x06001589 RID: 5513 RVA: 0x0005E566 File Offset: 0x0005C766
		public bool IsColumnSet
		{
			get
			{
				return this.HasFlag(_SqlMetaData._SqlMetadataFlags.IsColumnSet);
			}
			set
			{
				this.Set(_SqlMetaData._SqlMetadataFlags.IsColumnSet, value);
			}
		}

		// Token: 0x0600158A RID: 5514 RVA: 0x0005E574 File Offset: 0x0005C774
		private void Set(_SqlMetaData._SqlMetadataFlags flag, bool value)
		{
			this.flags = (value ? (this.flags | flag) : (this.flags & ~flag));
		}

		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x0600158B RID: 5515 RVA: 0x0005E592 File Offset: 0x0005C792
		internal bool Is2008DateTimeType
		{
			get
			{
				return SqlDbType.Date == this.type || SqlDbType.Time == this.type || SqlDbType.DateTime2 == this.type || SqlDbType.DateTimeOffset == this.type;
			}
		}

		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x0600158C RID: 5516 RVA: 0x0005E5BE File Offset: 0x0005C7BE
		internal bool IsLargeUdt
		{
			get
			{
				return this.type == SqlDbType.Udt && this.length == int.MaxValue;
			}
		}

		// Token: 0x0600158D RID: 5517 RVA: 0x0005E5DC File Offset: 0x0005C7DC
		public object Clone()
		{
			_SqlMetaData sqlMetaData = new _SqlMetaData(this.ordinal);
			sqlMetaData.CopyFrom(this);
			sqlMetaData.column = this.column;
			sqlMetaData.baseColumn = this.baseColumn;
			sqlMetaData.multiPartTableName = this.multiPartTableName;
			sqlMetaData.tableNum = this.tableNum;
			sqlMetaData.flags = this.flags;
			sqlMetaData.op = this.op;
			sqlMetaData.operand = this.operand;
			return sqlMetaData;
		}

		// Token: 0x0400087F RID: 2175
		internal string column;

		// Token: 0x04000880 RID: 2176
		internal string baseColumn;

		// Token: 0x04000881 RID: 2177
		internal MultiPartTableName multiPartTableName;

		// Token: 0x04000882 RID: 2178
		internal readonly int ordinal;

		// Token: 0x04000883 RID: 2179
		internal byte tableNum;

		// Token: 0x04000884 RID: 2180
		internal byte op;

		// Token: 0x04000885 RID: 2181
		internal ushort operand;

		// Token: 0x04000886 RID: 2182
		private _SqlMetaData._SqlMetadataFlags flags;

		// Token: 0x02000264 RID: 612
		[Flags]
		private enum _SqlMetadataFlags
		{
			// Token: 0x040016E9 RID: 5865
			None = 0,
			// Token: 0x040016EA RID: 5866
			Updatable = 1,
			// Token: 0x040016EB RID: 5867
			UpdateableUnknown = 2,
			// Token: 0x040016EC RID: 5868
			IsDifferentName = 4,
			// Token: 0x040016ED RID: 5869
			IsKey = 8,
			// Token: 0x040016EE RID: 5870
			IsHidden = 16,
			// Token: 0x040016EF RID: 5871
			IsExpression = 32,
			// Token: 0x040016F0 RID: 5872
			IsIdentity = 64,
			// Token: 0x040016F1 RID: 5873
			IsColumnSet = 128,
			// Token: 0x040016F2 RID: 5874
			IsUpdatableMask = 3
		}
	}
}
