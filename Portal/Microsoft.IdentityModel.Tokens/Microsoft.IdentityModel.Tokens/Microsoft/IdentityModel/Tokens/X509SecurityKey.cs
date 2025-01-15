using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Microsoft.IdentityModel.Logging;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000195 RID: 405
	public class X509SecurityKey : AsymmetricSecurityKey
	{
		// Token: 0x06001236 RID: 4662 RVA: 0x00044240 File Offset: 0x00042440
		internal X509SecurityKey(JsonWebKey webKey)
			: base(webKey)
		{
			this.Certificate = new X509Certificate2(Convert.FromBase64String(webKey.X5c[0]));
			this.X5t = Base64UrlEncoder.Encode(this.Certificate.GetCertHash());
			webKey.ConvertedSecurityKey = this;
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x00044298 File Offset: 0x00042498
		public X509SecurityKey(X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw LogHelper.LogArgumentNullException("certificate");
			}
			this.Certificate = certificate;
			this.KeyId = certificate.Thumbprint;
			this.X5t = Base64UrlEncoder.Encode(certificate.GetCertHash());
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x000442EC File Offset: 0x000424EC
		public X509SecurityKey(X509Certificate2 certificate, string keyId)
		{
			if (certificate == null)
			{
				throw LogHelper.LogArgumentNullException("certificate");
			}
			this.Certificate = certificate;
			if (!string.IsNullOrEmpty(keyId))
			{
				this.KeyId = keyId;
				this.X5t = Base64UrlEncoder.Encode(certificate.GetCertHash());
				return;
			}
			throw LogHelper.LogArgumentNullException("keyId");
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06001239 RID: 4665 RVA: 0x0004434D File Offset: 0x0004254D
		public override int KeySize
		{
			get
			{
				return this.PublicKey.KeySize;
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x0600123A RID: 4666 RVA: 0x0004435A File Offset: 0x0004255A
		public string X5t { get; }

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x0600123B RID: 4667 RVA: 0x00044364 File Offset: 0x00042564
		public AsymmetricAlgorithm PrivateKey
		{
			get
			{
				if (!this._privateKeyAvailabilityDetermined)
				{
					object thisLock = this.ThisLock;
					lock (thisLock)
					{
						if (!this._privateKeyAvailabilityDetermined)
						{
							this._privateKey = this.Certificate.GetRSAPrivateKey();
							this._privateKeyAvailabilityDetermined = true;
						}
					}
				}
				return this._privateKey;
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x0600123C RID: 4668 RVA: 0x000443CC File Offset: 0x000425CC
		public AsymmetricAlgorithm PublicKey
		{
			get
			{
				if (this._publicKey == null)
				{
					object thisLock = this.ThisLock;
					lock (thisLock)
					{
						if (this._publicKey == null)
						{
							this._publicKey = this.Certificate.GetRSAPublicKey();
						}
					}
				}
				return this._publicKey;
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x0600123D RID: 4669 RVA: 0x00044430 File Offset: 0x00042630
		private object ThisLock
		{
			get
			{
				return this._thisLock;
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x0600123E RID: 4670 RVA: 0x00044438 File Offset: 0x00042638
		[Obsolete("HasPrivateKey method is deprecated, please use PrivateKeyStatus.")]
		public override bool HasPrivateKey
		{
			get
			{
				return this.PrivateKey != null;
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x0600123F RID: 4671 RVA: 0x00044443 File Offset: 0x00042643
		public override PrivateKeyStatus PrivateKeyStatus
		{
			get
			{
				if (this.PrivateKey != null)
				{
					return PrivateKeyStatus.Exists;
				}
				return PrivateKeyStatus.DoesNotExist;
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06001240 RID: 4672 RVA: 0x00044450 File Offset: 0x00042650
		// (set) Token: 0x06001241 RID: 4673 RVA: 0x00044458 File Offset: 0x00042658
		public X509Certificate2 Certificate { get; private set; }

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06001242 RID: 4674 RVA: 0x00044461 File Offset: 0x00042661
		internal override string InternalId
		{
			get
			{
				return this.X5t;
			}
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x00044469 File Offset: 0x00042669
		public override bool CanComputeJwkThumbprint()
		{
			return this.PublicKey is RSA;
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x00044479 File Offset: 0x00042679
		public override byte[] ComputeJwkThumbprint()
		{
			return new RsaSecurityKey(this.PublicKey as RSA).ComputeJwkThumbprint();
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x00044490 File Offset: 0x00042690
		public override bool Equals(object obj)
		{
			X509SecurityKey x509SecurityKey = obj as X509SecurityKey;
			return x509SecurityKey != null && x509SecurityKey.Certificate.Thumbprint.ToString() == this.Certificate.Thumbprint.ToString();
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x000444CE File Offset: 0x000426CE
		public override int GetHashCode()
		{
			return this.Certificate.GetHashCode();
		}

		// Token: 0x040006E5 RID: 1765
		private AsymmetricAlgorithm _privateKey;

		// Token: 0x040006E6 RID: 1766
		private bool _privateKeyAvailabilityDetermined;

		// Token: 0x040006E7 RID: 1767
		private AsymmetricAlgorithm _publicKey;

		// Token: 0x040006E8 RID: 1768
		private object _thisLock = new object();
	}
}
