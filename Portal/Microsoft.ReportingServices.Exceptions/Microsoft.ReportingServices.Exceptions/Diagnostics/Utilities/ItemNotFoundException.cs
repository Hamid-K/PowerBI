using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200001D RID: 29
	[Serializable]
	internal sealed class ItemNotFoundException : ReportCatalogException
	{
		// Token: 0x06000164 RID: 356 RVA: 0x00003E5C File Offset: 0x0000205C
		public ItemNotFoundException(string itemPath, string parameterName)
			: base(ErrorCode.rsItemNotFound, ErrorStringsWrapper.rsItemNotFound(StringUtils.RemoveControlAndNonSpacesWhitespaceCharacters(itemPath)), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00003E78 File Offset: 0x00002078
		public ItemNotFoundException(string itemPath)
			: base(ErrorCode.rsItemNotFound, ErrorStringsWrapper.rsItemNotFound(StringUtils.RemoveControlAndNonSpacesWhitespaceCharacters(itemPath)), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00003E94 File Offset: 0x00002094
		private ItemNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
