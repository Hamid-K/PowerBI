using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using NLog.Common;
using NLog.Config;
using NLog.Internal;
using NLog.Layouts;

namespace NLog.Targets
{
	// Token: 0x02000051 RID: 81
	[Target("PerfCounter")]
	public class PerformanceCounterTarget : Target, IInstallable
	{
		// Token: 0x0600078E RID: 1934 RVA: 0x00012D77 File Offset: 0x00010F77
		public PerformanceCounterTarget()
		{
			this.CounterType = PerformanceCounterType.NumberOfItems32;
			this.IncrementValue = new SimpleLayout("1");
			this.InstanceName = string.Empty;
			this.CounterHelp = string.Empty;
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x00012DB0 File Offset: 0x00010FB0
		public PerformanceCounterTarget(string name)
			: this()
		{
			base.Name = name;
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000790 RID: 1936 RVA: 0x00012DBF File Offset: 0x00010FBF
		// (set) Token: 0x06000791 RID: 1937 RVA: 0x00012DC7 File Offset: 0x00010FC7
		public bool AutoCreate { get; set; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000792 RID: 1938 RVA: 0x00012DD0 File Offset: 0x00010FD0
		// (set) Token: 0x06000793 RID: 1939 RVA: 0x00012DD8 File Offset: 0x00010FD8
		[RequiredParameter]
		public string CategoryName { get; set; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000794 RID: 1940 RVA: 0x00012DE1 File Offset: 0x00010FE1
		// (set) Token: 0x06000795 RID: 1941 RVA: 0x00012DE9 File Offset: 0x00010FE9
		[RequiredParameter]
		public string CounterName { get; set; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000796 RID: 1942 RVA: 0x00012DF2 File Offset: 0x00010FF2
		// (set) Token: 0x06000797 RID: 1943 RVA: 0x00012DFA File Offset: 0x00010FFA
		public string InstanceName { get; set; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000798 RID: 1944 RVA: 0x00012E03 File Offset: 0x00011003
		// (set) Token: 0x06000799 RID: 1945 RVA: 0x00012E0B File Offset: 0x0001100B
		public string CounterHelp { get; set; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600079A RID: 1946 RVA: 0x00012E14 File Offset: 0x00011014
		// (set) Token: 0x0600079B RID: 1947 RVA: 0x00012E1C File Offset: 0x0001101C
		[DefaultValue(PerformanceCounterType.NumberOfItems32)]
		public PerformanceCounterType CounterType { get; set; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600079C RID: 1948 RVA: 0x00012E25 File Offset: 0x00011025
		// (set) Token: 0x0600079D RID: 1949 RVA: 0x00012E2D File Offset: 0x0001102D
		[DefaultValue(1)]
		public Layout IncrementValue { get; set; }

		// Token: 0x0600079E RID: 1950 RVA: 0x00012E38 File Offset: 0x00011038
		public void Install(InstallationContext installationContext)
		{
			Dictionary<string, List<PerformanceCounterTarget>> dictionary = base.LoggingConfiguration.AllTargets.OfType<PerformanceCounterTarget>().BucketSort((PerformanceCounterTarget c) => c.CategoryName);
			string categoryName = this.CategoryName;
			if (dictionary[categoryName].Any((PerformanceCounterTarget c) => c.created))
			{
				installationContext.Trace("Category '{0}' has already been installed.", new object[] { categoryName });
				return;
			}
			try
			{
				PerformanceCounterCategoryType performanceCounterCategoryType;
				CounterCreationDataCollection counterCreationDataCollection = PerformanceCounterTarget.GetCounterCreationDataCollection(dictionary[this.CategoryName], out performanceCounterCategoryType);
				if (PerformanceCounterCategory.Exists(categoryName))
				{
					installationContext.Debug("Deleting category '{0}'", new object[] { categoryName });
					PerformanceCounterCategory.Delete(categoryName);
				}
				installationContext.Debug("Creating category '{0}' with {1} counter(s) (Type: {2})", new object[] { categoryName, counterCreationDataCollection.Count, performanceCounterCategoryType });
				foreach (object obj in counterCreationDataCollection)
				{
					CounterCreationData counterCreationData = (CounterCreationData)obj;
					installationContext.Trace("  Counter: '{0}' Type: ({1}) Help: {2}", new object[] { counterCreationData.CounterName, counterCreationData.CounterType, counterCreationData.CounterHelp });
				}
				PerformanceCounterCategory.Create(categoryName, "Category created by NLog", performanceCounterCategoryType, counterCreationDataCollection);
			}
			catch (Exception ex)
			{
				if (ex.MustBeRethrownImmediately())
				{
					throw;
				}
				if (!installationContext.IgnoreFailures)
				{
					installationContext.Error("Error creating category '{0}': {1}", new object[] { categoryName, ex.Message });
					throw;
				}
				installationContext.Warning("Error creating category '{0}': {1}", new object[] { categoryName, ex.Message });
				if (ex.MustBeRethrown())
				{
					throw;
				}
			}
			finally
			{
				foreach (PerformanceCounterTarget performanceCounterTarget in dictionary[categoryName])
				{
					performanceCounterTarget.created = true;
				}
			}
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x000130A8 File Offset: 0x000112A8
		public void Uninstall(InstallationContext installationContext)
		{
			string categoryName = this.CategoryName;
			if (PerformanceCounterCategory.Exists(categoryName))
			{
				installationContext.Debug("Deleting category '{0}'", new object[] { categoryName });
				PerformanceCounterCategory.Delete(categoryName);
				return;
			}
			installationContext.Debug("Category '{0}' does not exist.", new object[] { categoryName });
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x000130F5 File Offset: 0x000112F5
		public bool? IsInstalled(InstallationContext installationContext)
		{
			if (!PerformanceCounterCategory.Exists(this.CategoryName))
			{
				return new bool?(false);
			}
			return new bool?(PerformanceCounterCategory.CounterExists(this.CounterName, this.CategoryName));
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x00013124 File Offset: 0x00011324
		protected override void Write(LogEventInfo logEvent)
		{
			if (this.EnsureInitialized())
			{
				string text = this.IncrementValue.Render(logEvent);
				long num;
				if (long.TryParse(text, out num))
				{
					this.perfCounter.IncrementBy(num);
					return;
				}
				InternalLogger.Error<string, string, string>("PerfCounterTarget(Name={0}): Error incrementing PerfCounter {1}. IncrementValue must be an integer but was <{2}>", base.Name, this.CounterName, text);
			}
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x00013175 File Offset: 0x00011375
		protected override void CloseTarget()
		{
			base.CloseTarget();
			if (this.perfCounter != null)
			{
				this.perfCounter.Close();
				this.perfCounter = null;
			}
			this.initialized = false;
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x000131A0 File Offset: 0x000113A0
		private static CounterCreationDataCollection GetCounterCreationDataCollection(IEnumerable<PerformanceCounterTarget> countersInCategory, out PerformanceCounterCategoryType categoryType)
		{
			categoryType = PerformanceCounterCategoryType.SingleInstance;
			CounterCreationDataCollection counterCreationDataCollection = new CounterCreationDataCollection();
			foreach (PerformanceCounterTarget performanceCounterTarget in countersInCategory)
			{
				if (!string.IsNullOrEmpty(performanceCounterTarget.InstanceName))
				{
					categoryType = PerformanceCounterCategoryType.MultiInstance;
				}
				counterCreationDataCollection.Add(new CounterCreationData(performanceCounterTarget.CounterName, performanceCounterTarget.CounterHelp, performanceCounterTarget.CounterType));
			}
			return counterCreationDataCollection;
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0001321C File Offset: 0x0001141C
		private bool EnsureInitialized()
		{
			if (!this.initialized)
			{
				this.initialized = true;
				if (this.AutoCreate)
				{
					using (InstallationContext installationContext = new InstallationContext())
					{
						this.Install(installationContext);
					}
				}
				try
				{
					this.perfCounter = new PerformanceCounter(this.CategoryName, this.CounterName, this.InstanceName, false);
				}
				catch (Exception ex)
				{
					InternalLogger.Error(ex, "PerfCounterTarget(Name={0}): Cannot open performance counter {1}/{2}/{3}.", new object[] { base.Name, this.CategoryName, this.CounterName, this.InstanceName });
					if (ex.MustBeRethrown())
					{
						throw;
					}
				}
			}
			return this.perfCounter != null;
		}

		// Token: 0x04000173 RID: 371
		private PerformanceCounter perfCounter;

		// Token: 0x04000174 RID: 372
		private bool initialized;

		// Token: 0x04000175 RID: 373
		private bool created;
	}
}
