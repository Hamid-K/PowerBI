using System;

namespace Microsoft.HostIntegration.PerformanceCounters
{
	// Token: 0x0200078A RID: 1930
	public abstract class PerformanceCountersContainer
	{
		// Token: 0x06003E53 RID: 15955 RVA: 0x000D13A4 File Offset: 0x000CF5A4
		public int[] SharedPerformanceCounterToSpecific(SharedPerformanceCountersContainer sharedPerformanceCounterContainer)
		{
			if (sharedPerformanceCounterContainer > SharedPerformanceCountersContainer.CommonDrda)
			{
				throw new Exception("BUGBUG: SharedPerformanceCounterToSpecific called with invalid performance counter container id");
			}
			if (this.commonPerformanceCounterIdentifierToSpecific == null)
			{
				throw new Exception("BUGBUG: SharedPerformanceCounterToSpecific called with no mapping available");
			}
			if (this.commonPerformanceCounterIdentifierToSpecific[(int)sharedPerformanceCounterContainer] == null)
			{
				throw new Exception("BUGBUG: SharedPerformanceCounterToSpecific called with non-defined identifers for performance counter container");
			}
			return this.commonPerformanceCounterIdentifierToSpecific[(int)sharedPerformanceCounterContainer];
		}

		// Token: 0x06003E54 RID: 15956 RVA: 0x000D13F0 File Offset: 0x000CF5F0
		protected PerformanceCountersContainer(PerformanceCountersContainer parentContainer, SharedPerformanceCountersContainer childContainerId)
		{
			this.parentContainer = parentContainer;
			this.mapPerformanceCounter = parentContainer.SharedPerformanceCounterToSpecific(childContainerId);
		}

		// Token: 0x06003E55 RID: 15957 RVA: 0x000D140C File Offset: 0x000CF60C
		protected PerformanceCountersContainer(string instanceName, string category, string categoryHelp, PerformanceCounterInformationCollection counters)
		{
			this.InternalConstructor(instanceName, category, categoryHelp, counters, false, null, true);
		}

		// Token: 0x06003E56 RID: 15958 RVA: 0x000D1422 File Offset: 0x000CF622
		protected PerformanceCountersContainer(string instanceName, string category, string categoryHelp, PerformanceCounterInformationCollection counters, int[][] commonPerformanceCounterIdToSpecific)
		{
			this.InternalConstructor(instanceName, category, categoryHelp, counters, true, commonPerformanceCounterIdToSpecific, true);
		}

		// Token: 0x06003E57 RID: 15959 RVA: 0x000D143C File Offset: 0x000CF63C
		private void InternalConstructor(string instanceName, string category, string categoryHelp, PerformanceCounterInformationCollection counters, bool useMappings, int[][] commonPerformanceCounterIdToSpecific, bool automaticallySupportCommonInstance)
		{
			if (useMappings)
			{
				if (commonPerformanceCounterIdToSpecific == null || commonPerformanceCounterIdToSpecific.Length == 0)
				{
					throw new ArgumentNullException("commonPerformanceCounterIdToSpecific");
				}
				if (commonPerformanceCounterIdToSpecific.Length != 2)
				{
					throw new ArgumentOutOfRangeException("commonPerformanceCounterIdToSpecific");
				}
				this.commonPerformanceCounterIdentifierToSpecific = commonPerformanceCounterIdToSpecific;
			}
			this.instanceName = instanceName;
			this.category = category;
			this.categoryHelp = categoryHelp;
			this.counters = counters;
			foreach (object obj in counters)
			{
				((PerformanceCounterInformation)obj).AutomaticallySupportCommonInstance = automaticallySupportCommonInstance;
			}
			this.categoryHandler = PerformanceCounterCategoryHandler.GetCategoryHandler(category, categoryHelp, counters);
			this.instanceHandler = this.categoryHandler.GetInstanceHandler(instanceName);
		}

		// Token: 0x06003E58 RID: 15960 RVA: 0x000D1500 File Offset: 0x000CF700
		public void InstallCounters()
		{
			this.categoryHandler.InstallCounters();
		}

		// Token: 0x06003E59 RID: 15961 RVA: 0x000D150D File Offset: 0x000CF70D
		public void RemoveCounters()
		{
			this.categoryHandler.RemoveCounters();
		}

		// Token: 0x06003E5A RID: 15962 RVA: 0x000D151A File Offset: 0x000CF71A
		public bool CountersExist()
		{
			return this.categoryHandler.CountersExist();
		}

		// Token: 0x06003E5B RID: 15963 RVA: 0x000D1528 File Offset: 0x000CF728
		protected GenericPerformanceCounter GetPerformanceCounter(int counterIdentifier)
		{
			if (this.parentContainer == null)
			{
				return this.instanceHandler.GetPerformanceCounter(counterIdentifier);
			}
			if (counterIdentifier < 0 || counterIdentifier >= this.mapPerformanceCounter.Length)
			{
				throw new ArgumentOutOfRangeException("counterIdentifier");
			}
			return this.parentContainer.GetPerformanceCounter(this.mapPerformanceCounter[counterIdentifier]);
		}

		// Token: 0x040024F9 RID: 9465
		private string category;

		// Token: 0x040024FA RID: 9466
		private string categoryHelp;

		// Token: 0x040024FB RID: 9467
		private PerformanceCounterInformationCollection counters;

		// Token: 0x040024FC RID: 9468
		private PerformanceCounterCategoryHandler categoryHandler;

		// Token: 0x040024FD RID: 9469
		private string instanceName;

		// Token: 0x040024FE RID: 9470
		private PerformanceCounterInstanceHandler instanceHandler;

		// Token: 0x040024FF RID: 9471
		private int[][] commonPerformanceCounterIdentifierToSpecific;

		// Token: 0x04002500 RID: 9472
		private PerformanceCountersContainer parentContainer;

		// Token: 0x04002501 RID: 9473
		private int[] mapPerformanceCounter;
	}
}
