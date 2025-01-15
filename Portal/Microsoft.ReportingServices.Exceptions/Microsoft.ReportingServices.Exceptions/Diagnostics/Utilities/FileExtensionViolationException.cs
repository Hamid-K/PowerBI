using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000098 RID: 152
	[Serializable]
	internal sealed class FileExtensionViolationException : ReportCatalogException
	{
		// Token: 0x06000270 RID: 624 RVA: 0x00005090 File Offset: 0x00003290
		public FileExtensionViolationException(string targetFileExtension, string sourceFileExtension)
			: base(ErrorCode.rsFileExtensionViolation, ErrorStringsWrapper.rsFileExtensionViolation(targetFileExtension, sourceFileExtension), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000271 RID: 625 RVA: 0x000050AB File Offset: 0x000032AB
		private FileExtensionViolationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
