using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000019 RID: 25
	[Serializable]
	internal sealed class InvalidMultipleElementCombinationException : ReportCatalogException
	{
		// Token: 0x0600015A RID: 346 RVA: 0x00003DAA File Offset: 0x00001FAA
		public InvalidMultipleElementCombinationException(string elementName1, string elementName2, string elementName3)
			: base(ErrorCode.rsInvalidMultipleElementCombination, ErrorStringsWrapper.rsInvalidMultipleElementCombination(elementName1, elementName2, elementName3), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00003DC3 File Offset: 0x00001FC3
		private InvalidMultipleElementCombinationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
