using System;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002CC RID: 716
	internal interface IStorage : ICommitOnClose
	{
		// Token: 0x06001989 RID: 6537
		int Read(long position, byte[] buffer, int offset, int count);

		// Token: 0x0600198A RID: 6538
		void Write(long position, byte[] buffer, int offset, int count);

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x0600198B RID: 6539
		long Length { get; }

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x0600198C RID: 6540
		bool CanRead { get; }

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x0600198D RID: 6541
		bool CanWrite { get; }
	}
}
