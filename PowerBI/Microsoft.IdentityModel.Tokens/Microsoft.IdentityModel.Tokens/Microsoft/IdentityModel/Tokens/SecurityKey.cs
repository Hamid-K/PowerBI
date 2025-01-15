using System;
using Microsoft.IdentityModel.Json;
using Microsoft.IdentityModel.Logging;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000171 RID: 369
	public abstract class SecurityKey
	{
		// Token: 0x0600109E RID: 4254 RVA: 0x00040A69 File Offset: 0x0003EC69
		internal SecurityKey(SecurityKey key)
		{
			this._cryptoProviderFactory = key._cryptoProviderFactory;
			this.KeyId = key.KeyId;
			this.SetInternalId();
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x00040A8F File Offset: 0x0003EC8F
		public SecurityKey()
		{
			this._cryptoProviderFactory = CryptoProviderFactory.Default;
			this.SetInternalId();
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x060010A0 RID: 4256 RVA: 0x00040AA8 File Offset: 0x0003ECA8
		[JsonIgnore]
		internal virtual string InternalId
		{
			get
			{
				return this._internalId.Value;
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x060010A1 RID: 4257
		public abstract int KeySize { get; }

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x060010A2 RID: 4258 RVA: 0x00040AB5 File Offset: 0x0003ECB5
		// (set) Token: 0x060010A3 RID: 4259 RVA: 0x00040ABD File Offset: 0x0003ECBD
		[JsonIgnore]
		public virtual string KeyId { get; set; }

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x060010A4 RID: 4260 RVA: 0x00040AC6 File Offset: 0x0003ECC6
		// (set) Token: 0x060010A5 RID: 4261 RVA: 0x00040ACE File Offset: 0x0003ECCE
		[JsonIgnore]
		public CryptoProviderFactory CryptoProviderFactory
		{
			get
			{
				return this._cryptoProviderFactory;
			}
			set
			{
				if (value == null)
				{
					throw LogHelper.LogArgumentNullException("value");
				}
				this._cryptoProviderFactory = value;
			}
		}

		// Token: 0x060010A6 RID: 4262 RVA: 0x00040AE6 File Offset: 0x0003ECE6
		public override string ToString()
		{
			return string.Format("{0}, KeyId: '{1}', InternalId: '{2}'.", base.GetType(), this.KeyId, this.InternalId);
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x00040B04 File Offset: 0x0003ED04
		public virtual bool CanComputeJwkThumbprint()
		{
			return false;
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x00040B07 File Offset: 0x0003ED07
		public virtual byte[] ComputeJwkThumbprint()
		{
			throw LogHelper.LogExceptionMessage(new NotSupportedException(LogHelper.FormatInvariant("IDX10710: Computing a JWK thumbprint is supported only on SymmetricSecurityKey, JsonWebKey, RsaSecurityKey, X509SecurityKey, and ECDsaSecurityKey.", Array.Empty<object>())));
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x00040B22 File Offset: 0x0003ED22
		public virtual bool IsSupportedAlgorithm(string algorithm)
		{
			return this.CryptoProviderFactory.IsSupportedAlgorithm(algorithm, this);
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x00040B31 File Offset: 0x0003ED31
		private void SetInternalId()
		{
			this._internalId = new Lazy<string>(delegate
			{
				if (this.CanComputeJwkThumbprint())
				{
					return Base64UrlEncoder.Encode(this.ComputeJwkThumbprint());
				}
				return string.Empty;
			});
		}

		// Token: 0x04000660 RID: 1632
		private CryptoProviderFactory _cryptoProviderFactory;

		// Token: 0x04000661 RID: 1633
		private Lazy<string> _internalId;
	}
}
