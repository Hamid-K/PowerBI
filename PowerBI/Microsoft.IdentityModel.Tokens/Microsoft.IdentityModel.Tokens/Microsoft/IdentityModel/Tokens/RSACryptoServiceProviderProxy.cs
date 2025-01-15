using System;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Logging;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x0200016E RID: 366
	public class RSACryptoServiceProviderProxy : RSA
	{
		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06001084 RID: 4228 RVA: 0x00040308 File Offset: 0x0003E508
		public override string SignatureAlgorithm
		{
			get
			{
				return this._rsa.SignatureAlgorithm;
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06001085 RID: 4229 RVA: 0x00040315 File Offset: 0x0003E515
		public override string KeyExchangeAlgorithm
		{
			get
			{
				return this._rsa.KeyExchangeAlgorithm;
			}
		}

		// Token: 0x06001086 RID: 4230 RVA: 0x00040324 File Offset: 0x0003E524
		public RSACryptoServiceProviderProxy(RSACryptoServiceProvider rsa)
		{
			if (rsa == null)
			{
				throw LogHelper.LogArgumentNullException("rsa");
			}
			bool flag = rsa.CspKeyContainerInfo.ProviderType == 1 || rsa.CspKeyContainerInfo.ProviderType == 12;
			bool flag2 = Type.GetType("Mono.Runtime") != null;
			if (flag && !rsa.CspKeyContainerInfo.HardwareDevice)
			{
				CspParameters cspParameters = new CspParameters();
				cspParameters.ProviderType = 24;
				cspParameters.KeyContainerName = rsa.CspKeyContainerInfo.KeyContainerName;
				cspParameters.KeyNumber = (int)rsa.CspKeyContainerInfo.KeyNumber;
				if (rsa.CspKeyContainerInfo.MachineKeyStore)
				{
					cspParameters.Flags = CspProviderFlags.UseMachineKeyStore;
				}
				cspParameters.Flags |= CspProviderFlags.UseExistingKey;
				try
				{
					this._rsa = new RSACryptoServiceProvider(cspParameters);
					this._disposeRsa = true;
					return;
				}
				catch (CryptographicException obj) when (flag2)
				{
					this._rsa = rsa;
					return;
				}
			}
			this._rsa = rsa;
		}

		// Token: 0x06001087 RID: 4231 RVA: 0x00040428 File Offset: 0x0003E628
		public byte[] Decrypt(byte[] input, bool fOAEP)
		{
			if (input == null || input.Length == 0)
			{
				throw LogHelper.LogArgumentNullException("input");
			}
			return this._rsa.Decrypt(input, fOAEP);
		}

		// Token: 0x06001088 RID: 4232 RVA: 0x00040449 File Offset: 0x0003E649
		public override byte[] DecryptValue(byte[] input)
		{
			if (input == null || input.Length == 0)
			{
				throw LogHelper.LogArgumentNullException("input");
			}
			return this._rsa.DecryptValue(input);
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x00040469 File Offset: 0x0003E669
		public byte[] Encrypt(byte[] input, bool fOAEP)
		{
			if (input == null || input.Length == 0)
			{
				throw LogHelper.LogArgumentNullException("input");
			}
			return this._rsa.Encrypt(input, fOAEP);
		}

		// Token: 0x0600108A RID: 4234 RVA: 0x0004048A File Offset: 0x0003E68A
		public override byte[] EncryptValue(byte[] input)
		{
			if (input == null || input.Length == 0)
			{
				throw LogHelper.LogArgumentNullException("input");
			}
			return this._rsa.EncryptValue(input);
		}

		// Token: 0x0600108B RID: 4235 RVA: 0x000404AA File Offset: 0x0003E6AA
		public byte[] SignData(byte[] input, object hash)
		{
			if (input == null || input.Length == 0)
			{
				throw LogHelper.LogArgumentNullException("input");
			}
			if (hash == null)
			{
				throw LogHelper.LogArgumentNullException("hash");
			}
			return this._rsa.SignData(input, hash);
		}

		// Token: 0x0600108C RID: 4236 RVA: 0x000404DC File Offset: 0x0003E6DC
		public bool VerifyData(byte[] input, object hash, byte[] signature)
		{
			if (input == null || input.Length == 0)
			{
				throw LogHelper.LogArgumentNullException("input");
			}
			if (hash == null)
			{
				throw LogHelper.LogArgumentNullException("hash");
			}
			if (signature == null || signature.Length == 0)
			{
				throw LogHelper.LogArgumentNullException("signature");
			}
			return this._rsa.VerifyData(input, hash, signature);
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x0004052C File Offset: 0x0003E72C
		public bool VerifyDataWithLength(byte[] input, int offset, int length, object hash, HashAlgorithmName hashAlgorithmName, byte[] signature)
		{
			if (input == null || input.Length == 0)
			{
				throw LogHelper.LogArgumentNullException("input");
			}
			if (hash == null)
			{
				throw LogHelper.LogArgumentNullException("hash");
			}
			if (signature == null || signature.Length == 0)
			{
				throw LogHelper.LogArgumentNullException("signature");
			}
			if (offset < 0)
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX10716: '{0}' must be greater than 0, was: '{1}'", new object[]
				{
					LogHelper.MarkAsNonPII("offset"),
					LogHelper.MarkAsNonPII(offset)
				})));
			}
			if (length < 1)
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX10655: '{0}' must be greater than 1, was: '{1}'", new object[]
				{
					LogHelper.MarkAsNonPII("length"),
					LogHelper.MarkAsNonPII(length)
				})));
			}
			if (offset + length > input.Length)
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX10717: '{0} + {1}' must not be greater than {2}, '{3} + {4} > {5}'.", new object[]
				{
					LogHelper.MarkAsNonPII("offset"),
					LogHelper.MarkAsNonPII("length"),
					LogHelper.MarkAsNonPII("input"),
					LogHelper.MarkAsNonPII(offset),
					LogHelper.MarkAsNonPII(length),
					LogHelper.MarkAsNonPII(input.Length)
				})));
			}
			return this._rsa.VerifyHash((hash as HashAlgorithm).ComputeHash(input, offset, length), signature, hashAlgorithmName, RSASignaturePadding.Pkcs1);
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x0004067B File Offset: 0x0003E87B
		public override RSAParameters ExportParameters(bool includePrivateParameters)
		{
			return this._rsa.ExportParameters(includePrivateParameters);
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x00040689 File Offset: 0x0003E889
		public override void ImportParameters(RSAParameters parameters)
		{
			this._rsa.ImportParameters(parameters);
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x00040697 File Offset: 0x0003E897
		protected override void Dispose(bool disposing)
		{
			if (!this._disposed)
			{
				this._disposed = true;
				if (disposing && this._disposeRsa)
				{
					this._rsa.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x04000617 RID: 1559
		private const int PROV_RSA_AES = 24;

		// Token: 0x04000618 RID: 1560
		private const int PROV_RSA_FULL = 1;

		// Token: 0x04000619 RID: 1561
		private const int PROV_RSA_SCHANNEL = 12;

		// Token: 0x0400061A RID: 1562
		private bool _disposed;

		// Token: 0x0400061B RID: 1563
		private bool _disposeRsa;

		// Token: 0x0400061C RID: 1564
		private RSACryptoServiceProvider _rsa;
	}
}
