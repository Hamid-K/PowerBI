using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000116 RID: 278
	internal sealed class RenderForHistory : RenderForNewSession
	{
		// Token: 0x06000B27 RID: 2855 RVA: 0x000297E0 File Offset: 0x000279E0
		public RenderForHistory(IExecutionDataProvider provider, ExecutionParameters execInfo)
			: base(provider, execInfo)
		{
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x000297EA File Offset: 0x000279EA
		protected override RenderStrategyBase GetExecutionStrategy()
		{
			return new RenderFromHistory(this);
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x00005C88 File Offset: 0x00003E88
		protected override ParameterInfoCollection GetReportParameterDefinitions()
		{
			return null;
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x000297F2 File Offset: 0x000279F2
		protected override ReportExecutionDefinition GetReportExecutionMetadata()
		{
			return base.DataProvider.GetHistorySnapshot(base.RequestInfo.Session, base.RequestInfo.ReportContext);
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000B2B RID: 2859 RVA: 0x000053DC File Offset: 0x000035DC
		protected override bool ClearShowHideStateBeforeExecution
		{
			get
			{
				return true;
			}
		}
	}
}
