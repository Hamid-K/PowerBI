using System;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x02000840 RID: 2112
	public class SecurityTokenTimeoutCache<Tkey, Tval> : TimeoutCache<Tkey, IntPtr>
	{
		// Token: 0x0600431E RID: 17182 RVA: 0x000E10B4 File Offset: 0x000DF2B4
		public new bool ContainsKey(Tkey key)
		{
			this.cacheLock.EnterUpgradeableReadLock();
			bool flag;
			try
			{
				if (this.cache.ContainsKey(key))
				{
					if (DateTime.Now.CompareTo(this.cache[key].Item2) < 0)
					{
						return true;
					}
					IntPtr item = this.cache[key].Item1;
					this.cacheLock.EnterWriteLock();
					try
					{
						this.cache.Remove(key);
					}
					finally
					{
						this.cacheLock.ExitWriteLock();
					}
					if (item != IntPtr.Zero)
					{
						SafeTokenHandle.CloseHandle(item);
					}
				}
				flag = false;
			}
			finally
			{
				this.cacheLock.ExitUpgradeableReadLock();
			}
			return flag;
		}
	}
}
