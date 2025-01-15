using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000034 RID: 52
	public interface IEngineHost
	{
		// Token: 0x06000127 RID: 295
		T QueryService<T>() where T : class;
	}
}
