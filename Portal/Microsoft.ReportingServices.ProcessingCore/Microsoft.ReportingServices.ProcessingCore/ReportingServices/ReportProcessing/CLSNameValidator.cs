using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000783 RID: 1923
	internal sealed class CLSNameValidator : NameValidator
	{
		// Token: 0x06006B6F RID: 27503 RVA: 0x001B3104 File Offset: 0x001B1304
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
