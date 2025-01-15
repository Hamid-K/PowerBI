using System;
using System.Configuration;
using System.Diagnostics;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000AE RID: 174
	public sealed class RSCounter : ICounter, IDisposable
	{
		// Token: 0x06000586 RID: 1414 RVA: 0x00010D52 File Offset: 0x0000EF52
		internal RSCounter(string categoryName, string counterName, string instanceName, RSTrace tracer)
		{
			this.Init(categoryName, counterName, instanceName, tracer, true);
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x00010D66 File Offset: 0x0000EF66
		internal RSCounter(string categoryName, string counterName, string instanceName, RSTrace tracer, bool privateCounter, bool resetCounter)
		{
			this.m_isPrivateCounter = privateCounter;
			this.Init(categoryName, counterName, instanceName, tracer, resetCounter);
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x00010D84 File Offset: 0x0000EF84
		private void Init(string categoryName, string counterName, string instanceName, RSTrace tracer, bool resetCounter)
		{
			Exception ex = null;
			try
			{
				this.m_Tracer = tracer;
				if (categoryName == null)
				{
					return;
				}
				if (instanceName == "UnitTests.Processing")
				{
					return;
				}
				if (!PerformanceCounterCategory.Exists(categoryName))
				{
					return;
				}
				this.m_name = counterName;
				this.m_PerformanceCounter = new PerformanceCounter(categoryName, counterName, instanceName, false);
				if (resetCounter)
				{
					this.m_PerformanceCounter.RawValue = 0L;
				}
			}
			catch (InvalidOperationException ex)
			{
			}
			catch (ConfigurationErrorsException ex)
			{
			}
			if (ex != null)
			{
				this.m_PerformanceCounter = null;
				if (this.m_Tracer != null)
				{
					if (this.m_isPrivateCounter)
					{
						this.m_Tracer.Trace(TraceLevel.Verbose, "Counter not created.  Category: {0}, Counter: {1}, Instance: {2}.  Error Message: {3}", new object[] { categoryName, counterName, instanceName, ex.Message });
					}
					else
					{
						this.m_Tracer.Trace(TraceLevel.Error, "Error creating counter.  Category: {0}, Counter: {1}, Instance: {2}.  Error Description: {3}", new object[]
						{
							categoryName,
							counterName,
							instanceName,
							ex.ToString()
						});
					}
				}
				if (!this.m_isPrivateCounter)
				{
					RSEventLog.Current.WriteError(Event.CantCreatePerfCounters, new object[] { counterName });
				}
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x00010EA4 File Offset: 0x0000F0A4
		public PerformanceCounterType Type
		{
			get
			{
				return PerformanceCounterType.RawBase;
			}
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00010EAB File Offset: 0x0000F0AB
		public void Increment()
		{
			if (this.m_PerformanceCounter != null)
			{
				this.m_PerformanceCounter.Increment();
			}
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x00010EC1 File Offset: 0x0000F0C1
		public void Decrement()
		{
			if (this.m_PerformanceCounter != null)
			{
				this.m_PerformanceCounter.Decrement();
			}
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x00010ED7 File Offset: 0x0000F0D7
		public void DecrementBy(long val)
		{
			if (this.m_PerformanceCounter != null)
			{
				this.m_PerformanceCounter.IncrementBy(0L - val);
			}
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00010EF1 File Offset: 0x0000F0F1
		public void IncrementBy(long val)
		{
			if (this.m_PerformanceCounter != null)
			{
				this.m_PerformanceCounter.IncrementBy(val);
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x0600058E RID: 1422 RVA: 0x00010F08 File Offset: 0x0000F108
		// (set) Token: 0x0600058F RID: 1423 RVA: 0x00010F20 File Offset: 0x0000F120
		public long RawValue
		{
			get
			{
				if (this.m_PerformanceCounter != null)
				{
					return this.m_PerformanceCounter.RawValue;
				}
				return 0L;
			}
			set
			{
				if (this.m_PerformanceCounter != null)
				{
					this.m_PerformanceCounter.RawValue = value;
				}
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000590 RID: 1424 RVA: 0x00010F36 File Offset: 0x0000F136
		public bool HasPerformanceCounter
		{
			get
			{
				return this.m_PerformanceCounter != null;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000591 RID: 1425 RVA: 0x00010F41 File Offset: 0x0000F141
		internal string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x00010F49 File Offset: 0x0000F149
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x00010F52 File Offset: 0x0000F152
		private void Dispose(bool disposing)
		{
			if (disposing && this.m_PerformanceCounter != null)
			{
				this.m_PerformanceCounter.Dispose();
				this.m_PerformanceCounter = null;
			}
		}

		// Token: 0x04000321 RID: 801
		private string m_name;

		// Token: 0x04000322 RID: 802
		private PerformanceCounter m_PerformanceCounter;

		// Token: 0x04000323 RID: 803
		private RSTrace m_Tracer;

		// Token: 0x04000324 RID: 804
		private bool m_isPrivateCounter;
	}
}
