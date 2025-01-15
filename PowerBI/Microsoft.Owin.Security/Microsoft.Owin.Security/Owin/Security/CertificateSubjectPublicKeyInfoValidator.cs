using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Security;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Win32;

namespace Microsoft.Owin.Security
{
	// Token: 0x02000008 RID: 8
	public class CertificateSubjectPublicKeyInfoValidator : ICertificateValidator
	{
		// Token: 0x06000013 RID: 19 RVA: 0x00002250 File Offset: 0x00000450
		public CertificateSubjectPublicKeyInfoValidator(IEnumerable<string> validBase64EncodedSubjectPublicKeyInfoHashes, SubjectPublicKeyInfoAlgorithm algorithm)
		{
			if (validBase64EncodedSubjectPublicKeyInfoHashes == null)
			{
				throw new ArgumentNullException("validBase64EncodedSubjectPublicKeyInfoHashes");
			}
			this._validBase64EncodedSubjectPublicKeyInfoHashes = new HashSet<string>(validBase64EncodedSubjectPublicKeyInfoHashes);
			if (this._validBase64EncodedSubjectPublicKeyInfoHashes.Count == 0)
			{
				throw new ArgumentOutOfRangeException("validBase64EncodedSubjectPublicKeyInfoHashes");
			}
			if (this._algorithm != SubjectPublicKeyInfoAlgorithm.Sha1 && this._algorithm != SubjectPublicKeyInfoAlgorithm.Sha256)
			{
				throw new ArgumentOutOfRangeException("algorithm");
			}
			this._algorithm = algorithm;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000022B8 File Offset: 0x000004B8
		public bool Validate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			if (sslPolicyErrors != SslPolicyErrors.None)
			{
				return false;
			}
			if (chain == null)
			{
				throw new ArgumentNullException("chain");
			}
			if (chain.ChainElements.Count < 2)
			{
				return false;
			}
			using (HashAlgorithm algorithm = this.CreateHashAlgorithm())
			{
				foreach (X509ChainElement chainElement in chain.ChainElements)
				{
					X509Certificate2 chainedCertificate = chainElement.Certificate;
					string base64Spki = Convert.ToBase64String(algorithm.ComputeHash(CertificateSubjectPublicKeyInfoValidator.ExtractSpkiBlob(chainedCertificate)));
					if (this._validBase64EncodedSubjectPublicKeyInfoHashes.Contains(base64Spki))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000235C File Offset: 0x0000055C
		private static byte[] ExtractSpkiBlob(X509Certificate2 certificate)
		{
			NativeMethods.CERT_CONTEXT certContext = (NativeMethods.CERT_CONTEXT)Marshal.PtrToStructure(certificate.Handle, typeof(NativeMethods.CERT_CONTEXT));
			NativeMethods.CERT_INFO certInfo = (NativeMethods.CERT_INFO)Marshal.PtrToStructure(certContext.pCertInfo, typeof(NativeMethods.CERT_INFO));
			NativeMethods.CERT_PUBLIC_KEY_INFO publicKeyInfo = certInfo.SubjectPublicKeyInfo;
			uint blobSize = 0U;
			IntPtr structType = new IntPtr(8);
			if (!NativeMethods.CryptEncodeObject(1U, structType, ref publicKeyInfo, null, ref blobSize))
			{
				int error = Marshal.GetLastWin32Error();
				throw new Win32Exception(error);
			}
			byte[] blob = new byte[blobSize];
			if (!NativeMethods.CryptEncodeObject(1U, structType, ref publicKeyInfo, blob, ref blobSize))
			{
				int error2 = Marshal.GetLastWin32Error();
				throw new Win32Exception(error2);
			}
			return blob;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000023F7 File Offset: 0x000005F7
		private HashAlgorithm CreateHashAlgorithm()
		{
			if (this._algorithm != SubjectPublicKeyInfoAlgorithm.Sha1)
			{
				return new SHA256CryptoServiceProvider();
			}
			return new SHA1CryptoServiceProvider();
		}

		// Token: 0x0400000C RID: 12
		private readonly HashSet<string> _validBase64EncodedSubjectPublicKeyInfoHashes;

		// Token: 0x0400000D RID: 13
		private readonly SubjectPublicKeyInfoAlgorithm _algorithm;
	}
}
