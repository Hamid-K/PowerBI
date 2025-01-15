using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportPublishing
{
	// Token: 0x020003B3 RID: 947
	internal sealed class DynamicImageObjectUniqueNameValidator : DynamicImageOrCustomUniqueNameValidator
	{
		// Token: 0x06002686 RID: 9862 RVA: 0x000B8D59 File Offset: 0x000B6F59
		internal DynamicImageObjectUniqueNameValidator()
		{
		}

		// Token: 0x06002687 RID: 9863 RVA: 0x000B8D61 File Offset: 0x000B6F61
		internal void Clear()
		{
			this.m_dictionary.Clear();
		}

		// Token: 0x06002688 RID: 9864 RVA: 0x000B8D6E File Offset: 0x000B6F6E
		internal override bool Validate(Severity severity, ObjectType objectType, string objectName, string propertyNameValue, ErrorContext errorContext)
		{
			Global.Tracer.Assert(false);
			return this.Validate(severity, "", objectType, objectName, propertyNameValue, errorContext);
		}

		// Token: 0x06002689 RID: 9865 RVA: 0x000B8D90 File Offset: 0x000B6F90
		internal override bool Validate(Severity severity, string propertyName, ObjectType objectType, string objectName, string propertyNameValue, ErrorContext errorContext)
		{
			bool flag = true;
			if (propertyNameValue == null || !base.IsUnique(propertyNameValue))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidObjectNameNotUnique, severity, objectType, objectName, propertyName, new string[] { propertyNameValue });
				flag = false;
			}
			if (propertyNameValue != null && !NameValidator.IsCLSCompliant(propertyNameValue))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidObjectNameNotCLSCompliant, severity, objectType, objectName, propertyName, new string[] { propertyNameValue });
				flag = false;
			}
			return flag;
		}
	}
}
