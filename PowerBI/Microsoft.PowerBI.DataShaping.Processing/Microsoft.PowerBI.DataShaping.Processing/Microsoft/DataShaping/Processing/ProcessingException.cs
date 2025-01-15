using System;
using System.Runtime.Serialization;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.PowerBI.Query.Contracts;

namespace Microsoft.DataShaping.Processing
{
	// Token: 0x02000012 RID: 18
	[Serializable]
	internal sealed class ProcessingException : DataShapeEngineException
	{
		// Token: 0x06000084 RID: 132 RVA: 0x00002B00 File Offset: 0x00000D00
		internal ProcessingException(string errorCode, string message, Exception innerException, ErrorSource source)
			: base(errorCode, message, innerException, source)
		{
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00002B0D File Offset: 0x00000D0D
		internal ProcessingException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00002B17 File Offset: 0x00000D17
		internal override string GetErrorDetails()
		{
			if (base.ErrorCode == "rsInternalError")
			{
				return base.GetErrorDetails();
			}
			return this.Message;
		}
	}
}
