using System;

namespace Microsoft.Cloud.Platform.Monitoring
{
	// Token: 0x0200009F RID: 159
	public interface IFailureAnalyzerFactory
	{
		// Token: 0x0600046B RID: 1131
		IFailureAnalyzer GetFailureAnalyzer(string streamId);
	}
}
