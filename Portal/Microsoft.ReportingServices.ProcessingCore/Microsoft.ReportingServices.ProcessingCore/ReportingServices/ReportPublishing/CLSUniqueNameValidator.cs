using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportPublishing
{
	// Token: 0x020003AB RID: 939
	internal sealed class CLSUniqueNameValidator : NameValidator
	{
		// Token: 0x06002671 RID: 9841 RVA: 0x000B87AE File Offset: 0x000B69AE
		internal CLSUniqueNameValidator(ProcessingErrorCode errorCodeNotCLS, ProcessingErrorCode errorCodeNotUnique, ProcessingErrorCode errorCodeNameLength)
			: base(false)
		{
			this.m_errorCodeNotCLS = errorCodeNotCLS;
			this.m_errorCodeNotUnique = errorCodeNotUnique;
			this.m_errorCodeNameLength = errorCodeNameLength;
			this.m_errorCodeCaseInsensitiveDuplicate = ProcessingErrorCode.rsNone;
		}

		// Token: 0x06002672 RID: 9842 RVA: 0x000B87D3 File Offset: 0x000B69D3
		internal CLSUniqueNameValidator(ProcessingErrorCode errorCodeNotCLS, ProcessingErrorCode errorCodeNotUnique, ProcessingErrorCode errorCodeNameLength, ProcessingErrorCode errorCodeCaseInsensitiveDuplicate)
			: base(true)
		{
			this.m_errorCodeNotCLS = errorCodeNotCLS;
			this.m_errorCodeNotUnique = errorCodeNotUnique;
			this.m_errorCodeNameLength = errorCodeNameLength;
			this.m_errorCodeCaseInsensitiveDuplicate = errorCodeCaseInsensitiveDuplicate;
		}

		// Token: 0x06002673 RID: 9843 RVA: 0x000B87FC File Offset: 0x000B69FC
		internal bool Validate(ObjectType objectType, string name, ErrorContext errorContext)
		{
			bool flag = true;
			if (string.IsNullOrEmpty(name) || name.Length > 256)
			{
				errorContext.Register(this.m_errorCodeNameLength, Severity.Error, objectType, name, "Name", new string[] { "256" });
				flag = false;
			}
			if (!NameValidator.IsCLSCompliant(name))
			{
				errorContext.Register(this.m_errorCodeNotCLS, Severity.Error, objectType, name, "Name", Array.Empty<string>());
				flag = false;
			}
			if (!base.IsUnique(name))
			{
				errorContext.Register(this.m_errorCodeNotUnique, Severity.Error, objectType, name, "Name", Array.Empty<string>());
				flag = false;
			}
			if (base.IsCaseInsensitiveDuplicate(name))
			{
				errorContext.Register(this.m_errorCodeCaseInsensitiveDuplicate, Severity.Warning, objectType, name, "Name", Array.Empty<string>());
			}
			return flag;
		}

		// Token: 0x06002674 RID: 9844 RVA: 0x000B88B4 File Offset: 0x000B6AB4
		internal bool Validate(string name, ObjectType objectType, string objectName, ErrorContext errorContext)
		{
			bool flag = true;
			if (string.IsNullOrEmpty(name) || name.Length > 256)
			{
				errorContext.Register(this.m_errorCodeNameLength, Severity.Error, objectType, objectName, "Name", new string[] { "256" });
				flag = false;
			}
			if (!NameValidator.IsCLSCompliant(name))
			{
				errorContext.Register(this.m_errorCodeNotCLS, Severity.Error, objectType, objectName, "Name", new string[] { name });
				flag = false;
			}
			if (!base.IsUnique(name))
			{
				errorContext.Register(this.m_errorCodeNotUnique, Severity.Error, objectType, objectName, "Name", new string[] { name });
				flag = false;
			}
			return flag;
		}

		// Token: 0x06002675 RID: 9845 RVA: 0x000B8954 File Offset: 0x000B6B54
		internal bool Validate(string name, string dataField, string dataSetName, ErrorContext errorContext)
		{
			bool flag = true;
			if (string.IsNullOrEmpty(name) || name.Length > 256)
			{
				errorContext.Register(this.m_errorCodeNameLength, Severity.Error, ObjectType.Field, dataField, "Name", new string[] { name, dataSetName, "256" });
				flag = false;
			}
			if (!NameValidator.IsCLSCompliant(name))
			{
				errorContext.Register(this.m_errorCodeNotCLS, Severity.Error, ObjectType.Field, dataField, "Name", new string[] { name, dataSetName });
				flag = false;
			}
			if (!base.IsUnique(name))
			{
				errorContext.Register(this.m_errorCodeNotUnique, Severity.Error, ObjectType.Field, dataField, "Name", new string[] { name, dataSetName });
				flag = false;
			}
			return flag;
		}

		// Token: 0x04001639 RID: 5689
		private ProcessingErrorCode m_errorCodeNotCLS;

		// Token: 0x0400163A RID: 5690
		private ProcessingErrorCode m_errorCodeNotUnique;

		// Token: 0x0400163B RID: 5691
		private ProcessingErrorCode m_errorCodeNameLength;

		// Token: 0x0400163C RID: 5692
		private ProcessingErrorCode m_errorCodeCaseInsensitiveDuplicate;
	}
}
