using System;
using System.Runtime.Serialization;
using System.Xml;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000012 RID: 18
	[Serializable]
	internal sealed class MalformedXmlException : ReportCatalogException
	{
		// Token: 0x06000149 RID: 329 RVA: 0x00003C92 File Offset: 0x00001E92
		public MalformedXmlException(XmlException ex)
			: base(ErrorCode.rsMalformedXml, ErrorStringsWrapper.rsMalformedXml(ex.Message), ex, null, Array.Empty<object>())
		{
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00003CAD File Offset: 0x00001EAD
		private MalformedXmlException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
