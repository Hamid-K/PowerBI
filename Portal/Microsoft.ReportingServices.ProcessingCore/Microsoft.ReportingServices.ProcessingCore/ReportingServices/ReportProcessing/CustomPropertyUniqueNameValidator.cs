using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000788 RID: 1928
	internal sealed class CustomPropertyUniqueNameValidator : NameValidator
	{
		// Token: 0x06006B7C RID: 27516 RVA: 0x001B33C1 File Offset: 0x001B15C1
		internal CustomPropertyUniqueNameValidator()
		{
		}

		// Token: 0x06006B7D RID: 27517 RVA: 0x001B33CC File Offset: 0x001B15CC
		internal bool Validate(Severity severity, ObjectType objectType, string objectName, string propertyNameValue, ErrorContext errorContext)
		{
			bool flag = true;
			if (propertyNameValue == null || !base.IsUnique(propertyNameValue))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidCustomPropertyName, severity, objectType, objectName, propertyNameValue, Array.Empty<string>());
				flag = false;
			}
			return flag;
		}
	}
}
