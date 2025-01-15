using System;
using System.Threading;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Diagnostics
{
	// Token: 0x02000048 RID: 72
	public class ProgressBar : IDisposable
	{
		// Token: 0x0600024C RID: 588 RVA: 0x00013414 File Offset: 0x00011614
		public ProgressBar(string taskDescription, long numItems = 1L, int reportFrequency = 1)
		{
			this.m_numItems = numItems;
			this.m_reportFrequency = reportFrequency;
			if (numItems <= 0L)
			{
				if (reportFrequency == 1)
				{
					Console.Write("{0}: ", taskDescription);
					return;
				}
				Console.Write("{0} (/{1}): ", taskDescription, reportFrequency);
				return;
			}
			else
			{
				if (numItems == 1L)
				{
					Console.Write("{0}: ", taskDescription);
					return;
				}
				if (reportFrequency == 1)
				{
					Console.Write("{0} ({1}): ", taskDescription, numItems);
					return;
				}
				Console.Write("{0} ({1}/{2}): ", taskDescription, numItems, reportFrequency);
				return;
			}
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0001349C File Offset: 0x0001169C
		public void Update(int numCompletedItems = 1)
		{
			long num;
			do
			{
				num = this.m_numCompletedItems + (long)numCompletedItems;
			}
			while (this.m_numCompletedItems != Interlocked.CompareExchange(ref this.m_numCompletedItems, num, this.m_numCompletedItems));
			if (num % (long)this.m_reportFrequency == 0L && this.m_numItems != 1L)
			{
				Console.Write(".");
			}
		}

		// Token: 0x0600024E RID: 590 RVA: 0x000134EC File Offset: 0x000116EC
		public void Dispose()
		{
			if (this.m_numItems == 1L || this.m_numCompletedItems == this.m_numItems)
			{
				Console.WriteLine("Done");
				return;
			}
			if (this.m_numItems <= 0L)
			{
				Console.WriteLine("Completed {0}", this.m_numCompletedItems);
				return;
			}
			Console.WriteLine("Completed {0}/{1}", this.m_numCompletedItems, this.m_numItems);
		}

		// Token: 0x04000064 RID: 100
		private long m_numItems;

		// Token: 0x04000065 RID: 101
		private long m_numCompletedItems;

		// Token: 0x04000066 RID: 102
		private int m_reportFrequency;
	}
}
