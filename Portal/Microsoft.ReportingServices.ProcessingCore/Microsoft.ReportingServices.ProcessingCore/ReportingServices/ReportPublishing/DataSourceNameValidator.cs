using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportPublishing
{
	// Token: 0x020003AF RID: 943
	internal sealed class DataSourceNameValidator : NameValidator
	{
		// Token: 0x0600267D RID: 9853 RVA: 0x000B8C57 File Offset: 0x000B6E57
		internal DataSourceNameValidator()
			: base(false)
		{
		}

		// Token: 0x0600267E RID: 9854 RVA: 0x000B8C60 File Offset: 0x000B6E60
		internal bool Validate(ObjectType objectType, string objectName, ErrorContext errorContext)
		{
			bool flag = true;
			if (string.IsNullOrEmpty(objectName) || objectName.Length > 256)
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidDataSourceNameLength, Severity.Error, objectType, objectName, "Name", new string[] { "256" });
				flag = false;
			}
			if (!base.IsUnique(objectName))
			{
				errorContext.Register(ProcessingErrorCode.rsDuplicateDataSourceName, Severity.Error, objectType, objectName, "Name", Array.Empty<string>());
				flag = false;
			}
			if (!NameValidator.IsCLSCompliant(objectName))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidDataSourceNameNotCLSCompliant, Severity.Error, objectType, objectName, "Name", Array.Empty<string>());
				flag = false;
			}
			return flag;
		}
	}
}
