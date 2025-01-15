using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000203 RID: 515
	internal interface IRichTextLogger
	{
		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x06001343 RID: 4931
		RSTrace Tracer { get; }

		// Token: 0x06001344 RID: 4932
		void RegisterOutOfRangeSizeWarning(string propertyName, string value, string minVal, string maxVal);

		// Token: 0x06001345 RID: 4933
		void RegisterInvalidValueWarning(string propertyName, string value, int charPosition);

		// Token: 0x06001346 RID: 4934
		void RegisterInvalidColorWarning(string propertyName, string value, int charPosition);

		// Token: 0x06001347 RID: 4935
		void RegisterInvalidSizeWarning(string propertyName, string value, int charPosition);
	}
}
