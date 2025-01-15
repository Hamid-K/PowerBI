using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000018 RID: 24
	[Serializable]
	internal sealed class InvalidElementCombinationException : ReportCatalogException
	{
		// Token: 0x06000158 RID: 344 RVA: 0x00003D88 File Offset: 0x00001F88
		public InvalidElementCombinationException(string elementName1, string elementName2)
			: base(ErrorCode.rsInvalidElementCombination, ErrorStringsWrapper.rsInvalidElementCombination(elementName1, elementName2), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00003DA0 File Offset: 0x00001FA0
		private InvalidElementCombinationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
