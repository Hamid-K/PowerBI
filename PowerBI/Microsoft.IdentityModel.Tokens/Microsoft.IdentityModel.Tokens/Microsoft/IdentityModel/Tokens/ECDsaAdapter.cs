using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Logging;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x0200012D RID: 301
	internal class ECDsaAdapter
	{
		// Token: 0x06000EC4 RID: 3780 RVA: 0x0003AD12 File Offset: 0x00038F12
		internal ECDsaAdapter()
		{
			CreateECDsaDelegate createECDsaDelegate;
			if ((createECDsaDelegate = ECDsaAdapter.<>O.<0>__ECDsaNotSupported) == null)
			{
				createECDsaDelegate = (ECDsaAdapter.<>O.<0>__ECDsaNotSupported = new CreateECDsaDelegate(ECDsaAdapter.ECDsaNotSupported));
			}
			this.CreateECDsaFunction = createECDsaDelegate;
			base..ctor();
			this.CreateECDsaFunction = new CreateECDsaDelegate(this.CreateECDsaUsingCNGKey);
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x0003AD4D File Offset: 0x00038F4D
		internal ECDsa CreateECDsa(JsonWebKey jsonWebKey, bool usePrivateKey)
		{
			return this.CreateECDsaFunction(jsonWebKey, usePrivateKey);
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x0003AD5C File Offset: 0x00038F5C
		private ECDsa CreateECDsaUsingCNGKey(JsonWebKey jsonWebKey, bool usePrivateKey)
		{
			if (jsonWebKey == null)
			{
				throw LogHelper.LogArgumentNullException("jsonWebKey");
			}
			if (jsonWebKey.Crv == null)
			{
				throw LogHelper.LogArgumentNullException("Crv");
			}
			if (jsonWebKey.X == null)
			{
				throw LogHelper.LogArgumentNullException("X");
			}
			if (jsonWebKey.Y == null)
			{
				throw LogHelper.LogArgumentNullException("Y");
			}
			GCHandle gchandle = default(GCHandle);
			ECDsa ecdsa;
			try
			{
				uint magicValue = ECDsaAdapter.GetMagicValue(jsonWebKey.Crv, usePrivateKey);
				uint keyByteCount = ECDsaAdapter.GetKeyByteCount(jsonWebKey.Crv);
				byte[] array;
				if (usePrivateKey)
				{
					array = new byte[(ulong)(3U * keyByteCount) + (ulong)((long)(2 * Marshal.SizeOf<uint>()))];
				}
				else
				{
					array = new byte[(ulong)(2U * keyByteCount) + (ulong)((long)(2 * Marshal.SizeOf<uint>()))];
				}
				gchandle = GCHandle.Alloc(array, GCHandleType.Pinned);
				IntPtr intPtr = gchandle.AddrOfPinnedObject();
				byte[] array2 = Base64UrlEncoder.DecodeBytes(jsonWebKey.X);
				if ((long)array2.Length > (long)((ulong)keyByteCount))
				{
					throw LogHelper.LogExceptionMessage(new ArgumentOutOfRangeException("jsonWebKey", LogHelper.FormatInvariant("IDX10675: Cannot create a ECDsa object from the '{0}', the bytes from the decoded value of '{1}' must be less than the size associated with the curve: '{2}'. Size was: '{3}'.", new object[]
					{
						LogHelper.MarkAsNonPII("jsonWebKey"),
						LogHelper.MarkAsNonPII("X"),
						keyByteCount,
						LogHelper.MarkAsNonPII(array2.Length)
					})));
				}
				byte[] array3 = Base64UrlEncoder.DecodeBytes(jsonWebKey.Y);
				if ((long)array3.Length > (long)((ulong)keyByteCount))
				{
					throw LogHelper.LogExceptionMessage(new ArgumentOutOfRangeException("jsonWebKey", LogHelper.FormatInvariant("IDX10675: Cannot create a ECDsa object from the '{0}', the bytes from the decoded value of '{1}' must be less than the size associated with the curve: '{2}'. Size was: '{3}'.", new object[]
					{
						LogHelper.MarkAsNonPII("jsonWebKey"),
						LogHelper.MarkAsNonPII("Y"),
						keyByteCount,
						LogHelper.MarkAsNonPII(array3.Length)
					})));
				}
				Marshal.WriteInt64(intPtr, 0, (long)((ulong)magicValue));
				Marshal.WriteInt64(intPtr, 4, (long)((ulong)keyByteCount));
				int num = 8;
				foreach (byte b in array2)
				{
					Marshal.WriteByte(intPtr, num++, b);
				}
				foreach (byte b2 in array3)
				{
					Marshal.WriteByte(intPtr, num++, b2);
				}
				if (usePrivateKey)
				{
					if (jsonWebKey.D == null)
					{
						throw LogHelper.LogArgumentNullException("D");
					}
					byte[] array5 = Base64UrlEncoder.DecodeBytes(jsonWebKey.D);
					if ((long)array5.Length > (long)((ulong)keyByteCount))
					{
						throw LogHelper.LogExceptionMessage(new ArgumentOutOfRangeException("jsonWebKey", LogHelper.FormatInvariant("IDX10675: Cannot create a ECDsa object from the '{0}', the bytes from the decoded value of '{1}' must be less than the size associated with the curve: '{2}'. Size was: '{3}'.", new object[]
						{
							LogHelper.MarkAsNonPII("jsonWebKey"),
							LogHelper.MarkAsNonPII("D"),
							keyByteCount,
							LogHelper.MarkAsNonPII(array5.Length)
						})));
					}
					foreach (byte b3 in array5)
					{
						Marshal.WriteByte(intPtr, num++, b3);
					}
					Marshal.Copy(intPtr, array, 0, array.Length);
					using (CngKey cngKey = CngKey.Import(array, CngKeyBlobFormat.EccPrivateBlob))
					{
						return new ECDsaCng(cngKey);
					}
				}
				Marshal.Copy(intPtr, array, 0, array.Length);
				using (CngKey cngKey2 = CngKey.Import(array, CngKeyBlobFormat.EccPublicBlob))
				{
					ecdsa = new ECDsaCng(cngKey2);
				}
			}
			catch (Exception ex)
			{
				throw LogHelper.LogExceptionMessage(new CryptographicException("IDX10689: Unable to create an ECDsa object. See inner exception for more details.", ex));
			}
			finally
			{
				if (gchandle.IsAllocated)
				{
					gchandle.Free();
				}
			}
			return ecdsa;
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x0003B0F4 File Offset: 0x000392F4
		internal static ECDsa ECDsaNotSupported(JsonWebKey jsonWebKey, bool usePrivateKey)
		{
			throw LogHelper.LogExceptionMessage(new PlatformNotSupportedException("IDX10690: ECDsa creation is not supported by the current platform. For more details, see https://aka.ms/IdentityModel/create-ecdsa"));
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x0003B108 File Offset: 0x00039308
		private static uint GetKeyByteCount(string curveId)
		{
			if (string.IsNullOrEmpty(curveId))
			{
				throw LogHelper.LogArgumentNullException("curveId");
			}
			uint num;
			if (!(curveId == "P-256"))
			{
				if (!(curveId == "P-384"))
				{
					if (!(curveId == "P-512") && !(curveId == "P-521"))
					{
						throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX10645: Elliptical Curve not supported for curveId: '{0}'", new object[] { LogHelper.MarkAsNonPII(curveId) })));
					}
					num = 66U;
				}
				else
				{
					num = 48U;
				}
			}
			else
			{
				num = 32U;
			}
			return num;
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x0003B194 File Offset: 0x00039394
		private static uint GetMagicValue(string curveId, bool willCreateSignatures)
		{
			if (string.IsNullOrEmpty(curveId))
			{
				throw LogHelper.LogArgumentNullException("curveId");
			}
			ECDsaAdapter.KeyBlobMagicNumber keyBlobMagicNumber;
			if (!(curveId == "P-256"))
			{
				if (!(curveId == "P-384"))
				{
					if (!(curveId == "P-512") && !(curveId == "P-521"))
					{
						throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX10645: Elliptical Curve not supported for curveId: '{0}'", new object[] { LogHelper.MarkAsNonPII(curveId) })));
					}
					if (willCreateSignatures)
					{
						keyBlobMagicNumber = ECDsaAdapter.KeyBlobMagicNumber.BCRYPT_ECDSA_PRIVATE_P521_MAGIC;
					}
					else
					{
						keyBlobMagicNumber = ECDsaAdapter.KeyBlobMagicNumber.BCRYPT_ECDSA_PUBLIC_P521_MAGIC;
					}
				}
				else if (willCreateSignatures)
				{
					keyBlobMagicNumber = ECDsaAdapter.KeyBlobMagicNumber.BCRYPT_ECDSA_PRIVATE_P384_MAGIC;
				}
				else
				{
					keyBlobMagicNumber = ECDsaAdapter.KeyBlobMagicNumber.BCRYPT_ECDSA_PUBLIC_P384_MAGIC;
				}
			}
			else if (willCreateSignatures)
			{
				keyBlobMagicNumber = ECDsaAdapter.KeyBlobMagicNumber.BCRYPT_ECDSA_PRIVATE_P256_MAGIC;
			}
			else
			{
				keyBlobMagicNumber = ECDsaAdapter.KeyBlobMagicNumber.BCRYPT_ECDSA_PUBLIC_P256_MAGIC;
			}
			return (uint)keyBlobMagicNumber;
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x0003B248 File Offset: 0x00039448
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static bool SupportsCNGKey()
		{
			bool flag;
			try
			{
				CngKeyBlobFormat eccPrivateBlob = CngKeyBlobFormat.EccPrivateBlob;
				flag = true;
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x040004B4 RID: 1204
		internal readonly CreateECDsaDelegate CreateECDsaFunction;

		// Token: 0x040004B5 RID: 1205
		internal static ECDsaAdapter Instance = new ECDsaAdapter();

		// Token: 0x0200026F RID: 623
		private enum KeyBlobMagicNumber : uint
		{
			// Token: 0x04000B2C RID: 2860
			BCRYPT_ECDSA_PUBLIC_P256_MAGIC = 827540293U,
			// Token: 0x04000B2D RID: 2861
			BCRYPT_ECDSA_PUBLIC_P384_MAGIC = 861094725U,
			// Token: 0x04000B2E RID: 2862
			BCRYPT_ECDSA_PUBLIC_P521_MAGIC = 894649157U,
			// Token: 0x04000B2F RID: 2863
			BCRYPT_ECDSA_PRIVATE_P256_MAGIC = 844317509U,
			// Token: 0x04000B30 RID: 2864
			BCRYPT_ECDSA_PRIVATE_P384_MAGIC = 877871941U,
			// Token: 0x04000B31 RID: 2865
			BCRYPT_ECDSA_PRIVATE_P521_MAGIC = 911426373U
		}

		// Token: 0x02000270 RID: 624
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000B32 RID: 2866
			public static CreateECDsaDelegate <0>__ECDsaNotSupported;
		}
	}
}
