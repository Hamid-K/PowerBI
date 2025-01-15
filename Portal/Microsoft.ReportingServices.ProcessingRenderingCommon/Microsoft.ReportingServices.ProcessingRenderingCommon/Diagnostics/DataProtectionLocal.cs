using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000044 RID: 68
	internal static class DataProtectionLocal
	{
		// Token: 0x170000CE RID: 206
		// (set) Token: 0x060001F9 RID: 505 RVA: 0x00007852 File Offset: 0x00005A52
		public static ProtectionMode GlobalProtectionMode
		{
			set
			{
				if (value == ProtectionMode.LocalSystemEncryption)
				{
					DataProtectionLocal.m_dwProtectionFlags = 4;
					return;
				}
				if (value == ProtectionMode.CurrentUserEncryption)
				{
					DataProtectionLocal.m_dwProtectionFlags = 0;
				}
			}
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00007868 File Offset: 0x00005A68
		public static string LocalProtectData(string data)
		{
			if (data == null)
			{
				return null;
			}
			byte[] array = DataProtectionLocal.LocalProtectData(Encoding.Unicode.GetBytes(data));
			string text = null;
			if (array != null)
			{
				text = Convert.ToBase64String(array);
			}
			return text;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00007898 File Offset: 0x00005A98
		public static string LocalUnprotectData(string data)
		{
			if (data == null)
			{
				return null;
			}
			byte[] array = DataProtectionLocal.LocalUnprotectData(Convert.FromBase64String(data));
			string text = null;
			if (array != null)
			{
				text = Encoding.Unicode.GetString(array);
				if (text != null && text.Length > 0 && text[text.Length - 1] == '\0')
				{
					text = text.Remove(text.Length - 1);
				}
			}
			return text;
		}

		// Token: 0x060001FC RID: 508 RVA: 0x000078F3 File Offset: 0x00005AF3
		public static byte[] LocalProtectData(byte[] data)
		{
			if (data == null)
			{
				return null;
			}
			return DataProtectionLocal.ProtectData(data, 1 | DataProtectionLocal.m_dwProtectionFlags);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00007907 File Offset: 0x00005B07
		public static byte[] LocalUnprotectData(byte[] data)
		{
			if (data == null)
			{
				return null;
			}
			return DataProtectionLocal.UnprotectData(data, 1);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00007918 File Offset: 0x00005B18
		private static byte[] UnprotectData(byte[] data, int dwFlags)
		{
			byte[] array = null;
			SafeCryptoBlobIn safeCryptoBlobIn = null;
			SafeCryptoBlobOut safeCryptoBlobOut = null;
			try
			{
				safeCryptoBlobIn = new SafeCryptoBlobIn(data);
				safeCryptoBlobOut = new SafeCryptoBlobOut();
				if (!NativeMethods.CryptUnprotectData(safeCryptoBlobIn, null, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, dwFlags, safeCryptoBlobOut))
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

		// Token: 0x060001FF RID: 511 RVA: 0x000079C8 File Offset: 0x00005BC8
		private static byte[] ProtectData(byte[] data, int dwFlags)
		{
			string text = "Default";
			byte[] array = null;
			SafeCryptoBlobIn safeCryptoBlobIn = null;
			SafeCryptoBlobOut safeCryptoBlobOut = null;
			try
			{
				safeCryptoBlobIn = new SafeCryptoBlobIn(data);
				safeCryptoBlobOut = new SafeCryptoBlobOut();
				bool flag = NativeMethods.CryptProtectData(safeCryptoBlobIn, text, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, dwFlags, safeCryptoBlobOut);
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

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000200 RID: 512 RVA: 0x00007A84 File Offset: 0x00005C84
		public static IDataProtection Instance
		{
			[DebuggerStepThrough]
			get
			{
				if (DataProtectionLocal.m_dpInstance == null)
				{
					DataProtectionLocal.m_dpInstance = new DataProtectionLocal.DataProtectionLocalInstance();
				}
				return DataProtectionLocal.m_dpInstance;
			}
		}

		// Token: 0x040000FA RID: 250
		private static int m_dwProtectionFlags = 4;

		// Token: 0x040000FB RID: 251
		private static IDataProtection m_dpInstance;

		// Token: 0x020000E3 RID: 227
		private sealed class DataProtectionLocalInstance : IDataProtection
		{
			// Token: 0x060007A9 RID: 1961 RVA: 0x0001454F File Offset: 0x0001274F
			public byte[] ProtectData(string unprotectedData, string tag)
			{
				if (unprotectedData == null)
				{
					return null;
				}
				return DataProtectionLocal.LocalProtectData(Encoding.Unicode.GetBytes(unprotectedData));
			}

			// Token: 0x060007AA RID: 1962 RVA: 0x00014568 File Offset: 0x00012768
			public string UnprotectDataToString(byte[] protectedData, string tag)
			{
				if (protectedData == null)
				{
					return null;
				}
				byte[] array = DataProtectionLocal.LocalUnprotectData(protectedData);
				if (array == null)
				{
					return null;
				}
				if (protectedData.Length == 0)
				{
					return string.Empty;
				}
				return Encoding.Unicode.GetString(array);
			}
		}
	}
}
