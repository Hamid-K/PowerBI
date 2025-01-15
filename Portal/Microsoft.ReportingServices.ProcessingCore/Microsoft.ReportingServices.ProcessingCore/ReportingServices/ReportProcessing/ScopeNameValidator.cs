using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000785 RID: 1925
	internal sealed class ScopeNameValidator : NameValidator
	{
		// Token: 0x06006B75 RID: 27509 RVA: 0x001B32A3 File Offset: 0x001B14A3
		internal bool Validate(bool isGrouping, string scopeName, ObjectType objectType, string objectName, ErrorContext errorContext)
		{
			return this.Validate(isGrouping, scopeName, objectType, objectName, errorContext, true);
		}

		// Token: 0x06006B76 RID: 27510 RVA: 0x001B32B4 File Offset: 0x001B14B4
		internal bool Validate(bool isGrouping, string scopeName, ObjectType objectType, string objectName, ErrorContext errorContext, bool enforceCLSCompliance)
		{
			bool flag = true;
			if (!NameValidator.IsCLSCompliant(scopeName) && enforceCLSCompliance)
			{
				if (isGrouping)
				{
					errorContext.Register(ProcessingErrorCode.rsInvalidGroupingNameNotCLSCompliant, Severity.Error, objectType, objectName, "Name", new string[] { scopeName });
				}
				else
				{
					errorContext.Register(ProcessingErrorCode.rsInvalidNameNotCLSCompliant, Severity.Error, objectType, objectName, "Name", Array.Empty<string>());
				}
				flag = false;
			}
			if (!base.IsUnique(scopeName))
			{
				errorContext.Register(ProcessingErrorCode.rsDuplicateScopeName, Severity.Error, objectType, objectName, "Name", new string[] { scopeName });
				flag = false;
			}
			return flag;
		}
	}
}
