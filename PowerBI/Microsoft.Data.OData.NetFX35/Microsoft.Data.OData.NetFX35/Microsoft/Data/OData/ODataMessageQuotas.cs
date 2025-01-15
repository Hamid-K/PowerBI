using System;

namespace Microsoft.Data.OData
{
	// Token: 0x020001BB RID: 443
	public sealed class ODataMessageQuotas
	{
		// Token: 0x06000D16 RID: 3350 RVA: 0x0002E6E0 File Offset: 0x0002C8E0
		public ODataMessageQuotas()
		{
			this.maxPartsPerBatch = 100;
			this.maxOperationsPerChangeset = 1000;
			this.maxNestingDepth = 100;
			this.maxReceivedMessageSize = 1048576L;
			this.maxEntityPropertyMappingsPerType = 100;
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x0002E718 File Offset: 0x0002C918
		public ODataMessageQuotas(ODataMessageQuotas other)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataMessageQuotas>(other, "other");
			this.maxPartsPerBatch = other.maxPartsPerBatch;
			this.maxOperationsPerChangeset = other.maxOperationsPerChangeset;
			this.maxNestingDepth = other.maxNestingDepth;
			this.maxReceivedMessageSize = other.maxReceivedMessageSize;
			this.maxEntityPropertyMappingsPerType = other.maxEntityPropertyMappingsPerType;
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000D18 RID: 3352 RVA: 0x0002E772 File Offset: 0x0002C972
		// (set) Token: 0x06000D19 RID: 3353 RVA: 0x0002E77A File Offset: 0x0002C97A
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

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000D1A RID: 3354 RVA: 0x0002E78E File Offset: 0x0002C98E
		// (set) Token: 0x06000D1B RID: 3355 RVA: 0x0002E796 File Offset: 0x0002C996
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

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000D1C RID: 3356 RVA: 0x0002E7AA File Offset: 0x0002C9AA
		// (set) Token: 0x06000D1D RID: 3357 RVA: 0x0002E7B2 File Offset: 0x0002C9B2
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

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000D1E RID: 3358 RVA: 0x0002E7C6 File Offset: 0x0002C9C6
		// (set) Token: 0x06000D1F RID: 3359 RVA: 0x0002E7CE File Offset: 0x0002C9CE
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

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000D20 RID: 3360 RVA: 0x0002E7E2 File Offset: 0x0002C9E2
		// (set) Token: 0x06000D21 RID: 3361 RVA: 0x0002E7EA File Offset: 0x0002C9EA
		public int MaxEntityPropertyMappingsPerType
		{
			get
			{
				return this.maxEntityPropertyMappingsPerType;
			}
			set
			{
				ExceptionUtils.CheckIntegerNotNegative(value, "MaxEntityPropertyMappingsPerType");
				this.maxEntityPropertyMappingsPerType = value;
			}
		}

		// Token: 0x04000495 RID: 1173
		private int maxPartsPerBatch;

		// Token: 0x04000496 RID: 1174
		private int maxOperationsPerChangeset;

		// Token: 0x04000497 RID: 1175
		private int maxNestingDepth;

		// Token: 0x04000498 RID: 1176
		private long maxReceivedMessageSize;

		// Token: 0x04000499 RID: 1177
		private int maxEntityPropertyMappingsPerType;
	}
}
