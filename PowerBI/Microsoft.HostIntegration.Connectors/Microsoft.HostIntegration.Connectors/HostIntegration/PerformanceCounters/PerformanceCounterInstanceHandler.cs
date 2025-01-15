using System;
using System.Collections;

namespace Microsoft.HostIntegration.PerformanceCounters
{
	// Token: 0x02000789 RID: 1929
	public class PerformanceCounterInstanceHandler
	{
		// Token: 0x06003E50 RID: 15952 RVA: 0x000D1251 File Offset: 0x000CF451
		internal PerformanceCounterInstanceHandler(string instanceName, PerformanceCounterCategoryHandler categoryHandler)
		{
			this.instanceName = instanceName;
			this.categoryHandler = categoryHandler;
		}

		// Token: 0x06003E51 RID: 15953 RVA: 0x000D1274 File Offset: 0x000CF474
		public GenericPerformanceCounter GetPerformanceCounter(int identifier)
		{
			CounterHelper counterHelper = null;
			object obj = PerformanceCounterInstanceHandler.lockObject;
			lock (obj)
			{
				if (this.identifierToCounterHelpers == null)
				{
					throw new InvalidOperationException("identifierToCounterHelpers is null");
				}
				counterHelper = (CounterHelper)this.identifierToCounterHelpers[identifier];
				if (counterHelper == null)
				{
					counterHelper = new CounterHelper();
					if (this.categoryHandler == null)
					{
						throw new InvalidOperationException("categoryHandler is null");
					}
					PerformanceCounterInformation performanceCounterInformation = this.categoryHandler.GetPerformanceCounterInformation(identifier);
					if (performanceCounterInformation == null)
					{
						throw new InvalidOperationException("counterInformation is null");
					}
					counterHelper.counterInformation = performanceCounterInformation;
					counterHelper.identifier = identifier;
					counterHelper.singleInstanced = !performanceCounterInformation.HasMultipleCounterInstancesPerCounter;
					if (counterHelper.singleInstanced)
					{
						counterHelper.counter = performanceCounterInformation.GetSingleInstanceCounter(this.instanceName);
					}
					else
					{
						counterHelper.counter = null;
					}
					this.identifierToCounterHelpers[identifier] = counterHelper;
				}
			}
			if (counterHelper.singleInstanced)
			{
				return counterHelper.counter;
			}
			if (counterHelper.counterInformation == null)
			{
				throw new InvalidOperationException("counterHelper.counterInformation is null");
			}
			return counterHelper.counterInformation.GetMultipleInstanceCounter(this.instanceName);
		}

		// Token: 0x040024F5 RID: 9461
		private static object lockObject = new object();

		// Token: 0x040024F6 RID: 9462
		private string instanceName;

		// Token: 0x040024F7 RID: 9463
		private PerformanceCounterCategoryHandler categoryHandler;

		// Token: 0x040024F8 RID: 9464
		private Hashtable identifierToCounterHelpers = new Hashtable();
	}
}
