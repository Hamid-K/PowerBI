using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000029 RID: 41
	internal abstract class ConcurrentSetItem<KeyType, ItemType> where ItemType : ConcurrentSetItem<KeyType, ItemType>
	{
		// Token: 0x0600015A RID: 346
		public abstract int Compare(ItemType other);

		// Token: 0x0600015B RID: 347
		public abstract int Compare(KeyType key);
	}
}
