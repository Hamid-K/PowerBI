using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportPublishing
{
	// Token: 0x020003B1 RID: 945
	internal abstract class DynamicImageOrCustomUniqueNameValidator : UniqueNameValidator
	{
		// Token: 0x06002681 RID: 9857
		internal abstract bool Validate(Severity severity, string propertyName, ObjectType objectType, string objectName, string propertyNameValue, ErrorContext errorContext);
	}
}
