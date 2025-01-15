using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000093 RID: 147
	[Serializable]
	internal sealed class LogonFailedException : ReportCatalogException
	{
		// Token: 0x06000263 RID: 611 RVA: 0x00004FA1 File Offset: 0x000031A1
		public LogonFailedException(Exception innerException, string userName)
			: base(ErrorCode.rsLogonFailed, ErrorStringsWrapper.rsLogonFailed, innerException, LogonFailedException.BuildLogFileMessage(userName), Array.Empty<object>())
		{
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00004FBF File Offset: 0x000031BF
		public LogonFailedException(Exception innerException)
			: this(innerException, null)
		{
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00004FC9 File Offset: 0x000031C9
		private static string BuildLogFileMessage(string userName)
		{
			if (string.IsNullOrEmpty(userName))
			{
				return null;
			}
			return string.Format(CultureInfo.InvariantCulture, "Logon attempt for user '{0}' failed.", userName);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00004FE5 File Offset: 0x000031E5
		private LogonFailedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
