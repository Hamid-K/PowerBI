using System;
using System.Threading;
using Microsoft.IdentityModel.Logging;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000176 RID: 374
	public abstract class SignatureProvider : IDisposable
	{
		// Token: 0x060010E0 RID: 4320 RVA: 0x00040CA0 File Offset: 0x0003EEA0
		protected SignatureProvider(SecurityKey key, string algorithm)
		{
			if (key == null)
			{
				throw LogHelper.LogArgumentNullException("key");
			}
			this.Key = key;
			if (!string.IsNullOrEmpty(algorithm))
			{
				this.Algorithm = algorithm;
				this._referenceCount = 1;
				return;
			}
			throw LogHelper.LogArgumentNullException("algorithm");
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x00040CEC File Offset: 0x0003EEEC
		internal int AddRef()
		{
			return Interlocked.Increment(ref this._referenceCount);
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x060010E2 RID: 4322 RVA: 0x00040CF9 File Offset: 0x0003EEF9
		// (set) Token: 0x060010E3 RID: 4323 RVA: 0x00040D01 File Offset: 0x0003EF01
		public string Algorithm { get; private set; }

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x060010E4 RID: 4324 RVA: 0x00040D0A File Offset: 0x0003EF0A
		// (set) Token: 0x060010E5 RID: 4325 RVA: 0x00040D12 File Offset: 0x0003EF12
		public string Context { get; set; }

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x060010E6 RID: 4326 RVA: 0x00040D1B File Offset: 0x0003EF1B
		// (set) Token: 0x060010E7 RID: 4327 RVA: 0x00040D23 File Offset: 0x0003EF23
		public CryptoProviderCache CryptoProviderCache { get; set; }

		// Token: 0x060010E8 RID: 4328 RVA: 0x00040D2C File Offset: 0x0003EF2C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060010E9 RID: 4329
		protected abstract void Dispose(bool disposing);

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x060010EA RID: 4330 RVA: 0x00040D3B File Offset: 0x0003EF3B
		// (set) Token: 0x060010EB RID: 4331 RVA: 0x00040D43 File Offset: 0x0003EF43
		public SecurityKey Key { get; private set; }

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x060010EC RID: 4332 RVA: 0x00040D4C File Offset: 0x0003EF4C
		internal virtual int ObjectPoolSize
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x060010ED RID: 4333 RVA: 0x00040D4F File Offset: 0x0003EF4F
		internal int RefCount
		{
			get
			{
				return this._referenceCount;
			}
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x00040D57 File Offset: 0x0003EF57
		internal int Release()
		{
			if (this._referenceCount > 0)
			{
				Interlocked.Decrement(ref this._referenceCount);
			}
			return this._referenceCount;
		}

		// Token: 0x060010EF RID: 4335
		public abstract byte[] Sign(byte[] input);

		// Token: 0x060010F0 RID: 4336
		public abstract bool Verify(byte[] input, byte[] signature);

		// Token: 0x060010F1 RID: 4337 RVA: 0x00040D74 File Offset: 0x0003EF74
		public virtual bool Verify(byte[] input, int inputOffset, int inputLength, byte[] signature, int signatureOffset, int signatureLength)
		{
			throw LogHelper.LogExceptionMessage(new NotImplementedException());
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x060010F2 RID: 4338 RVA: 0x00040D80 File Offset: 0x0003EF80
		// (set) Token: 0x060010F3 RID: 4339 RVA: 0x00040D88 File Offset: 0x0003EF88
		public bool WillCreateSignatures { get; protected set; }

		// Token: 0x04000670 RID: 1648
		private int _referenceCount;
	}
}
