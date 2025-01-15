using System;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.Cloud.Platform.Configuration;
using Microsoft.Cloud.Platform.ConfigurationManagement;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.ConfigurationClasses.Monitoring
{
	// Token: 0x02000446 RID: 1094
	[Serializable]
	public sealed class AnalysisResolutionConfig : ConfigurationClass
	{
		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x060021ED RID: 8685 RVA: 0x0007D910 File Offset: 0x0007BB10
		// (set) Token: 0x060021EE RID: 8686 RVA: 0x0007D918 File Offset: 0x0007BB18
		[ConfigurationProperty]
		public string Name
		{
			get
			{
				return this.m_Name;
			}
			set
			{
				base.ValidateRegexMatching(value, ".+");
				this.m_Name = value;
			}
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x060021EF RID: 8687 RVA: 0x0007D92D File Offset: 0x0007BB2D
		// (set) Token: 0x060021F0 RID: 8688 RVA: 0x0007D935 File Offset: 0x0007BB35
		[ConfigurationProperty]
		public ConfigurationCollection<int> DifferentiatorIndexes { get; set; }

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x060021F1 RID: 8689 RVA: 0x0007D93E File Offset: 0x0007BB3E
		// (set) Token: 0x060021F2 RID: 8690 RVA: 0x0007D946 File Offset: 0x0007BB46
		[ConfigurationProperty]
		public int TimePeriodLength
		{
			get
			{
				return this.m_TimePeriodLength;
			}
			set
			{
				base.ValidateMoreOrEqual((double)value, 1.0);
				base.ValidateLessOrEqual((double)value, 3600.0);
				this.m_TimePeriodLength = value;
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x060021F3 RID: 8691 RVA: 0x0007D971 File Offset: 0x0007BB71
		// (set) Token: 0x060021F4 RID: 8692 RVA: 0x0007D979 File Offset: 0x0007BB79
		[ConfigurationProperty]
		public int MaxPeriodsInHistory
		{
			get
			{
				return this.m_MaxPeriodsInHistory;
			}
			set
			{
				base.ValidateMoreOrEqual((double)value, 3.0);
				base.ValidateLessOrEqual((double)value, 300.0);
				this.m_MaxPeriodsInHistory = value;
			}
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x060021F5 RID: 8693 RVA: 0x0007D9A4 File Offset: 0x0007BBA4
		// (set) Token: 0x060021F6 RID: 8694 RVA: 0x0007D9AC File Offset: 0x0007BBAC
		[ConfigurationProperty]
		public int FaultThresholdPerPeriod
		{
			get
			{
				return this.m_FaultThresholdPerPeriod;
			}
			set
			{
				base.ValidateMoreOrEqual((double)value, 1.0);
				base.ValidateLessOrEqual((double)value, 100.0);
				this.m_FaultThresholdPerPeriod = value;
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x060021F7 RID: 8695 RVA: 0x0007D9D7 File Offset: 0x0007BBD7
		// (set) Token: 0x060021F8 RID: 8696 RVA: 0x0007D9DF File Offset: 0x0007BBDF
		[ConfigurationProperty]
		public int TrafficQuotaPerPeriod
		{
			get
			{
				return this.m_TrafficQuotaPerPeriod;
			}
			set
			{
				base.ValidateMoreOrEqual((double)value, 1.0);
				base.ValidateLessOrEqual((double)value, 999999.0);
				this.m_TrafficQuotaPerPeriod = value;
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x060021F9 RID: 8697 RVA: 0x0007DA0A File Offset: 0x0007BC0A
		// (set) Token: 0x060021FA RID: 8698 RVA: 0x0007DA12 File Offset: 0x0007BC12
		[ConfigurationProperty]
		public int AlarmThreshold
		{
			get
			{
				return this.m_AlarmThreshold;
			}
			set
			{
				base.ValidateMoreOrEqual((double)value, 1.0);
				base.ValidateLessOrEqual((double)value, 299.0);
				this.m_AlarmThreshold = value;
			}
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x060021FB RID: 8699 RVA: 0x0007DA3D File Offset: 0x0007BC3D
		// (set) Token: 0x060021FC RID: 8700 RVA: 0x0007DA45 File Offset: 0x0007BC45
		[ConfigurationProperty]
		public double SuccessTolerance
		{
			get
			{
				return this.m_SuccessTolerance;
			}
			set
			{
				base.ValidateMoreOrEqual(value, 0.0);
				base.ValidateLessOrEqual(value, 3.0);
				this.m_SuccessTolerance = value;
			}
		}

		// Token: 0x060021FD RID: 8701 RVA: 0x0007DA70 File Offset: 0x0007BC70
		public override string ToString()
		{
			IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
			string text = "{0} {1}";
			object[] array = new object[2];
			int num = 0;
			IFormatProvider invariantCulture2 = CultureInfo.InvariantCulture;
			string text2 = "Resolution name: {0}. Number of indices: {1}. Indices: {2}. Period length: {3}. Traffic quota: {4}.";
			object[] array2 = new object[5];
			array2[0] = this.Name;
			array2[1] = this.DifferentiatorIndexes.Count;
			array2[2] = string.Join(",", this.DifferentiatorIndexes.Select((int i) => i.ToString(CultureInfo.InvariantCulture)).ToArray<string>());
			array2[3] = this.TimePeriodLength;
			array2[4] = this.TrafficQuotaPerPeriod;
			array[num] = string.Format(invariantCulture2, text2, array2);
			array[1] = string.Format(CultureInfo.InvariantCulture, "Threshold per period: {0}. Max periods in history: {1}. Alarm threshold: {2}. Success tolerance: {3}.", new object[] { this.FaultThresholdPerPeriod, this.MaxPeriodsInHistory, this.AlarmThreshold, this.SuccessTolerance });
			return new StringBuilder(string.Format(invariantCulture, text, array)).ToString();
		}

		// Token: 0x060021FE RID: 8702 RVA: 0x0007DB78 File Offset: 0x0007BD78
		public void ValidateConfiguration(int numOfDiffs)
		{
			ExtendedDiagnostics.EnsureArgumentIsPositive(numOfDiffs, "numOfDiffs");
			if (this.Name == null)
			{
				throw new CCSValidationException("Received a null Resolution name");
			}
			this.Name = this.Name;
			this.AlarmThreshold = this.AlarmThreshold;
			this.FaultThresholdPerPeriod = this.FaultThresholdPerPeriod;
			this.MaxPeriodsInHistory = this.MaxPeriodsInHistory;
			this.SuccessTolerance = this.SuccessTolerance;
			this.TimePeriodLength = this.TimePeriodLength;
			this.TrafficQuotaPerPeriod = this.TrafficQuotaPerPeriod;
			if (this.AlarmThreshold > this.MaxPeriodsInHistory)
			{
				throw new CCSValidationException(string.Format(CultureInfo.InvariantCulture, "AlarmThreshold: {0} is bigger than MaxPeriodsInHistory: {1}", new object[] { this.AlarmThreshold, this.MaxPeriodsInHistory }));
			}
			FailureAnalyzerConfigurationHelper.ValidateCollectionSize(this.DifferentiatorIndexes, 1, numOfDiffs, "indices");
			FailureAnalyzerConfigurationHelper.ValidateUniqueness<int>(this.DifferentiatorIndexes, "index");
			int num = numOfDiffs - 1;
			foreach (int num2 in this.DifferentiatorIndexes)
			{
				if (num2 < 0 || num2 > num)
				{
					throw new CCSValidationException(string.Format(CultureInfo.InvariantCulture, "Index {0} is out of range. Received: {0}. Min: {1}. Max: {2}.", new object[] { num2, 0, num }));
				}
			}
		}

		// Token: 0x04000BB7 RID: 2999
		private string m_Name;

		// Token: 0x04000BB8 RID: 3000
		private int m_TimePeriodLength;

		// Token: 0x04000BB9 RID: 3001
		private double m_SuccessTolerance;

		// Token: 0x04000BBA RID: 3002
		private int m_MaxPeriodsInHistory;

		// Token: 0x04000BBB RID: 3003
		private int m_FaultThresholdPerPeriod;

		// Token: 0x04000BBC RID: 3004
		private int m_TrafficQuotaPerPeriod;

		// Token: 0x04000BBD RID: 3005
		private int m_AlarmThreshold;

		// Token: 0x04000BBF RID: 3007
		private const int c_minNumOfIndices = 1;

		// Token: 0x04000BC0 RID: 3008
		private const int c_minAllowedIndex = 0;
	}
}
