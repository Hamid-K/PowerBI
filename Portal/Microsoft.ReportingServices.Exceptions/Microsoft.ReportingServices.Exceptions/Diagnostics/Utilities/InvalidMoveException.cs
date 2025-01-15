using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000026 RID: 38
	[Serializable]
	internal sealed class InvalidMoveException : ReportCatalogException
	{
		// Token: 0x06000176 RID: 374 RVA: 0x00003F9A File Offset: 0x0000219A
		public InvalidMoveException(string itemPath, string targetPath)
			: base(ErrorCode.rsInvalidMove, ErrorStringsWrapper.rsInvalidMove(itemPath, targetPath), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00003FB2 File Offset: 0x000021B2
		private InvalidMoveException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
