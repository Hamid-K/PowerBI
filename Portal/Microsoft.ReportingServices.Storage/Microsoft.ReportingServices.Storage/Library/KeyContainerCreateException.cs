using System;
using System.Runtime.Serialization;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000023 RID: 35
	[Serializable]
	internal sealed class KeyContainerCreateException : ReportCatalogException
	{
		// Token: 0x06000111 RID: 273 RVA: 0x00007DEF File Offset: 0x00005FEF
		public KeyContainerCreateException(string accountName)
			: base(ErrorCode.rsReportServerKeyContainerError, ErrorStringsWrapper.rsReportServerKeyContainerError(accountName), null, null, Array.Empty<object>())
		{
			RSEventLog.Current.WriteError(Event.IsDisabled, Array.Empty<object>());
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00007E17 File Offset: 0x00006017
		public KeyContainerCreateException(string accountName, Exception innerException)
			: base(ErrorCode.rsReportServerKeyContainerError, ErrorStringsWrapper.rsReportServerKeyContainerError(accountName), innerException, null, Array.Empty<object>())
		{
			RSEventLog.Current.WriteError(Event.IsDisabled, Array.Empty<object>());
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00007D2D File Offset: 0x00005F2D
		private KeyContainerCreateException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
