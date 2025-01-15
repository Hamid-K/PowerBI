using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200060C RID: 1548
	internal interface IErrorContext
	{
		// Token: 0x0600554D RID: 21837
		void Register(ProcessingErrorCode code, Severity severity, params string[] arguments);

		// Token: 0x0600554E RID: 21838
		void Register(ProcessingErrorCode code, Severity severity, ObjectType objectType, string objectName, string propertyName, params string[] arguments);
	}
}
