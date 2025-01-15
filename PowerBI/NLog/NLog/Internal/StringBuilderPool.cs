using System;
using System.Text;
using System.Threading;

namespace NLog.Internal
{
	// Token: 0x02000145 RID: 325
	internal class StringBuilderPool
	{
		// Token: 0x06000FC4 RID: 4036 RVA: 0x00028828 File Offset: 0x00026A28
		public StringBuilderPool(int poolCapacity, int initialBuilderCapacity = 1024, int maxBuilderCapacity = 524288)
		{
			this._fastPool = new StringBuilder(10 * initialBuilderCapacity);
			this._slowPool = new StringBuilder[poolCapacity];
			for (int i = 0; i < this._slowPool.Length; i++)
			{
				this._slowPool[i] = new StringBuilder(initialBuilderCapacity);
			}
			this._maxBuilderCapacity = maxBuilderCapacity;
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x00028880 File Offset: 0x00026A80
		public StringBuilderPool.ItemHolder Acquire()
		{
			StringBuilder stringBuilder = this._fastPool;
			if (stringBuilder == null || stringBuilder != Interlocked.CompareExchange<StringBuilder>(ref this._fastPool, null, stringBuilder))
			{
				for (int i = 0; i < this._slowPool.Length; i++)
				{
					stringBuilder = this._slowPool[i];
					if (stringBuilder != null && stringBuilder == Interlocked.CompareExchange<StringBuilder>(ref this._slowPool[i], null, stringBuilder))
					{
						return new StringBuilderPool.ItemHolder(stringBuilder, this, i);
					}
				}
				return new StringBuilderPool.ItemHolder(new StringBuilder(), null, 0);
			}
			return new StringBuilderPool.ItemHolder(stringBuilder, this, -1);
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x000288FC File Offset: 0x00026AFC
		private void Release(StringBuilder stringBuilder, int poolIndex)
		{
			if (stringBuilder.Length > this._maxBuilderCapacity)
			{
				int num = ((poolIndex == -1) ? (this._maxBuilderCapacity * 10) : this._maxBuilderCapacity);
				if (stringBuilder.Length > num)
				{
					stringBuilder = new StringBuilder(num / 2);
				}
			}
			stringBuilder.Length = 0;
			if (poolIndex == -1)
			{
				this._fastPool = stringBuilder;
				return;
			}
			this._slowPool[poolIndex] = stringBuilder;
		}

		// Token: 0x04000437 RID: 1079
		private StringBuilder _fastPool;

		// Token: 0x04000438 RID: 1080
		private readonly StringBuilder[] _slowPool;

		// Token: 0x04000439 RID: 1081
		private readonly int _maxBuilderCapacity;

		// Token: 0x02000282 RID: 642
		public struct ItemHolder : IDisposable
		{
			// Token: 0x0600168A RID: 5770 RVA: 0x0003AFBF File Offset: 0x000391BF
			public ItemHolder(StringBuilder stringBuilder, StringBuilderPool owner, int poolIndex)
			{
				this.Item = stringBuilder;
				this._owner = owner;
				this._poolIndex = poolIndex;
			}

			// Token: 0x0600168B RID: 5771 RVA: 0x0003AFD6 File Offset: 0x000391D6
			public void Dispose()
			{
				if (this._owner != null)
				{
					this._owner.Release(this.Item, this._poolIndex);
				}
			}

			// Token: 0x040006D1 RID: 1745
			public readonly StringBuilder Item;

			// Token: 0x040006D2 RID: 1746
			private readonly StringBuilderPool _owner;

			// Token: 0x040006D3 RID: 1747
			private readonly int _poolIndex;
		}
	}
}
