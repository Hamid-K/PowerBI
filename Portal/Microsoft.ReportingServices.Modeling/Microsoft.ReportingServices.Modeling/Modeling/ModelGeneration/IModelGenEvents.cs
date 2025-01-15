using System;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration
{
	// Token: 0x020000EB RID: 235
	internal interface IModelGenEvents : ICancelEvent
	{
		// Token: 0x06000C17 RID: 3095
		void RaiseProgress(ProgressEventArgs e);

		// Token: 0x06000C18 RID: 3096
		void RaiseLog(ModelGenLogEventArgs e);
	}
}
