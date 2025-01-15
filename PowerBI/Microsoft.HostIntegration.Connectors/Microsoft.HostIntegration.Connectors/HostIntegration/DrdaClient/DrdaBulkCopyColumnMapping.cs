using System;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x020009A9 RID: 2473
	public sealed class DrdaBulkCopyColumnMapping
	{
		// Token: 0x06004C96 RID: 19606 RVA: 0x00132749 File Offset: 0x00130949
		public DrdaBulkCopyColumnMapping()
		{
			this._destColumn = string.Empty;
			this._sourceColumn = string.Empty;
			this._destOrdinal = 0;
			this._sourceOrdinal = 0;
		}

		// Token: 0x06004C97 RID: 19607 RVA: 0x00132775 File Offset: 0x00130975
		public DrdaBulkCopyColumnMapping(int sourceColumnOrdinal, int destinationOrdinal)
			: this()
		{
			this.SourceOrdinal = sourceColumnOrdinal;
			this.DestinationOrdinal = destinationOrdinal;
		}

		// Token: 0x06004C98 RID: 19608 RVA: 0x0013278B File Offset: 0x0013098B
		public DrdaBulkCopyColumnMapping(int sourceColumnOrdinal, string destinationColumn)
			: this()
		{
			this.SourceOrdinal = sourceColumnOrdinal;
			this.DestinationColumn = destinationColumn;
		}

		// Token: 0x06004C99 RID: 19609 RVA: 0x001327A1 File Offset: 0x001309A1
		public DrdaBulkCopyColumnMapping(string sourceColumn, int destinationOrdinal)
			: this()
		{
			this.SourceColumn = sourceColumn;
			this.DestinationOrdinal = destinationOrdinal;
		}

		// Token: 0x06004C9A RID: 19610 RVA: 0x001327B7 File Offset: 0x001309B7
		public DrdaBulkCopyColumnMapping(string sourceColumn, string destinationColumn)
			: this()
		{
			this.SourceColumn = sourceColumn;
			this.DestinationColumn = destinationColumn;
		}

		// Token: 0x1700128B RID: 4747
		// (get) Token: 0x06004C9C RID: 19612 RVA: 0x001327EC File Offset: 0x001309EC
		// (set) Token: 0x06004C9B RID: 19611 RVA: 0x001327CD File Offset: 0x001309CD
		public string DestinationColumn
		{
			get
			{
				return this._destColumn;
			}
			set
			{
				this._destColumn = (string.IsNullOrWhiteSpace(value) ? string.Empty : value);
				this._destOrdinal = -1;
			}
		}

		// Token: 0x1700128C RID: 4748
		// (get) Token: 0x06004C9E RID: 19614 RVA: 0x00132819 File Offset: 0x00130A19
		// (set) Token: 0x06004C9D RID: 19613 RVA: 0x001327F4 File Offset: 0x001309F4
		public int DestinationOrdinal
		{
			get
			{
				return this._destOrdinal;
			}
			set
			{
				if (value < 0)
				{
					throw new IndexOutOfRangeException(value.ToString());
				}
				this._destOrdinal = value;
				this._destColumn = string.Empty;
			}
		}

		// Token: 0x1700128D RID: 4749
		// (get) Token: 0x06004CA0 RID: 19616 RVA: 0x00132840 File Offset: 0x00130A40
		// (set) Token: 0x06004C9F RID: 19615 RVA: 0x00132821 File Offset: 0x00130A21
		public string SourceColumn
		{
			get
			{
				return this._sourceColumn;
			}
			set
			{
				this._sourceColumn = (string.IsNullOrWhiteSpace(value) ? string.Empty : value);
				this._sourceOrdinal = -1;
			}
		}

		// Token: 0x1700128E RID: 4750
		// (get) Token: 0x06004CA2 RID: 19618 RVA: 0x0013286D File Offset: 0x00130A6D
		// (set) Token: 0x06004CA1 RID: 19617 RVA: 0x00132848 File Offset: 0x00130A48
		public int SourceOrdinal
		{
			get
			{
				return this._sourceOrdinal;
			}
			set
			{
				if (value < 0)
				{
					throw new IndexOutOfRangeException(value.ToString());
				}
				this._sourceOrdinal = value;
				this._sourceColumn = string.Empty;
			}
		}

		// Token: 0x06004CA3 RID: 19619 RVA: 0x00132875 File Offset: 0x00130A75
		internal DrdaBulkCopyMappingsType GetMappingType()
		{
			if (this._sourceOrdinal == -1)
			{
				if (this._destOrdinal == -1)
				{
					return DrdaBulkCopyMappingsType.StringToString;
				}
				return DrdaBulkCopyMappingsType.StringToOrdinal;
			}
			else
			{
				if (this._destOrdinal == -1)
				{
					return DrdaBulkCopyMappingsType.OrdinalToString;
				}
				return DrdaBulkCopyMappingsType.OrdinalToOrdinal;
			}
		}

		// Token: 0x04003C9F RID: 15519
		private int _destOrdinal;

		// Token: 0x04003CA0 RID: 15520
		private int _sourceOrdinal;

		// Token: 0x04003CA1 RID: 15521
		private string _destColumn;

		// Token: 0x04003CA2 RID: 15522
		private string _sourceColumn;
	}
}
