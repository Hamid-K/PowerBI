using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.DataShaping.InternalContracts;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.PowerBI.Query.Contracts;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation
{
	// Token: 0x02000052 RID: 82
	[Serializable]
	internal sealed class DataShapeQueryTranslationException : DataShapeEngineException, IErrorContextContainer
	{
		// Token: 0x060003D6 RID: 982 RVA: 0x0000CAFC File Offset: 0x0000ACFC
		private DataShapeQueryTranslationException(string errorCode, string message, Exception innerException)
			: base(errorCode, message, innerException)
		{
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000CB07 File Offset: 0x0000AD07
		private DataShapeQueryTranslationException(string errorCode, string message, Exception innerException, ErrorSource source, IReadOnlyList<AdditionalMessage> additionalMessages, TranslationErrorContext errorContext)
			: base(errorCode, message, innerException, source, additionalMessages)
		{
			this.m_errorContext = errorContext;
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0000CB1E File Offset: 0x0000AD1E
		internal static DataShapeQueryTranslationException Create(Exception innerException)
		{
			return new DataShapeQueryTranslationException("rsInternalError", DsqtStrings.UnexpectedError, innerException);
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000CB30 File Offset: 0x0000AD30
		internal static DataShapeQueryTranslationException Create(string message)
		{
			return new DataShapeQueryTranslationException("rsInternalError", message, null);
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000CB40 File Offset: 0x0000AD40
		internal static DataShapeQueryTranslationException Create(TranslationErrorContext errorContext)
		{
			ErrorSource errorSource;
			string errorMessage = DataShapeQueryTranslationException.GetErrorMessage(errorContext, out errorSource);
			List<AdditionalMessage> additionalMessages = DataShapeQueryTranslationException.GetAdditionalMessages(errorContext);
			return new DataShapeQueryTranslationException("rsDataShapeQueryTranslationError", errorMessage, null, errorSource, additionalMessages, errorContext);
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0000CB6C File Offset: 0x0000AD6C
		internal DataShapeQueryTranslationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.m_errorContext = (TranslationErrorContext)info.GetValue("ErrorContext", typeof(TranslationErrorContext));
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0000CB96 File Offset: 0x0000AD96
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ErrorContext", this.m_errorContext);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0000CBB4 File Offset: 0x0000ADB4
		private static string GetErrorMessage(TranslationErrorContext errorContext, out ErrorSource source)
		{
			source = ErrorSource.Unknown;
			if (errorContext == null)
			{
				return DataShapeQueryTranslationException.DefaultExceptionMessage;
			}
			TranslationMessage highestPriorityError = errorContext.GetHighestPriorityError();
			if (highestPriorityError == null)
			{
				return DataShapeQueryTranslationException.DefaultExceptionMessage;
			}
			source = highestPriorityError.Source;
			return highestPriorityError.Message;
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000CBEC File Offset: 0x0000ADEC
		private static List<AdditionalMessage> GetAdditionalMessages(TranslationErrorContext errorContext)
		{
			if (errorContext == null)
			{
				return null;
			}
			List<AdditionalMessage> list = new List<AdditionalMessage>();
			foreach (TranslationMessage translationMessage in errorContext.Messages)
			{
				AdditionalMessage additionalMessage = new AdditionalMessage(translationMessage.ErrorCode.ToString(), translationMessage.Severity.ToString(), translationMessage.Message, translationMessage.ObjectType.ToString(), translationMessage.ObjectId, translationMessage.PropertyName, translationMessage.AffectedItems, translationMessage.Line, translationMessage.Position);
				list.Add(additionalMessage);
			}
			return list;
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060003DF RID: 991 RVA: 0x0000CCB0 File Offset: 0x0000AEB0
		public TranslationErrorContext TranslationMessages
		{
			get
			{
				return this.m_errorContext;
			}
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0000CCB8 File Offset: 0x0000AEB8
		public bool HasMessages()
		{
			return this.m_errorContext != null && this.m_errorContext.HasMessage;
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000CCCF File Offset: 0x0000AECF
		public string SummarizeMessages()
		{
			if (this.m_errorContext == null)
			{
				return string.Empty;
			}
			return this.m_errorContext.SummarizeMessages();
		}

		// Token: 0x040001A1 RID: 417
		private const string ErrorContextName = "ErrorContext";

		// Token: 0x040001A2 RID: 418
		private static readonly string DefaultExceptionMessage = DsqtStrings.InternalDataShapeQueryError("rsDataShapeQueryTranslationError");

		// Token: 0x040001A3 RID: 419
		private readonly TranslationErrorContext m_errorContext;
	}
}
