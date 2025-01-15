using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000074 RID: 116
	[Serializable]
	public class DepthLimitExceededException : Exception
	{
		// Token: 0x060001BC RID: 444 RVA: 0x00002FDF File Offset: 0x000011DF
		public DepthLimitExceededException(string message)
			: base(message)
		{
		}
	}
}
