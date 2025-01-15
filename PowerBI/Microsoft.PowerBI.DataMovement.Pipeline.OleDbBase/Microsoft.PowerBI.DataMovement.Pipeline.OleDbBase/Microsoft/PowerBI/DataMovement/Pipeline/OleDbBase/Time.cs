using System;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x020000C9 RID: 201
	public struct Time
	{
		// Token: 0x06000383 RID: 899 RVA: 0x0000A61D File Offset: 0x0000881D
		public Time(TimeSpan timeSpan)
		{
			this.timeSpan = timeSpan;
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000384 RID: 900 RVA: 0x0000A626 File Offset: 0x00008826
		public TimeSpan TimeSpan
		{
			get
			{
				return this.timeSpan;
			}
		}

		// Token: 0x04000383 RID: 899
		private readonly TimeSpan timeSpan;
	}
}
