using System;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000036 RID: 54
	internal abstract class ClearTextXmlaStream : XmlaStream
	{
		// Token: 0x0600022B RID: 555 RVA: 0x0000BD92 File Offset: 0x00009F92
		private protected ClearTextXmlaStream(bool isXmlaTracingSupported)
			: base(isXmlaTracingSupported)
		{
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000BD9B File Offset: 0x00009F9B
		public override XmlaDataType GetRequestDataType()
		{
			return XmlaDataType.TextXml;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000BD9E File Offset: 0x00009F9E
		public override XmlaDataType GetResponseDataType()
		{
			return XmlaDataType.TextXml;
		}
	}
}
