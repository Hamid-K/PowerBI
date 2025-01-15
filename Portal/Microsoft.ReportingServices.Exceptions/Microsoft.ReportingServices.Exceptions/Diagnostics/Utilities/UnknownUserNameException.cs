using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000071 RID: 113
	[Serializable]
	internal sealed class UnknownUserNameException : ReportCatalogException
	{
		// Token: 0x06000218 RID: 536 RVA: 0x00004A24 File Offset: 0x00002C24
		public UnknownUserNameException(string userName)
			: base(ErrorCode.rsUnknownUserName, ErrorStringsWrapper.rsUnknownUserName(userName), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00004A3B File Offset: 0x00002C3B
		public UnknownUserNameException(string userName, Exception innerException)
			: base(ErrorCode.rsUnknownUserName, ErrorStringsWrapper.rsUnknownUserName(userName), innerException, null, Array.Empty<object>())
		{
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00004A52 File Offset: 0x00002C52
		private UnknownUserNameException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
