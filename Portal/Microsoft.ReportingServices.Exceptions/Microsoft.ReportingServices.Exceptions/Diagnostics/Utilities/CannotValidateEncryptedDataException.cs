using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200008C RID: 140
	[Serializable]
	internal sealed class CannotValidateEncryptedDataException : ReportCatalogException
	{
		// Token: 0x06000254 RID: 596 RVA: 0x00004EA4 File Offset: 0x000030A4
		public CannotValidateEncryptedDataException()
			: base(ErrorCode.rsCannotValidateEncryptedData, ErrorStringsWrapper.rsCannotValidateEncryptedData, null, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00004EBA File Offset: 0x000030BA
		public CannotValidateEncryptedDataException(Exception e)
			: base(ErrorCode.rsCannotValidateEncryptedData, ErrorStringsWrapper.rsCannotValidateEncryptedData, e, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00004ED0 File Offset: 0x000030D0
		private CannotValidateEncryptedDataException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
