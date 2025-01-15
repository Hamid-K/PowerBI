using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.DataProcessing;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x020007F9 RID: 2041
	internal abstract class RestartContext
	{
		// Token: 0x060071E6 RID: 29158 RVA: 0x001D8F69 File Offset: 0x001D7169
		public RestartContext(RestartMode mode)
		{
			this.m_restartMode = mode;
		}

		// Token: 0x170026AD RID: 9901
		// (get) Token: 0x060071E7 RID: 29159 RVA: 0x001D8F78 File Offset: 0x001D7178
		public RestartMode RestartMode
		{
			get
			{
				return this.m_restartMode;
			}
		}

		// Token: 0x170026AE RID: 9902
		// (get) Token: 0x060071E8 RID: 29160 RVA: 0x001D8F80 File Offset: 0x001D7180
		public bool IsRowLevelRestart
		{
			get
			{
				return this.m_restartMode != RestartMode.Rom;
			}
		}

		// Token: 0x060071E9 RID: 29161
		public abstract RowSkippingControlFlag DoesNotMatchRowRecordField(OnDemandProcessingContext odpContext, Microsoft.ReportingServices.ReportIntermediateFormat.RecordField[] recordFields);

		// Token: 0x060071EA RID: 29162 RVA: 0x001D8F90 File Offset: 0x001D7190
		public RowSkippingControlFlag CompareFieldWithScopeValueAndStopOnInequality(OnDemandProcessingContext odpContext, Microsoft.ReportingServices.ReportIntermediateFormat.RecordField field, object scopeValue, bool isSortedAscending, ObjectType objectType, string objectName, string propertyName)
		{
			if (field == null)
			{
				throw new ReportProcessingException(ErrorCode.rsMissingFieldInStartAt);
			}
			int num = odpContext.CompareAndStopOnError(field.FieldValue, scopeValue, objectType, objectName, propertyName, false);
			if (num < 0)
			{
				if (!isSortedAscending)
				{
					return RowSkippingControlFlag.Stop;
				}
				return RowSkippingControlFlag.Skip;
			}
			else
			{
				if (num <= 0)
				{
					return RowSkippingControlFlag.ExactMatch;
				}
				if (!isSortedAscending)
				{
					return RowSkippingControlFlag.Skip;
				}
				return RowSkippingControlFlag.Stop;
			}
		}

		// Token: 0x060071EB RID: 29163
		public abstract List<ScopeValueFieldName> GetScopeValueFieldNameCollection(Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet);

		// Token: 0x060071EC RID: 29164
		public abstract void TraceStartAtRecoveryMessage();

		// Token: 0x04003A8A RID: 14986
		private readonly RestartMode m_restartMode;
	}
}
