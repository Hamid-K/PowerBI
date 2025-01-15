using System;
using System.ComponentModel;

namespace Microsoft.InfoNav
{
	// Token: 0x0200001A RID: 26
	[ImmutableObject(true)]
	public class ConceptualColumnStatistics
	{
		// Token: 0x06000058 RID: 88 RVA: 0x00002563 File Offset: 0x00000763
		public ConceptualColumnStatistics(int distinctValueCount, PrimitiveValue minValue, PrimitiveValue maxValue)
		{
			this._distinctValueCount = distinctValueCount;
			this._minValue = minValue;
			this._maxValue = maxValue;
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002580 File Offset: 0x00000780
		public int DistinctValueCount
		{
			get
			{
				return this._distinctValueCount;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002588 File Offset: 0x00000788
		public PrimitiveValue MinValue
		{
			get
			{
				return this._minValue;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002590 File Offset: 0x00000790
		public PrimitiveValue MaxValue
		{
			get
			{
				return this._maxValue;
			}
		}

		// Token: 0x04000067 RID: 103
		private readonly int _distinctValueCount;

		// Token: 0x04000068 RID: 104
		private readonly PrimitiveValue _minValue;

		// Token: 0x04000069 RID: 105
		private readonly PrimitiveValue _maxValue;
	}
}
