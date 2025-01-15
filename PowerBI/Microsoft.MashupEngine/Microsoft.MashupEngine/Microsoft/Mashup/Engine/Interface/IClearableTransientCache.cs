using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000060 RID: 96
	public interface IClearableTransientCache : IDisposable
	{
		// Token: 0x06000191 RID: 401
		void Add(Action clearAction);

		// Token: 0x06000192 RID: 402
		void Clear();
	}
}
