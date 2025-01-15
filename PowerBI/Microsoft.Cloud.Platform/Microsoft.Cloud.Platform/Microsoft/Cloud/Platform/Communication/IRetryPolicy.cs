using System;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004EC RID: 1260
	public interface IRetryPolicy
	{
		// Token: 0x0600265B RID: 9819
		object CreateInitialState();

		// Token: 0x0600265C RID: 9820
		bool ShouldRetryToTheSameEndpoint(EndpointFault endpointFault, object state);

		// Token: 0x0600265D RID: 9821
		bool ShouldRetryToDifferentEndpoint(EndpointFault endpointFault, object state);
	}
}
