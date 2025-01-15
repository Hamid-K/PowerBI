using System;
using Microsoft.Data.Common;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000051 RID: 81
	public sealed class SqlBulkCopyColumnMapping
	{
		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06000818 RID: 2072 RVA: 0x00012B50 File Offset: 0x00010D50
		// (set) Token: 0x06000819 RID: 2073 RVA: 0x00012B68 File Offset: 0x00010D68
		public string DestinationColumn
		{
			get
			{
				if (this._destinationColumnName != null)
				{
					return this._destinationColumnName;
				}
				return string.Empty;
			}
			set
			{
				this._destinationColumnOrdinal = (this._internalDestinationColumnOrdinal = -1);
				this._destinationColumnName = value;
			}
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x0600081A RID: 2074 RVA: 0x00012B8C File Offset: 0x00010D8C
		// (set) Token: 0x0600081B RID: 2075 RVA: 0x00012B94 File Offset: 0x00010D94
		public int DestinationOrdinal
		{
			get
			{
				return this._destinationColumnOrdinal;
			}
			set
			{
				if (value >= 0)
				{
					this._destinationColumnName = null;
					this._internalDestinationColumnOrdinal = value;
					this._destinationColumnOrdinal = value;
					return;
				}
				throw ADP.IndexOutOfRange(value);
			}
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x0600081C RID: 2076 RVA: 0x00012BC3 File Offset: 0x00010DC3
		// (set) Token: 0x0600081D RID: 2077 RVA: 0x00012BDC File Offset: 0x00010DDC
		public string SourceColumn
		{
			get
			{
				if (this._sourceColumnName != null)
				{
					return this._sourceColumnName;
				}
				return string.Empty;
			}
			set
			{
				this._sourceColumnOrdinal = (this._internalSourceColumnOrdinal = -1);
				this._sourceColumnName = value;
			}
		}

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x0600081E RID: 2078 RVA: 0x00012C00 File Offset: 0x00010E00
		// (set) Token: 0x0600081F RID: 2079 RVA: 0x00012C08 File Offset: 0x00010E08
		public int SourceOrdinal
		{
			get
			{
				return this._sourceColumnOrdinal;
			}
			set
			{
				if (value >= 0)
				{
					this._sourceColumnName = null;
					this._internalSourceColumnOrdinal = value;
					this._sourceColumnOrdinal = value;
					return;
				}
				throw ADP.IndexOutOfRange(value);
			}
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x00012C37 File Offset: 0x00010E37
		public SqlBulkCopyColumnMapping()
		{
			this._internalSourceColumnOrdinal = -1;
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x00012C46 File Offset: 0x00010E46
		public SqlBulkCopyColumnMapping(string sourceColumn, string destinationColumn)
		{
			this.SourceColumn = sourceColumn;
			this.DestinationColumn = destinationColumn;
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x00012C5C File Offset: 0x00010E5C
		public SqlBulkCopyColumnMapping(int sourceColumnOrdinal, string destinationColumn)
		{
			this.SourceOrdinal = sourceColumnOrdinal;
			this.DestinationColumn = destinationColumn;
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x00012C72 File Offset: 0x00010E72
		public SqlBulkCopyColumnMapping(string sourceColumn, int destinationOrdinal)
		{
			this.SourceColumn = sourceColumn;
			this.DestinationOrdinal = destinationOrdinal;
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x00012C88 File Offset: 0x00010E88
		public SqlBulkCopyColumnMapping(int sourceColumnOrdinal, int destinationOrdinal)
		{
			this.SourceOrdinal = sourceColumnOrdinal;
			this.DestinationOrdinal = destinationOrdinal;
		}

		// Token: 0x04000122 RID: 290
		internal string _destinationColumnName;

		// Token: 0x04000123 RID: 291
		internal int _destinationColumnOrdinal;

		// Token: 0x04000124 RID: 292
		internal string _sourceColumnName;

		// Token: 0x04000125 RID: 293
		internal int _sourceColumnOrdinal;

		// Token: 0x04000126 RID: 294
		internal int _internalDestinationColumnOrdinal;

		// Token: 0x04000127 RID: 295
		internal int _internalSourceColumnOrdinal;
	}
}
