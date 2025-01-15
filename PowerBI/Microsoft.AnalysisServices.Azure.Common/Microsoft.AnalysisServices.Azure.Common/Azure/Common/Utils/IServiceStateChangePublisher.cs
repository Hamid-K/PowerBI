using System;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common.Utils
{
	// Token: 0x02000141 RID: 321
	public interface IServiceStateChangePublisher : IIdentifiable
	{
		// Token: 0x0600115A RID: 4442
		void Subscribe(IServiceStateChangeSubscriber subscriber);

		// Token: 0x0600115B RID: 4443
		void Unsubscribe(IServiceStateChangeSubscriber subscriber);
	}
}
