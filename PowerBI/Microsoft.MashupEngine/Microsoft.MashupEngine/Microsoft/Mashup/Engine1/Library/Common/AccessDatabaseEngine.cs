using System;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Mashup.Common;
using Microsoft.Win32;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x0200102A RID: 4138
	internal static class AccessDatabaseEngine
	{
		// Token: 0x17001EC8 RID: 7880
		// (get) Token: 0x06006BED RID: 27629 RVA: 0x001737E9 File Offset: 0x001719E9
		public static string ProviderName
		{
			get
			{
				if (!AccessDatabaseEngine.initialized)
				{
					if (AccessDatabaseEngine.IsInvalidAceConfiguration())
					{
						AccessDatabaseEngine.WorkaroundInvalidAceConfiguration();
					}
					AccessDatabaseEngine.initialized = true;
				}
				return "Microsoft.ACE.OLEDB.12.0";
			}
		}

		// Token: 0x06006BEE RID: 27630 RVA: 0x0017380C File Offset: 0x00171A0C
		private static bool IsInvalidAceConfiguration()
		{
			try
			{
				OleDbConnectionStringBuilder oleDbConnectionStringBuilder = new OleDbConnectionStringBuilder();
				oleDbConnectionStringBuilder.DataSource = Path.GetTempPath();
				oleDbConnectionStringBuilder.Provider = "Microsoft.ACE.OLEDB.12.0";
				oleDbConnectionStringBuilder["Extended Properties"] = "Excel 12.0";
				using (OleDbConnection oleDbConnection = new OleDbConnection(oleDbConnectionStringBuilder.ToString()))
				{
					oleDbConnection.Open();
				}
			}
			catch (SEHException)
			{
				return true;
			}
			catch (Exception ex)
			{
				if (!SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
			}
			return false;
		}

		// Token: 0x06006BEF RID: 27631 RVA: 0x001738A0 File Offset: 0x00171AA0
		private static void WorkaroundInvalidAceConfiguration()
		{
			try
			{
				object value = Registry.GetValue("HKEY_CLASSES_ROOT\\CLSID\\{3BE786A0-0366-4F5C-9434-25CF162E475E}\\InprocServer32", null, null);
				if (value is string)
				{
					AccessDatabaseEngine.LoadLibrary(AccessDatabaseEngine.GetAceWstrPath(Path.GetDirectoryName((string)value)));
				}
			}
			catch (SecurityException)
			{
			}
			catch (IOException)
			{
			}
		}

		// Token: 0x06006BF0 RID: 27632 RVA: 0x001738FC File Offset: 0x00171AFC
		private static string GetAceWstrPath(string acePath)
		{
			string text = Path.Combine(acePath, CultureInfo.CurrentCulture.LCID.ToString(CultureInfo.InvariantCulture) + "\\ACEWSTR.DLL");
			if (File.Exists(text))
			{
				return text;
			}
			text = Path.Combine(acePath, "1033\\ACEWSTR.DLL");
			if (File.Exists(text))
			{
				return text;
			}
			string[] directories = Directory.GetDirectories(acePath);
			for (int i = 0; i < directories.Length; i++)
			{
				text = Path.Combine(directories[i], "ACEWSTR.DLL");
				if (File.Exists(text))
				{
					return text;
				}
			}
			return string.Empty;
		}

		// Token: 0x06006BF1 RID: 27633
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern IntPtr LoadLibrary(string handle);

		// Token: 0x04003C25 RID: 15397
		private static bool initialized;

		// Token: 0x04003C26 RID: 15398
		private const string providerName = "Microsoft.ACE.OLEDB.12.0";
	}
}
