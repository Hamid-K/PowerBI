using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.IdentityModel.Logging;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000177 RID: 375
	public class SigningCredentials
	{
		// Token: 0x060010F4 RID: 4340 RVA: 0x00040D91 File Offset: 0x0003EF91
		protected SigningCredentials(X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw LogHelper.LogArgumentNullException("certificate");
			}
			this.Key = new X509SecurityKey(certificate);
			this.Algorithm = "RS256";
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x00040DBE File Offset: 0x0003EFBE
		protected SigningCredentials(X509Certificate2 certificate, string algorithm)
		{
			if (certificate == null)
			{
				throw LogHelper.LogArgumentNullException("certificate");
			}
			this.Key = new X509SecurityKey(certificate);
			this.Algorithm = algorithm;
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x00040DE7 File Offset: 0x0003EFE7
		public SigningCredentials(SecurityKey key, string algorithm)
		{
			this.Key = key;
			this.Algorithm = algorithm;
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x00040DFD File Offset: 0x0003EFFD
		public SigningCredentials(SecurityKey key, string algorithm, string digest)
		{
			this.Key = key;
			this.Algorithm = algorithm;
			this.Digest = digest;
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x060010F8 RID: 4344 RVA: 0x00040E1A File Offset: 0x0003F01A
		// (set) Token: 0x060010F9 RID: 4345 RVA: 0x00040E22 File Offset: 0x0003F022
		public string Algorithm
		{
			get
			{
				return this._algorithm;
			}
			private set
			{
				if (!string.IsNullOrEmpty(value))
				{
					this._algorithm = value;
					return;
				}
				throw LogHelper.LogArgumentNullException("algorithm");
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x060010FA RID: 4346 RVA: 0x00040E40 File Offset: 0x0003F040
		// (set) Token: 0x060010FB RID: 4347 RVA: 0x00040E48 File Offset: 0x0003F048
		public string Digest
		{
			get
			{
				return this._digest;
			}
			private set
			{
				if (!string.IsNullOrEmpty(value))
				{
					this._digest = value;
					return;
				}
				throw LogHelper.LogArgumentNullException("digest");
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x060010FC RID: 4348 RVA: 0x00040E66 File Offset: 0x0003F066
		// (set) Token: 0x060010FD RID: 4349 RVA: 0x00040E6E File Offset: 0x0003F06E
		public CryptoProviderFactory CryptoProviderFactory { get; set; }

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x060010FE RID: 4350 RVA: 0x00040E77 File Offset: 0x0003F077
		// (set) Token: 0x060010FF RID: 4351 RVA: 0x00040E7F File Offset: 0x0003F07F
		public SecurityKey Key
		{
			get
			{
				return this._key;
			}
			private set
			{
				if (value == null)
				{
					throw LogHelper.LogArgumentNullException("key");
				}
				this._key = value;
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06001100 RID: 4352 RVA: 0x00040E97 File Offset: 0x0003F097
		public string Kid
		{
			get
			{
				return this.Key.KeyId;
			}
		}

		// Token: 0x04000676 RID: 1654
		private string _algorithm;

		// Token: 0x04000677 RID: 1655
		private string _digest;

		// Token: 0x04000678 RID: 1656
		private SecurityKey _key;
	}
}
