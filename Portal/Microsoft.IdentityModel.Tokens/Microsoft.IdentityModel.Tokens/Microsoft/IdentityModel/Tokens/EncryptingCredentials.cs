using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.IdentityModel.Logging;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000130 RID: 304
	public class EncryptingCredentials
	{
		// Token: 0x06000EDA RID: 3802 RVA: 0x0003B4A0 File Offset: 0x000396A0
		protected EncryptingCredentials(X509Certificate2 certificate, string alg, string enc)
		{
			if (certificate == null)
			{
				throw LogHelper.LogArgumentNullException("certificate");
			}
			this.Key = new X509SecurityKey(certificate);
			this.Alg = alg;
			this.Enc = enc;
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x0003B4D7 File Offset: 0x000396D7
		public EncryptingCredentials(SecurityKey key, string alg, string enc)
		{
			this.Key = key;
			this.Alg = alg;
			this.Enc = enc;
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x0003B4FB File Offset: 0x000396FB
		public EncryptingCredentials(SymmetricSecurityKey key, string enc)
			: this(key, "none", enc)
		{
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000EDD RID: 3805 RVA: 0x0003B50A File Offset: 0x0003970A
		// (set) Token: 0x06000EDE RID: 3806 RVA: 0x0003B512 File Offset: 0x00039712
		public string Alg
		{
			get
			{
				return this._alg;
			}
			private set
			{
				if (!string.IsNullOrEmpty(value))
				{
					this._alg = value;
					return;
				}
				throw LogHelper.LogArgumentNullException("alg");
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000EDF RID: 3807 RVA: 0x0003B530 File Offset: 0x00039730
		// (set) Token: 0x06000EE0 RID: 3808 RVA: 0x0003B538 File Offset: 0x00039738
		public string Enc
		{
			get
			{
				return this._enc;
			}
			private set
			{
				if (!string.IsNullOrEmpty(value))
				{
					this._enc = value;
					return;
				}
				throw LogHelper.LogArgumentNullException("enc");
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000EE1 RID: 3809 RVA: 0x0003B556 File Offset: 0x00039756
		// (set) Token: 0x06000EE2 RID: 3810 RVA: 0x0003B55E File Offset: 0x0003975E
		public SecurityKey KeyExchangePublicKey { get; set; }

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000EE3 RID: 3811 RVA: 0x0003B567 File Offset: 0x00039767
		// (set) Token: 0x06000EE4 RID: 3812 RVA: 0x0003B56F File Offset: 0x0003976F
		public CryptoProviderFactory CryptoProviderFactory { get; set; }

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000EE5 RID: 3813 RVA: 0x0003B578 File Offset: 0x00039778
		// (set) Token: 0x06000EE6 RID: 3814 RVA: 0x0003B580 File Offset: 0x00039780
		public bool SetDefaultCtyClaim { get; set; } = true;

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000EE7 RID: 3815 RVA: 0x0003B589 File Offset: 0x00039789
		// (set) Token: 0x06000EE8 RID: 3816 RVA: 0x0003B591 File Offset: 0x00039791
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

		// Token: 0x040004B8 RID: 1208
		private string _alg;

		// Token: 0x040004B9 RID: 1209
		private string _enc;

		// Token: 0x040004BA RID: 1210
		private SecurityKey _key;
	}
}
