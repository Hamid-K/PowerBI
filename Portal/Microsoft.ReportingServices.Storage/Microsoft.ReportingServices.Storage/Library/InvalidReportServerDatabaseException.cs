using System;
using System.Runtime.Serialization;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200001F RID: 31
	[Serializable]
	internal sealed class InvalidReportServerDatabaseException : ReportCatalogException
	{
		// Token: 0x06000108 RID: 264 RVA: 0x00007D04 File Offset: 0x00005F04
		public InvalidReportServerDatabaseException(string storedVersion, string expectedVersion)
			: base(ErrorCode.rsInvalidReportServerDatabase, ErrorStringsWrapper.rsInvalidReportServerDatabase(storedVersion, expectedVersion), null, null, Array.Empty<object>())
		{
			RSEventLog.Current.WriteError(Event.InvalidDBVersion, Array.Empty<object>());
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00007D2D File Offset: 0x00005F2D
		private InvalidReportServerDatabaseException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
