using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000027 RID: 39
	[Serializable]
	internal sealed class InvalidDestinationException : ReportCatalogException
	{
		// Token: 0x06000178 RID: 376 RVA: 0x00003FBC File Offset: 0x000021BC
		public InvalidDestinationException(string sourcePath, string targetPath)
			: base(ErrorCode.rsInvalidDestination, ErrorStringsWrapper.rsInvalidDestination(sourcePath, targetPath), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00003FD4 File Offset: 0x000021D4
		private InvalidDestinationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
