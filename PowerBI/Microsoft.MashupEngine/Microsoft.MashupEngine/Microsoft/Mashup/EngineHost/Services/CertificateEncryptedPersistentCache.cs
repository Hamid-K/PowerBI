using System;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001995 RID: 6549
	public sealed class CertificateEncryptedPersistentCache : EncryptedPersistentCache
	{
		// Token: 0x0600A623 RID: 42531 RVA: 0x00225C01 File Offset: 0x00223E01
		public CertificateEncryptedPersistentCache(PersistentCache cache, SymmetricAlgorithm algorithm, string encryptionKeyPathName, string certThumbprint)
			: base(cache, algorithm, encryptionKeyPathName)
		{
			this.certThumbprint = certThumbprint;
			this.certificate = CertificateEncryptedPersistentCache.GetCertificate(certThumbprint);
		}

		// Token: 0x0600A624 RID: 42532 RVA: 0x00225C24 File Offset: 0x00223E24
		protected override byte[] UnencryptEncryptionKey(byte[] encryptedEncryptionKey)
		{
			byte[] array2;
			try
			{
				byte[] array;
				if (this.TryDecryptWithOAEPPadding(encryptedEncryptionKey, out array))
				{
					array2 = array;
				}
				else
				{
					array2 = ((RSACryptoServiceProvider)this.certificate.PrivateKey).Decrypt(encryptedEncryptionKey, false);
				}
			}
			catch (CryptographicException ex)
			{
				throw new PersistentCacheException(Strings.Cache_Certificate_Cryptographic_Exception(this.certThumbprint, ex.Message), null);
			}
			return array2;
		}

		// Token: 0x0600A625 RID: 42533 RVA: 0x00225C84 File Offset: 0x00223E84
		protected override byte[] EncryptEncryptionKey(byte[] encryptionKey)
		{
			byte[] array;
			try
			{
				array = ((RSACryptoServiceProvider)this.certificate.PublicKey.Key).Encrypt(encryptionKey, true);
			}
			catch (CryptographicException ex)
			{
				throw new PersistentCacheException(Strings.Cache_Certificate_Cryptographic_Exception(this.certThumbprint, ex.Message), null);
			}
			return array;
		}

		// Token: 0x0600A626 RID: 42534 RVA: 0x00225CDC File Offset: 0x00223EDC
		private bool TryDecryptWithOAEPPadding(byte[] encryptedEncryptionKey, out byte[] encryptionKey)
		{
			bool flag;
			try
			{
				encryptionKey = ((RSACryptoServiceProvider)this.certificate.PrivateKey).Decrypt(encryptedEncryptionKey, true);
				flag = true;
			}
			catch (CryptographicException)
			{
				encryptionKey = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600A627 RID: 42535 RVA: 0x00225D20 File Offset: 0x00223F20
		private static X509Certificate2 GetCertificate(string thumbprint)
		{
			X509Certificate2 x509Certificate;
			if (!CertificateEncryptedPersistentCache.TryGetCertificate(thumbprint, StoreLocation.CurrentUser, out x509Certificate))
			{
				try
				{
					CertificateEncryptedPersistentCache.TryGetCertificate(thumbprint, StoreLocation.LocalMachine, out x509Certificate);
				}
				catch (SecurityException)
				{
				}
			}
			if (x509Certificate == null)
			{
				throw new PersistentCacheException(Strings.Cache_Certificate_Not_Found(thumbprint), null);
			}
			if (!(x509Certificate.PrivateKey is RSA) || !(x509Certificate.PublicKey.Key is RSA))
			{
				throw new PersistentCacheException(Strings.Cache_Certificate_NotRSA(thumbprint), null);
			}
			return x509Certificate;
		}

		// Token: 0x0600A628 RID: 42536 RVA: 0x00225D94 File Offset: 0x00223F94
		private static bool TryGetCertificate(string thumbprint, StoreLocation location, out X509Certificate2 certificate)
		{
			X509Store x509Store = new X509Store(location);
			x509Store.Open(OpenFlags.ReadOnly);
			X509Certificate2Collection x509Certificate2Collection = x509Store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, false);
			if (x509Certificate2Collection.Count > 1)
			{
				throw new PersistentCacheException(Strings.Cache_Certificate_Additional_Cert(thumbprint), null);
			}
			if (x509Certificate2Collection.Count != 1)
			{
				certificate = null;
				return false;
			}
			certificate = x509Certificate2Collection[0];
			if (!CertificateManager.IsCertificateChainValid(X509Chain.Create(), certificate))
			{
				throw new PersistentCacheException(Strings.Cache_Certificate_Verification_Failed(thumbprint), null);
			}
			return true;
		}

		// Token: 0x04005676 RID: 22134
		private readonly string certThumbprint;

		// Token: 0x04005677 RID: 22135
		private readonly X509Certificate2 certificate;
	}
}
