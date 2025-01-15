using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000088 RID: 136
	[Serializable]
	internal sealed class ServerConfigurationErrorException : ReportCatalogException
	{
		// Token: 0x0600024D RID: 589 RVA: 0x00004DFA File Offset: 0x00002FFA
		public ServerConfigurationErrorException(Exception innerException, string additionalTraceMessage)
			: base(ErrorCode.rsServerConfigurationError, ErrorStringsWrapper.rsServerConfigurationError(null), innerException, additionalTraceMessage, Array.Empty<object>())
		{
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00004E14 File Offset: 0x00003014
		public ServerConfigurationErrorException(Exception innerException, string additionalTraceMessage, string additionalMessage)
			: base(ErrorCode.rsServerConfigurationError, ErrorStringsWrapper.rsServerConfigurationError(additionalMessage), innerException, additionalTraceMessage, Array.Empty<object>())
		{
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00004E2E File Offset: 0x0000302E
		public ServerConfigurationErrorException(string additionalTraceMessage)
			: base(ErrorCode.rsServerConfigurationError, ErrorStringsWrapper.rsServerConfigurationError(null), null, additionalTraceMessage, Array.Empty<object>())
		{
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00004E48 File Offset: 0x00003048
		private ServerConfigurationErrorException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
