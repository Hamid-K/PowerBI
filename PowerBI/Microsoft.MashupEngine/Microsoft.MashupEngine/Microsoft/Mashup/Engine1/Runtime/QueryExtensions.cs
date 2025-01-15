using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020015DC RID: 5596
	internal static class QueryExtensions
	{
		// Token: 0x06008CC1 RID: 36033 RVA: 0x001D81E1 File Offset: 0x001D63E1
		public static IEngineHost GetEngineHost(this Query query)
		{
			QueryExtensions.Visitor visitor = new QueryExtensions.Visitor();
			visitor.VisitQuery(query);
			return visitor.result;
		}

		// Token: 0x020015DD RID: 5597
		private sealed class Visitor : QueryVisitor
		{
			// Token: 0x06008CC2 RID: 36034 RVA: 0x001D81F8 File Offset: 0x001D63F8
			protected override Query VisitDataSource(DataSourceQuery query)
			{
				IEngineHost engineHost = query.EngineHost;
				if (engineHost != null)
				{
					if (this.result == null)
					{
						this.result = engineHost;
					}
					else if (this.result != engineHost)
					{
						QueryExtensions.CompositeQueryEngineHost compositeQueryEngineHost = this.result as QueryExtensions.CompositeQueryEngineHost;
						if (compositeQueryEngineHost == null)
						{
							compositeQueryEngineHost = new QueryExtensions.CompositeQueryEngineHost(this.result);
							this.result = compositeQueryEngineHost;
						}
						compositeQueryEngineHost.Add(engineHost);
					}
					this.result = query.EngineHost;
				}
				return query;
			}

			// Token: 0x04004CC5 RID: 19653
			public IEngineHost result;
		}

		// Token: 0x020015DE RID: 5598
		private sealed class CompositeQueryEngineHost : IEngineHost
		{
			// Token: 0x06008CC4 RID: 36036 RVA: 0x001D825F File Offset: 0x001D645F
			public CompositeQueryEngineHost(IEngineHost engineHost)
			{
				this.engineHosts = new HashSet<IEngineHost>(EqualityComparer<IEngineHost>.Default);
				this.engineHosts.Add(engineHost);
				this.cache = new Dictionary<Type, object>();
			}

			// Token: 0x06008CC5 RID: 36037 RVA: 0x001D828F File Offset: 0x001D648F
			public void Add(IEngineHost engineHost)
			{
				this.engineHosts.Add(engineHost);
			}

			// Token: 0x06008CC6 RID: 36038 RVA: 0x001D82A0 File Offset: 0x001D64A0
			public T QueryService<T>() where T : class
			{
				object obj;
				if (this.cache.TryGetValue(typeof(T), out obj))
				{
					return (T)((object)obj);
				}
				T t = default(T);
				foreach (IEngineHost engineHost in this.engineHosts)
				{
					T t2 = engineHost.QueryService<T>();
					if (t == null)
					{
						t = t2;
					}
					else if (t2 != null && t2 != t)
					{
						t = default(T);
						break;
					}
				}
				this.cache.Add(typeof(T), t);
				return t;
			}

			// Token: 0x04004CC6 RID: 19654
			private readonly HashSet<IEngineHost> engineHosts;

			// Token: 0x04004CC7 RID: 19655
			private readonly Dictionary<Type, object> cache;
		}
	}
}
