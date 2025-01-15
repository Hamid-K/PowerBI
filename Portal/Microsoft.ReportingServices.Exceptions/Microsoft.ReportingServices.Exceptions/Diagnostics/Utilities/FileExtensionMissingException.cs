using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000097 RID: 151
	[Serializable]
	internal sealed class FileExtensionMissingException : ReportCatalogException
	{
		// Token: 0x0600026E RID: 622 RVA: 0x0000506D File Offset: 0x0000326D
		public FileExtensionMissingException()
			: base(ErrorCode.rsFileExtensionRequired, ErrorStringsWrapper.rsFileExtensionRequired, null, null, Array.Empty<object>())
		{
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00005086 File Offset: 0x00003286
		private FileExtensionMissingException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
