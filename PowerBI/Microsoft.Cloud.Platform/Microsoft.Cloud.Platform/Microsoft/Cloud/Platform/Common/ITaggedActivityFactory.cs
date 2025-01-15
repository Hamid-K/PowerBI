using System;

namespace Microsoft.Cloud.Platform.Common
{
	// Token: 0x02000545 RID: 1349
	public interface ITaggedActivityFactory
	{
		// Token: 0x060028F7 RID: 10487
		IDisposable CreateTaggedActivity(ActivityTag tag);
	}
}
