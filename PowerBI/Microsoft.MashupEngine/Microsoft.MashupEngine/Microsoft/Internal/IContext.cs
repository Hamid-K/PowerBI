using System;

namespace Microsoft.Internal
{
	// Token: 0x020001AE RID: 430
	internal interface IContext<T> where T : struct, IDisposable
	{
		// Token: 0x06000831 RID: 2097
		T Enter();
	}
}
