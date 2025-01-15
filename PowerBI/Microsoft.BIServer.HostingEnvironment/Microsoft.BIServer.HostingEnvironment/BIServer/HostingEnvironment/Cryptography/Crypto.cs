using System;
using System.Linq;
using System.Security;
using System.Text;
using Microsoft.BIServer.HostingEnvironment.Cryptography.Exceptions;

namespace Microsoft.BIServer.HostingEnvironment.Cryptography
{
	// Token: 0x02000034 RID: 52
	public abstract class Crypto : ICrypto
	{
		// Token: 0x0600014C RID: 332
		protected abstract byte[] EncryptInternal(byte[] data);

		// Token: 0x0600014D RID: 333
		protected abstract byte[] DecryptInternal(byte[] data);

		// Token: 0x0600014E RID: 334
		public abstract string GetName();

		// Token: 0x0600014F RID: 335 RVA: 0x0000518A File Offset: 0x0000338A
		public byte[] Encrypt(byte[] unprotectedData)
		{
			return this.Encrypt(unprotectedData, null);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00005194 File Offset: 0x00003394
		public byte[] Encrypt(byte[] unprotectedData, string tag)
		{
			if (unprotectedData == null)
			{
				return null;
			}
			byte[] array = null;
			byte[] array2;
			try
			{
				if (string.IsNullOrEmpty(tag))
				{
					array = unprotectedData;
				}
				else
				{
					byte[] bytes = Encoding.Unicode.GetBytes(tag);
					array = new byte[unprotectedData.Length + bytes.Length];
					bytes.CopyTo(array, 0);
					unprotectedData.CopyTo(array, bytes.Length);
				}
				array2 = this.EncryptInternal(array);
			}
			finally
			{
				if (tag != null)
				{
					this.Clear(array);
				}
				array = null;
			}
			return array2;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00005208 File Offset: 0x00003408
		public byte[] Encrypt(string unprotectedData)
		{
			return this.Encrypt(unprotectedData, null);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00005214 File Offset: 0x00003414
		public byte[] Encrypt(string unprotectedData, string tag)
		{
			byte[] array = null;
			if (unprotectedData != null)
			{
				array = Encoding.Unicode.GetBytes(unprotectedData);
			}
			return this.Encrypt(array, tag);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000523A File Offset: 0x0000343A
		public string EncryptToString(string unprotectedData)
		{
			return this.EncryptToString(unprotectedData, null);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00005244 File Offset: 0x00003444
		public string EncryptToString(string unprotectedData, string tag)
		{
			if (unprotectedData == null)
			{
				return null;
			}
			byte[] bytes = Encoding.Unicode.GetBytes(unprotectedData);
			return Convert.ToBase64String(this.Encrypt(bytes, tag));
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000526F File Offset: 0x0000346F
		public byte[] Decrypt(byte[] protectedData)
		{
			return this.Decrypt(protectedData, null);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000527C File Offset: 0x0000347C
		public byte[] Decrypt(byte[] protectedData, string tag)
		{
			if (protectedData == null)
			{
				return null;
			}
			byte[] array = this.DecryptInternal(protectedData);
			if (tag == null)
			{
				return array;
			}
			byte[] array2;
			try
			{
				byte[] bytes = Encoding.Unicode.GetBytes(tag);
				if (!array.Take(bytes.Length).SequenceEqual(bytes))
				{
					throw new CryptoException.TagMissing("Encrypted data is missing expected tag information.");
				}
				array2 = array.Skip(bytes.Length).ToArray<byte>();
			}
			finally
			{
				this.Clear(array);
				array = null;
			}
			return array2;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x000052F0 File Offset: 0x000034F0
		public string DecryptToString(string protectedData)
		{
			return this.DecryptToString(protectedData, null);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x000052FC File Offset: 0x000034FC
		public string DecryptToString(string protectedData, string tag)
		{
			byte[] array = Convert.FromBase64String(protectedData);
			return this.DecryptToString(array, tag);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00005318 File Offset: 0x00003518
		public string DecryptToString(byte[] protectedData)
		{
			return this.DecryptToString(protectedData, null);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00005324 File Offset: 0x00003524
		public string DecryptToString(byte[] protectedData, string tag)
		{
			byte[] array = this.Decrypt(protectedData, tag);
			if (array == null)
			{
				return null;
			}
			if (array.Length == 0)
			{
				return string.Empty;
			}
			return Encoding.Unicode.GetString(array);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00005354 File Offset: 0x00003554
		public SecureString DecryptToSecureString(string protectedData)
		{
			return this.DecryptToSecureString(protectedData, null);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00005360 File Offset: 0x00003560
		public SecureString DecryptToSecureString(string protectedData, string tag)
		{
			byte[] array = Convert.FromBase64String(protectedData);
			return this.DecryptToSecureString(array, tag);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000537C File Offset: 0x0000357C
		public SecureString DecryptToSecureString(byte[] protectedData)
		{
			return this.DecryptToSecureString(protectedData, null);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00005388 File Offset: 0x00003588
		public SecureString DecryptToSecureString(byte[] protectedData, string tag)
		{
			if (protectedData == null)
			{
				return null;
			}
			SecureString secureString = new SecureString();
			foreach (char c in this.DecryptToString(protectedData, tag))
			{
				secureString.AppendChar(c);
			}
			secureString.MakeReadOnly();
			return secureString;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x000053D0 File Offset: 0x000035D0
		private void Clear(byte[] data)
		{
			Array.Clear(data, 0, data.Length);
		}
	}
}
