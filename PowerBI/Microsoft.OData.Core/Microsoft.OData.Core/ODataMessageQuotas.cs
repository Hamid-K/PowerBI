using System;

namespace Microsoft.OData
{
	// Token: 0x02000096 RID: 150
	public sealed class ODataMessageQuotas
	{
		// Token: 0x06000562 RID: 1378 RVA: 0x0000CEEB File Offset: 0x0000B0EB
		public ODataMessageQuotas()
		{
			this.maxPartsPerBatch = 100;
			this.maxOperationsPerChangeset = 1000;
			this.maxNestingDepth = 100;
			this.maxReceivedMessageSize = 1048576L;
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x0000CF1C File Offset: 0x0000B11C
		public ODataMessageQuotas(ODataMessageQuotas other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageQuotas>(other, "other");
			this.maxPartsPerBatch = other.maxPartsPerBatch;
			this.maxOperationsPerChangeset = other.maxOperationsPerChangeset;
			this.maxNestingDepth = other.maxNestingDepth;
			this.maxReceivedMessageSize = other.maxReceivedMessageSize;
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000564 RID: 1380 RVA: 0x0000CF6B File Offset: 0x0000B16B
		// (set) Token: 0x06000565 RID: 1381 RVA: 0x0000CF73 File Offset: 0x0000B173
		public int MaxPartsPerBatch
		{
			get
			{
				return this.maxPartsPerBatch;
			}
			set
			{
				ExceptionUtils.CheckIntegerNotNegative(value, "MaxPartsPerBatch");
				this.maxPartsPerBatch = value;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000566 RID: 1382 RVA: 0x0000CF87 File Offset: 0x0000B187
		// (set) Token: 0x06000567 RID: 1383 RVA: 0x0000CF8F File Offset: 0x0000B18F
		public int MaxOperationsPerChangeset
		{
			get
			{
				return this.maxOperationsPerChangeset;
			}
			set
			{
				ExceptionUtils.CheckIntegerNotNegative(value, "MaxOperationsPerChangeset");
				this.maxOperationsPerChangeset = value;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000568 RID: 1384 RVA: 0x0000CFA3 File Offset: 0x0000B1A3
		// (set) Token: 0x06000569 RID: 1385 RVA: 0x0000CFAB File Offset: 0x0000B1AB
		public int MaxNestingDepth
		{
			get
			{
				return this.maxNestingDepth;
			}
			set
			{
				ExceptionUtils.CheckIntegerPositive(value, "MaxNestingDepth");
				this.maxNestingDepth = value;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600056A RID: 1386 RVA: 0x0000CFBF File Offset: 0x0000B1BF
		// (set) Token: 0x0600056B RID: 1387 RVA: 0x0000CFC7 File Offset: 0x0000B1C7
		public long MaxReceivedMessageSize
		{
			get
			{
				return this.maxReceivedMessageSize;
			}
			set
			{
				ExceptionUtils.CheckLongPositive(value, "MaxReceivedMessageSize");
				this.maxReceivedMessageSize = value;
			}
		}

		// Token: 0x04000246 RID: 582
		private int maxPartsPerBatch;

		// Token: 0x04000247 RID: 583
		private int maxOperationsPerChangeset;

		// Token: 0x04000248 RID: 584
		private int maxNestingDepth;

		// Token: 0x04000249 RID: 585
		private long maxReceivedMessageSize;
	}
}
