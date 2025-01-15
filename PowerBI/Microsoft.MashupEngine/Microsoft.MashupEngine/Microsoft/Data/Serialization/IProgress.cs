using System;

namespace Microsoft.Data.Serialization
{
	// Token: 0x02000154 RID: 340
	public interface IProgress
	{
		// Token: 0x1700020C RID: 524
		// (get) Token: 0x060005F5 RID: 1525
		long Rows { get; }

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x060005F6 RID: 1526
		long ExceptionRows { get; }
	}
}
