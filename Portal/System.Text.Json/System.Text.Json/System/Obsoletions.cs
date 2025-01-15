using System;

namespace System
{
	// Token: 0x0200000D RID: 13
	internal static class Obsoletions
	{
		// Token: 0x04000005 RID: 5
		internal const string SharedUrlFormat = "https://aka.ms/dotnet-warnings/{0}";

		// Token: 0x04000006 RID: 6
		internal const string SystemTextEncodingUTF7Message = "The UTF-7 encoding is insecure and should not be used. Consider using UTF-8 instead.";

		// Token: 0x04000007 RID: 7
		internal const string SystemTextEncodingUTF7DiagId = "SYSLIB0001";

		// Token: 0x04000008 RID: 8
		internal const string PrincipalPermissionAttributeMessage = "PrincipalPermissionAttribute is not honored by the runtime and must not be used.";

		// Token: 0x04000009 RID: 9
		internal const string PrincipalPermissionAttributeDiagId = "SYSLIB0002";

		// Token: 0x0400000A RID: 10
		internal const string CodeAccessSecurityMessage = "Code Access Security is not supported or honored by the runtime.";

		// Token: 0x0400000B RID: 11
		internal const string CodeAccessSecurityDiagId = "SYSLIB0003";

		// Token: 0x0400000C RID: 12
		internal const string ConstrainedExecutionRegionMessage = "The Constrained Execution Region (CER) feature is not supported.";

		// Token: 0x0400000D RID: 13
		internal const string ConstrainedExecutionRegionDiagId = "SYSLIB0004";

		// Token: 0x0400000E RID: 14
		internal const string GlobalAssemblyCacheMessage = "The Global Assembly Cache is not supported.";

		// Token: 0x0400000F RID: 15
		internal const string GlobalAssemblyCacheDiagId = "SYSLIB0005";

		// Token: 0x04000010 RID: 16
		internal const string ThreadAbortMessage = "Thread.Abort is not supported and throws PlatformNotSupportedException.";

		// Token: 0x04000011 RID: 17
		internal const string ThreadResetAbortMessage = "Thread.ResetAbort is not supported and throws PlatformNotSupportedException.";

		// Token: 0x04000012 RID: 18
		internal const string ThreadAbortDiagId = "SYSLIB0006";

		// Token: 0x04000013 RID: 19
		internal const string DefaultCryptoAlgorithmsMessage = "The default implementation of this cryptography algorithm is not supported.";

		// Token: 0x04000014 RID: 20
		internal const string DefaultCryptoAlgorithmsDiagId = "SYSLIB0007";

		// Token: 0x04000015 RID: 21
		internal const string CreatePdbGeneratorMessage = "The CreatePdbGenerator API is not supported and throws PlatformNotSupportedException.";

		// Token: 0x04000016 RID: 22
		internal const string CreatePdbGeneratorDiagId = "SYSLIB0008";

		// Token: 0x04000017 RID: 23
		internal const string AuthenticationManagerMessage = "The AuthenticationManager Authenticate and PreAuthenticate methods are not supported and throw PlatformNotSupportedException.";

		// Token: 0x04000018 RID: 24
		internal const string AuthenticationManagerDiagId = "SYSLIB0009";

		// Token: 0x04000019 RID: 25
		internal const string RemotingApisMessage = "This Remoting API is not supported and throws PlatformNotSupportedException.";

		// Token: 0x0400001A RID: 26
		internal const string RemotingApisDiagId = "SYSLIB0010";

		// Token: 0x0400001B RID: 27
		internal const string BinaryFormatterMessage = "BinaryFormatter serialization is obsolete and should not be used. See https://aka.ms/binaryformatter for more information.";

		// Token: 0x0400001C RID: 28
		internal const string BinaryFormatterDiagId = "SYSLIB0011";

		// Token: 0x0400001D RID: 29
		internal const string CodeBaseMessage = "Assembly.CodeBase and Assembly.EscapedCodeBase are only included for .NET Framework compatibility. Use Assembly.Location instead.";

		// Token: 0x0400001E RID: 30
		internal const string CodeBaseDiagId = "SYSLIB0012";

		// Token: 0x0400001F RID: 31
		internal const string EscapeUriStringMessage = "Uri.EscapeUriString can corrupt the Uri string in some cases. Consider using Uri.EscapeDataString for query string components instead.";

		// Token: 0x04000020 RID: 32
		internal const string EscapeUriStringDiagId = "SYSLIB0013";

		// Token: 0x04000021 RID: 33
		internal const string WebRequestMessage = "WebRequest, HttpWebRequest, ServicePoint, and WebClient are obsolete. Use HttpClient instead.";

		// Token: 0x04000022 RID: 34
		internal const string WebRequestDiagId = "SYSLIB0014";

		// Token: 0x04000023 RID: 35
		internal const string DisablePrivateReflectionAttributeMessage = "DisablePrivateReflectionAttribute has no effect in .NET 6.0+.";

		// Token: 0x04000024 RID: 36
		internal const string DisablePrivateReflectionAttributeDiagId = "SYSLIB0015";

		// Token: 0x04000025 RID: 37
		internal const string GetContextInfoMessage = "Use the Graphics.GetContextInfo overloads that accept arguments for better performance and fewer allocations.";

		// Token: 0x04000026 RID: 38
		internal const string GetContextInfoDiagId = "SYSLIB0016";

		// Token: 0x04000027 RID: 39
		internal const string StrongNameKeyPairMessage = "Strong name signing is not supported and throws PlatformNotSupportedException.";

		// Token: 0x04000028 RID: 40
		internal const string StrongNameKeyPairDiagId = "SYSLIB0017";

		// Token: 0x04000029 RID: 41
		internal const string ReflectionOnlyLoadingMessage = "ReflectionOnly loading is not supported and throws PlatformNotSupportedException.";

		// Token: 0x0400002A RID: 42
		internal const string ReflectionOnlyLoadingDiagId = "SYSLIB0018";

		// Token: 0x0400002B RID: 43
		internal const string RuntimeEnvironmentMessage = "RuntimeEnvironment members SystemConfigurationFile, GetRuntimeInterfaceAsIntPtr, and GetRuntimeInterfaceAsObject are not supported and throw PlatformNotSupportedException.";

		// Token: 0x0400002C RID: 44
		internal const string RuntimeEnvironmentDiagId = "SYSLIB0019";

		// Token: 0x0400002D RID: 45
		internal const string JsonSerializerOptionsIgnoreNullValuesMessage = "JsonSerializerOptions.IgnoreNullValues is obsolete. To ignore null values when serializing, set DefaultIgnoreCondition to JsonIgnoreCondition.WhenWritingNull.";

		// Token: 0x0400002E RID: 46
		internal const string JsonSerializerOptionsIgnoreNullValuesDiagId = "SYSLIB0020";

		// Token: 0x0400002F RID: 47
		internal const string DerivedCryptographicTypesMessage = "Derived cryptographic types are obsolete. Use the Create method on the base type instead.";

		// Token: 0x04000030 RID: 48
		internal const string DerivedCryptographicTypesDiagId = "SYSLIB0021";

		// Token: 0x04000031 RID: 49
		internal const string RijndaelMessage = "The Rijndael and RijndaelManaged types are obsolete. Use Aes instead.";

		// Token: 0x04000032 RID: 50
		internal const string RijndaelDiagId = "SYSLIB0022";

		// Token: 0x04000033 RID: 51
		internal const string RNGCryptoServiceProviderMessage = "RNGCryptoServiceProvider is obsolete. To generate a random number, use one of the RandomNumberGenerator static methods instead.";

		// Token: 0x04000034 RID: 52
		internal const string RNGCryptoServiceProviderDiagId = "SYSLIB0023";

		// Token: 0x04000035 RID: 53
		internal const string AppDomainCreateUnloadMessage = "Creating and unloading AppDomains is not supported and throws an exception.";

		// Token: 0x04000036 RID: 54
		internal const string AppDomainCreateUnloadDiagId = "SYSLIB0024";

		// Token: 0x04000037 RID: 55
		internal const string SuppressIldasmAttributeMessage = "SuppressIldasmAttribute has no effect in .NET 6.0+.";

		// Token: 0x04000038 RID: 56
		internal const string SuppressIldasmAttributeDiagId = "SYSLIB0025";

		// Token: 0x04000039 RID: 57
		internal const string X509CertificateImmutableMessage = "X509Certificate and X509Certificate2 are immutable. Use the appropriate constructor to create a new certificate.";

		// Token: 0x0400003A RID: 58
		internal const string X509CertificateImmutableDiagId = "SYSLIB0026";

		// Token: 0x0400003B RID: 59
		internal const string PublicKeyPropertyMessage = "PublicKey.Key is obsolete. Use the appropriate method to get the public key, such as GetRSAPublicKey.";

		// Token: 0x0400003C RID: 60
		internal const string PublicKeyPropertyDiagId = "SYSLIB0027";

		// Token: 0x0400003D RID: 61
		internal const string X509CertificatePrivateKeyMessage = "X509Certificate2.PrivateKey is obsolete. Use the appropriate method to get the private key, such as GetRSAPrivateKey, or use the CopyWithPrivateKey method to create a new instance with a private key.";

		// Token: 0x0400003E RID: 62
		internal const string X509CertificatePrivateKeyDiagId = "SYSLIB0028";

		// Token: 0x0400003F RID: 63
		internal const string ProduceLegacyHmacValuesMessage = "ProduceLegacyHmacValues is obsolete. Producing legacy HMAC values is not supported.";

		// Token: 0x04000040 RID: 64
		internal const string ProduceLegacyHmacValuesDiagId = "SYSLIB0029";

		// Token: 0x04000041 RID: 65
		internal const string UseManagedSha1Message = "HMACSHA1 always uses the algorithm implementation provided by the platform. Use a constructor without the useManagedSha1 parameter.";

		// Token: 0x04000042 RID: 66
		internal const string UseManagedSha1DiagId = "SYSLIB0030";

		// Token: 0x04000043 RID: 67
		internal const string CryptoConfigEncodeOIDMessage = "EncodeOID is obsolete. Use the ASN.1 functionality provided in System.Formats.Asn1.";

		// Token: 0x04000044 RID: 68
		internal const string CryptoConfigEncodeOIDDiagId = "SYSLIB0031";

		// Token: 0x04000045 RID: 69
		internal const string CorruptedStateRecoveryMessage = "Recovery from corrupted process state exceptions is not supported; HandleProcessCorruptedStateExceptionsAttribute is ignored.";

		// Token: 0x04000046 RID: 70
		internal const string CorruptedStateRecoveryDiagId = "SYSLIB0032";

		// Token: 0x04000047 RID: 71
		internal const string Rfc2898CryptDeriveKeyMessage = "Rfc2898DeriveBytes.CryptDeriveKey is obsolete and is not supported. Use PasswordDeriveBytes.CryptDeriveKey instead.";

		// Token: 0x04000048 RID: 72
		internal const string Rfc2898CryptDeriveKeyDiagId = "SYSLIB0033";

		// Token: 0x04000049 RID: 73
		internal const string CmsSignerCspParamsCtorMessage = "CmsSigner(CspParameters) is obsolete and is not supported. Use an alternative constructor instead.";

		// Token: 0x0400004A RID: 74
		internal const string CmsSignerCspParamsCtorDiagId = "SYSLIB0034";

		// Token: 0x0400004B RID: 75
		internal const string SignerInfoCounterSigMessage = "ComputeCounterSignature without specifying a CmsSigner is obsolete and is not supported. Use the overload that accepts a CmsSigner.";

		// Token: 0x0400004C RID: 76
		internal const string SignerInfoCounterSigDiagId = "SYSLIB0035";

		// Token: 0x0400004D RID: 77
		internal const string RegexCompileToAssemblyMessage = "Regex.CompileToAssembly is obsolete and not supported. Use the GeneratedRegexAttribute with the regular expression source generator instead.";

		// Token: 0x0400004E RID: 78
		internal const string RegexCompileToAssemblyDiagId = "SYSLIB0036";

		// Token: 0x0400004F RID: 79
		internal const string AssemblyNameMembersMessage = "AssemblyName members HashAlgorithm, ProcessorArchitecture, and VersionCompatibility are obsolete and not supported.";

		// Token: 0x04000050 RID: 80
		internal const string AssemblyNameMembersDiagId = "SYSLIB0037";

		// Token: 0x04000051 RID: 81
		internal const string SystemDataSerializationFormatBinaryMessage = "SerializationFormat.Binary is obsolete and should not be used. See https://aka.ms/serializationformat-binary-obsolete for more information.";

		// Token: 0x04000052 RID: 82
		internal const string SystemDataSerializationFormatBinaryDiagId = "SYSLIB0038";

		// Token: 0x04000053 RID: 83
		internal const string TlsVersion10and11Message = "TLS versions 1.0 and 1.1 have known vulnerabilities and are not recommended. Use a newer TLS version instead, or use SslProtocols.None to defer to OS defaults.";

		// Token: 0x04000054 RID: 84
		internal const string TlsVersion10and11DiagId = "SYSLIB0039";

		// Token: 0x04000055 RID: 85
		internal const string EncryptionPolicyMessage = "EncryptionPolicy.NoEncryption and AllowEncryption significantly reduce security and should not be used in production code.";

		// Token: 0x04000056 RID: 86
		internal const string EncryptionPolicyDiagId = "SYSLIB0040";

		// Token: 0x04000057 RID: 87
		internal const string Rfc2898OutdatedCtorMessage = "The default hash algorithm and iteration counts in Rfc2898DeriveBytes constructors are outdated and insecure. Use a constructor that accepts the hash algorithm and the number of iterations.";

		// Token: 0x04000058 RID: 88
		internal const string Rfc2898OutdatedCtorDiagId = "SYSLIB0041";

		// Token: 0x04000059 RID: 89
		internal const string EccXmlExportImportMessage = "ToXmlString and FromXmlString have no implementation for ECC types, and are obsolete. Use a standard import and export format such as ExportSubjectPublicKeyInfo or ImportSubjectPublicKeyInfo for public keys and ExportPkcs8PrivateKey or ImportPkcs8PrivateKey for private keys.";

		// Token: 0x0400005A RID: 90
		internal const string EccXmlExportImportDiagId = "SYSLIB0042";

		// Token: 0x0400005B RID: 91
		internal const string EcDhPublicKeyBlobMessage = "ECDiffieHellmanPublicKey.ToByteArray() and the associated constructor do not have a consistent and interoperable implementation on all platforms. Use ECDiffieHellmanPublicKey.ExportSubjectPublicKeyInfo() instead.";

		// Token: 0x0400005C RID: 92
		internal const string EcDhPublicKeyBlobDiagId = "SYSLIB0043";

		// Token: 0x0400005D RID: 93
		internal const string AssemblyNameCodeBaseMessage = "AssemblyName.CodeBase and AssemblyName.EscapedCodeBase are obsolete. Using them for loading an assembly is not supported.";

		// Token: 0x0400005E RID: 94
		internal const string AssemblyNameCodeBaseDiagId = "SYSLIB0044";

		// Token: 0x0400005F RID: 95
		internal const string CryptoStringFactoryMessage = "Cryptographic factory methods accepting an algorithm name are obsolete. Use the parameterless Create factory method on the algorithm type instead.";

		// Token: 0x04000060 RID: 96
		internal const string CryptoStringFactoryDiagId = "SYSLIB0045";

		// Token: 0x04000061 RID: 97
		internal const string ControlledExecutionRunMessage = "ControlledExecution.Run method may corrupt the process and should not be used in production code.";

		// Token: 0x04000062 RID: 98
		internal const string ControlledExecutionRunDiagId = "SYSLIB0046";

		// Token: 0x04000063 RID: 99
		internal const string XmlSecureResolverMessage = "XmlSecureResolver is obsolete. Use XmlResolver.ThrowingResolver instead when attempting to forbid XML external entity resolution.";

		// Token: 0x04000064 RID: 100
		internal const string XmlSecureResolverDiagId = "SYSLIB0047";

		// Token: 0x04000065 RID: 101
		internal const string RsaEncryptDecryptValueMessage = "RSA.EncryptValue and DecryptValue are not supported and throw NotSupportedException. Use RSA.Encrypt and RSA.Decrypt instead.";

		// Token: 0x04000066 RID: 102
		internal const string RsaEncryptDecryptDiagId = "SYSLIB0048";

		// Token: 0x04000067 RID: 103
		internal const string JsonSerializerOptionsAddContextMessage = "JsonSerializerOptions.AddContext is obsolete. To register a JsonSerializerContext, use either the TypeInfoResolver or TypeInfoResolverChain properties.";

		// Token: 0x04000068 RID: 104
		internal const string JsonSerializerOptionsAddContextDiagId = "SYSLIB0049";

		// Token: 0x04000069 RID: 105
		internal const string LegacyFormatterMessage = "Formatter-based serialization is obsolete and should not be used.";

		// Token: 0x0400006A RID: 106
		internal const string LegacyFormatterDiagId = "SYSLIB0050";

		// Token: 0x0400006B RID: 107
		internal const string LegacyFormatterImplMessage = "This API supports obsolete formatter-based serialization. It should not be called or extended by application code.";

		// Token: 0x0400006C RID: 108
		internal const string LegacyFormatterImplDiagId = "SYSLIB0051";

		// Token: 0x0400006D RID: 109
		internal const string RegexExtensibilityImplMessage = "This API supports obsolete mechanisms for Regex extensibility. It is not supported.";

		// Token: 0x0400006E RID: 110
		internal const string RegexExtensibilityDiagId = "SYSLIB0052";

		// Token: 0x0400006F RID: 111
		internal const string AesGcmTagConstructorMessage = "AesGcm should indicate the required tag size for encryption and decryption. Use a constructor that accepts the tag size.";

		// Token: 0x04000070 RID: 112
		internal const string AesGcmTagConstructorDiagId = "SYSLIB0053";
	}
}
