using System;
using System.Collections;
using System.Diagnostics;

namespace Microsoft.HostIntegration.PerformanceCounters
{
	// Token: 0x02000786 RID: 1926
	public class PerformanceCounterCategoryHandler
	{
		// Token: 0x06003E47 RID: 15943 RVA: 0x000D1034 File Offset: 0x000CF234
		internal static PerformanceCounterCategoryHandler GetCategoryHandler(string categoryName, string categoryHelp, PerformanceCounterInformationCollection counterInformationCollection)
		{
			PerformanceCounterCategoryHandler performanceCounterCategoryHandler = (PerformanceCounterCategoryHandler)PerformanceCounterCategoryHandler.categoryNameToCategoryHandlers[categoryName];
			if (performanceCounterCategoryHandler == null)
			{
				Hashtable hashtable = PerformanceCounterCategoryHandler.categoryNameToCategoryHandlers;
				lock (hashtable)
				{
					performanceCounterCategoryHandler = (PerformanceCounterCategoryHandler)PerformanceCounterCategoryHandler.categoryNameToCategoryHandlers[categoryName];
					if (performanceCounterCategoryHandler == null)
					{
						performanceCounterCategoryHandler = new PerformanceCounterCategoryHandler(categoryName, categoryHelp, counterInformationCollection);
						PerformanceCounterCategoryHandler.categoryNameToCategoryHandlers[categoryName] = performanceCounterCategoryHandler;
					}
				}
			}
			return performanceCounterCategoryHandler;
		}

		// Token: 0x06003E48 RID: 15944 RVA: 0x000D10AC File Offset: 0x000CF2AC
		private PerformanceCounterCategoryHandler(string categoryName, string categoryHelp, PerformanceCounterInformationCollection counterInformationCollection)
		{
			this.categoryName = categoryName;
			this.categoryHelp = categoryHelp;
			this.counterInformationCollection = counterInformationCollection;
			this.instanceNameToInstanceHandlers = new Hashtable();
			this.identityToInformation = new Hashtable();
			foreach (object obj in counterInformationCollection)
			{
				PerformanceCounterInformation performanceCounterInformation = (PerformanceCounterInformation)obj;
				this.identityToInformation[performanceCounterInformation.Identity] = performanceCounterInformation;
			}
		}

		// Token: 0x06003E49 RID: 15945 RVA: 0x000D1144 File Offset: 0x000CF344
		public PerformanceCounterInstanceHandler GetInstanceHandler(string instanceName)
		{
			PerformanceCounterInstanceHandler performanceCounterInstanceHandler = (PerformanceCounterInstanceHandler)this.instanceNameToInstanceHandlers[instanceName];
			if (performanceCounterInstanceHandler == null)
			{
				Hashtable hashtable = this.instanceNameToInstanceHandlers;
				lock (hashtable)
				{
					performanceCounterInstanceHandler = (PerformanceCounterInstanceHandler)this.instanceNameToInstanceHandlers[instanceName];
					if (performanceCounterInstanceHandler == null)
					{
						performanceCounterInstanceHandler = new PerformanceCounterInstanceHandler(instanceName, this);
						this.instanceNameToInstanceHandlers[instanceName] = performanceCounterInstanceHandler;
					}
				}
			}
			return performanceCounterInstanceHandler;
		}

		// Token: 0x06003E4A RID: 15946 RVA: 0x000D11C0 File Offset: 0x000CF3C0
		public void InstallCounters()
		{
			if (PerformanceCounterCategory.Exists(this.categoryName))
			{
				PerformanceCounterCategory.Delete(this.categoryName);
			}
			CounterCreationDataCollection creationDataCollection = this.counterInformationCollection.CreationDataCollection;
			PerformanceCounterCategory.Create(this.categoryName, this.categoryHelp, PerformanceCounterCategoryType.MultiInstance, creationDataCollection);
		}

		// Token: 0x06003E4B RID: 15947 RVA: 0x000D1205 File Offset: 0x000CF405
		public void RemoveCounters()
		{
			PerformanceCounterCategory.Delete(this.categoryName);
		}

		// Token: 0x06003E4C RID: 15948 RVA: 0x000D1212 File Offset: 0x000CF412
		public bool CountersExist()
		{
			return PerformanceCounterCategory.Exists(this.categoryName);
		}

		// Token: 0x06003E4D RID: 15949 RVA: 0x000D121F File Offset: 0x000CF41F
		internal PerformanceCounterInformation GetPerformanceCounterInformation(int identity)
		{
			PerformanceCounterInformation performanceCounterInformation = (PerformanceCounterInformation)this.identityToInformation[identity];
			if (performanceCounterInformation == null)
			{
				throw new ArgumentOutOfRangeException("identity");
			}
			return performanceCounterInformation;
		}

		// Token: 0x040024E7 RID: 9447
		private static Hashtable categoryNameToCategoryHandlers = new Hashtable();

		// Token: 0x040024E8 RID: 9448
		private string categoryName;

		// Token: 0x040024E9 RID: 9449
		private string categoryHelp;

		// Token: 0x040024EA RID: 9450
		private PerformanceCounterInformationCollection counterInformationCollection;

		// Token: 0x040024EB RID: 9451
		private Hashtable identityToInformation;

		// Token: 0x040024EC RID: 9452
		private Hashtable instanceNameToInstanceHandlers;
	}
}
