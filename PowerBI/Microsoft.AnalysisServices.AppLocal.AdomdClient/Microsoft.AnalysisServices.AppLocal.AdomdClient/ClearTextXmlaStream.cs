using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200001E RID: 30
	internal abstract class ClearTextXmlaStream : XmlaStream
	{
		// Token: 0x0600019F RID: 415 RVA: 0x00008E96 File Offset: 0x00007096
		private protected ClearTextXmlaStream(bool isXmlaTracingSupported)
			: base(isXmlaTracingSupported)
		{
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00008E9F File Offset: 0x0000709F
		public override XmlaDataType GetRequestDataType()
		{
			return XmlaDataType.TextXml;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00008EA2 File Offset: 0x000070A2
		public override XmlaDataType GetResponseDataType()
		{
			return XmlaDataType.TextXml;
		}
	}
}
