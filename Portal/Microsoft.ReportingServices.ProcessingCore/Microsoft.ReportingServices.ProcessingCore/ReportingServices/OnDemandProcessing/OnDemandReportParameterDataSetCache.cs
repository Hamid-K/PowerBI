using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x02000837 RID: 2103
	internal class OnDemandReportParameterDataSetCache : ReportParameterDataSetCache
	{
		// Token: 0x060075CE RID: 30158 RVA: 0x001E8BDC File Offset: 0x001E6DDC
		internal OnDemandReportParameterDataSetCache(ProcessReportParameters aParamProcessor, ParameterInfo aParameter, Microsoft.ReportingServices.ReportIntermediateFormat.ParameterDef aParamDef, bool aProcessValidValues, bool aProcessDefaultValues)
			: base(aParamProcessor, aParameter, aParamDef, aProcessValidValues, aProcessDefaultValues)
		{
		}

		// Token: 0x060075CF RID: 30159 RVA: 0x001E8BEC File Offset: 0x001E6DEC
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
