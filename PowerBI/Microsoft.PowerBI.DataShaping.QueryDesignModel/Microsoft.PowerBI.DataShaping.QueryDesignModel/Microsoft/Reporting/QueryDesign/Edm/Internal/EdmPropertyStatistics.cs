using System;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x02000216 RID: 534
	internal sealed class EdmPropertyStatistics
	{
		// Token: 0x060018C3 RID: 6339 RVA: 0x00043A08 File Offset: 0x00041C08
		internal EdmPropertyStatistics(int distinctValueCount)
		{
			this._distinctValueCount = distinctValueCount;
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x060018C4 RID: 6340 RVA: 0x00043A17 File Offset: 0x00041C17
		public int DistinctValueCount
		{
			get
			{
				return this._distinctValueCount;
			}
		}

		// Token: 0x04000D30 RID: 3376
		private readonly int _distinctValueCount;
	}
}
