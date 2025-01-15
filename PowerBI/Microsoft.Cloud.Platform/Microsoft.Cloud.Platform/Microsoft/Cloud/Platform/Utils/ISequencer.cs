using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000281 RID: 641
	public interface ISequencer
	{
		// Token: 0x06001112 RID: 4370
		IAsyncResult BeginExecute(AsyncCallback userCallback, object userContext);

		// Token: 0x06001113 RID: 4371
		void EndExecute(IAsyncResult asyncResult);
	}
}
