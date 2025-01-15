using System;

namespace Microsoft.OData.Core
{
	// Token: 0x0200017F RID: 383
	public sealed class ODataMessageQuotas
	{
		// Token: 0x06000DFA RID: 3578 RVA: 0x00031C13 File Offset: 0x0002FE13
		public ODataMessageQuotas()
		{
			this.maxPartsPerBatch = 100;
			this.maxOperationsPerChangeset = 1000;
			this.maxNestingDepth = 100;
			this.maxReceivedMessageSize = 1048576L;
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x00031C44 File Offset: 0x0002FE44
		public ODataMessageQuotas(ODataMessageQuotas other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageQuotas>(other, "other");
			this.maxPartsPerBatch = other.maxPartsPerBatch;
			this.maxOperationsPerChangeset = other.maxOperationsPerChangeset;
			this.maxNestingDepth = other.maxNestingDepth;
			this.maxReceivedMessageSize = other.maxReceivedMessageSize;
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000DFC RID: 3580 RVA: 0x00031C92 File Offset: 0x0002FE92
		// (set) Token: 0x06000DFD RID: 3581 RVA: 0x00031C9A File Offset: 0x0002FE9A
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

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000DFE RID: 3582 RVA: 0x00031CAE File Offset: 0x0002FEAE
		// (set) Token: 0x06000DFF RID: 3583 RVA: 0x00031CB6 File Offset: 0x0002FEB6
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

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000E00 RID: 3584 RVA: 0x00031CCA File Offset: 0x0002FECA
		// (set) Token: 0x06000E01 RID: 3585 RVA: 0x00031CD2 File Offset: 0x0002FED2
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

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000E02 RID: 3586 RVA: 0x00031CE6 File Offset: 0x0002FEE6
		// (set) Token: 0x06000E03 RID: 3587 RVA: 0x00031CEE File Offset: 0x0002FEEE
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

		// Token: 0x04000611 RID: 1553
		private int maxPartsPerBatch;

		// Token: 0x04000612 RID: 1554
		private int maxOperationsPerChangeset;

		// Token: 0x04000613 RID: 1555
		private int maxNestingDepth;

		// Token: 0x04000614 RID: 1556
		private long maxReceivedMessageSize;
	}
}
