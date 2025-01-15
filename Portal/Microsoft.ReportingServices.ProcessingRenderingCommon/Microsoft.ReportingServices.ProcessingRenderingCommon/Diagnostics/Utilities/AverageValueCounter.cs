using System;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000B0 RID: 176
	internal sealed class AverageValueCounter : ICounter, IDisposable
	{
		// Token: 0x0600059D RID: 1437 RVA: 0x00011017 File Offset: 0x0000F217
		public AverageValueCounter(RSCounter averageCounter, RSCounter baseCounter)
		{
			this.m_averageCounter = averageCounter;
			this.m_baseCounter = baseCounter;
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x0001102D File Offset: 0x0000F22D
		public void Increment()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x00011034 File Offset: 0x0000F234
		public void IncrementBy(long val)
		{
			this.m_averageCounter.IncrementBy(val);
			this.m_baseCounter.Increment();
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x0001104D File Offset: 0x0000F24D
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x00011056 File Offset: 0x0000F256
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.m_averageCounter != null)
				{
					((IDisposable)this.m_averageCounter).Dispose();
					this.m_averageCounter = null;
				}
				if (this.m_baseCounter != null)
				{
					((IDisposable)this.m_baseCounter).Dispose();
					this.m_baseCounter = null;
				}
			}
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x0001108F File Offset: 0x0000F28F
		public void Decrement()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x00011096 File Offset: 0x0000F296
		public void DecrementBy(long val)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x0001109D File Offset: 0x0000F29D
		// (set) Token: 0x060005A5 RID: 1445 RVA: 0x000110A4 File Offset: 0x0000F2A4
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

		// Token: 0x04000327 RID: 807
		private RSCounter m_averageCounter;

		// Token: 0x04000328 RID: 808
		private RSCounter m_baseCounter;
	}
}
