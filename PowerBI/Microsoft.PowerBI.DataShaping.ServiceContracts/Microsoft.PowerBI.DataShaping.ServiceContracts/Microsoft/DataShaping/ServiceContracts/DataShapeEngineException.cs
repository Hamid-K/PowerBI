using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.DataShaping.Common;
using Microsoft.PowerBI.Query.Contracts;

namespace Microsoft.DataShaping.ServiceContracts
{
	// Token: 0x0200000B RID: 11
	[Serializable]
	public abstract class DataShapeEngineException : Exception
	{
		// Token: 0x0600002F RID: 47 RVA: 0x00002658 File Offset: 0x00000858
		protected DataShapeEngineException(string errorCode, string message)
			: this(errorCode, message, ErrorSource.Unknown)
		{
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002663 File Offset: 0x00000863
		protected DataShapeEngineException(string errorCode, string message, ErrorSource errorSource)
			: base(message)
		{
			this.ErrorCode = errorCode;
			this.ErrorSource = errorSource;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000267A File Offset: 0x0000087A
		protected DataShapeEngineException(string errorCode, string message, Exception innerException)
			: this(errorCode, message, innerException, ErrorSource.Unknown)
		{
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002686 File Offset: 0x00000886
		protected DataShapeEngineException(string errorCode, string message, Exception innerException, ErrorSource errorSource)
			: this(errorCode, message, innerException, errorSource, null)
		{
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002694 File Offset: 0x00000894
		protected DataShapeEngineException(string errorCode, string message, Exception innerException, ErrorSource errorSource, IReadOnlyList<AdditionalMessage> additionalMessages)
			: base(message, innerException)
		{
			this.ErrorCode = errorCode;
			this.ErrorSource = errorSource;
			this.AdditionalMessages = additionalMessages;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000026B8 File Offset: 0x000008B8
		protected DataShapeEngineException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.ErrorCode = (string)info.GetValue("ErrorCode", typeof(string));
			this.ErrorSource = (ErrorSource)info.GetValue("ErrorSource", typeof(ErrorSource));
			this.AdditionalMessages = (IReadOnlyList<AdditionalMessage>)info.GetValue("AdditionalMessages", typeof(IReadOnlyList<AdditionalMessage>));
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000035 RID: 53 RVA: 0x0000272D File Offset: 0x0000092D
		// (set) Token: 0x06000036 RID: 54 RVA: 0x00002735 File Offset: 0x00000935
		internal string ErrorCode { get; private set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000037 RID: 55 RVA: 0x0000273E File Offset: 0x0000093E
		// (set) Token: 0x06000038 RID: 56 RVA: 0x00002746 File Offset: 0x00000946
		internal ErrorSource ErrorSource { get; private set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000039 RID: 57 RVA: 0x0000274F File Offset: 0x0000094F
		// (set) Token: 0x0600003A RID: 58 RVA: 0x00002757 File Offset: 0x00000957
		internal IReadOnlyList<AdditionalMessage> AdditionalMessages { get; private set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002760 File Offset: 0x00000960
		internal string Language
		{
			get
			{
				return "en-US";
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002768 File Offset: 0x00000968
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ErrorCode", this.ErrorCode);
			info.AddValue("ErrorSource", this.ErrorSource);
			info.AddValue("AdditionalMessages", this.AdditionalMessages);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000027B5 File Offset: 0x000009B5
		internal virtual string GetErrorDetails()
		{
			return ErrorUtils.GetInnermostException<Exception>(this).Message;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000027C2 File Offset: 0x000009C2
		internal DataShapeEngineErrorInfo ToErrorInfo()
		{
			return new DataShapeEngineErrorInfo(this.ErrorCode, this.ErrorSource);
		}

		// Token: 0x04000070 RID: 112
		private const string ErrorCodeSlotName = "ErrorCode";

		// Token: 0x04000071 RID: 113
		private const string ErrorSourceSlotName = "ErrorSource";

		// Token: 0x04000072 RID: 114
		private const string AdditionalMessagesSlotName = "AdditionalMessages";

		// Token: 0x04000073 RID: 115
		internal const string DataShapingErrorLanguage = "en-US";
	}
}
