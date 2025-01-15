using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x0200003E RID: 62
	public interface IEngineHook
	{
		// Token: 0x06000139 RID: 313
		T Hook<T>(Func<T> service) where T : class;
	}
}
