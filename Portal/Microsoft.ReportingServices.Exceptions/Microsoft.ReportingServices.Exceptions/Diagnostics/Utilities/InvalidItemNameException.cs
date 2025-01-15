using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200001C RID: 28
	[Serializable]
	internal sealed class InvalidItemNameException : ReportCatalogException
	{
		// Token: 0x06000162 RID: 354 RVA: 0x00003E35 File Offset: 0x00002035
		public InvalidItemNameException(string invalidName, int maxItemNameLength)
			: base(ErrorCode.rsInvalidItemName, ErrorStringsWrapper.rsInvalidItemName(StringUtils.RemoveControlAndNonSpacesWhitespaceCharacters(invalidName), maxItemNameLength), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00003E52 File Offset: 0x00002052
		private InvalidItemNameException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
