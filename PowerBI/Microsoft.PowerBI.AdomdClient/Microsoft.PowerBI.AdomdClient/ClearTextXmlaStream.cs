using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200001E RID: 30
	internal abstract class ClearTextXmlaStream : XmlaStream
	{
		// Token: 0x06000192 RID: 402 RVA: 0x00008B96 File Offset: 0x00006D96
		private protected ClearTextXmlaStream(bool isXmlaTracingSupported)
			: base(isXmlaTracingSupported)
		{
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00008B9F File Offset: 0x00006D9F
		public override XmlaDataType GetRequestDataType()
		{
			return XmlaDataType.TextXml;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00008BA2 File Offset: 0x00006DA2
		public override XmlaDataType GetResponseDataType()
		{
			return XmlaDataType.TextXml;
		}
	}
}
