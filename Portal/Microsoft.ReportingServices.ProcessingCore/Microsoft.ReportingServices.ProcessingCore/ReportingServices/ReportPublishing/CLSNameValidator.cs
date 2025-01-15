using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportPublishing
{
	// Token: 0x020003AA RID: 938
	internal sealed class CLSNameValidator : NameValidator
	{
		// Token: 0x0600266F RID: 9839 RVA: 0x000B8754 File Offset: 0x000B6954
		internal CLSNameValidator()
			: base(false)
		{
		}

		// Token: 0x06002670 RID: 9840 RVA: 0x000B8760 File Offset: 0x000B6960
		internal static bool ValidateDataElementName(ref string elementName, string defaultName, ObjectType objectType, string objectName, string propertyName, ErrorContext errorContext)
		{
			Global.Tracer.Assert(defaultName != null);
			if (elementName == null)
			{
				elementName = defaultName;
			}
			else if (!NameValidator.IsCLSCompliant(elementName))
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidDataElementNameNotCLSCompliant, Severity.Error, objectType, objectName, null, new string[] { propertyName, elementName });
				return false;
			}
			return true;
		}
	}
}
