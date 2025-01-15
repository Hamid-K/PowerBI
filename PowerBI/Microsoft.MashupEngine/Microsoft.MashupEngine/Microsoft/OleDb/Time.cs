using System;

namespace Microsoft.OleDb
{
	// Token: 0x02001F31 RID: 7985
	public struct Time
	{
		// Token: 0x0600C3A2 RID: 50082 RVA: 0x002730DC File Offset: 0x002712DC
		public Time(TimeSpan timeSpan)
		{
			this.timeSpan = timeSpan;
		}

		// Token: 0x17002FCA RID: 12234
		// (get) Token: 0x0600C3A3 RID: 50083 RVA: 0x002730E5 File Offset: 0x002712E5
		public TimeSpan TimeSpan
		{
			get
			{
				return this.timeSpan;
			}
		}

		// Token: 0x040064A2 RID: 25762
		private readonly TimeSpan timeSpan;
	}
}
