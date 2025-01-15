using System;
using System.Collections.Generic;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x02000842 RID: 2114
	public class SsoCredentialsTimeoutCache<Tkey, Tval> : TimeoutCache<Tkey, SsoCredentials>
	{
		// Token: 0x06004322 RID: 17186 RVA: 0x000E1298 File Offset: 0x000DF498
		public new void Flush()
		{
			this.cacheLock.EnterWriteLock();
			try
			{
				IEnumerable<Tkey> keys = this.cache.Keys;
				List<Tkey> list = new List<Tkey>();
				foreach (Tkey tkey in keys)
				{
					SsoCredentials item = this.cache[tkey].Item1;
					if (DateTime.Now.CompareTo(this.cache[tkey].Item2) >= 0)
					{
						list.Add(tkey);
					}
				}
				foreach (Tkey tkey2 in list)
				{
					this.cache.Remove(tkey2);
				}
			}
			finally
			{
				this.cacheLock.ExitWriteLock();
			}
		}
	}
}
