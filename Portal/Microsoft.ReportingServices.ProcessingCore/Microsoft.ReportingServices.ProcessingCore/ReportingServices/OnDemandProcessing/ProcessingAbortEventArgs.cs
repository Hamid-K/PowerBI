using System;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x020007E7 RID: 2023
	internal sealed class ProcessingAbortEventArgs : EventArgs
	{
		// Token: 0x06007163 RID: 29027 RVA: 0x001D7A3B File Offset: 0x001D5C3B
		internal ProcessingAbortEventArgs(string uniqueName)
		{
			this.m_uniqueName = uniqueName;
		}

		// Token: 0x1700268E RID: 9870
		// (get) Token: 0x06007164 RID: 29028 RVA: 0x001D7A4A File Offset: 0x001D5C4A
		internal string UniqueName
		{
			get
			{
				return this.m_uniqueName;
			}
		}

		// Token: 0x04003A6B RID: 14955
		private string m_uniqueName;
	}
}
