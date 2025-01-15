using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000016 RID: 22
	[Serializable]
	internal sealed class InvalidElementException : ReportCatalogException
	{
		// Token: 0x06000152 RID: 338 RVA: 0x00003D25 File Offset: 0x00001F25
		public InvalidElementException(string elementName, Exception innerException)
			: base(ErrorCode.rsInvalidElement, ErrorStringsWrapper.rsInvalidElement(elementName), innerException, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00003D3C File Offset: 0x00001F3C
		public InvalidElementException(string elementName)
			: this(elementName, null)
		{
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00003D46 File Offset: 0x00001F46
		private InvalidElementException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
