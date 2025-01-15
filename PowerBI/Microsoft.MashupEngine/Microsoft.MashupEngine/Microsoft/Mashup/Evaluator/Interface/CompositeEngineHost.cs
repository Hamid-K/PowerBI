using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001DC7 RID: 7623
	public class CompositeEngineHost : IEngineHost
	{
		// Token: 0x0600BCE2 RID: 48354 RVA: 0x00265662 File Offset: 0x00263862
		public CompositeEngineHost(params IEngineHost[] engineHosts)
		{
			this.engineHosts = engineHosts;
		}

		// Token: 0x0600BCE3 RID: 48355 RVA: 0x00265674 File Offset: 0x00263874
		T IEngineHost.QueryService<T>()
		{
			IEngineHost[] array = this.engineHosts;
			for (int i = 0; i < array.Length; i++)
			{
				T t = array[i].QueryService<T>();
				if (t != null)
				{
					return t;
				}
			}
			return default(T);
		}

		// Token: 0x04006062 RID: 24674
		private readonly IEngineHost[] engineHosts;
	}
}
