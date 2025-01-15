using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportPublishing
{
	// Token: 0x020003AE RID: 942
	internal sealed class ParameterNameValidator : NameValidator
	{
		// Token: 0x0600267B RID: 9851 RVA: 0x000B8BD5 File Offset: 0x000B6DD5
		internal ParameterNameValidator()
			: base(false)
		{
		}

		// Token: 0x0600267C RID: 9852 RVA: 0x000B8BE0 File Offset: 0x000B6DE0
		internal bool Validate(string parameterName, ObjectType objectType, string objectName, ErrorContext errorContext)
		{
			bool flag = true;
			if (string.IsNullOrEmpty(parameterName) || parameterName.Length > 256)
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidParameterNameLength, Severity.Error, objectType, objectName, "Name", new string[] { parameterName, "256" });
				flag = false;
			}
			if (!NameValidator.IsCLSCompliant(parameterName))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidParameterNameNotCLSCompliant, Severity.Error, objectType, objectName, "Name", new string[] { parameterName });
				flag = false;
			}
			return flag;
		}
	}
}
