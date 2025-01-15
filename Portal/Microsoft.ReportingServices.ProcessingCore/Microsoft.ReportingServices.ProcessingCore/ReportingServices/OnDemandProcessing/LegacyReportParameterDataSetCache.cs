using System;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x02000838 RID: 2104
	internal class LegacyReportParameterDataSetCache : ReportParameterDataSetCache
	{
		// Token: 0x060075D0 RID: 30160 RVA: 0x001E8C14 File Offset: 0x001E6E14
		internal LegacyReportParameterDataSetCache(ProcessReportParameters aParamProcessor, ParameterInfo aParameter, ParameterDef aParamDef, bool aProcessValidValues, bool aProcessDefaultValues)
			: base(aParamProcessor, aParameter, aParamDef, aProcessValidValues, aProcessDefaultValues)
		{
		}

		// Token: 0x060075D1 RID: 30161 RVA: 0x001E8C24 File Offset: 0x001E6E24
		internal override object GetFieldValue(object aRow, int col)
		{
			FieldImpl[] array = (FieldImpl[])aRow;
			if (array[col].IsMissing)
			{
				return null;
			}
			return array[col].Value;
		}
	}
}
