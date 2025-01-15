using System;
using System.Collections.Generic;
using Microsoft.Mashup.Common;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001103 RID: 4355
	internal sealed class Pool : IPool, IDisposable
	{
		// Token: 0x060071F0 RID: 29168 RVA: 0x00187AAC File Offset: 0x00185CAC
		public Pool()
		{
			this.objectsByKey = new Dictionary<string, List<IPoolable>>();
		}

		// Token: 0x060071F1 RID: 29169 RVA: 0x00187AC0 File Offset: 0x00185CC0
		public bool TryGet(string key, out IPoolable poolable)
		{
			List<IPoolable> list;
			if (this.objectsByKey.TryGetValue(key, out list))
			{
				while (list.Count > 0)
				{
					poolable = list[list.Count - 1];
					list.RemoveAt(list.Count - 1);
					if (poolable.IsValid)
					{
						return true;
					}
					this.SafeDispose(poolable);
				}
				this.objectsByKey.Remove(key);
			}
			poolable = null;
			return false;
		}

		// Token: 0x060071F2 RID: 29170 RVA: 0x00187B2C File Offset: 0x00185D2C
		public void Add(IPoolable poolable)
		{
			string key = poolable.Key;
			List<IPoolable> list;
			if (!this.objectsByKey.TryGetValue(key, out list))
			{
				list = (this.objectsByKey[key] = new List<IPoolable>());
			}
			list.Add(poolable);
		}

		// Token: 0x060071F3 RID: 29171 RVA: 0x00187B6A File Offset: 0x00185D6A
		public void Purge()
		{
			this.Purge((IPoolable p) => !p.IsValid);
		}

		// Token: 0x060071F4 RID: 29172 RVA: 0x00187B91 File Offset: 0x00185D91
		public void Clear()
		{
			this.Purge((IPoolable p) => true);
		}

		// Token: 0x060071F5 RID: 29173 RVA: 0x00187BB8 File Offset: 0x00185DB8
		public void Dispose()
		{
			foreach (KeyValuePair<string, List<IPoolable>> keyValuePair in this.objectsByKey)
			{
				foreach (IPoolable poolable in keyValuePair.Value)
				{
					this.SafeDispose(poolable);
				}
			}
		}

		// Token: 0x060071F6 RID: 29174 RVA: 0x00187C48 File Offset: 0x00185E48
		private void Purge(Func<IPoolable, bool> shouldDispose)
		{
			List<string> list = new List<string>();
			foreach (KeyValuePair<string, List<IPoolable>> keyValuePair in this.objectsByKey)
			{
				for (int i = keyValuePair.Value.Count - 1; i >= 0; i--)
				{
					IPoolable poolable = keyValuePair.Value[i];
					if (shouldDispose(poolable))
					{
						this.SafeDispose(poolable);
						keyValuePair.Value.RemoveAt(i);
					}
				}
				if (keyValuePair.Value.Count == 0)
				{
					list.Add(keyValuePair.Key);
				}
			}
			for (int j = 0; j < list.Count; j++)
			{
				this.objectsByKey.Remove(list[j]);
			}
		}

		// Token: 0x060071F7 RID: 29175 RVA: 0x00187D28 File Offset: 0x00185F28
		private void SafeDispose(IDisposable disposable)
		{
			try
			{
				disposable.Dispose();
			}
			catch (Exception ex)
			{
				if (!SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
			}
		}

		// Token: 0x04003EF6 RID: 16118
		private readonly Dictionary<string, List<IPoolable>> objectsByKey;
	}
}
