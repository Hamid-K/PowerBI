using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.DataShaping.InternalContracts;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.PowerBI.Query.Contracts;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x0200004A RID: 74
	[Serializable]
	internal sealed class DataShapeGenerationException : DataShapeEngineException, IErrorContextContainer
	{
		// Token: 0x06000271 RID: 625 RVA: 0x0000A91E File Offset: 0x00008B1E
		private DataShapeGenerationException(string errorCode, string message, Exception innerException)
			: base(errorCode, message, innerException)
		{
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000A929 File Offset: 0x00008B29
		private DataShapeGenerationException(string errorCode, string message, Exception innerException, ErrorSource source, IReadOnlyList<AdditionalMessage> additionalMessages, DataShapeGenerationErrorContext errorContext)
			: base(errorCode, message, innerException, source, additionalMessages)
		{
			this._errorContext = errorContext;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000A940 File Offset: 0x00008B40
		internal DataShapeGenerationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this._errorContext = (DataShapeGenerationErrorContext)info.GetValue("ErrorContext", typeof(DataShapeGenerationErrorContext));
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000274 RID: 628 RVA: 0x0000A96A File Offset: 0x00008B6A
		internal DataShapeGenerationErrorContext GenerationErrorContext
		{
			get
			{
				return this._errorContext;
			}
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000A972 File Offset: 0x00008B72
		internal static DataShapeGenerationException Create(Exception innerException)
		{
			return new DataShapeGenerationException("rsInternalError", DataShapeGenerationException.DefaultExceptionMessage, innerException);
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000A984 File Offset: 0x00008B84
		internal static DataShapeGenerationException Create(DataShapeGenerationErrorContext errorContext)
		{
			ErrorSource errorSource;
			string text;
			string errorMessage = DataShapeGenerationException.GetErrorMessage(errorContext, out errorSource, out text);
			List<AdditionalMessage> additionalMessages = DataShapeGenerationException.GetAdditionalMessages(errorContext);
			return new DataShapeGenerationException(text, errorMessage, null, errorSource, additionalMessages, errorContext);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000A9AE File Offset: 0x00008BAE
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ErrorContext", this.GenerationErrorContext);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000A9CC File Offset: 0x00008BCC
		private static string GetErrorMessage(DataShapeGenerationErrorContext errorContext, out ErrorSource source, out string errorCode)
		{
			errorCode = "rsDataShapeQueryGenerationError";
			source = ErrorSource.Unknown;
			if (errorContext == null)
			{
				return DataShapeGenerationException.DefaultExceptionMessage;
			}
			DataShapeGenerationMessage highestPriorityError = errorContext.GetHighestPriorityError();
			if (highestPriorityError == null)
			{
				return DataShapeGenerationException.DefaultExceptionMessage;
			}
			errorCode = highestPriorityError.ErrorCode.ToString();
			source = highestPriorityError.Source;
			return highestPriorityError.Message;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000AA20 File Offset: 0x00008C20
		private static List<AdditionalMessage> GetAdditionalMessages(DataShapeGenerationErrorContext errorContext)
		{
			if (errorContext == null)
			{
				return null;
			}
			List<AdditionalMessage> list = new List<AdditionalMessage>();
			foreach (DataShapeGenerationMessage dataShapeGenerationMessage in errorContext.Messages)
			{
				list.Add(new AdditionalMessage(dataShapeGenerationMessage.ErrorCode.ToString(), dataShapeGenerationMessage.Severity.ToString(), dataShapeGenerationMessage.Message, null, null, null, dataShapeGenerationMessage.AffectedItems, null, null));
			}
			return list;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000AACC File Offset: 0x00008CCC
		public bool HasMessages()
		{
			return this._errorContext != null && this._errorContext.HasMessage;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000AAE3 File Offset: 0x00008CE3
		public string SummarizeMessages()
		{
			if (this._errorContext == null)
			{
				return string.Empty;
			}
			return this._errorContext.SummarizeMessages();
		}

		// Token: 0x040001FA RID: 506
		private const string ErrorContextName = "ErrorContext";

		// Token: 0x040001FB RID: 507
		private static readonly string DefaultExceptionMessage = DataShapeGenerationMessages.InternalDataShapeGenerationError("rsDataShapeQueryGenerationError");

		// Token: 0x040001FC RID: 508
		private readonly DataShapeGenerationErrorContext _errorContext;
	}
}
