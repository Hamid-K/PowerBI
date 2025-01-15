using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Microsoft.Owin.Mapping
{
	// Token: 0x02000025 RID: 37
	public class MapMiddleware
	{
		// Token: 0x060001C0 RID: 448 RVA: 0x000049D3 File Offset: 0x00002BD3
		public MapMiddleware(Func<IDictionary<string, object>, Task> next, MapOptions options)
		{
			if (next == null)
			{
				throw new ArgumentNullException("next");
			}
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			PathString pathMatch = options.PathMatch;
			this._next = next;
			this._options = options;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00004A0C File Offset: 0x00002C0C
		public Task Invoke(IDictionary<string, object> environment)
		{
			MapMiddleware.<Invoke>d__3 <Invoke>d__;
			<Invoke>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<Invoke>d__.<>4__this = this;
			<Invoke>d__.environment = environment;
			<Invoke>d__.<>1__state = -1;
			<Invoke>d__.<>t__builder.Start<MapMiddleware.<Invoke>d__3>(ref <Invoke>d__);
			return <Invoke>d__.<>t__builder.Task;
		}

		// Token: 0x04000050 RID: 80
		private readonly Func<IDictionary<string, object>, Task> _next;

		// Token: 0x04000051 RID: 81
		private readonly MapOptions _options;
	}
}
