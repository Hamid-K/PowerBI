using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000786 RID: 1926
	internal sealed class ParameterNameValidator : NameValidator
	{
		// Token: 0x06006B78 RID: 27512 RVA: 0x001B3344 File Offset: 0x001B1544
		internal bool Validate(string parameterName, ObjectType objectType, string objectName, ErrorContext errorContext)
		{
			bool flag = true;
			if (!NameValidator.IsCLSCompliant(parameterName))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidParameterNameNotCLSCompliant, Severity.Error, objectType, objectName, "Name", new string[] { parameterName });
				flag = false;
			}
			return flag;
		}
	}
}
