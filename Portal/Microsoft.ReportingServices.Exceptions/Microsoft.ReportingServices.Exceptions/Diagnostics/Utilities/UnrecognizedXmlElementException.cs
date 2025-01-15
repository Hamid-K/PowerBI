using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000014 RID: 20
	[Serializable]
	internal sealed class UnrecognizedXmlElementException : ReportCatalogException
	{
		// Token: 0x0600014D RID: 333 RVA: 0x00003CD6 File Offset: 0x00001ED6
		public UnrecognizedXmlElementException(string elementName)
			: base(ErrorCode.rsUnrecognizedXmlElement, ErrorStringsWrapper.rsUnrecognizedXmlElement(elementName), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00003CEC File Offset: 0x00001EEC
		private UnrecognizedXmlElementException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
