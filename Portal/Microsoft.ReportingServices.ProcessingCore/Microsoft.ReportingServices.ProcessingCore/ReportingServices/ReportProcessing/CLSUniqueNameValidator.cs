using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000784 RID: 1924
	internal sealed class CLSUniqueNameValidator : NameValidator
	{
		// Token: 0x06006B71 RID: 27505 RVA: 0x001B315A File Offset: 0x001B135A
		internal CLSUniqueNameValidator(ProcessingErrorCode errorCodeNotCLS, ProcessingErrorCode errorCodeNotUnique)
		{
			this.m_errorCodeNotCLS = errorCodeNotCLS;
			this.m_errorCodeNotUnique = errorCodeNotUnique;
		}

		// Token: 0x06006B72 RID: 27506 RVA: 0x001B3170 File Offset: 0x001B1370
		internal bool Validate(ObjectType objectType, string name, ErrorContext errorContext)
		{
			bool flag = true;
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
			return flag;
		}

		// Token: 0x06006B73 RID: 27507 RVA: 0x001B31CC File Offset: 0x001B13CC
		internal bool Validate(string name, ObjectType objectType, string objectName, ErrorContext errorContext)
		{
			bool flag = true;
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

		// Token: 0x06006B74 RID: 27508 RVA: 0x001B3234 File Offset: 0x001B1434
		internal bool Validate(string name, string dataField, string dataSetName, ErrorContext errorContext)
		{
			bool flag = true;
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

		// Token: 0x04003611 RID: 13841
		private ProcessingErrorCode m_errorCodeNotCLS;

		// Token: 0x04003612 RID: 13842
		private ProcessingErrorCode m_errorCodeNotUnique;
	}
}
