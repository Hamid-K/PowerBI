using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000090 RID: 144
	[Serializable]
	internal sealed class RPCException : ReportCatalogException
	{
		// Token: 0x0600025D RID: 605 RVA: 0x00004F3A File Offset: 0x0000313A
		public RPCException(Exception exceptionFromRPC)
			: base(ErrorCode.rsRPCError, exceptionFromRPC.Message, exceptionFromRPC.InnerException, null, Array.Empty<object>())
		{
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00004F56 File Offset: 0x00003156
		private RPCException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
