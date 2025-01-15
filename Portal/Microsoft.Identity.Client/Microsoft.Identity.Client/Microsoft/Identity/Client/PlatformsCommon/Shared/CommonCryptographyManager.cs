using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.PlatformsCommon.Interfaces;
using Microsoft.Identity.Client.Utils;

namespace Microsoft.Identity.Client.PlatformsCommon.Shared
{
	// Token: 0x020001EC RID: 492
	[Preserve(AllMembers = true)]
	internal class CommonCryptographyManager : ICryptographyManager
	{
		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x0600151E RID: 5406 RVA: 0x000466D8 File Offset: 0x000448D8
		protected ILoggerAdapter Logger { get; }

		// Token: 0x0600151F RID: 5407 RVA: 0x000466E0 File Offset: 0x000448E0
		public CommonCryptographyManager(ILoggerAdapter logger = null)
		{
			this.Logger = logger;
		}

		// Token: 0x06001520 RID: 5408 RVA: 0x000466EF File Offset: 0x000448EF
		public string CreateBase64UrlEncodedSha256Hash(string input)
		{
			if (!string.IsNullOrEmpty(input))
			{
				return Base64UrlHelpers.Encode(this.CreateSha256HashBytes(input));
			}
			return null;
		}

		// Token: 0x06001521 RID: 5409 RVA: 0x00046708 File Offset: 0x00044908
		public string GenerateCodeVerifier()
		{
			byte[] array = new byte[96];
			using (RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create())
			{
				randomNumberGenerator.GetBytes(array);
			}
			return Base64UrlHelpers.Encode(array);
		}

		// Token: 0x06001522 RID: 5410 RVA: 0x0004674C File Offset: 0x0004494C
		public string CreateSha256Hash(string input)
		{
			if (!string.IsNullOrEmpty(input))
			{
				return Convert.ToBase64String(this.CreateSha256HashBytes(input));
			}
			return null;
		}

		// Token: 0x06001523 RID: 5411 RVA: 0x00046764 File Offset: 0x00044964
		public byte[] CreateSha256HashBytes(string input)
		{
			byte[] array;
			using (SHA256 sha = SHA256.Create())
			{
				array = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
			}
			return array;
		}

		// Token: 0x06001524 RID: 5412 RVA: 0x000467A8 File Offset: 0x000449A8
		public virtual byte[] SignWithCertificate(string message, X509Certificate2 certificate, RSASignaturePadding signaturePadding)
		{
			CommonCryptographyManager.<>c__DisplayClass10_0 CS$<>8__locals1;
			CS$<>8__locals1.signaturePadding = signaturePadding;
			CS$<>8__locals1.certificate = certificate;
			if (!CommonCryptographyManager.s_certificateToRsaMap.TryGetValue(CS$<>8__locals1.certificate.Thumbprint, out CS$<>8__locals1.rsa))
			{
				if (CommonCryptographyManager.s_certificateToRsaMap.Count >= CommonCryptographyManager.s_maximumMapSize)
				{
					CommonCryptographyManager.s_certificateToRsaMap.Clear();
				}
				CS$<>8__locals1.rsa = CS$<>8__locals1.certificate.GetRSAPrivateKey();
			}
			if (CS$<>8__locals1.rsa == null)
			{
				string text = "certificate_not_rsa";
				PublicKey publicKey = CS$<>8__locals1.certificate.PublicKey;
				string text2;
				if (publicKey == null)
				{
					text2 = null;
				}
				else
				{
					Oid oid = publicKey.Oid;
					text2 = ((oid != null) ? oid.FriendlyName : null);
				}
				throw new MsalClientException(text, MsalErrorMessage.CertMustBeRsa(text2));
			}
			byte[] array;
			try
			{
				array = CommonCryptographyManager.<SignWithCertificate>g__SignDataAndCacheProvider|10_0(message, ref CS$<>8__locals1);
			}
			catch (Exception ex)
			{
				ILoggerAdapter logger = this.Logger;
				if (logger != null)
				{
					logger.Warning(string.Format("Exception occurred when signing data with a certificate. {0}", ex));
				}
				CS$<>8__locals1.rsa = CS$<>8__locals1.certificate.GetRSAPrivateKey();
				array = CommonCryptographyManager.<SignWithCertificate>g__SignDataAndCacheProvider|10_0(message, ref CS$<>8__locals1);
			}
			return array;
		}

		// Token: 0x06001526 RID: 5414 RVA: 0x000468BA File Offset: 0x00044ABA
		[CompilerGenerated]
		internal static byte[] <SignWithCertificate>g__SignDataAndCacheProvider|10_0(string message, ref CommonCryptographyManager.<>c__DisplayClass10_0 A_1)
		{
			byte[] array = A_1.rsa.SignData(Encoding.UTF8.GetBytes(message), HashAlgorithmName.SHA256, A_1.signaturePadding);
			CommonCryptographyManager.s_certificateToRsaMap[A_1.certificate.Thumbprint] = A_1.rsa;
			return array;
		}

		// Token: 0x040008C1 RID: 2241
		private static readonly ConcurrentDictionary<string, RSA> s_certificateToRsaMap = new ConcurrentDictionary<string, RSA>();

		// Token: 0x040008C2 RID: 2242
		private static readonly int s_maximumMapSize = 1000;
	}
}
