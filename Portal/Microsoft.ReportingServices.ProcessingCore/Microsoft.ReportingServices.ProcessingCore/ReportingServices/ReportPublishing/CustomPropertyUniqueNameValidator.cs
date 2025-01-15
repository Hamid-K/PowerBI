using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportPublishing
{
	// Token: 0x020003B2 RID: 946
	internal sealed class CustomPropertyUniqueNameValidator : DynamicImageOrCustomUniqueNameValidator
	{
		// Token: 0x06002683 RID: 9859 RVA: 0x000B8CFB File Offset: 0x000B6EFB
		internal CustomPropertyUniqueNameValidator()
		{
		}

		// Token: 0x06002684 RID: 9860 RVA: 0x000B8D03 File Offset: 0x000B6F03
		internal override bool Validate(Severity severity, ObjectType objectType, string objectName, string propertyNameValue, ErrorContext errorContext)
		{
			Global.Tracer.Assert(false);
			return this.Validate(severity, "", objectType, objectName, propertyNameValue, errorContext);
		}

		// Token: 0x06002685 RID: 9861 RVA: 0x000B8D24 File Offset: 0x000B6F24
		internal override bool Validate(Severity severity, string propertyName, ObjectType objectType, string objectName, string propertyNameValue, ErrorContext errorContext)
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
