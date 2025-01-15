using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Microsoft.Mashup.OAuth
{
	// Token: 0x02000017 RID: 23
	internal static class JwtHelpers
	{
		// Token: 0x0600008E RID: 142 RVA: 0x000041C0 File Offset: 0x000023C0
		public static string MakeSignature(string clientId, X509Certificate2 certificate, string audience, bool useX5C)
		{
			long unixTime = JwtHelpers.GetUnixTime(DateTime.UtcNow);
			string text;
			if (useX5C)
			{
				text = string.Format(CultureInfo.InvariantCulture, "{{\"x5c\":\"{0}\",\"kid\":\"{1}\",\"alg\":\"RS256\"}}", Convert.ToBase64String(certificate.GetRawCertData()), JwtHelpers.ToBase64Url(certificate.GetCertHash()));
			}
			else
			{
				text = string.Format(CultureInfo.InvariantCulture, "{{\"x5t\":\"{0}\",\"alg\":\"RS256\"}}", JwtHelpers.HackBinary(certificate.Thumbprint));
			}
			string text2 = string.Format(CultureInfo.InvariantCulture, "{{\"sub\":\"{0}\",\"iss\":\"{0}\",\"jti\":\"{1}\",\"exp\":{2},\"nbf\":{3},\"aud\":\"{4}\"}}", new object[]
			{
				clientId,
				Guid.NewGuid().ToString(""),
				unixTime + 300L,
				unixTime - 1800L,
				audience
			});
			string text3 = JwtHelpers.ToBase64Url(text) + "." + JwtHelpers.ToBase64Url(text2);
			byte[] bytes = Encoding.UTF8.GetBytes(text3);
			string text4;
			using (RSACryptoServiceProvider rsacryptoServiceProvider = JwtHelpers.CreateRsaProvider(certificate))
			{
				RSAPKCS1SignatureFormatter rsapkcs1SignatureFormatter = new RSAPKCS1SignatureFormatter(rsacryptoServiceProvider);
				rsapkcs1SignatureFormatter.SetHashAlgorithm(typeof(SHA256).FullName);
				byte[] array = new SHA256Cng().ComputeHash(bytes);
				byte[] array2 = rsapkcs1SignatureFormatter.CreateSignature(array);
				text4 = text3 + "." + JwtHelpers.ToBase64Url(array2);
			}
			return text4;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00004308 File Offset: 0x00002508
		public static X509Certificate2 CertificateFromThumbprint(string thumbprint)
		{
			X509Certificate2Collection x509Certificate2Collection;
			if (!JwtHelpers.TryGetSingleCertificateFromThumbprint(thumbprint, out x509Certificate2Collection) || x509Certificate2Collection.Count == 0)
			{
				return null;
			}
			return JwtHelpers.GetValidCertificate(x509Certificate2Collection, new string[] { thumbprint });
		}

		// Token: 0x06000090 RID: 144 RVA: 0x0000433C File Offset: 0x0000253C
		public static X509Certificate2 CertificateFromSubjectNameIssuer(string subjectName, string issuer)
		{
			X509Certificate2Collection x509Certificate2Collection;
			if (!JwtHelpers.TryGetSingleCertificateFromSubjectNameIssuer(subjectName, issuer, out x509Certificate2Collection) || x509Certificate2Collection.Count == 0)
			{
				return null;
			}
			return JwtHelpers.GetValidCertificate(x509Certificate2Collection, new string[] { subjectName, issuer });
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00004374 File Offset: 0x00002574
		private static X509Certificate2 GetValidCertificate(X509Certificate2Collection certificates, params string[] searchValues)
		{
			X509Certificate2 x509Certificate = certificates[0];
			if (OAuthSettings.CertificateValidationEnabled && !JwtHelpers.IsCertificateChainValid(x509Certificate))
			{
				throw new OAuthException(OAuthStrings.CertificateVerificationFailed(string.Join(", ", searchValues)));
			}
			return x509Certificate;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000043B0 File Offset: 0x000025B0
		private static bool TryGetSingleCertificateFromThumbprint(string thumbprint, out X509Certificate2Collection certificates)
		{
			string[] array = new string[] { thumbprint };
			return (JwtHelpers.TryGetCertificate(StoreLocation.CurrentUser, array, JwtHelpers.thumbprintFindTypes, out certificates) || JwtHelpers.TryGetCertificate(StoreLocation.LocalMachine, array, JwtHelpers.thumbprintFindTypes, out certificates)) && certificates.Count == 1;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000043F8 File Offset: 0x000025F8
		private static bool TryGetSingleCertificateFromSubjectNameIssuer(string subjectName, string issuer, out X509Certificate2Collection certificates)
		{
			string[] array = new string[] { subjectName, issuer };
			X509Certificate2Collection x509Certificate2Collection;
			JwtHelpers.TryGetCertificate(StoreLocation.CurrentUser, array, JwtHelpers.sniFindTypes, out x509Certificate2Collection);
			X509Certificate2Collection x509Certificate2Collection2;
			JwtHelpers.TryGetCertificate(StoreLocation.LocalMachine, array, JwtHelpers.sniFindTypes, out x509Certificate2Collection2);
			if (x509Certificate2Collection == null && x509Certificate2Collection2 == null)
			{
				certificates = x509Certificate2Collection;
				return false;
			}
			X509Certificate2 latestCertificate = JwtHelpers.GetLatestCertificate(x509Certificate2Collection);
			X509Certificate2 latestCertificate2 = JwtHelpers.GetLatestCertificate(x509Certificate2Collection2);
			if (latestCertificate == null && latestCertificate2 == null)
			{
				certificates = null;
				return false;
			}
			X509Certificate2 x509Certificate;
			if (latestCertificate != null && latestCertificate2 != null)
			{
				x509Certificate = ((latestCertificate.NotAfter > latestCertificate2.NotAfter) ? latestCertificate : latestCertificate2);
			}
			else
			{
				x509Certificate = latestCertificate ?? latestCertificate2;
			}
			certificates = new X509Certificate2Collection(x509Certificate);
			return x509Certificate != null;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00004498 File Offset: 0x00002698
		private static X509Certificate2 GetLatestCertificate(X509Certificate2Collection certificates)
		{
			if (certificates == null)
			{
				return null;
			}
			X509Certificate2 x509Certificate = null;
			foreach (X509Certificate2 x509Certificate2 in certificates)
			{
				if (x509Certificate == null)
				{
					if (x509Certificate2.NotAfter > DateTime.Now)
					{
						x509Certificate = x509Certificate2;
					}
				}
				else if (x509Certificate2.NotBefore > x509Certificate.NotBefore && x509Certificate2.NotAfter > DateTime.Now)
				{
					x509Certificate = x509Certificate2;
				}
			}
			return x509Certificate;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00004508 File Offset: 0x00002708
		private static bool TryGetCertificate(StoreLocation storeLocation, string[] searchValues, X509FindType[] findTypes, out X509Certificate2Collection certificates)
		{
			try
			{
				X509Store x509Store = new X509Store(StoreName.My, storeLocation);
				x509Store.Open(OpenFlags.ReadOnly);
				X509Certificate2Collection x509Certificate2Collection = x509Store.Certificates;
				if (searchValues.Length != findTypes.Length)
				{
					certificates = null;
					return false;
				}
				for (int i = 0; i < searchValues.Length; i++)
				{
					x509Certificate2Collection = x509Certificate2Collection.Find(findTypes[i], searchValues[i], false);
				}
				if (x509Certificate2Collection.Count > 0)
				{
					certificates = x509Certificate2Collection;
					return true;
				}
			}
			catch (Exception ex)
			{
				if (!SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
			}
			certificates = null;
			return false;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00004588 File Offset: 0x00002788
		private static RSACryptoServiceProvider CreateRsaProvider(X509Certificate2 certificate)
		{
			RSACryptoServiceProvider rsacryptoServiceProvider = (RSACryptoServiceProvider)certificate.PrivateKey;
			CspParameters cspParameters = new CspParameters();
			cspParameters.ProviderType = 24;
			cspParameters.KeyContainerName = rsacryptoServiceProvider.CspKeyContainerInfo.KeyContainerName;
			cspParameters.KeyNumber = (int)rsacryptoServiceProvider.CspKeyContainerInfo.KeyNumber;
			if (rsacryptoServiceProvider.CspKeyContainerInfo.MachineKeyStore)
			{
				cspParameters.Flags = CspProviderFlags.UseMachineKeyStore;
			}
			cspParameters.Flags |= CspProviderFlags.UseExistingKey;
			return new RSACryptoServiceProvider(cspParameters);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000045FC File Offset: 0x000027FC
		private static long GetUnixTime(DateTime time)
		{
			return (time.Ticks - JwtHelpers.epoch.Ticks) / 10000000L;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004625 File Offset: 0x00002825
		private static int FromHex(char c)
		{
			return "0123456789ABCDEF".IndexOf(c);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004634 File Offset: 0x00002834
		private static string HackBinary(string s)
		{
			s = s.ToUpperInvariant();
			byte[] array = new byte[s.Length / 2];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (byte)(JwtHelpers.FromHex(s[2 * i]) * 16 + JwtHelpers.FromHex(s[2 * i + 1]));
			}
			return JwtHelpers.ToBase64Url(array);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004694 File Offset: 0x00002894
		private static bool IsCertificateChainValid(X509Certificate2 certificate)
		{
			X509Chain x509Chain = X509Chain.Create();
			x509Chain.ChainPolicy.RevocationFlag = X509RevocationFlag.ExcludeRoot;
			x509Chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;
			x509Chain.ChainPolicy.UrlRetrievalTimeout = new TimeSpan(0, 0, 10);
			x509Chain.ChainPolicy.VerificationTime = DateTime.Now;
			if (!OAuthSettings.RejectOnUnknownRevocation)
			{
				x509Chain.ChainPolicy.VerificationFlags = X509VerificationFlags.IgnoreEndRevocationUnknown | X509VerificationFlags.IgnoreCtlSignerRevocationUnknown | X509VerificationFlags.IgnoreCertificateAuthorityRevocationUnknown;
			}
			return x509Chain.Build(certificate);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004701 File Offset: 0x00002901
		private static string ToBase64Url(string text)
		{
			return JwtHelpers.ToBase64Url(Encoding.UTF8.GetBytes(text));
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00004713 File Offset: 0x00002913
		private static string ToBase64Url(byte[] bytes)
		{
			return Convert.ToBase64String(bytes).Replace('+', '-').Replace('/', '_')
				.TrimEnd(new char[] { '=' });
		}

		// Token: 0x0400009D RID: 157
		private static readonly DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		// Token: 0x0400009E RID: 158
		private const int secondsAtStart = 300;

		// Token: 0x0400009F RID: 159
		private const int secondsAtEnd = 1800;

		// Token: 0x040000A0 RID: 160
		private const int PROV_RSA_AES = 24;

		// Token: 0x040000A1 RID: 161
		private const string hex = "0123456789ABCDEF";

		// Token: 0x040000A2 RID: 162
		private static readonly X509FindType[] thumbprintFindTypes = new X509FindType[1];

		// Token: 0x040000A3 RID: 163
		private static readonly X509FindType[] sniFindTypes = new X509FindType[]
		{
			X509FindType.FindBySubjectName,
			X509FindType.FindByIssuerName
		};
	}
}
