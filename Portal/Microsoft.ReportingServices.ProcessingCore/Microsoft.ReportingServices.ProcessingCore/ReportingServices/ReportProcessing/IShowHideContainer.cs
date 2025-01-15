using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006C6 RID: 1734
	internal interface IShowHideContainer
	{
		// Token: 0x06005CFE RID: 23806
		void BeginProcessContainer(ReportProcessing.ProcessingContext context);

		// Token: 0x06005CFF RID: 23807
		void EndProcessContainer(ReportProcessing.ProcessingContext context);
	}
}
