using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportPublishing
{
	// Token: 0x020003AC RID: 940
	internal sealed class VariableNameValidator : NameValidator
	{
		// Token: 0x06002676 RID: 9846 RVA: 0x000B8A07 File Offset: 0x000B6C07
		internal VariableNameValidator()
			: base(false)
		{
		}

		// Token: 0x06002677 RID: 9847 RVA: 0x000B8A10 File Offset: 0x000B6C10
		internal bool Validate(string name, ObjectType objectType, string objectName, ErrorContext errorContext, bool isGrouping, string groupingName)
		{
			bool flag = true;
			if (string.IsNullOrEmpty(name) || name.Length > 256)
			{
				if (isGrouping)
				{
					errorContext.Register(ProcessingErrorCode.rsInvalidGroupingVariableNameLength, Severity.Error, objectType, objectName, "Variable", new string[] { name, groupingName, "256" });
				}
				else
				{
					errorContext.Register(ProcessingErrorCode.rsInvalidVariableNameLength, Severity.Error, objectType, objectName, "Variable", new string[] { name, "256" });
				}
				flag = false;
			}
			if (!NameValidator.IsCLSCompliant(name))
			{
				if (isGrouping)
				{
					errorContext.Register(ProcessingErrorCode.rsInvalidGroupingVariableNameNotCLSCompliant, Severity.Error, objectType, objectName, "Variable", new string[] { name, groupingName });
				}
				else
				{
					errorContext.Register(ProcessingErrorCode.rsInvalidVariableNameNotCLSCompliant, Severity.Error, objectType, objectName, "Variable", new string[] { name });
				}
				flag = false;
			}
			if (!base.IsUnique(name))
			{
				if (isGrouping)
				{
					errorContext.Register(ProcessingErrorCode.rsDuplicateGroupingVariableName, Severity.Error, objectType, objectName, "Variable", new string[] { name, groupingName });
				}
				else
				{
					errorContext.Register(ProcessingErrorCode.rsDuplicateVariableName, Severity.Error, objectType, objectName, "Variable", new string[] { name });
				}
				flag = false;
			}
			return flag;
		}
	}
}
