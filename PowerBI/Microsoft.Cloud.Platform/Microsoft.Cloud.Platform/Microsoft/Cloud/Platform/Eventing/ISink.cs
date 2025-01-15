using System;
using Microsoft.Cloud.Platform.Eventing.Base;

namespace Microsoft.Cloud.Platform.Eventing
{
	// Token: 0x0200038A RID: 906
	public interface ISink
	{
		// Token: 0x06001C11 RID: 7185
		void Initialize(ISinkServices services, SinkIdentifier sid);

		// Token: 0x06001C12 RID: 7186
		void Submit(WireEventBase pubEvent);

		// Token: 0x06001C13 RID: 7187
		void OnBatchCompleted();

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06001C14 RID: 7188
		SinkProperties Properties { get; }

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06001C15 RID: 7189
		SinkIdentifier Id { get; }
	}
}
