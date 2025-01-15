using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x020020C8 RID: 8392
	[NullableContext(1)]
	[Nullable(0)]
	internal class SecureObjectPool<[Nullable(2)] T, [Nullable(0)] TCaller> where TCaller : ISecurePooledObjectUser
	{
		// Token: 0x060119C4 RID: 72132 RVA: 0x003C35C1 File Offset: 0x003C17C1
		public void TryAdd(TCaller caller, SecurePooledObject<T> item)
		{
			if (caller.PoolUserId == item.Owner)
			{
				item.Owner = -1;
				AllocFreeConcurrentStack<SecurePooledObject<T>>.TryAdd(item);
			}
		}

		// Token: 0x060119C5 RID: 72133 RVA: 0x003C35E5 File Offset: 0x003C17E5
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

		// Token: 0x060119C6 RID: 72134 RVA: 0x003C361C File Offset: 0x003C181C
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
