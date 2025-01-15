using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000017 RID: 23
	[Serializable]
	internal sealed class ElementTypeMismatchException : ReportCatalogException
	{
		// Token: 0x06000155 RID: 341 RVA: 0x00003D50 File Offset: 0x00001F50
		public ElementTypeMismatchException(string elementName)
			: base(ErrorCode.rsElementTypeMismatch, ErrorStringsWrapper.rsElementTypeMismatch(elementName), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00003D67 File Offset: 0x00001F67
		public ElementTypeMismatchException(string elementName, Exception innerException)
			: base(ErrorCode.rsElementTypeMismatch, ErrorStringsWrapper.rsElementTypeMismatch(elementName), innerException, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00003D7E File Offset: 0x00001F7E
		private ElementTypeMismatchException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
