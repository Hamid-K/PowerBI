using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001E39 RID: 7737
	public sealed class MutableEngineHost : IEngineHost
	{
		// Token: 0x0600BE5C RID: 48732 RVA: 0x0026809B File Offset: 0x0026629B
		public MutableEngineHost()
		{
			this.engineHosts = new List<IEngineHost>();
			this.services = new Dictionary<Type, object>();
		}

		// Token: 0x0600BE5D RID: 48733 RVA: 0x002680BC File Offset: 0x002662BC
		public void Add(IEngineHost engineHost)
		{
			Dictionary<Type, object> dictionary = this.services;
			lock (dictionary)
			{
				this.engineHosts.Add(engineHost);
				this.services.Clear();
			}
		}

		// Token: 0x0600BE5E RID: 48734 RVA: 0x00268110 File Offset: 0x00266310
		public T QueryService<T>() where T : class
		{
			Dictionary<Type, object> dictionary = this.services;
			T t;
			lock (dictionary)
			{
				object obj;
				if (!this.services.TryGetValue(typeof(T), out obj))
				{
					for (int i = this.engineHosts.Count - 1; i >= 0; i--)
					{
						obj = this.engineHosts[i].QueryService<T>();
						if (obj != null)
						{
							this.services.Add(typeof(T), obj);
							break;
						}
					}
				}
				t = (T)((object)obj);
			}
			return t;
		}

		// Token: 0x040060F4 RID: 24820
		private readonly List<IEngineHost> engineHosts;

		// Token: 0x040060F5 RID: 24821
		private readonly Dictionary<Type, object> services;
	}
}
