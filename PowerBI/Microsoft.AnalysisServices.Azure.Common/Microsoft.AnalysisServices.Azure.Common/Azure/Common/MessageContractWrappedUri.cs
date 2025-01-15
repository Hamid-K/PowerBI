using System;
using System.ServiceModel;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x0200005B RID: 91
	[MessageContract]
	public class MessageContractWrappedUri
	{
		// Token: 0x04000159 RID: 345
		[MessageHeader]
		public Uri Uri;
	}
}
