using System;
using Microsoft.ReportingServices.DataExtensions;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200062B RID: 1579
	internal sealed class ProgressivePublishingResult
	{
		// Token: 0x060056E3 RID: 22243 RVA: 0x0016EA68 File Offset: 0x0016CC68
		internal ProgressivePublishingResult(ProgressiveReport report, ISerializableValues resultValues, DataSourceInfoCollection dataSources, ProcessingMessageList warnings)
		{
			this.m_report = report;
			this.m_resultValues = resultValues;
			this.m_dataSources = dataSources;
			this.m_warnings = warnings;
		}

		// Token: 0x17001FAA RID: 8106
		// (get) Token: 0x060056E4 RID: 22244 RVA: 0x0016EA8D File Offset: 0x0016CC8D
		public ProgressiveReport Report
		{
			get
			{
				return this.m_report;
			}
		}

		// Token: 0x17001FAB RID: 8107
		// (get) Token: 0x060056E5 RID: 22245 RVA: 0x0016EA95 File Offset: 0x0016CC95
		public ISerializableValues ResultValues
		{
			get
			{
				return this.m_resultValues;
			}
		}

		// Token: 0x17001FAC RID: 8108
		// (get) Token: 0x060056E6 RID: 22246 RVA: 0x0016EA9D File Offset: 0x0016CC9D
		public DataSourceInfoCollection DataSources
		{
			get
			{
				return this.m_dataSources;
			}
		}

		// Token: 0x17001FAD RID: 8109
		// (get) Token: 0x060056E7 RID: 22247 RVA: 0x0016EAA5 File Offset: 0x0016CCA5
		internal ProcessingMessageList Warnings
		{
			get
			{
				return this.m_warnings;
			}
		}

		// Token: 0x04002DDD RID: 11741
		private readonly ProgressiveReport m_report;

		// Token: 0x04002DDE RID: 11742
		private readonly ISerializableValues m_resultValues;

		// Token: 0x04002DDF RID: 11743
		private readonly DataSourceInfoCollection m_dataSources;

		// Token: 0x04002DE0 RID: 11744
		private readonly ProcessingMessageList m_warnings;
	}
}
