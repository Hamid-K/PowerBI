using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000787 RID: 1927
	internal sealed class DataSourceNameValidator : NameValidator
	{
		// Token: 0x06006B7A RID: 27514 RVA: 0x001B3388 File Offset: 0x001B1588
		internal bool Validate(ObjectType objectType, string objectName, ErrorContext errorContext)
		{
			bool flag = true;
			if (!base.IsUnique(objectName))
			{
				errorContext.Register(ProcessingErrorCode.rsDuplicateDataSourceName, Severity.Error, objectType, objectName, "Name", Array.Empty<string>());
				flag = false;
			}
			return flag;
		}
	}
}
