using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000047 RID: 71
	internal abstract class Encryption : IDataProtection
	{
		// Token: 0x06000226 RID: 550 RVA: 0x0000ADB7 File Offset: 0x00008FB7
		public byte[] ProtectData(string unprotectedData, string tag)
		{
			return this.Encrypt(unprotectedData, tag);
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000ADC1 File Offset: 0x00008FC1
		public string UnprotectDataToString(byte[] protectedData, string tag)
		{
			return this.DecryptToString(protectedData, tag);
		}

		// Token: 0x06000228 RID: 552
		protected abstract byte[] EncryptInternal(byte[] data);

		// Token: 0x06000229 RID: 553
		public abstract byte[] Decrypt(byte[] data, bool useSalt = true);

		// Token: 0x0600022A RID: 554
		protected abstract bool IsEncryptionChecked();

		// Token: 0x0600022B RID: 555
		protected abstract void SetEncryptionChecked();

		// Token: 0x0600022C RID: 556 RVA: 0x0000ADCC File Offset: 0x00008FCC
		public void CheckEncryptionOnce()
		{
			if (!this.IsEncryptionChecked())
			{
				byte[] array = this.EncryptInternal(Encryption.unencoded);
				byte[] array2 = this.Decrypt(array, true);
				RSTrace.CryptoTrace.Assert(Encryption.unencoded.SequenceEqual(array2), "Basic Encryption Test");
				this.SetEncryptionChecked();
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000AE16 File Offset: 0x00009016
		public byte[] Encrypt(byte[] data)
		{
			this.CheckEncryptionOnce();
			return this.EncryptInternal(data);
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600022E RID: 558 RVA: 0x0000AE25 File Offset: 0x00009025
		public static int OldUntaggedVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600022F RID: 559 RVA: 0x0000AE28 File Offset: 0x00009028
		public static int OldUnsaltedVersion
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000230 RID: 560 RVA: 0x0000AE2B File Offset: 0x0000902B
		public static int CurrentVersion
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000AE2E File Offset: 0x0000902E
		public static bool IsCurrentVersion(int version)
		{
			return version == Encryption.CurrentVersion;
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000AE38 File Offset: 0x00009038
		public byte[] Encrypt(string data)
		{
			if (data == null)
			{
				return null;
			}
			byte[] bytes = Encoding.Unicode.GetBytes(data);
			return this.Encrypt(bytes);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000AE60 File Offset: 0x00009060
		public string EncryptToString(string data)
		{
			byte[] array = this.Encrypt(data);
			if (array == null)
			{
				return null;
			}
			return Convert.ToBase64String(array);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000AE80 File Offset: 0x00009080
		public string EncryptToString(string unprotectedData, string tag)
		{
			byte[] array = this.Encrypt(unprotectedData, tag);
			if (array == null)
			{
				return null;
			}
			return Convert.ToBase64String(array);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000AEA4 File Offset: 0x000090A4
		public byte[] Encrypt(string unprotectedData, string tag)
		{
			byte[] array = null;
			if (unprotectedData != null)
			{
				array = Encoding.Unicode.GetBytes(unprotectedData);
			}
			return this.Encrypt(array, tag);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000AECC File Offset: 0x000090CC
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
				array2 = this.Encrypt(array);
			}
			finally
			{
				if (tag != null && array != null)
				{
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = 0;
					}
				}
				array = null;
			}
			return array2;
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000AF50 File Offset: 0x00009150
		public static SecureString ConvertToSecureString(string data)
		{
			SecureString secureString = new SecureString();
			if (data != null)
			{
				foreach (char c in data)
				{
					secureString.AppendChar(c);
				}
				return secureString;
			}
			return null;
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000AF8C File Offset: 0x0000918C
		public string Decrypt(string data)
		{
			if (data == null)
			{
				return null;
			}
			byte[] array = Convert.FromBase64String(data);
			byte[] array2 = this.Decrypt(array, true);
			string text = null;
			if (array2 != null)
			{
				text = Encoding.Unicode.GetString(array2);
				if (text != null && text.Length > 0 && text[text.Length - 1] == '\0')
				{
					text = text.Remove(text.Length - 1);
				}
			}
			return text;
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000AFEB File Offset: 0x000091EB
		public string DecryptToString(string protectedData)
		{
			return this.DecryptToString(Encryption.CurrentVersion, protectedData, null);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000AFFA File Offset: 0x000091FA
		public string DecryptToString(string protectedData, string tag)
		{
			return this.DecryptToString(Encryption.CurrentVersion, protectedData, tag);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000B00C File Offset: 0x0000920C
		public string DecryptToString(int version, string protectedData, string tag)
		{
			if (protectedData == null)
			{
				return null;
			}
			byte[] array = Convert.FromBase64String(protectedData);
			return this.DecryptToString(version, array, tag);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000B02E File Offset: 0x0000922E
		public string DecryptToString(byte[] protectedData, string tag)
		{
			return this.DecryptToString(Encryption.CurrentVersion, protectedData, tag);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000B040 File Offset: 0x00009240
		public string DecryptToString(int version, byte[] protectedData, string tag)
		{
			byte[] array = this.Decrypt(version, protectedData, tag);
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

		// Token: 0x0600023E RID: 574 RVA: 0x0000B071 File Offset: 0x00009271
		public byte[] Decrypt(int version, byte[] protectedData, string tag)
		{
			if (version == Encryption.OldUntaggedVersion)
			{
				return this.Decrypt(protectedData, null, false);
			}
			if (version == Encryption.OldUnsaltedVersion)
			{
				return this.Decrypt(protectedData, tag, false);
			}
			if (version == Encryption.CurrentVersion)
			{
				return this.Decrypt(protectedData, tag, true);
			}
			throw new CannotValidateEncryptedDataException();
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000B0B0 File Offset: 0x000092B0
		public byte[] Decrypt(byte[] protectedData, string tag, bool useSalt)
		{
			if (protectedData == null)
			{
				return null;
			}
			byte[] array = this.Decrypt(protectedData, useSalt);
			if (tag == null)
			{
				return array;
			}
			byte[] array3;
			try
			{
				byte[] bytes = Encoding.Unicode.GetBytes(tag);
				bool flag = false;
				if (array.Length >= bytes.Length)
				{
					int num = 0;
					while (num < bytes.Length && array[num] == bytes[num])
					{
						num++;
					}
					if (num == bytes.Length)
					{
						flag = true;
					}
				}
				if (!flag)
				{
					if (RSTrace.CryptoTrace.TraceError)
					{
						RSTrace.CryptoTrace.Trace(TraceLevel.Error, "Encrypted data is missing expected tag information.");
					}
					throw new CannotValidateEncryptedDataException();
				}
				byte[] array2 = new byte[array.Length - bytes.Length];
				Array.Copy(array, bytes.Length, array2, 0, array2.Length);
				array3 = array2;
			}
			finally
			{
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = 0;
				}
				array = null;
			}
			return array3;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000B180 File Offset: 0x00009380
		protected static byte[] EncryptNative(byte[] data, int cryptoFlags)
		{
			if (data == null)
			{
				return null;
			}
			string text = "Default";
			byte[] array = null;
			SafeCryptoBlobIn safeCryptoBlobIn = null;
			SafeCryptoBlobOut safeCryptoBlobOut = null;
			try
			{
				safeCryptoBlobIn = new SafeCryptoBlobIn(data);
				safeCryptoBlobOut = new SafeCryptoBlobOut();
				bool flag = NativeMethods.CryptProtectData(safeCryptoBlobIn, text, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, cryptoFlags, safeCryptoBlobOut);
				safeCryptoBlobIn.ZeroBuffer();
				if (!flag)
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					throw new ServerConfigurationErrorException(string.Format(CultureInfo.InvariantCulture, "CryptProtectData: Win32 error:{0}", lastWin32Error));
				}
				NativeMethods.DATA_BLOB blob = safeCryptoBlobOut.Blob;
				array = new byte[blob.cbData];
				Marshal.Copy(blob.pbData, array, 0, blob.cbData);
			}
			finally
			{
				if (safeCryptoBlobIn != null)
				{
					safeCryptoBlobIn.Close();
				}
				if (safeCryptoBlobOut != null)
				{
					safeCryptoBlobOut.Close();
				}
			}
			return array;
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000B240 File Offset: 0x00009440
		protected static byte[] DecryptNative(byte[] data, int cryptoFlags)
		{
			if (data == null)
			{
				return null;
			}
			byte[] array = null;
			SafeCryptoBlobIn safeCryptoBlobIn = null;
			SafeCryptoBlobOut safeCryptoBlobOut = null;
			try
			{
				safeCryptoBlobIn = new SafeCryptoBlobIn(data);
				safeCryptoBlobOut = new SafeCryptoBlobOut();
				if (!NativeMethods.CryptUnprotectData(safeCryptoBlobIn, null, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, cryptoFlags, safeCryptoBlobOut))
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					throw new ServerConfigurationErrorException(string.Format(CultureInfo.InvariantCulture, "CryptUnprotectData: Win32 error:{0}", lastWin32Error));
				}
				NativeMethods.DATA_BLOB blob = safeCryptoBlobOut.Blob;
				array = new byte[blob.cbData];
				Marshal.Copy(blob.pbData, array, 0, blob.cbData);
				safeCryptoBlobOut.ZeroBuffer();
			}
			finally
			{
				if (safeCryptoBlobIn != null)
				{
					safeCryptoBlobIn.Close();
				}
				if (safeCryptoBlobOut != null)
				{
					safeCryptoBlobOut.Close();
				}
			}
			return array;
		}

		// Token: 0x04000218 RID: 536
		private static readonly byte[] unencoded = Encoding.ASCII.GetBytes("test1");
	}
}
