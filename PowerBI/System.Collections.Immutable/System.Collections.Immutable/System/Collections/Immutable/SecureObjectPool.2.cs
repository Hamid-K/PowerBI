using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000046 RID: 70
	[NullableContext(1)]
	[Nullable(0)]
	internal class SecureObjectPool<[Nullable(2)] T, [Nullable(0)] TCaller> where TCaller : ISecurePooledObjectUser
	{
		// Token: 0x0600036C RID: 876 RVA: 0x00009225 File Offset: 0x00007425
		public void TryAdd(TCaller caller, SecurePooledObject<T> item)
		{
			if (caller.PoolUserId == item.Owner)
			{
				item.Owner = -1;
				AllocFreeConcurrentStack<SecurePooledObject<T>>.TryAdd(item);
			}
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00009249 File Offset: 0x00007449
		public bool TryTake(TCaller caller, [Nullable(new byte[] { 2, 1 })] out SecurePooledObject<T> item)
		{
			if (caller.PoolUserId != -1 && AllocFreeConcurrentStack<SecurePooledObject<T>>.TryTake(out item))
			{
				item.Owner = caller.PoolUserId;
				return true;
			}
			item = null;
			return false;
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00009280 File Offset: 0x00007480
		public SecurePooledObject<T> PrepNew(TCaller caller, T newValue)
		{
			Requires.NotNullAllowStructs<T>(newValue, "newValue");
			return new SecurePooledObject<T>(newValue)
			{
				Owner = caller.PoolUserId
			};
		}
	}
}
