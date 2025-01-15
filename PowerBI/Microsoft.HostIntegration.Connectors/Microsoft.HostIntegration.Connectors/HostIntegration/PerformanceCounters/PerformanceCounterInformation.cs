using System;
using System.Diagnostics;

namespace Microsoft.HostIntegration.PerformanceCounters
{
	// Token: 0x0200078B RID: 1931
	public class PerformanceCounterInformation
	{
		// Token: 0x17000EBA RID: 3770
		// (get) Token: 0x06003E5C RID: 15964 RVA: 0x000D1577 File Offset: 0x000CF777
		// (set) Token: 0x06003E5D RID: 15965 RVA: 0x000D157F File Offset: 0x000CF77F
		public bool AutomaticallySupportCommonInstance { get; set; }

		// Token: 0x06003E5E RID: 15966 RVA: 0x000D1588 File Offset: 0x000CF788
		public PerformanceCounterInformation(int counterIdentity, string categoryName, string counterName, string counterHelp, PerformanceCounterType counterType, bool hasMultipleCounterInstancesPerCounter)
		{
			this.counterIdentity = counterIdentity;
			this.categoryName = categoryName;
			this.counterName = counterName;
			this.counterNameBase = counterName + " base";
			this.counterHelp = counterHelp;
			this.counterType = counterType;
			this.hasMultipleCounterInstancesPerCounter = hasMultipleCounterInstancesPerCounter;
		}

		// Token: 0x17000EBB RID: 3771
		// (get) Token: 0x06003E5F RID: 15967 RVA: 0x000D15D9 File Offset: 0x000CF7D9
		public int Identity
		{
			get
			{
				return this.counterIdentity;
			}
		}

		// Token: 0x17000EBC RID: 3772
		// (get) Token: 0x06003E60 RID: 15968 RVA: 0x000D15E1 File Offset: 0x000CF7E1
		public string CategoryName
		{
			get
			{
				return this.categoryName;
			}
		}

		// Token: 0x17000EBD RID: 3773
		// (get) Token: 0x06003E61 RID: 15969 RVA: 0x000D15E9 File Offset: 0x000CF7E9
		public string CounterName
		{
			get
			{
				return this.counterName;
			}
		}

		// Token: 0x17000EBE RID: 3774
		// (get) Token: 0x06003E62 RID: 15970 RVA: 0x000D15F1 File Offset: 0x000CF7F1
		public string CounterNameBase
		{
			get
			{
				return this.counterNameBase;
			}
		}

		// Token: 0x17000EBF RID: 3775
		// (get) Token: 0x06003E63 RID: 15971 RVA: 0x000D15F9 File Offset: 0x000CF7F9
		public bool HasMultipleCounterInstancesPerCounter
		{
			get
			{
				return this.hasMultipleCounterInstancesPerCounter;
			}
		}

		// Token: 0x06003E64 RID: 15972 RVA: 0x000D1604 File Offset: 0x000CF804
		public void AddTo(CounterCreationDataCollection creationDataCollection)
		{
			creationDataCollection.Add(new CounterCreationData
			{
				CounterName = this.counterName,
				CounterHelp = this.counterHelp,
				CounterType = this.counterType
			});
			if (this.counterType == PerformanceCounterType.AverageTimer32 || this.counterType == PerformanceCounterType.AverageCount64)
			{
				creationDataCollection.Add(new CounterCreationData
				{
					CounterName = this.counterNameBase,
					CounterHelp = this.counterHelp,
					CounterType = PerformanceCounterType.AverageBase
				});
			}
		}

		// Token: 0x06003E65 RID: 15973 RVA: 0x000D1690 File Offset: 0x000CF890
		internal GenericPerformanceCounter GetSingleInstanceCounter(string instanceName)
		{
			if (this.hasMultipleCounterInstancesPerCounter)
			{
				throw new Exception("FATAL: trying to get a single instance on a multiple instance counter, identity = " + this.counterIdentity.ToString());
			}
			PerformanceCounterType performanceCounterType = this.counterType;
			if (performanceCounterType == PerformanceCounterType.NumberOfItems32)
			{
				return new UpDownCounter(this, instanceName);
			}
			if (performanceCounterType == PerformanceCounterType.RateOfCountsPerSecond32)
			{
				return new PerSecondCounter(this, instanceName);
			}
			if (performanceCounterType != PerformanceCounterType.AverageTimer32)
			{
				throw new Exception("FATAL: asked for a type of counter we do not support, type = " + this.counterType.ToString());
			}
			return new AverageTimeCounter(this, instanceName);
		}

		// Token: 0x06003E66 RID: 15974 RVA: 0x000D171C File Offset: 0x000CF91C
		internal GenericPerformanceCounter GetMultipleInstanceCounter(string instanceName)
		{
			if (!this.hasMultipleCounterInstancesPerCounter)
			{
				throw new Exception("FATAL: trying to get a multi instance on a single instance counter, identity = " + this.counterIdentity.ToString());
			}
			PerformanceCounterType performanceCounterType = this.counterType;
			if (performanceCounterType == PerformanceCounterType.NumberOfItems32)
			{
				return new UpDownCounter(this, instanceName);
			}
			if (performanceCounterType == PerformanceCounterType.RateOfCountsPerSecond32)
			{
				return new PerSecondCounter(this, instanceName);
			}
			if (performanceCounterType != PerformanceCounterType.AverageTimer32)
			{
				throw new Exception("FATAL: asked for a type of counter we do not support, type = " + this.counterType.ToString());
			}
			return new AverageTimeCounter(this, instanceName);
		}

		// Token: 0x04002502 RID: 9474
		private int counterIdentity;

		// Token: 0x04002503 RID: 9475
		private string categoryName;

		// Token: 0x04002504 RID: 9476
		private string counterName;

		// Token: 0x04002505 RID: 9477
		private string counterNameBase;

		// Token: 0x04002506 RID: 9478
		private string counterHelp;

		// Token: 0x04002507 RID: 9479
		private PerformanceCounterType counterType;

		// Token: 0x04002508 RID: 9480
		private bool hasMultipleCounterInstancesPerCounter;
	}
}
