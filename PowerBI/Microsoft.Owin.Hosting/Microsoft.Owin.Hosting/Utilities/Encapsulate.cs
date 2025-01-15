using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.Owin.Hosting.Utilities
{
	// Token: 0x0200000A RID: 10
	internal class Encapsulate
	{
		// Token: 0x06000039 RID: 57 RVA: 0x00002EC4 File Offset: 0x000010C4
		public Encapsulate(Func<IDictionary<string, object>, Task> app, IList<KeyValuePair<string, object>> environmentData)
		{
			this._app = app;
			this._environmentData = environmentData;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002EDC File Offset: 0x000010DC
		public Task Invoke(IDictionary<string, object> environment)
		{
			if (environment == null)
			{
				throw new ArgumentNullException("environment");
			}
			for (int i = 0; i < this._environmentData.Count; i++)
			{
				KeyValuePair<string, object> pair = this._environmentData[i];
				object obj;
				if (!environment.TryGetValue(pair.Key, out obj) || obj == null)
				{
					environment[pair.Key] = pair.Value;
				}
			}
			return this._app(environment);
		}

		// Token: 0x04000027 RID: 39
		private readonly Func<IDictionary<string, object>, Task> _app;

		// Token: 0x04000028 RID: 40
		private readonly IList<KeyValuePair<string, object>> _environmentData;
	}
}
