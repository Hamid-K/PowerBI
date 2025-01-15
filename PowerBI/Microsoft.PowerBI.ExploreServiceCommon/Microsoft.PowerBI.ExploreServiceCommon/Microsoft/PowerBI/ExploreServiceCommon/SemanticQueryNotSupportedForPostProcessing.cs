using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.ExploreServiceCommon
{
	// Token: 0x02000020 RID: 32
	[Serializable]
	public class SemanticQueryNotSupportedForPostProcessing : Exception
	{
		// Token: 0x060000FB RID: 251 RVA: 0x00004241 File Offset: 0x00002441
		public SemanticQueryNotSupportedForPostProcessing(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x060000FC RID: 252 RVA: 0x0000424B File Offset: 0x0000244B
		public SemanticQueryNotSupportedForPostProcessing()
		{
		}
	}
}
