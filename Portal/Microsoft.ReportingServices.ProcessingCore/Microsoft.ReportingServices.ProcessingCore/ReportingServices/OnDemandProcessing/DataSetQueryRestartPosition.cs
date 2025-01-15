using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.ReportingServices.DataProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x020007F8 RID: 2040
	internal sealed class DataSetQueryRestartPosition
	{
		// Token: 0x060071E1 RID: 29153 RVA: 0x001D8DDF File Offset: 0x001D6FDF
		public DataSetQueryRestartPosition(List<RestartContext> restartPosition)
		{
			this.m_restartPosition = restartPosition;
			this.m_enableRowSkipping = true;
		}

		// Token: 0x060071E2 RID: 29154 RVA: 0x001D8DF8 File Offset: 0x001D6FF8
		[Conditional("DEBUG")]
		private void ValidateRestartPosition(List<RestartContext> restartPosition)
		{
			Global.Tracer.Assert(restartPosition != null && restartPosition.Count > 0, "Restart position should be non-null and non-empty");
			foreach (RestartContext restartContext in restartPosition)
			{
				Global.Tracer.Assert(restartContext.RestartMode != RestartMode.Rom, "DataSetQueryRestartPosition does not handle ROM restart values");
			}
		}

		// Token: 0x060071E3 RID: 29155 RVA: 0x001D8E78 File Offset: 0x001D7078
		public List<ScopeValueFieldName> GetQueryRestartPosition(Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet)
		{
			List<ScopeValueFieldName> list = null;
			for (int i = 0; i < this.m_restartPosition.Count; i++)
			{
				RestartContext restartContext = this.m_restartPosition[i];
				if (restartContext.RestartMode == RestartMode.Query)
				{
					if (list == null)
					{
						list = new List<ScopeValueFieldName>();
					}
					list.AddRange(restartContext.GetScopeValueFieldNameCollection(dataSet));
				}
			}
			return list;
		}

		// Token: 0x060071E4 RID: 29156 RVA: 0x001D8ECC File Offset: 0x001D70CC
		public bool ShouldSkip(OnDemandProcessingContext odpContext, Microsoft.ReportingServices.ReportIntermediateFormat.RecordRow row)
		{
			if (row != null && this.m_enableRowSkipping)
			{
				foreach (RestartContext restartContext in this.m_restartPosition)
				{
					switch (restartContext.DoesNotMatchRowRecordField(odpContext, row.RecordFields))
					{
					case RowSkippingControlFlag.Skip:
						if (restartContext.RestartMode == RestartMode.Query)
						{
							restartContext.TraceStartAtRecoveryMessage();
						}
						return true;
					case RowSkippingControlFlag.Stop:
						return false;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x060071E5 RID: 29157 RVA: 0x001D8F60 File Offset: 0x001D7160
		public void DisableRowSkipping(Microsoft.ReportingServices.ReportIntermediateFormat.RecordRow row)
		{
			this.m_enableRowSkipping = false;
		}

		// Token: 0x04003A88 RID: 14984
		private List<RestartContext> m_restartPosition;

		// Token: 0x04003A89 RID: 14985
		private bool m_enableRowSkipping;
	}
}
