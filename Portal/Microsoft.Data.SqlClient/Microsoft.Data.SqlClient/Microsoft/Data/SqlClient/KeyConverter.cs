using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000CD RID: 205
	internal sealed class KeyConverter
	{
		// Token: 0x06000ED5 RID: 3797 RVA: 0x0002EFA4 File Offset: 0x0002D1A4
		internal static RSA CreateRSAFromPublicKeyBlob(byte[] keyBlob)
		{
			CngKey cngKey = CngKey.Import(keyBlob, CngKeyBlobFormat.GenericPublicBlob);
			return new RSACng(cngKey);
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x0002EFC4 File Offset: 0x0002D1C4
		internal static ECDiffieHellman CreateECDiffieHellmanFromPublicKeyBlob(byte[] keyBlob)
		{
			CngKey cngKey = CngKey.Import(keyBlob, CngKeyBlobFormat.GenericPublicBlob);
			return new ECDiffieHellmanCng(cngKey);
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x0002EFE4 File Offset: 0x0002D1E4
		internal static ECDiffieHellman CreateECDiffieHellman(int keySize)
		{
			return new ECDiffieHellmanCng(keySize)
			{
				KeyDerivationFunction = ECDiffieHellmanKeyDerivationFunction.Hash,
				HashAlgorithm = CngAlgorithm.Sha256
			};
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x0002F00C File Offset: 0x0002D20C
		public static byte[] GetECDiffieHellmanPublicKeyBlob(ECDiffieHellman ecDiffieHellman)
		{
			ECDiffieHellmanCng ecdiffieHellmanCng = ecDiffieHellman as ECDiffieHellmanCng;
			if (ecdiffieHellmanCng != null)
			{
				return ecdiffieHellmanCng.Key.Export(CngKeyBlobFormat.EccPublicBlob);
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x0002F03C File Offset: 0x0002D23C
		internal static byte[] DeriveKey(ECDiffieHellman ecDiffieHellman, ECDiffieHellmanPublicKey publicKey)
		{
			ECDiffieHellmanCng ecdiffieHellmanCng = ecDiffieHellman as ECDiffieHellmanCng;
			if (ecdiffieHellmanCng != null)
			{
				return ecdiffieHellmanCng.DeriveKeyMaterial(publicKey);
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x0002F060 File Offset: 0x0002D260
		internal static RSA GetRSAFromCertificate(X509Certificate2 certificate)
		{
			RSAParameters rsaparameters;
			using (RSA rsapublicKey = certificate.GetRSAPublicKey())
			{
				rsaparameters = rsapublicKey.ExportParameters(false);
			}
			RSACng rsacng = new RSACng();
			rsacng.ImportParameters(rsaparameters);
			return rsacng;
		}
	}
}
