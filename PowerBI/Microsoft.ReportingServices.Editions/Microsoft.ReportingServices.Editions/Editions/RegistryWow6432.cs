using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.ReportingServices.Editions
{
	// Token: 0x02000019 RID: 25
	[CLSCompliant(false)]
	public static class RegistryWow6432
	{
		// Token: 0x06000068 RID: 104
		[DllImport("Advapi32.dll")]
		private static extern uint RegOpenKeyEx(UIntPtr hKey, string lpSubKey, uint ulOptions, int samDesired, out int phkResult);

		// Token: 0x06000069 RID: 105
		[DllImport("Advapi32.dll")]
		private static extern uint RegCloseKey(int hKey);

		// Token: 0x0600006A RID: 106
		[DllImport("advapi32.dll")]
		public static extern int RegQueryValueEx(int hKey, string lpValueName, int lpReserved, ref uint lpType, StringBuilder lpData, ref uint lpcbData);

		// Token: 0x0600006B RID: 107 RVA: 0x00003474 File Offset: 0x00001674
		public static byte[] GetRegKey64(UIntPtr inHive, string inKeyName, string inPropertyName)
		{
			return RegistryWow6432.GetRegKey64(inHive, inKeyName, RegSam.Wow6464Key, inPropertyName);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003483 File Offset: 0x00001683
		public static byte[] GetRegKey32(UIntPtr inHive, string inKeyName, string inPropertyName)
		{
			return RegistryWow6432.GetRegKey64(inHive, inKeyName, RegSam.Wow6432Key, inPropertyName);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003494 File Offset: 0x00001694
		public static byte[] GetRegKey64(UIntPtr inHive, string inKeyName, RegSam in32Or64Key, string inPropertyName)
		{
			int num = 0;
			byte[] array;
			try
			{
				uint num2 = RegistryWow6432.RegOpenKeyEx(RegHive.HkeyLocalMachine, inKeyName, 0U, (int)(RegSam.QueryValue | in32Or64Key), out num);
				if (num2 != 0U)
				{
					array = null;
				}
				else
				{
					uint num3 = 0U;
					uint num4 = 1024U;
					StringBuilder stringBuilder = new StringBuilder(1024);
					RegistryWow6432.RegQueryValueEx(num, inPropertyName, 0, ref num3, stringBuilder, ref num4);
					array = Encoding.ASCII.GetBytes(stringBuilder.ToString());
				}
			}
			finally
			{
				if (num != 0)
				{
					RegistryWow6432.RegCloseKey(num);
				}
			}
			return array;
		}
	}
}
