using System;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002A6 RID: 678
	internal interface IBufferedStreamHintProvider
	{
		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x060018B9 RID: 6329
		bool CanProvideHints { get; }

		// Token: 0x060018BA RID: 6330
		BufferedStreamHint GetReadHint(int availableBufferSpace, long readPosition);
	}
}
