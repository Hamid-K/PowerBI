using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200020C RID: 524
	[Serializable]
	internal class RichTextParseException : Exception
	{
		// Token: 0x060013E4 RID: 5092 RVA: 0x00051766 File Offset: 0x0004F966
		internal RichTextParseException(string message)
			: base(message)
		{
		}

		// Token: 0x060013E5 RID: 5093 RVA: 0x0005176F File Offset: 0x0004F96F
		protected RichTextParseException(SerializationInfo serializationInfo, StreamingContext context)
			: base(serializationInfo, context)
		{
		}
	}
}
