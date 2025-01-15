using System;
using System.Globalization;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

namespace Microsoft.Cloud.Platform.MonitoredUtils
{
	// Token: 0x0200011E RID: 286
	public static class ObfuscationUtility
	{
		// Token: 0x060007A3 RID: 1955 RVA: 0x0001A692 File Offset: 0x00018892
		public static string SerializeByteArray(byte[] bytes)
		{
			return Convert.ToBase64String(bytes);
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0001A69A File Offset: 0x0001889A
		public static byte[] DeserializeByteArray(string serializedBytes)
		{
			return Convert.FromBase64String(serializedBytes);
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0001A6A2 File Offset: 0x000188A2
		public static byte[] GetBytesFromString(string data)
		{
			return Encoding.Unicode.GetBytes(data);
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x0001A6AF File Offset: 0x000188AF
		public static string GetStringFromBytes(byte[] data)
		{
			return Encoding.Unicode.GetString(data);
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x0001A6BC File Offset: 0x000188BC
		public static void CreateKeyFile(string certFilePath, int keyId, string outputFilePath)
		{
			if (string.IsNullOrWhiteSpace(certFilePath))
			{
				throw new ArgumentException("Creation of key file aborted.  The public RSA key .cer file path was not provided.");
			}
			if (string.IsNullOrWhiteSpace(outputFilePath))
			{
				throw new ArgumentException("Creation of key file aborted.  The output file path was not provided.");
			}
			if (!File.Exists(certFilePath))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The certificate file specified does not exist.  Certificate File: {0}", new object[] { certFilePath }));
			}
			using (AesCryptoServiceProvider aesCryptoServiceProvider = new AesCryptoServiceProvider())
			{
				X509Certificate2 x509Certificate;
				if (!ObfuscationUtility.TryRetrieveCertificateFromStore(certFilePath, out x509Certificate))
				{
					throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Unable to generate valid certificate from certificate file {0}", new object[] { certFilePath }));
				}
				using (RSAEncryption rsaencryption = new RSAEncryption((RSACryptoServiceProvider)x509Certificate.PublicKey.Key, keyId))
				{
					string text = rsaencryption.ComputePiiReplacement(aesCryptoServiceProvider.Key);
					new XDocument(new XDeclaration("1.0", "utf-8", "yes"), new object[]
					{
						new XElement("AesKeyInfo", new object[]
						{
							new XElement("KeyId", keyId),
							new XElement("Key", text)
						})
					}).Save(outputFilePath);
				}
			}
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x0001A804 File Offset: 0x00018A04
		public static void CreateKeyFileFromCertFile(string aesKeyElement, string certFilePath, int keyId, string outputKeyFilePath, string outputEncryptedKeyFilePath)
		{
			if (string.IsNullOrWhiteSpace(aesKeyElement))
			{
				throw new ArgumentException("Creation of key file aborted.  The AES key element path was not provided.");
			}
			if (string.IsNullOrWhiteSpace(certFilePath))
			{
				throw new ArgumentException("Creation of key file aborted.  The public RSA key .cer file path was not provided.");
			}
			if (string.IsNullOrWhiteSpace(outputKeyFilePath))
			{
				throw new ArgumentException("Creation of key file aborted.  The output file path was not provided.");
			}
			if (string.IsNullOrWhiteSpace(outputEncryptedKeyFilePath))
			{
				throw new ArgumentException("Creation of key file aborted.  The output file path was not provided.");
			}
			if (!File.Exists(certFilePath))
			{
				throw new ArgumentException(string.Format("The certificate file specified does not exist.  Certificate File: {0}", certFilePath));
			}
			using (AesCryptoServiceProvider aesCryptoServiceProvider = new AesCryptoServiceProvider())
			{
				using (RSAEncryption rsaencryption = new RSAEncryption((RSACryptoServiceProvider)new X509Certificate2(certFilePath).PublicKey.Key, keyId))
				{
					string text = Convert.ToBase64String(aesCryptoServiceProvider.Key);
					string text2 = rsaencryption.ComputePiiReplacement(aesCryptoServiceProvider.Key);
					new XDocument(new XDeclaration("1.0", "utf-8", "yes"), new object[]
					{
						new XElement("AesKeyInfo", new object[]
						{
							new XElement("KeyId", keyId),
							new XElement("Key", text2)
						})
					}).Save(outputEncryptedKeyFilePath);
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.XmlResolver = null;
					xmlDocument.LoadXml(string.Format("<Item><Key>{0}</Key><Value>{1}</Value></Item>", aesKeyElement, text));
					xmlDocument.Save(outputKeyFilePath);
				}
			}
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x0001A974 File Offset: 0x00018B74
		public static bool TruncateValueForEncryption(string value, out string truncatedValue)
		{
			truncatedValue = null;
			int num = 3315;
			bool flag = value.Length > num;
			if (flag)
			{
				truncatedValue = value.Substring(0, num);
			}
			return flag;
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x0001A9A0 File Offset: 0x00018BA0
		public static bool TryRetrieveCertificateFromStore(string certSerialNumber, out X509Certificate2 certificate)
		{
			bool flag = false;
			certificate = null;
			if (certSerialNumber != null && !string.IsNullOrEmpty(certSerialNumber))
			{
				flag = ObfuscationUtility.TryRetrieveCertificateFromStoreLocation(certSerialNumber, out certificate, StoreLocation.CurrentUser);
				if (!flag)
				{
					flag = ObfuscationUtility.TryRetrieveCertificateFromStoreLocation(certSerialNumber, out certificate, StoreLocation.LocalMachine);
				}
			}
			return flag;
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x0001A9D4 File Offset: 0x00018BD4
		public static bool TryRetrieveCertificateFromStoreLocation(string certSerialNumber, out X509Certificate2 certificate, StoreLocation storeLocation)
		{
			bool flag = false;
			bool flag2 = false;
			X509Store x509Store = null;
			certificate = null;
			try
			{
				x509Store = new X509Store(StoreName.My, storeLocation);
				x509Store.Open(OpenFlags.OpenExistingOnly);
				flag2 = true;
				X509Certificate2Collection x509Certificate2Collection = x509Store.Certificates.Find(X509FindType.FindBySerialNumber, certSerialNumber, false);
				if (x509Certificate2Collection != null && x509Certificate2Collection.Count > 0)
				{
					certificate = x509Certificate2Collection[0];
					flag = true;
				}
			}
			catch (CryptographicException)
			{
			}
			catch (SecurityException)
			{
			}
			finally
			{
				if (flag2)
				{
					x509Store.Close();
					flag2 = false;
				}
			}
			return flag;
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0001AA60 File Offset: 0x00018C60
		public static string ProtectDataValue(string dataValue, AesEncryption aesEncryptionProvider, HMACSHA256Hashing hashingprovider)
		{
			if (string.IsNullOrWhiteSpace(dataValue) || dataValue == "-")
			{
				return dataValue;
			}
			if (hashingprovider == null)
			{
				throw new NotSupportedException("Hashing obfuscator is missing while the replacement strategy is Hashing");
			}
			if (aesEncryptionProvider == null)
			{
				throw new NotSupportedException("Encryption obfuscator is missing while the replacement strategy is Encrypting");
			}
			string valueToHash = dataValue;
			StringBuilder stringBuilder = new StringBuilder("<PII");
			Func<string> func = delegate
			{
				if (string.IsNullOrEmpty(valueToHash))
				{
					valueToHash = dataValue.ToUpperInvariant();
				}
				return valueToHash;
			};
			stringBuilder.AppendFormat(":{0}{1}({2})", "H", hashingprovider.GetKeyIdentifier(), hashingprovider.ComputePiiReplacement(ObfuscationUtility.GetBytesFromString(func())));
			string dataValue2;
			if (!ObfuscationUtility.TruncateValueForEncryption(dataValue, out dataValue2))
			{
				dataValue2 = dataValue;
			}
			stringBuilder.AppendFormat(":{0}{1}({2})", "E", aesEncryptionProvider.GetKeyIdentifier(), aesEncryptionProvider.ComputePiiReplacement(ObfuscationUtility.GetBytesFromString(dataValue2)));
			stringBuilder.Append(">");
			return stringBuilder.ToString();
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x0001AB5C File Offset: 0x00018D5C
		public static string DecryptDataValue(string encryptedDataValue, AesEncryption aesEncryptionProvider, HMACSHA256Hashing hashingprovider)
		{
			if (string.IsNullOrWhiteSpace(encryptedDataValue) || encryptedDataValue == "-")
			{
				return encryptedDataValue;
			}
			if (hashingprovider == null)
			{
				throw new NotSupportedException("Hashing obfuscator is missing while the replacement strategy is Hashing");
			}
			if (aesEncryptionProvider == null)
			{
				throw new NotSupportedException("Encryption obfuscator is missing while the replacement strategy is Encrypting");
			}
			string text = string.Concat(new object[]
			{
				"\\<PII\\:H",
				hashingprovider.GetKeyIdentifier(),
				"\\(.*\\)\\:E",
				aesEncryptionProvider.GetKeyIdentifier(),
				"\\((.*)\\)\\>"
			});
			Match match = Regex.Match(encryptedDataValue, text);
			if (match.Success && !string.IsNullOrWhiteSpace(match.Groups[1].Value))
			{
				return aesEncryptionProvider.DecryptObfuscatedData(match.Groups[1].Value);
			}
			return encryptedDataValue;
		}
	}
}
