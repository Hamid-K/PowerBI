using System;
using System.Linq;
using System.Runtime.Caching;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000C8 RID: 200
	internal abstract class VirtualizationBasedSecurityEnclaveProviderBase : EnclaveProviderBase
	{
		// Token: 0x06000E13 RID: 3603 RVA: 0x0000E34C File Offset: 0x0000C54C
		internal override void GetEnclaveSession(EnclaveSessionParameters enclaveSessionParameters, bool generateCustomData, bool isRetry, out SqlEnclaveSession sqlEnclaveSession, out long counter, out byte[] customData, out int customDataLength)
		{
			base.GetEnclaveSessionHelper(enclaveSessionParameters, false, isRetry, out sqlEnclaveSession, out counter, out customData, out customDataLength);
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x0002D400 File Offset: 0x0002B600
		internal override SqlEnclaveAttestationParameters GetAttestationParameters(string attestationUrl, byte[] customData, int customDataLength)
		{
			ECDiffieHellman ecdiffieHellman = KeyConverter.CreateECDiffieHellman(384);
			return new SqlEnclaveAttestationParameters(3, Array.Empty<byte>(), ecdiffieHellman);
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x0002D424 File Offset: 0x0002B624
		internal override void CreateEnclaveSession(byte[] attestationInfo, ECDiffieHellman clientDHKey, EnclaveSessionParameters enclaveSessionParameters, byte[] customData, int customDataLength, out SqlEnclaveSession sqlEnclaveSession, out long counter)
		{
			sqlEnclaveSession = null;
			counter = 0L;
			try
			{
				EnclaveProviderBase.ThreadRetryCache.Remove(Thread.CurrentThread.ManagedThreadId.ToString(), null);
				sqlEnclaveSession = base.GetEnclaveSessionFromCache(enclaveSessionParameters, out counter);
				if (sqlEnclaveSession == null)
				{
					if (string.IsNullOrEmpty(enclaveSessionParameters.AttestationUrl))
					{
						throw SQL.AttestationFailed(Strings.FailToCreateEnclaveSession, null);
					}
					AttestationInfo attestationInfo2 = new AttestationInfo(attestationInfo);
					this.VerifyEnclavePolicy(attestationInfo2.EnclaveReportPackage);
					this.VerifyAttestationInfo(enclaveSessionParameters.AttestationUrl, attestationInfo2.HealthReport, attestationInfo2.EnclaveReportPackage);
					byte[] sharedSecret = this.GetSharedSecret(attestationInfo2.Identity, attestationInfo2.EnclaveDHInfo, clientDHKey);
					sqlEnclaveSession = base.AddEnclaveSessionToCache(enclaveSessionParameters, sharedSecret, attestationInfo2.SessionId, out counter);
				}
			}
			finally
			{
				base.UpdateEnclaveSessionLockStatus(sqlEnclaveSession);
			}
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x0000CE20 File Offset: 0x0000B020
		internal override void InvalidateEnclaveSession(EnclaveSessionParameters enclaveSessionParameters, SqlEnclaveSession enclaveSessionToInvalidate)
		{
			base.InvalidateEnclaveSessionHelper(enclaveSessionParameters, enclaveSessionToInvalidate);
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x0002D4F4 File Offset: 0x0002B6F4
		private void VerifyAttestationInfo(string attestationUrl, HealthReport healthReport, EnclaveReportPackage enclaveReportPackage)
		{
			bool flag = false;
			X509ChainStatusFlags x509ChainStatusFlags;
			for (;;)
			{
				bool flag2 = false;
				X509Certificate2Collection signingCertificate = this.GetSigningCertificate(attestationUrl, flag);
				if (!this.VerifyHealthReportAgainstRootCertificate(signingCertificate, healthReport.Certificate, out x509ChainStatusFlags) || x509ChainStatusFlags != X509ChainStatusFlags.NoError)
				{
					if (flag)
					{
						break;
					}
					flag = true;
					flag2 = true;
				}
				if (!flag2)
				{
					goto Block_3;
				}
			}
			throw SQL.AttestationFailed(string.Format(Strings.VerifyHealthCertificateChainFormat, attestationUrl, x509ChainStatusFlags), null);
			Block_3:
			this.VerifyEnclaveReportSignature(enclaveReportPackage, healthReport.Certificate);
		}

		// Token: 0x06000E18 RID: 3608
		protected abstract byte[] MakeRequest(string url);

		// Token: 0x06000E19 RID: 3609 RVA: 0x0002D554 File Offset: 0x0002B754
		private X509Certificate2Collection GetSigningCertificate(string attestationUrl, bool forceUpdate)
		{
			attestationUrl = this.GetAttestationUrl(attestationUrl);
			X509Certificate2Collection x509Certificate2Collection = (X509Certificate2Collection)VirtualizationBasedSecurityEnclaveProviderBase.rootSigningCertificateCache[attestationUrl];
			if (forceUpdate || x509Certificate2Collection == null || this.AnyCertificatesExpired(x509Certificate2Collection))
			{
				byte[] array = this.MakeRequest(attestationUrl);
				X509Certificate2Collection x509Certificate2Collection2 = new X509Certificate2Collection();
				try
				{
					x509Certificate2Collection2.Import(array);
				}
				catch (CryptographicException ex)
				{
					throw SQL.AttestationFailed(string.Format(Strings.GetAttestationSigningCertificateFailedInvalidCertificate, attestationUrl), ex);
				}
				VirtualizationBasedSecurityEnclaveProviderBase.rootSigningCertificateCache.Add(attestationUrl, x509Certificate2Collection2, DateTime.Now.AddDays(1.0), null);
			}
			return (X509Certificate2Collection)VirtualizationBasedSecurityEnclaveProviderBase.rootSigningCertificateCache[attestationUrl];
		}

		// Token: 0x06000E1A RID: 3610
		protected abstract string GetAttestationUrl(string attestationUrl);

		// Token: 0x06000E1B RID: 3611 RVA: 0x0002D600 File Offset: 0x0002B800
		private bool AnyCertificatesExpired(X509Certificate2Collection certificates)
		{
			return certificates.OfType<X509Certificate2>().Any((X509Certificate2 c) => c.NotAfter < DateTime.Now);
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x0002D62C File Offset: 0x0002B82C
		private bool VerifyHealthReportAgainstRootCertificate(X509Certificate2Collection signingCerts, X509Certificate2 healthReportCert, out X509ChainStatusFlags chainStatus)
		{
			X509Chain x509Chain = new X509Chain();
			chainStatus = X509ChainStatusFlags.NoError;
			foreach (X509Certificate2 x509Certificate in signingCerts)
			{
				x509Chain.ChainPolicy.ExtraStore.Add(x509Certificate);
			}
			x509Chain.ChainPolicy.RevocationMode = X509RevocationMode.NoCheck;
			if (x509Chain.Build(healthReportCert))
			{
				return true;
			}
			bool flag = false;
			foreach (X509ChainStatus x509ChainStatus in x509Chain.ChainStatus)
			{
				if (x509ChainStatus.Status != X509ChainStatusFlags.UntrustedRoot)
				{
					chainStatus = x509ChainStatus.Status;
					return true;
				}
				flag = true;
			}
			if (flag)
			{
				for (int j = 0; j < x509Chain.ChainElements.Count; j++)
				{
					X509ChainElement x509ChainElement = x509Chain.ChainElements[x509Chain.ChainElements.Count - 1 - j];
					foreach (X509Certificate2 x509Certificate2 in signingCerts)
					{
						if (x509ChainElement.Certificate.Thumbprint == x509Certificate2.Thumbprint)
						{
							return true;
						}
					}
				}
				chainStatus = X509ChainStatusFlags.UntrustedRoot;
				return true;
			}
			return false;
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x0002D744 File Offset: 0x0002B944
		private void VerifyEnclaveReportSignature(EnclaveReportPackage enclaveReportPackage, X509Certificate2 healthReportCert)
		{
			uint num = Convert.ToUInt32(enclaveReportPackage.PackageHeader.GetSizeInPayload()) + enclaveReportPackage.PackageHeader.SignedStatementSize + enclaveReportPackage.PackageHeader.SignatureSize;
			if (num != enclaveReportPackage.PackageHeader.PackageSize)
			{
				throw new ArgumentException(Strings.VerifyEnclaveReportFormatFailed);
			}
			using (RSA rsafromCertificate = KeyConverter.GetRSAFromCertificate(healthReportCert))
			{
				if (!rsafromCertificate.VerifyData(enclaveReportPackage.ReportAsBytes, enclaveReportPackage.SignatureBlob, HashAlgorithmName.SHA256, RSASignaturePadding.Pss))
				{
					throw new ArgumentException(Strings.VerifyEnclaveReportFailed);
				}
			}
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x0002D7E0 File Offset: 0x0002B9E0
		private void VerifyEnclavePolicy(EnclaveReportPackage enclaveReportPackage)
		{
			EnclaveIdentity identity = enclaveReportPackage.Report.Identity;
			this.VerifyEnclavePolicyProperty("OwnerId", identity.OwnerId, VirtualizationBasedSecurityEnclaveProviderBase.ExpectedPolicy.OwnerId);
			this.VerifyEnclavePolicyProperty("AuthorId", identity.AuthorId, VirtualizationBasedSecurityEnclaveProviderBase.ExpectedPolicy.AuthorId);
			this.VerifyEnclavePolicyProperty("FamilyId", identity.FamilyId, VirtualizationBasedSecurityEnclaveProviderBase.ExpectedPolicy.FamilyId);
			this.VerifyEnclavePolicyProperty("ImageId", identity.ImageId, VirtualizationBasedSecurityEnclaveProviderBase.ExpectedPolicy.ImageId);
			this.VerifyEnclavePolicyProperty("EnclaveSvn", identity.EnclaveSvn, VirtualizationBasedSecurityEnclaveProviderBase.ExpectedPolicy.EnclaveSvn);
			this.VerifyEnclavePolicyProperty("SecureKernelSvn", identity.SecureKernelSvn, VirtualizationBasedSecurityEnclaveProviderBase.ExpectedPolicy.SecureKernelSvn);
			this.VerifyEnclavePolicyProperty("PlatformSvn", identity.PlatformSvn, VirtualizationBasedSecurityEnclaveProviderBase.ExpectedPolicy.PlatformSvn);
			if (identity.Flags != VirtualizationBasedSecurityEnclaveProviderBase.ExpectedPolicy.Flags)
			{
				throw new InvalidOperationException(Strings.VerifyEnclaveDebuggable);
			}
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x0002D8D4 File Offset: 0x0002BAD4
		private void VerifyEnclavePolicyProperty(string property, byte[] actual, byte[] expected)
		{
			if (!actual.SequenceEqual(expected))
			{
				string text = string.Format(Strings.VerifyEnclavePolicyFailedFormat, property, BitConverter.ToString(actual), BitConverter.ToString(expected));
				throw new ArgumentException(text);
			}
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x0002D90C File Offset: 0x0002BB0C
		private void VerifyEnclavePolicyProperty(string property, uint actual, uint expected)
		{
			if (actual < expected)
			{
				string text = string.Format(Strings.VerifyEnclavePolicyFailedFormat, property, actual, expected);
				throw new ArgumentException(text);
			}
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x0002D93C File Offset: 0x0002BB3C
		private byte[] GetSharedSecret(EnclavePublicKey enclavePublicKey, EnclaveDiffieHellmanInfo enclaveDHInfo, ECDiffieHellman clientDHKey)
		{
			using (RSA rsa = KeyConverter.CreateRSAFromPublicKeyBlob(enclavePublicKey.PublicKey))
			{
				if (!rsa.VerifyData(enclaveDHInfo.PublicKey, enclaveDHInfo.PublicKeySignature, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1))
				{
					throw new ArgumentException(Strings.GetSharedSecretFailed);
				}
			}
			byte[] array;
			using (ECDiffieHellman ecdiffieHellman = KeyConverter.CreateECDiffieHellmanFromPublicKeyBlob(enclaveDHInfo.PublicKey))
			{
				array = KeyConverter.DeriveKey(clientDHKey, ecdiffieHellman.PublicKey);
			}
			return array;
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x0002D9CC File Offset: 0x0002BBCC
		// Note: this type is marked as 'beforefieldinit'.
		static VirtualizationBasedSecurityEnclaveProviderBase()
		{
			EnclaveIdentity enclaveIdentity = new EnclaveIdentity();
			enclaveIdentity.OwnerId = new byte[]
			{
				16, 32, 48, 64, 65, 49, 33, 17, 0, 0,
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				0, 0
			};
			enclaveIdentity.UniqueId = new byte[0];
			enclaveIdentity.AuthorId = new byte[]
			{
				4, 55, 202, 226, 83, 125, 139, 155, 7, 118,
				182, 27, 17, 230, 206, 211, 210, 50, 233, 48,
				143, 96, 226, 26, 218, 178, 253, 145, 227, 218,
				149, 152
			};
			EnclaveIdentity enclaveIdentity2 = enclaveIdentity;
			byte[] array = new byte[16];
			array[0] = 254;
			array[1] = 254;
			enclaveIdentity2.FamilyId = array;
			enclaveIdentity.ImageId = new byte[]
			{
				25, 23, 18, 0, 1, 5, 32, 19, 0, 5,
				20, 3, 18, 1, 34, 5
			};
			enclaveIdentity.EnclaveSvn = 0U;
			enclaveIdentity.SecureKernelSvn = 0U;
			enclaveIdentity.PlatformSvn = 1U;
			enclaveIdentity.Flags = 0U;
			enclaveIdentity.SigningLevel = 0U;
			enclaveIdentity.Reserved = 0U;
			VirtualizationBasedSecurityEnclaveProviderBase.ExpectedPolicy = enclaveIdentity;
		}

		// Token: 0x0400062C RID: 1580
		private static readonly MemoryCache rootSigningCertificateCache = new MemoryCache("RootSigningCertificateCache", null);

		// Token: 0x0400062D RID: 1581
		private const int DiffieHellmanKeySize = 384;

		// Token: 0x0400062E RID: 1582
		private const int VsmHGSProtocolId = 3;

		// Token: 0x0400062F RID: 1583
		private static readonly EnclaveIdentity ExpectedPolicy;
	}
}
