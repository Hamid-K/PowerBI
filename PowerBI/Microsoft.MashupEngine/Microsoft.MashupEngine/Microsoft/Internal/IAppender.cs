using System;

namespace Microsoft.Internal
{
	// Token: 0x020001B8 RID: 440
	internal interface IAppender<T> : IDisposable
	{
		// Token: 0x06000864 RID: 2148
		void Append(T item);
	}
}
