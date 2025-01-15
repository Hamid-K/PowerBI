using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportPublishing
{
	// Token: 0x020003AD RID: 941
	internal sealed class ScopeNameValidator : NameValidator
	{
		// Token: 0x06002678 RID: 9848 RVA: 0x000B8B37 File Offset: 0x000B6D37
		internal ScopeNameValidator()
			: base(false)
		{
		}

		// Token: 0x06002679 RID: 9849 RVA: 0x000B8B40 File Offset: 0x000B6D40
		internal bool Validate(bool isGrouping, string scopeName, ObjectType objectType, string objectName, ErrorContext errorContext)
		{
			return this.Validate(isGrouping, scopeName, objectType, objectName, errorContext, true);
		}

		// Token: 0x0600267A RID: 9850 RVA: 0x000B8B50 File Offset: 0x000B6D50
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
