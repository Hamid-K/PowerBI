using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001CC RID: 460
	public interface IContextStack
	{
		// Token: 0x06000BCE RID: 3022
		T GetContextMember<T>(int key);

		// Token: 0x06000BCF RID: 3023
		IDisposable Restore();
	}
}
