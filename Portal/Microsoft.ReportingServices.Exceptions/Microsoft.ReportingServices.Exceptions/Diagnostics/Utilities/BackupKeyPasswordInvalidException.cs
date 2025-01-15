using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200008F RID: 143
	[Serializable]
	internal sealed class BackupKeyPasswordInvalidException : ReportCatalogException
	{
		// Token: 0x0600025B RID: 603 RVA: 0x00004F1A File Offset: 0x0000311A
		public BackupKeyPasswordInvalidException()
			: base(ErrorCode.rsBackupKeyPasswordInvalid, ErrorStringsWrapper.rsBackupKeyPasswordInvalid, null, null, Array.Empty<object>())
		{
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00004F30 File Offset: 0x00003130
		private BackupKeyPasswordInvalidException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
