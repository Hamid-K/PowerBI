using System;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000AF RID: 175
	public sealed class TotalPerSecondCounter : ICounter, IDisposable
	{
		// Token: 0x06000594 RID: 1428 RVA: 0x00010F71 File Offset: 0x0000F171
		public TotalPerSecondCounter(RSCounter perSecondCounter, RSCounter totalCounter)
		{
			this.m_perSecondCounter = perSecondCounter;
			this.m_totalCounter = totalCounter;
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x00010F87 File Offset: 0x0000F187
		public void Increment()
		{
			this.m_perSecondCounter.Increment();
			this.m_totalCounter.Increment();
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x00010F9F File Offset: 0x0000F19F
		public void IncrementBy(long val)
		{
			this.m_perSecondCounter.IncrementBy(val);
			this.m_totalCounter.IncrementBy(val);
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x00010FB9 File Offset: 0x0000F1B9
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00010FC2 File Offset: 0x0000F1C2
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.m_perSecondCounter != null)
				{
					((IDisposable)this.m_perSecondCounter).Dispose();
					this.m_perSecondCounter = null;
				}
				if (this.m_totalCounter != null)
				{
					((IDisposable)this.m_totalCounter).Dispose();
					this.m_totalCounter = null;
				}
			}
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x00010FFB File Offset: 0x0000F1FB
		public void Decrement()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x00011002 File Offset: 0x0000F202
		public void DecrementBy(long val)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x00011009 File Offset: 0x0000F209
		// (set) Token: 0x0600059C RID: 1436 RVA: 0x00011010 File Offset: 0x0000F210
		public long RawValue
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x04000325 RID: 805
		private RSCounter m_perSecondCounter;

		// Token: 0x04000326 RID: 806
		private RSCounter m_totalCounter;
	}
}
