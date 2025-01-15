using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x02000841 RID: 2113
	public class SqlConnTimeoutCache<Tkey, Tval> : TimeoutCache<Tkey, SqlConnection>
	{
		// Token: 0x06004320 RID: 17184 RVA: 0x000E1180 File Offset: 0x000DF380
		public new void Flush()
		{
			this.cacheLock.EnterWriteLock();
			try
			{
				IEnumerable<Tkey> keys = this.cache.Keys;
				List<Tkey> list = new List<Tkey>();
				foreach (Tkey tkey in keys)
				{
					SqlConnection item = this.cache[tkey].Item1;
					if (DateTime.Now.CompareTo(this.cache[tkey].Item2) >= 0 || item.State == ConnectionState.Closed || item.State == ConnectionState.Broken)
					{
						item.Dispose();
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
