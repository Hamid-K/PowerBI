using System;
using System.Threading;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000044 RID: 68
	public struct CancellationToken<T>
	{
		// Token: 0x06000109 RID: 265 RVA: 0x00003E13 File Offset: 0x00002013
		public CancellationToken(CancellationToken token, T tag)
		{
			this.Token = token;
			this.Tag = tag;
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00003E23 File Offset: 0x00002023
		public readonly CancellationToken Token { get; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00003E2B File Offset: 0x0000202B
		public readonly T Tag { get; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00003E34 File Offset: 0x00002034
		public bool CanBeCancelled
		{
			get
			{
				return this.Token.CanBeCanceled;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00003E50 File Offset: 0x00002050
		public bool IsCancellationRequested
		{
			get
			{
				return this.Token.IsCancellationRequested;
			}
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00003E6C File Offset: 0x0000206C
		public void ThrowIfCancellationRequested()
		{
			this.Token.ThrowIfCancellationRequested();
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00003E88 File Offset: 0x00002088
		public CancellationTokenRegistration Register(Action callback)
		{
			return this.Token.Register(callback);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00003EA4 File Offset: 0x000020A4
		public CancellationTokenRegistration Register(Action callback, bool useSynchronizationContext)
		{
			return this.Token.Register(callback, useSynchronizationContext);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00003EC4 File Offset: 0x000020C4
		public CancellationTokenRegistration Register(Action<object> callback, object state)
		{
			return this.Token.Register(callback, state);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00003EE4 File Offset: 0x000020E4
		public CancellationTokenRegistration Register(Action<object> callback, object state, bool useSynchronizationContext)
		{
			return this.Token.Register(callback, state, useSynchronizationContext);
		}

		// Token: 0x040000C9 RID: 201
		public static CancellationToken<T> None = CancellationToken.None.WithTag(default(T));
	}
}
