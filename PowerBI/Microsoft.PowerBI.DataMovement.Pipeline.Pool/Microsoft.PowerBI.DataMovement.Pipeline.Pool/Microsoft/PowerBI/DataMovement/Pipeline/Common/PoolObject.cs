using System;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Common
{
	// Token: 0x02000008 RID: 8
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class PoolObject<[global::System.Runtime.CompilerServices.Nullable(2)] TPoolObjectKey, [global::System.Runtime.CompilerServices.Nullable(0)] TObject> : IDisposable where TObject : IDisposable
	{
		// Token: 0x06000009 RID: 9 RVA: 0x000020CE File Offset: 0x000002CE
		internal PoolObject(TPoolObjectKey key, TObject obj, Action<PoolObject<TPoolObjectKey, TObject>> onDispose)
		{
			this.Key = key;
			this.Object = obj;
			this.m_onDispose = onDispose;
			this.MarkAccess();
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000020F1 File Offset: 0x000002F1
		// (set) Token: 0x0600000B RID: 11 RVA: 0x000020F9 File Offset: 0x000002F9
		internal TPoolObjectKey Key { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002102 File Offset: 0x00000302
		// (set) Token: 0x0600000D RID: 13 RVA: 0x0000210A File Offset: 0x0000030A
		internal TObject Object { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002113 File Offset: 0x00000313
		// (set) Token: 0x0600000F RID: 15 RVA: 0x0000211B File Offset: 0x0000031B
		internal DateTime LastAccessed { get; private set; }

		// Token: 0x06000010 RID: 16 RVA: 0x00002124 File Offset: 0x00000324
		internal void MarkAccess()
		{
			this.LastAccessed = DateTime.UtcNow;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002134 File Offset: 0x00000334
		public void Dispose()
		{
			if (!this.m_disposed)
			{
				if (this.Object != null)
				{
					TObject @object = this.Object;
					@object.Dispose();
				}
				if (this.m_onDispose != null)
				{
					this.m_onDispose(this);
				}
				this.m_disposed = true;
			}
		}

		// Token: 0x04000012 RID: 18
		private Action<PoolObject<TPoolObjectKey, TObject>> m_onDispose;

		// Token: 0x04000013 RID: 19
		private bool m_disposed;
	}
}
