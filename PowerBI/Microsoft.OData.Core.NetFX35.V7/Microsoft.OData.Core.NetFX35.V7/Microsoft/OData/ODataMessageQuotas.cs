using System;

namespace Microsoft.OData
{
	// Token: 0x02000070 RID: 112
	public sealed class ODataMessageQuotas
	{
		// Token: 0x060003A2 RID: 930 RVA: 0x0000AA0F File Offset: 0x00008C0F
		public ODataMessageQuotas()
		{
			this.maxPartsPerBatch = 100;
			this.maxOperationsPerChangeset = 1000;
			this.maxNestingDepth = 100;
			this.maxReceivedMessageSize = 1048576L;
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000AA40 File Offset: 0x00008C40
		public ODataMessageQuotas(ODataMessageQuotas other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageQuotas>(other, "other");
			this.maxPartsPerBatch = other.maxPartsPerBatch;
			this.maxOperationsPerChangeset = other.maxOperationsPerChangeset;
			this.maxNestingDepth = other.maxNestingDepth;
			this.maxReceivedMessageSize = other.maxReceivedMessageSize;
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x0000AA8F File Offset: 0x00008C8F
		// (set) Token: 0x060003A5 RID: 933 RVA: 0x0000AA97 File Offset: 0x00008C97
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

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x0000AAAB File Offset: 0x00008CAB
		// (set) Token: 0x060003A7 RID: 935 RVA: 0x0000AAB3 File Offset: 0x00008CB3
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

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x0000AAC7 File Offset: 0x00008CC7
		// (set) Token: 0x060003A9 RID: 937 RVA: 0x0000AACF File Offset: 0x00008CCF
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

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060003AA RID: 938 RVA: 0x0000AAE3 File Offset: 0x00008CE3
		// (set) Token: 0x060003AB RID: 939 RVA: 0x0000AAEB File Offset: 0x00008CEB
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

		// Token: 0x040001E5 RID: 485
		private int maxPartsPerBatch;

		// Token: 0x040001E6 RID: 486
		private int maxOperationsPerChangeset;

		// Token: 0x040001E7 RID: 487
		private int maxNestingDepth;

		// Token: 0x040001E8 RID: 488
		private long maxReceivedMessageSize;
	}
}
