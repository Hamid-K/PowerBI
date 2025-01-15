using System;
using System.Runtime.ConstrainedExecution;
using System.Threading;

namespace Microsoft.Data.ProviderBase
{
	// Token: 0x0200015F RID: 351
	internal sealed class DbConnectionPoolAuthenticationContext
	{
		// Token: 0x06001A67 RID: 6759 RVA: 0x0006C0EE File Offset: 0x0006A2EE
		internal DbConnectionPoolAuthenticationContext(byte[] accessToken, DateTime expirationTime)
		{
			this._accessToken = accessToken;
			this._expirationTime = expirationTime;
			this._isUpdateInProgress = 0;
		}

		// Token: 0x06001A68 RID: 6760 RVA: 0x0006C10B File Offset: 0x0006A30B
		internal static DbConnectionPoolAuthenticationContext ChooseAuthenticationContextToUpdate(DbConnectionPoolAuthenticationContext context1, DbConnectionPoolAuthenticationContext context2)
		{
			if (!(context1.ExpirationTime > context2.ExpirationTime))
			{
				return context2;
			}
			return context1;
		}

		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x06001A69 RID: 6761 RVA: 0x0006C123 File Offset: 0x0006A323
		internal byte[] AccessToken
		{
			get
			{
				return this._accessToken;
			}
		}

		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x06001A6A RID: 6762 RVA: 0x0006C12B File Offset: 0x0006A32B
		internal DateTime ExpirationTime
		{
			get
			{
				return this._expirationTime;
			}
		}

		// Token: 0x06001A6B RID: 6763 RVA: 0x0006C134 File Offset: 0x0006A334
		internal bool LockToUpdate()
		{
			int num = Interlocked.CompareExchange(ref this._isUpdateInProgress, 1, 0);
			return num == 0;
		}

		// Token: 0x06001A6C RID: 6764 RVA: 0x0006C154 File Offset: 0x0006A354
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal void ReleaseLockToUpdate()
		{
			int num = Interlocked.CompareExchange(ref this._isUpdateInProgress, 0, 1);
		}

		// Token: 0x04000AB4 RID: 2740
		private const int STATUS_LOCKED = 1;

		// Token: 0x04000AB5 RID: 2741
		private const int STATUS_UNLOCKED = 0;

		// Token: 0x04000AB6 RID: 2742
		private readonly byte[] _accessToken;

		// Token: 0x04000AB7 RID: 2743
		private readonly DateTime _expirationTime;

		// Token: 0x04000AB8 RID: 2744
		private int _isUpdateInProgress;
	}
}
