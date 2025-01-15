using System;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Logging;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000165 RID: 357
	internal static class AesAead
	{
		// Token: 0x06001070 RID: 4208 RVA: 0x0003FF0A File Offset: 0x0003E10A
		public static void CheckArgumentsForNull(byte[] nonce, byte[] plaintext, byte[] ciphertext, byte[] tag)
		{
			if (nonce == null)
			{
				throw LogHelper.LogArgumentNullException("nonce");
			}
			if (plaintext == null)
			{
				throw LogHelper.LogArgumentNullException("plaintext");
			}
			if (ciphertext == null)
			{
				throw LogHelper.LogArgumentNullException("ciphertext");
			}
			if (tag == null)
			{
				throw LogHelper.LogArgumentNullException("tag");
			}
		}

		// Token: 0x06001071 RID: 4209 RVA: 0x0003FF44 File Offset: 0x0003E144
		public unsafe static void Decrypt(SafeKeyHandle keyHandle, byte[] nonce, byte[] associatedData, byte[] ciphertext, byte[] tag, byte[] plaintext, bool clearPlaintextOnFailure)
		{
			byte* ptr;
			if (plaintext == null || plaintext.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &plaintext[0];
			}
			byte* ptr2;
			if (nonce == null || nonce.Length == 0)
			{
				ptr2 = null;
			}
			else
			{
				ptr2 = &nonce[0];
			}
			byte* ptr3;
			if (ciphertext == null || ciphertext.Length == 0)
			{
				ptr3 = null;
			}
			else
			{
				ptr3 = &ciphertext[0];
			}
			byte* ptr4;
			if (tag == null || tag.Length == 0)
			{
				ptr4 = null;
			}
			else
			{
				ptr4 = &tag[0];
			}
			byte* ptr5;
			if (associatedData == null || associatedData.Length == 0)
			{
				ptr5 = null;
			}
			else
			{
				ptr5 = &associatedData[0];
			}
			Interop.BCrypt.BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO bcrypt_AUTHENTICATED_CIPHER_MODE_INFO = Interop.BCrypt.BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO.Create();
			bcrypt_AUTHENTICATED_CIPHER_MODE_INFO.pbNonce = ptr2;
			bcrypt_AUTHENTICATED_CIPHER_MODE_INFO.cbNonce = nonce.Length;
			bcrypt_AUTHENTICATED_CIPHER_MODE_INFO.pbTag = ptr4;
			bcrypt_AUTHENTICATED_CIPHER_MODE_INFO.cbTag = tag.Length;
			bcrypt_AUTHENTICATED_CIPHER_MODE_INFO.pbAuthData = ptr5;
			if (associatedData == null)
			{
				bcrypt_AUTHENTICATED_CIPHER_MODE_INFO.cbAuthData = 0;
			}
			else
			{
				bcrypt_AUTHENTICATED_CIPHER_MODE_INFO.cbAuthData = associatedData.Length;
			}
			int num;
			Interop.BCrypt.NTSTATUS ntstatus = Interop.BCrypt.BCryptDecrypt(keyHandle, ptr3, ciphertext.Length, new IntPtr((void*)(&bcrypt_AUTHENTICATED_CIPHER_MODE_INFO)), null, 0, ptr, plaintext.Length, out num, 0);
			if (ntstatus == Interop.BCrypt.NTSTATUS.STATUS_SUCCESS)
			{
				return;
			}
			if (ntstatus != (Interop.BCrypt.NTSTATUS)3221266434U)
			{
				throw LogHelper.LogExceptionMessage(Interop.BCrypt.CreateCryptographicException(ntstatus));
			}
			if (clearPlaintextOnFailure)
			{
				CryptographicOperations.ZeroMemory(plaintext);
			}
			throw LogHelper.LogExceptionMessage(new CryptographicException(LogHelper.FormatInvariant("IDX10714: Unable to perform the decryption. There is a authentication tag mismatch.", Array.Empty<object>())));
		}

		// Token: 0x06001072 RID: 4210 RVA: 0x00040088 File Offset: 0x0003E288
		internal unsafe static void Encrypt(SafeKeyHandle keyHandle, byte[] nonce, byte[] associatedData, byte[] plaintext, byte[] ciphertext, byte[] tag)
		{
			fixed (byte[] array = plaintext)
			{
				byte* ptr;
				if (plaintext == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				fixed (byte[] array2 = nonce)
				{
					byte* ptr2;
					if (nonce == null || array2.Length == 0)
					{
						ptr2 = null;
					}
					else
					{
						ptr2 = &array2[0];
					}
					fixed (byte[] array3 = ciphertext)
					{
						byte* ptr3;
						if (ciphertext == null || array3.Length == 0)
						{
							ptr3 = null;
						}
						else
						{
							ptr3 = &array3[0];
						}
						fixed (byte[] array4 = tag)
						{
							byte* ptr4;
							if (tag == null || array4.Length == 0)
							{
								ptr4 = null;
							}
							else
							{
								ptr4 = &array4[0];
							}
							fixed (byte[] array5 = associatedData)
							{
								byte* ptr5;
								if (associatedData == null || array5.Length == 0)
								{
									ptr5 = null;
								}
								else
								{
									ptr5 = &array5[0];
								}
								Interop.BCrypt.BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO bcrypt_AUTHENTICATED_CIPHER_MODE_INFO = Interop.BCrypt.BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO.Create();
								bcrypt_AUTHENTICATED_CIPHER_MODE_INFO.pbNonce = ptr2;
								bcrypt_AUTHENTICATED_CIPHER_MODE_INFO.cbNonce = nonce.Length;
								bcrypt_AUTHENTICATED_CIPHER_MODE_INFO.pbTag = ptr4;
								bcrypt_AUTHENTICATED_CIPHER_MODE_INFO.cbTag = tag.Length;
								bcrypt_AUTHENTICATED_CIPHER_MODE_INFO.pbAuthData = ptr5;
								if (associatedData == null)
								{
									bcrypt_AUTHENTICATED_CIPHER_MODE_INFO.cbAuthData = 0;
								}
								else
								{
									bcrypt_AUTHENTICATED_CIPHER_MODE_INFO.cbAuthData = associatedData.Length;
								}
								int num;
								Interop.BCrypt.NTSTATUS ntstatus = Interop.BCrypt.BCryptEncrypt(keyHandle, ptr, plaintext.Length, new IntPtr((void*)(&bcrypt_AUTHENTICATED_CIPHER_MODE_INFO)), null, 0, ptr3, ciphertext.Length, out num, 0);
								if (ntstatus != Interop.BCrypt.NTSTATUS.STATUS_SUCCESS)
								{
									throw Interop.BCrypt.CreateCryptographicException(ntstatus);
								}
							}
						}
					}
				}
			}
		}
	}
}
