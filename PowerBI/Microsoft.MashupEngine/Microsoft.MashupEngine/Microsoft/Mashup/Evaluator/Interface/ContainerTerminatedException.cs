using System;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001DC9 RID: 7625
	[Serializable]
	public class ContainerTerminatedException : Exception
	{
		// Token: 0x0600BCE7 RID: 48359 RVA: 0x00002FDF File Offset: 0x000011DF
		public ContainerTerminatedException(string message)
			: base(message)
		{
		}
	}
}
