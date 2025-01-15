using System;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Storage.Database
{
	// Token: 0x02000051 RID: 81
	public interface IMaintenanceDatabaseClient : IDatabaseClient, IIdentifiable
	{
		// Token: 0x060001EC RID: 492
		IAsyncResult BeginResetDatabase(AsyncCallback callback, object asyncState);

		// Token: 0x060001ED RID: 493
		void EndResetDatabase(IAsyncResult asyncResult);
	}
}
