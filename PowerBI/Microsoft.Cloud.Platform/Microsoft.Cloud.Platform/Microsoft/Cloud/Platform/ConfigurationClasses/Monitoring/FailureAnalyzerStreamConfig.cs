using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Cloud.Platform.Configuration;
using Microsoft.Cloud.Platform.ConfigurationManagement;

namespace Microsoft.Cloud.Platform.ConfigurationClasses.Monitoring
{
	// Token: 0x02000445 RID: 1093
	[Serializable]
	public sealed class FailureAnalyzerStreamConfig : ConfigurationClass
	{
		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x060021E4 RID: 8676 RVA: 0x0007D722 File Offset: 0x0007B922
		// (set) Token: 0x060021E5 RID: 8677 RVA: 0x0007D72A File Offset: 0x0007B92A
		[ConfigurationProperty]
		public string StreamId
		{
			get
			{
				return this.m_StreamId;
			}
			set
			{
				base.ValidateRegexMatching(value, ".+");
				this.m_StreamId = value;
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x060021E6 RID: 8678 RVA: 0x0007D73F File Offset: 0x0007B93F
		// (set) Token: 0x060021E7 RID: 8679 RVA: 0x0007D747 File Offset: 0x0007B947
		[ConfigurationProperty]
		public int NumOfDifferentiators
		{
			get
			{
				return this.m_NumOfDifferentiators;
			}
			set
			{
				base.ValidateMoreOrEqual((double)value, 1.0);
				base.ValidateLessOrEqual((double)value, 3.0);
				this.m_NumOfDifferentiators = value;
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x060021E8 RID: 8680 RVA: 0x0007D772 File Offset: 0x0007B972
		// (set) Token: 0x060021E9 RID: 8681 RVA: 0x0007D77A File Offset: 0x0007B97A
		[ConfigurationProperty]
		public ConfigurationCollection<AnalysisResolutionConfig> AnalysisResolutionConfigList { get; set; }

		// Token: 0x060021EA RID: 8682 RVA: 0x0007D784 File Offset: 0x0007B984
		public override string ToString()
		{
			IEnumerable<string> enumerable = this.AnalysisResolutionConfigList.Select((AnalysisResolutionConfig resolutionConfig) => resolutionConfig.ToString());
			return string.Format(CultureInfo.InvariantCulture, "Stream id: {0}. Number of differentiators: {1}. Number of resolutions: {2}. Individual resolution configs in following lines: {3} {4}", new object[]
			{
				this.StreamId,
				this.NumOfDifferentiators,
				this.AnalysisResolutionConfigList.Count,
				Environment.NewLine,
				string.Join(Environment.NewLine, enumerable.ToArray<string>())
			});
		}

		// Token: 0x060021EB RID: 8683 RVA: 0x0007D818 File Offset: 0x0007BA18
		public void ValidateConfiguration()
		{
			this.NumOfDifferentiators = this.NumOfDifferentiators;
			if (this.StreamId == null)
			{
				throw new CCSValidationException("Received a null Stream ID");
			}
			this.StreamId = this.StreamId;
			FailureAnalyzerConfigurationHelper.ValidateCollectionSize(this.AnalysisResolutionConfigList, 1, 3, "resolutions");
			FailureAnalyzerConfigurationHelper.ValidateUniqueness<string>(this.AnalysisResolutionConfigList.Select((AnalysisResolutionConfig resolutionConfig) => resolutionConfig.Name), "resolution mame");
			foreach (AnalysisResolutionConfig analysisResolutionConfig in this.AnalysisResolutionConfigList)
			{
				analysisResolutionConfig.ValidateConfiguration(this.NumOfDifferentiators);
			}
			FailureAnalyzerConfigurationHelper.ValidateUniqueness<string>(this.AnalysisResolutionConfigList.Select(delegate(AnalysisResolutionConfig resolutionConfig)
			{
				IEnumerable<int> enumerable = resolutionConfig.DifferentiatorIndexes.OrderBy((int i) => i);
				string text = string.Empty;
				foreach (int num in enumerable)
				{
					text = text + num.ToString(CultureInfo.InvariantCulture) + ",";
				}
				return text;
			}), "indices of resolution");
		}

		// Token: 0x04000BB2 RID: 2994
		private string m_StreamId;

		// Token: 0x04000BB3 RID: 2995
		private int m_NumOfDifferentiators;

		// Token: 0x04000BB5 RID: 2997
		private const int c_maxResolutions = 3;

		// Token: 0x04000BB6 RID: 2998
		private const int c_minResolutions = 1;
	}
}
