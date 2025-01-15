using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x0200048B RID: 1163
	public interface IBindingData
	{
		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x060023EF RID: 9199
		Binding Binding { get; }

		// Token: 0x060023F0 RID: 9200
		void AddBehaviors(ServiceEndpoint endpoint);
	}
}
