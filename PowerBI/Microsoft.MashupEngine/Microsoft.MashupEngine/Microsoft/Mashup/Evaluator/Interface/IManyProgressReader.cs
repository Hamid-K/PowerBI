using System;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001DF6 RID: 7670
	public interface IManyProgressReader
	{
		// Token: 0x0600BDAB RID: 48555
		void AddReader(IProgressReader reader);

		// Token: 0x0600BDAC RID: 48556
		void RemoveReader(IProgressReader reader);
	}
}
