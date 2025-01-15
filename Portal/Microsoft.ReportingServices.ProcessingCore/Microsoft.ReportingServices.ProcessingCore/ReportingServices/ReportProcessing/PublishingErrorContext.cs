using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000630 RID: 1584
	internal sealed class PublishingErrorContext : ErrorContext
	{
		// Token: 0x060056F3 RID: 22259 RVA: 0x0016EC36 File Offset: 0x0016CE36
		internal override ProcessingMessage Register(ProcessingErrorCode code, Severity severity, ObjectType objectType, string objectName, string propertyName, params string[] arguments)
		{
			return this.Register(code, severity, objectType, objectName, propertyName, null, arguments);
		}

		// Token: 0x060056F4 RID: 22260 RVA: 0x0016EC48 File Offset: 0x0016CE48
		internal override ProcessingMessage Register(ProcessingErrorCode code, Severity severity, ObjectType objectType, string objectName, string propertyName, ProcessingMessageList innerMessages, params string[] arguments)
		{
			return this.Register(null, code, severity, objectType, objectName, propertyName, innerMessages, arguments);
		}

		// Token: 0x060056F5 RID: 22261 RVA: 0x0016EC68 File Offset: 0x0016CE68
		internal override ProcessingMessage Register(string diagnosticDetails, ProcessingErrorCode code, Severity severity, ObjectType objectType, string objectName, string propertyName, ProcessingMessageList innerMessages, params string[] arguments)
		{
			if (this.m_suspendErrors)
			{
				return null;
			}
			if (this.m_messages == null)
			{
				this.m_messages = new ProcessingMessageList();
			}
			ProcessingMessage processingMessage = null;
			if (this.m_messages.Count < 100 || (severity == Severity.Error && !this.m_hasError))
			{
				processingMessage = ErrorContext.CreateProcessingMessage(code, severity, objectType, objectName, propertyName, diagnosticDetails, innerMessages, arguments);
				this.m_messages.Add(processingMessage);
			}
			if (severity == Severity.Error)
			{
				this.m_hasError = true;
			}
			return processingMessage;
		}

		// Token: 0x04002DE5 RID: 11749
		private const int MaxNumberOfMessages = 100;
	}
}
