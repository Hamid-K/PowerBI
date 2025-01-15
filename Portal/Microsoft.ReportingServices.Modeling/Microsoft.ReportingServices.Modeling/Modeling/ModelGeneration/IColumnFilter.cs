using System;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration
{
	// Token: 0x020000E2 RID: 226
	internal interface IColumnFilter
	{
		// Token: 0x06000BE1 RID: 3041
		bool IsMatch(DsvColumn column);
	}
}
