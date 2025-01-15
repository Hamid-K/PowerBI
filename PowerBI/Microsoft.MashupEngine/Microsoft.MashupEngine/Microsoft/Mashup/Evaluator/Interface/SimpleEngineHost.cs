using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001E49 RID: 7753
	public class SimpleEngineHost<T> : IEngineHost
	{
		// Token: 0x0600BE8C RID: 48780 RVA: 0x0026881D File Offset: 0x00266A1D
		public SimpleEngineHost(T service)
		{
			this.service = service;
		}

		// Token: 0x0600BE8D RID: 48781 RVA: 0x0026882C File Offset: 0x00266A2C
		U IEngineHost.QueryService<U>()
		{
			if (typeof(T) == typeof(U))
			{
				return (U)((object)this.service);
			}
			return default(U);
		}

		// Token: 0x0400610B RID: 24843
		private readonly T service;
	}
}
