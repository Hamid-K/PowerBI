using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Security.Cryptography;

namespace Microsoft.Mashup.Engine1.Library.Crypto
{
	// Token: 0x02000D7B RID: 3451
	internal sealed class CryptoModule : Module
	{
		// Token: 0x17001BB5 RID: 7093
		// (get) Token: 0x06005E13 RID: 24083 RVA: 0x00144B5F File Offset: 0x00142D5F
		public override string Name
		{
			get
			{
				return "Crypto";
			}
		}

		// Token: 0x17001BB6 RID: 7094
		// (get) Token: 0x06005E14 RID: 24084 RVA: 0x00144B66 File Offset: 0x00142D66
		public override Keys ExportKeys
		{
			get
			{
				if (this.exportKeys == null)
				{
					this.exportKeys = Keys.New(9, delegate(int index)
					{
						switch (index)
						{
						case 0:
							return CryptoModule.CryptoAlgorithm.Type.GetName();
						case 1:
							return CryptoModule.CryptoAlgorithm.SHA1.GetName();
						case 2:
							return CryptoModule.CryptoAlgorithm.SHA256.GetName();
						case 3:
							return "Crypto.CreateHmac";
						case 4:
							return "Crypto.CreateHash";
						case 5:
							return "Web.SignForOAuth1";
						case 6:
							return CryptoModule.OAuth1.Type.GetName();
						case 7:
							return CryptoModule.OAuth1.HMACSHA1.GetName();
						case 8:
							return CryptoModule.OAuth1.RSASHA1.GetName();
						default:
							throw new InvalidOperationException();
						}
					});
				}
				return this.exportKeys;
			}
		}

		// Token: 0x06005E15 RID: 24085 RVA: 0x00144BA4 File Offset: 0x00142DA4
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			if (this.exports == null)
			{
				this.exports = RecordValue.New(this.ExportKeys, delegate(int index)
				{
					switch (index)
					{
					case 0:
						return CryptoModule.CryptoAlgorithm.Type;
					case 1:
						return CryptoModule.CryptoAlgorithm.SHA1;
					case 2:
						return CryptoModule.CryptoAlgorithm.SHA256;
					case 3:
						return CryptoModule.Crypto.CreateHmac;
					case 4:
						return CryptoModule.Crypto.CreateHash;
					case 5:
						return new CryptoModule.Web.SignForOAuth1FunctionValue(hostEnvironment);
					case 6:
						return CryptoModule.OAuth1.Type;
					case 7:
						return CryptoModule.OAuth1.HMACSHA1;
					case 8:
						return CryptoModule.OAuth1.RSASHA1;
					default:
						throw new InvalidOperationException();
					}
				});
			}
			return this.exports;
		}

		// Token: 0x06005E16 RID: 24086 RVA: 0x00144BEC File Offset: 0x00142DEC
		public static X509Certificate2 CertificateFromThumbprint(IEngineHost hostEnvironment, TextValue thumbprint)
		{
			X509Certificate2Collection x509Certificate2Collection;
			if ((!CryptoModule.TryGetCertificate(StoreName.My, StoreLocation.CurrentUser, thumbprint.String, out x509Certificate2Collection) && !CryptoModule.TryGetCertificate(StoreName.My, StoreLocation.LocalMachine, thumbprint.String, out x509Certificate2Collection)) || x509Certificate2Collection.Count != 1)
			{
				throw ValueException.NewExpressionError<Message1>(Strings.Certificate_NoSingleThumbprint((x509Certificate2Collection != null) ? x509Certificate2Collection.Count : 0), thumbprint, null);
			}
			X509Certificate2 x509Certificate = x509Certificate2Collection[0];
			if (hostEnvironment.GetConfigurationProperty("CertificateValidationEnabled", true) && !CryptoModule.IsCertificateChainValid(x509Certificate))
			{
				throw ValueException.NewExpressionError<Message0>(Strings.Certificate_VerificationFailed, thumbprint, null);
			}
			return x509Certificate;
		}

		// Token: 0x06005E17 RID: 24087 RVA: 0x00144C70 File Offset: 0x00142E70
		private static bool TryGetCertificate(StoreName storeName, StoreLocation storeLocation, string thumbprint, out X509Certificate2Collection certificates)
		{
			try
			{
				X509Store x509Store = new X509Store(storeName, storeLocation);
				x509Store.Open(OpenFlags.ReadOnly);
				X509Certificate2Collection x509Certificate2Collection = x509Store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, false);
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

		// Token: 0x06005E18 RID: 24088 RVA: 0x00144CCC File Offset: 0x00142ECC
		private static TextValue AsTextSafe(Value value)
		{
			CryptoModule.CheckType(value, ValueKind.Text);
			return value.AsText;
		}

		// Token: 0x06005E19 RID: 24089 RVA: 0x00144CDB File Offset: 0x00142EDB
		private static BinaryValue AsBinarySafe(Value value)
		{
			CryptoModule.CheckType(value, ValueKind.Binary);
			return value.AsBinary;
		}

		// Token: 0x06005E1A RID: 24090 RVA: 0x00144CEC File Offset: 0x00142EEC
		private static void CheckType(Value value, ValueKind kind)
		{
			if (value.Kind != kind)
			{
				throw ValueException.NewExpressionError<Message2>(Strings.ValueException_CastTypeMismatch_Complex(Enum.GetName(typeof(ValueKind), value.Kind), Enum.GetName(typeof(ValueKind), kind)), null, null);
			}
		}

		// Token: 0x06005E1B RID: 24091 RVA: 0x00144D40 File Offset: 0x00142F40
		private static bool IsCertificateChainValid(X509Certificate2 certificate)
		{
			X509Chain x509Chain = X509Chain.Create();
			x509Chain.ChainPolicy.RevocationFlag = X509RevocationFlag.ExcludeRoot;
			x509Chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;
			x509Chain.ChainPolicy.UrlRetrievalTimeout = new TimeSpan(0, 0, 10);
			x509Chain.ChainPolicy.VerificationTime = DateTime.Now;
			x509Chain.ChainPolicy.VerificationFlags = X509VerificationFlags.IgnoreEndRevocationUnknown | X509VerificationFlags.IgnoreCtlSignerRevocationUnknown | X509VerificationFlags.IgnoreCertificateAuthorityRevocationUnknown;
			return x509Chain.Build(certificate);
		}

		// Token: 0x04003386 RID: 13190
		private static readonly TextValue CertificateTypeX509 = TextValue.New("X509.2");

		// Token: 0x04003387 RID: 13191
		private Keys exportKeys;

		// Token: 0x04003388 RID: 13192
		private RecordValue exports;

		// Token: 0x02000D7C RID: 3452
		private enum Exports
		{
			// Token: 0x0400338A RID: 13194
			CryptoAlgorithm_Type,
			// Token: 0x0400338B RID: 13195
			CryptoAlgorithm_SHA1,
			// Token: 0x0400338C RID: 13196
			CryptoAlgorithm_SHA256,
			// Token: 0x0400338D RID: 13197
			Crypto_CreateHmac,
			// Token: 0x0400338E RID: 13198
			Crypto_CreateHash,
			// Token: 0x0400338F RID: 13199
			Web_SignForOAuth1,
			// Token: 0x04003390 RID: 13200
			OAuth1_Type,
			// Token: 0x04003391 RID: 13201
			OAuth1_HMACSHA1,
			// Token: 0x04003392 RID: 13202
			OAuth1_RSASHA1,
			// Token: 0x04003393 RID: 13203
			Count
		}

		// Token: 0x02000D7D RID: 3453
		private static class Crypto
		{
			// Token: 0x04003394 RID: 13204
			public static readonly FunctionValue CreateHmac = new CryptoModule.Crypto.CreateHmacFunctionValue();

			// Token: 0x04003395 RID: 13205
			public static readonly FunctionValue CreateHash = new CryptoModule.Crypto.CreateHashFunctionValue();

			// Token: 0x02000D7E RID: 3454
			private class CreateHmacFunctionValue : NativeFunctionValue3<BinaryValue, NumberValue, Value, BinaryValue>
			{
				// Token: 0x06005E1F RID: 24095 RVA: 0x00144DCC File Offset: 0x00142FCC
				public CreateHmacFunctionValue()
					: base(TypeValue.Binary, 3, "algorithm", CryptoModule.CryptoAlgorithm.Type, "password", TypeValue.Any, "value", TypeValue.Binary)
				{
				}

				// Token: 0x06005E20 RID: 24096 RVA: 0x00144E04 File Offset: 0x00143004
				public override BinaryValue TypedInvoke(NumberValue algorithm, Value password, BinaryValue value)
				{
					byte[] asBytes = CryptoModule.AsBinarySafe(password).AsBytes;
					CryptoModule.CryptoAlgorithms value2 = CryptoModule.CryptoAlgorithm.Type.GetValue(algorithm);
					HMAC hmac;
					if (value2 != CryptoModule.CryptoAlgorithms.SHA1)
					{
						if (value2 != CryptoModule.CryptoAlgorithms.SHA256)
						{
							throw ValueException.NewExpressionError<Message0>(Strings.Encryption_InvalidAlgorithm, algorithm, null);
						}
						hmac = new HMACSHA256Cng(asBytes);
					}
					else
					{
						hmac = new HMACSHA1Cng(asBytes);
					}
					return BinaryValue.New(hmac.ComputeHash(value.AsBytes));
				}
			}

			// Token: 0x02000D7F RID: 3455
			private class CreateHashFunctionValue : NativeFunctionValue2<BinaryValue, NumberValue, BinaryValue>
			{
				// Token: 0x06005E21 RID: 24097 RVA: 0x00144E62 File Offset: 0x00143062
				public CreateHashFunctionValue()
					: base(TypeValue.Binary, 2, "algorithm", CryptoModule.CryptoAlgorithm.Type, "value", TypeValue.Binary)
				{
				}

				// Token: 0x06005E22 RID: 24098 RVA: 0x00144E84 File Offset: 0x00143084
				public override BinaryValue TypedInvoke(NumberValue algorithm, BinaryValue value)
				{
					CryptoModule.CryptoAlgorithms value2 = CryptoModule.CryptoAlgorithm.Type.GetValue(algorithm);
					if (value2 == CryptoModule.CryptoAlgorithms.SHA1)
					{
						throw ValueException.NewExpressionError<Message0>(Strings.Encryption_InvalidAlgorithm, algorithm, null);
					}
					if (value2 != CryptoModule.CryptoAlgorithms.SHA256)
					{
						throw ValueException.NewExpressionError<Message0>(Strings.Encryption_InvalidAlgorithm, algorithm, null);
					}
					HashAlgorithm hashAlgorithm = SHA256.Create();
					return BinaryValue.New(hashAlgorithm.ComputeHash(value.AsBytes));
				}
			}
		}

		// Token: 0x02000D80 RID: 3456
		private enum CryptoAlgorithms
		{
			// Token: 0x04003397 RID: 13207
			SHA1,
			// Token: 0x04003398 RID: 13208
			SHA256
		}

		// Token: 0x02000D81 RID: 3457
		private static class CryptoAlgorithm
		{
			// Token: 0x04003399 RID: 13209
			public static readonly IntEnumTypeValue<CryptoModule.CryptoAlgorithms> Type = new IntEnumTypeValue<CryptoModule.CryptoAlgorithms>("CryptoAlgorithm.Type");

			// Token: 0x0400339A RID: 13210
			public static readonly NumberValue SHA1 = CryptoModule.CryptoAlgorithm.Type.NewEnumValue("CryptoAlgorithm.SHA1", 0, CryptoModule.CryptoAlgorithms.SHA1, null);

			// Token: 0x0400339B RID: 13211
			public static readonly NumberValue SHA256 = CryptoModule.CryptoAlgorithm.Type.NewEnumValue("CryptoAlgorithm.SHA256", 1, CryptoModule.CryptoAlgorithms.SHA256, null);
		}

		// Token: 0x02000D82 RID: 3458
		public static class Web
		{
			// Token: 0x02000D83 RID: 3459
			public class SignForOAuth1FunctionValue : NativeFunctionValue3<BinaryValue, TextValue, BinaryValue, Value>
			{
				// Token: 0x06005E24 RID: 24100 RVA: 0x00144F18 File Offset: 0x00143118
				public SignForOAuth1FunctionValue(IEngineHost engineHost)
					: base(TypeValue.Binary, "format", TypeValue.Text, "data", TypeValue.Binary, "secret", TypeValue.Any)
				{
					this.engineHost = engineHost;
				}

				// Token: 0x06005E25 RID: 24101 RVA: 0x00144F4C File Offset: 0x0014314C
				public override BinaryValue TypedInvoke(TextValue format, BinaryValue data, Value secret)
				{
					string @string = format.String;
					if (@string == "HMAC-SHA1")
					{
						return CryptoModule.Web.SignForOAuth1FunctionValue.HmacSha1(data, CryptoModule.AsBinarySafe(secret));
					}
					if (!(@string == "RSA-SHA1"))
					{
						throw ValueException.NewExpressionError<Message0>(Strings.OAuth1_UnsupportedType, format, null);
					}
					return CryptoModule.Web.SignForOAuth1FunctionValue.RsaSha1(this.engineHost, data, CryptoModule.AsTextSafe(secret));
				}

				// Token: 0x06005E26 RID: 24102 RVA: 0x00144FA8 File Offset: 0x001431A8
				private static BinaryValue HmacSha1(BinaryValue data, BinaryValue secret)
				{
					return CryptoModule.Crypto.CreateHmac.Invoke(CryptoModule.CryptoAlgorithm.SHA1, secret, data).AsBinary;
				}

				// Token: 0x06005E27 RID: 24103 RVA: 0x00144FC0 File Offset: 0x001431C0
				private static BinaryValue RsaSha1(IEngineHost hostEnvironment, BinaryValue data, TextValue thumbprint)
				{
					X509Certificate2 x509Certificate = CryptoModule.CertificateFromThumbprint(hostEnvironment, thumbprint);
					if (!x509Certificate.HasPrivateKey)
					{
						throw ValueException.NewExpressionError<Message0>(Strings.Certificate_NoPrivateKey, thumbprint, null);
					}
					RSAPKCS1SignatureFormatter rsapkcs1SignatureFormatter = new RSAPKCS1SignatureFormatter(x509Certificate.PrivateKey);
					rsapkcs1SignatureFormatter.SetHashAlgorithm("SHA1");
					HashAlgorithm hashAlgorithm = SHA1.Create();
					return BinaryValue.New(rsapkcs1SignatureFormatter.CreateSignature(hashAlgorithm.ComputeHash(data.AsBytes)));
				}

				// Token: 0x0400339C RID: 13212
				private IEngineHost engineHost;
			}
		}

		// Token: 0x02000D84 RID: 3460
		private static class OAuth1
		{
			// Token: 0x0400339D RID: 13213
			public const string HMACSHA1String = "HMAC-SHA1";

			// Token: 0x0400339E RID: 13214
			public const string RSASHA1String = "RSA-SHA1";

			// Token: 0x0400339F RID: 13215
			public static readonly TextEnumTypeValue Type = new TextEnumTypeValue("OAuth1.Type");

			// Token: 0x040033A0 RID: 13216
			public static readonly TextValue HMACSHA1 = CryptoModule.OAuth1.Type.NewEnumValue("OAuth1.HMACSHA1", "HMAC-SHA1", null);

			// Token: 0x040033A1 RID: 13217
			public static readonly TextValue RSASHA1 = CryptoModule.OAuth1.Type.NewEnumValue("OAuth1.RSASHA1", "RSA-SHA1", null);
		}
	}
}
