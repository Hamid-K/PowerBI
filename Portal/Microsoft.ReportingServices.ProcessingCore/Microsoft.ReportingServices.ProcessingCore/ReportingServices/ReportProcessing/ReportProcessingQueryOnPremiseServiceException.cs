using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020005F5 RID: 1525
	[Serializable]
	internal sealed class ReportProcessingQueryOnPremiseServiceException : ReportProcessingException
	{
		// Token: 0x06005441 RID: 21569 RVA: 0x00162123 File Offset: 0x00160323
		public ReportProcessingQueryOnPremiseServiceException(ErrorCode errorCode, Exception innerException, params object[] arguments)
			: base(errorCode, innerException, arguments)
		{
		}

		// Token: 0x06005442 RID: 21570 RVA: 0x0016212E File Offset: 0x0016032E
		private ReportProcessingQueryOnPremiseServiceException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06005443 RID: 21571 RVA: 0x00162138 File Offset: 0x00160338
		protected override List<RSException.AdditionalMessage> GetAdditionalMessages()
		{
			return new List<RSException.AdditionalMessage>(new RSException.AdditionalMessage[]
			{
				new RSException.AdditionalMessage("OnPremiseServiceException", "Error", base.InnerException.Message, null, null, null, null)
			});
		}

		// Token: 0x04002CDF RID: 11487
		private const string OnPremiseServiceExceptionCode = "OnPremiseServiceException";
	}
}
