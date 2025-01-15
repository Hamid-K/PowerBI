using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006C3 RID: 1731
	internal interface IIndexInto
	{
		// Token: 0x06005CFB RID: 23803
		object GetChildAt(int index, out NonComputedUniqueNames nonCompNames);
	}
}
