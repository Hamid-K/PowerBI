using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportPublishing
{
	// Token: 0x020003B0 RID: 944
	internal abstract class UniqueNameValidator : NameValidator
	{
		// Token: 0x0600267F RID: 9855 RVA: 0x000B8CEA File Offset: 0x000B6EEA
		internal UniqueNameValidator()
			: base(false)
		{
		}

		// Token: 0x06002680 RID: 9856
		internal abstract bool Validate(Severity severity, ObjectType objectType, string objectName, string propertyNameValue, ErrorContext errorContext);
	}
}
