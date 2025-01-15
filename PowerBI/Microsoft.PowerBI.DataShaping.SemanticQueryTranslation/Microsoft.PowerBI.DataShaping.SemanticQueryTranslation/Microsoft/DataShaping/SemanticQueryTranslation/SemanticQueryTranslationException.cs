using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.PowerBI.Query.Contracts;

namespace Microsoft.DataShaping.SemanticQueryTranslation
{
	// Token: 0x0200000A RID: 10
	[Serializable]
	internal sealed class SemanticQueryTranslationException : DataShapeEngineException
	{
		// Token: 0x06000016 RID: 22 RVA: 0x0000248E File Offset: 0x0000068E
		private SemanticQueryTranslationException(string errorCode, string message, Exception innerException)
			: base(errorCode, message, innerException)
		{
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002499 File Offset: 0x00000699
		private SemanticQueryTranslationException(string errorCode, string errorMessage, ErrorSource source, SemanticQueryTranslationErrorContext errorContext)
			: base(errorCode, errorMessage, source)
		{
			this._errorContext = errorContext;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000024AC File Offset: 0x000006AC
		internal SemanticQueryTranslationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this._errorContext = (SemanticQueryTranslationErrorContext)info.GetValue("ErrorContext", typeof(SemanticQueryTranslationErrorContext));
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000024D6 File Offset: 0x000006D6
		internal static SemanticQueryTranslationException Create(Exception innerException)
		{
			return new SemanticQueryTranslationException("rsInternalError", SemanticQueryTranslationException.DefaultExceptionMessage, innerException);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000024E8 File Offset: 0x000006E8
		internal static SemanticQueryTranslationException Create(SemanticQueryTranslationErrorContext errorContext)
		{
			global::System.ValueTuple<string, string, ErrorSource> valueTuple = SemanticQueryTranslationException.ExtractErrorInfo(errorContext);
			return new SemanticQueryTranslationException(valueTuple.Item1, valueTuple.Item2, valueTuple.Item3, errorContext);
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002514 File Offset: 0x00000714
		internal SemanticQueryTranslationErrorContext ErrorContext
		{
			get
			{
				return this._errorContext;
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000251C File Offset: 0x0000071C
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ErrorContext", this.ErrorContext);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002538 File Offset: 0x00000738
		[return: global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Code", "Message", "Source" })]
		private static global::System.ValueTuple<string, string, ErrorSource> ExtractErrorInfo(SemanticQueryTranslationErrorContext errorContext)
		{
			if (errorContext != null)
			{
				SemanticQueryTranslationMessage highestPriorityError = errorContext.GetHighestPriorityError();
				if (highestPriorityError != null)
				{
					return new global::System.ValueTuple<string, string, ErrorSource>(highestPriorityError.ErrorCode, highestPriorityError.Message, highestPriorityError.Source);
				}
			}
			return new global::System.ValueTuple<string, string, ErrorSource>("UnknownError", SemanticQueryTranslationException.DefaultExceptionMessage, ErrorSource.PowerBI);
		}

		// Token: 0x04000038 RID: 56
		private const string ErrorContextName = "ErrorContext";

		// Token: 0x04000039 RID: 57
		private static readonly string DefaultExceptionMessage = "An error occurred in SemanticQueryTranslation";

		// Token: 0x0400003A RID: 58
		private readonly SemanticQueryTranslationErrorContext _errorContext;
	}
}
