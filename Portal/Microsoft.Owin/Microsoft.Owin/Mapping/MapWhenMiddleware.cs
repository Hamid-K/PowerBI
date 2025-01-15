using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.Owin.Mapping
{
	// Token: 0x02000027 RID: 39
	public class MapWhenMiddleware
	{
		// Token: 0x060001C7 RID: 455 RVA: 0x00004A81 File Offset: 0x00002C81
		public MapWhenMiddleware(Func<IDictionary<string, object>, Task> next, MapWhenOptions options)
		{
			if (next == null)
			{
				throw new ArgumentNullException("next");
			}
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			this._next = next;
			this._options = options;
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00004AB4 File Offset: 0x00002CB4
		public async Task Invoke(IDictionary<string, object> environment)
		{
			IOwinContext context = new OwinContext(environment);
			if (this._options.Predicate != null)
			{
				if (this._options.Predicate(context))
				{
					await this._options.Branch(environment);
				}
				else
				{
					await this._next(environment);
				}
			}
			else if (await this._options.PredicateAsync(context))
			{
				await this._options.Branch(environment);
			}
			else
			{
				await this._next(environment);
			}
		}

		// Token: 0x04000054 RID: 84
		private readonly Func<IDictionary<string, object>, Task> _next;

		// Token: 0x04000055 RID: 85
		private readonly MapWhenOptions _options;
	}
}
