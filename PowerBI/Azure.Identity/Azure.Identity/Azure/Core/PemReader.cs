using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Azure.Core
{
	// Token: 0x02000018 RID: 24
	internal static class PemReader
	{
		// Token: 0x06000067 RID: 103 RVA: 0x000032F8 File Offset: 0x000014F8
		public static X509Certificate2 LoadCertificate(ReadOnlySpan<char> data, byte[] cer = null, PemReader.KeyType keyType = PemReader.KeyType.Auto, bool allowCertificateOnly = false, X509KeyStorageFlags keyStorageFlags = X509KeyStorageFlags.DefaultKeySet)
		{
			byte[] array = null;
			PemReader.PemField pemField;
			while (PemReader.TryRead(data, out pemField))
			{
				if (MemoryExtensions.Equals(pemField.Label, MemoryExtensions.AsSpan("CERTIFICATE"), StringComparison.Ordinal))
				{
					cer = pemField.FromBase64Data();
				}
				else if (MemoryExtensions.Equals(pemField.Label, MemoryExtensions.AsSpan("PRIVATE KEY"), StringComparison.Ordinal))
				{
					array = pemField.FromBase64Data();
				}
				int num = pemField.Start + pemField.Length;
				if (num >= data.Length)
				{
					break;
				}
				data = data.Slice(num);
			}
			if (cer == null)
			{
				throw new InvalidDataException("The certificate is missing the public key");
			}
			if (array == null)
			{
				if (allowCertificateOnly)
				{
					return new X509Certificate2(cer, null, keyStorageFlags);
				}
				throw new InvalidDataException("The certificate is missing the private key");
			}
			else
			{
				if (keyType == PemReader.KeyType.Auto)
				{
					string text = LightweightPkcs8Decoder.DecodePrivateKeyOid(array);
					PemReader.KeyType keyType2;
					if (!(text == "1.2.840.113549.1.1.1"))
					{
						if (!(text == "1.2.840.10045.2.1"))
						{
							throw new NotSupportedException("The private key algorithm ID " + text + " is not supported");
						}
						keyType2 = PemReader.KeyType.ECDsa;
					}
					else
					{
						keyType2 = PemReader.KeyType.RSA;
					}
					keyType = keyType2;
				}
				if (keyType != PemReader.KeyType.ECDsa)
				{
					return PemReader.CreateRsaCertificate(cer, array, keyStorageFlags);
				}
				object obj = null;
				if (obj == null)
				{
					throw new NotSupportedException("Reading an ECDsa certificate from a PEM file is not supported");
				}
				return obj;
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003410 File Offset: 0x00001610
		private static X509Certificate2 CreateRsaCertificate(byte[] cer, byte[] key, X509KeyStorageFlags keyStorageFlags)
		{
			if (!PemReader.s_rsaInitializedImportPkcs8PrivateKeyMethod)
			{
				PemReader.s_rsaImportPkcs8PrivateKeyMethod = typeof(RSA).GetMethod("ImportPkcs8PrivateKey", BindingFlags.Instance | BindingFlags.Public, null, new Type[]
				{
					typeof(ReadOnlySpan<byte>),
					typeof(int).MakeByRefType()
				}, null);
				PemReader.s_rsaInitializedImportPkcs8PrivateKeyMethod = true;
			}
			if (PemReader.s_rsaCopyWithPrivateKeyMethod == null)
			{
				MethodInfo method = typeof(RSACertificateExtensions).GetMethod("CopyWithPrivateKey", BindingFlags.Static | BindingFlags.Public, null, new Type[]
				{
					typeof(X509Certificate2),
					typeof(RSA)
				}, null);
				if (method == null)
				{
					throw new PlatformNotSupportedException("The current platform does not support reading a private key from a PEM file");
				}
				PemReader.s_rsaCopyWithPrivateKeyMethod = method;
			}
			RSA rsa = null;
			X509Certificate2 x509Certificate3;
			try
			{
				if (PemReader.s_rsaImportPkcs8PrivateKeyMethod != null)
				{
					rsa = RSA.Create();
					int num;
					((PemReader.ImportPrivateKeyDelegate)PemReader.s_rsaImportPkcs8PrivateKeyMethod.CreateDelegate(typeof(PemReader.ImportPrivateKeyDelegate), rsa))(key, out num);
					if (key.Length != num)
					{
						throw new InvalidDataException("Invalid PKCS#8 Data");
					}
				}
				else
				{
					rsa = LightweightPkcs8Decoder.DecodeRSAPkcs8(key);
				}
				using (X509Certificate2 x509Certificate = new X509Certificate2(cer, null, keyStorageFlags))
				{
					X509Certificate2 x509Certificate2 = (X509Certificate2)PemReader.s_rsaCopyWithPrivateKeyMethod.Invoke(null, new object[] { x509Certificate, rsa });
					if (x509Certificate2.PrivateKey == null)
					{
						x509Certificate2.PrivateKey = rsa;
					}
					rsa = null;
					x509Certificate3 = x509Certificate2;
				}
			}
			finally
			{
				if (rsa != null)
				{
					rsa.Dispose();
				}
			}
			return x509Certificate3;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003584 File Offset: 0x00001784
		public static bool TryRead(ReadOnlySpan<char> data, out PemReader.PemField field)
		{
			field = default(PemReader.PemField);
			int num = MemoryExtensions.IndexOf<char>(data, MemoryExtensions.AsSpan("-----BEGIN "));
			if (num < 0)
			{
				return false;
			}
			ReadOnlySpan<char> readOnlySpan = data.Slice(num + "-----BEGIN ".Length);
			int num2 = MemoryExtensions.IndexOf<char>(readOnlySpan, MemoryExtensions.AsSpan("-----"));
			if (num2 < 0)
			{
				return false;
			}
			readOnlySpan = readOnlySpan.Slice(0, num2);
			int num3 = num + "-----BEGIN ".Length + num2 + "-----".Length;
			data = data.Slice(num3);
			string text = "-----END " + readOnlySpan.ToString() + "-----";
			num2 = MemoryExtensions.IndexOf<char>(data, MemoryExtensions.AsSpan(text));
			if (num2 < 0)
			{
				return false;
			}
			int num4 = num3 + num2 + text.Length - num;
			field = new PemReader.PemField(num, readOnlySpan, data.Slice(0, num2), num4);
			return true;
		}

		// Token: 0x0400003B RID: 59
		private const string Prolog = "-----BEGIN ";

		// Token: 0x0400003C RID: 60
		private const string Epilog = "-----END ";

		// Token: 0x0400003D RID: 61
		private const string LabelEnd = "-----";

		// Token: 0x0400003E RID: 62
		private const string RSAAlgorithmId = "1.2.840.113549.1.1.1";

		// Token: 0x0400003F RID: 63
		private const string ECDsaAlgorithmId = "1.2.840.10045.2.1";

		// Token: 0x04000040 RID: 64
		private static bool s_rsaInitializedImportPkcs8PrivateKeyMethod;

		// Token: 0x04000041 RID: 65
		private static MethodInfo s_rsaImportPkcs8PrivateKeyMethod;

		// Token: 0x04000042 RID: 66
		private static MethodInfo s_rsaCopyWithPrivateKeyMethod;

		// Token: 0x02000096 RID: 150
		// (Invoke) Token: 0x060004B0 RID: 1200
		private delegate void ImportPrivateKeyDelegate(ReadOnlySpan<byte> blob, out int bytesRead);

		// Token: 0x02000097 RID: 151
		public enum KeyType
		{
			// Token: 0x040002B8 RID: 696
			Unknown = -1,
			// Token: 0x040002B9 RID: 697
			Auto,
			// Token: 0x040002BA RID: 698
			RSA,
			// Token: 0x040002BB RID: 699
			ECDsa
		}

		// Token: 0x02000098 RID: 152
		public ref struct PemField
		{
			// Token: 0x060004B3 RID: 1203 RVA: 0x0000EC3E File Offset: 0x0000CE3E
			internal PemField(int start, ReadOnlySpan<char> label, ReadOnlySpan<char> data, int length)
			{
				this.Start = start;
				this.Label = label;
				this.Data = data;
				this.Length = length;
			}

			// Token: 0x17000147 RID: 327
			// (get) Token: 0x060004B4 RID: 1204 RVA: 0x0000EC5D File Offset: 0x0000CE5D
			public readonly int Start { get; }

			// Token: 0x17000148 RID: 328
			// (get) Token: 0x060004B5 RID: 1205 RVA: 0x0000EC65 File Offset: 0x0000CE65
			public readonly ReadOnlySpan<char> Label { get; }

			// Token: 0x17000149 RID: 329
			// (get) Token: 0x060004B6 RID: 1206 RVA: 0x0000EC6D File Offset: 0x0000CE6D
			public readonly ReadOnlySpan<char> Data { get; }

			// Token: 0x1700014A RID: 330
			// (get) Token: 0x060004B7 RID: 1207 RVA: 0x0000EC75 File Offset: 0x0000CE75
			public readonly int Length { get; }

			// Token: 0x060004B8 RID: 1208 RVA: 0x0000EC80 File Offset: 0x0000CE80
			public byte[] FromBase64Data()
			{
				return Convert.FromBase64String(this.Data.ToString());
			}
		}
	}
}
