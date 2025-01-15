using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200001B RID: 27
	[Serializable]
	internal sealed class ItemPathLengthExceededException : ReportCatalogException
	{
		// Token: 0x06000160 RID: 352 RVA: 0x00003E0A File Offset: 0x0000200A
		public ItemPathLengthExceededException(string itemPath)
			: base(ErrorCode.rsItemPathLengthExceeded, ErrorStringsWrapper.rsItemPathLengthExceeded(StringUtils.RemoveControlAndNonSpacesWhitespaceCharacters(itemPath), CatalogItemNames.MaxItemPathLength), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00003E2B File Offset: 0x0000202B
		private ItemPathLengthExceededException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
