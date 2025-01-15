using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001F13 RID: 7955
	public static class Com
	{
		// Token: 0x0600C2CD RID: 49869 RVA: 0x00270C0C File Offset: 0x0026EE0C
		public static string ProgIDFromCLSID(Guid clsid)
		{
			string text;
			if (Com.ProgIDFromCLSID(ref clsid, out text) < 0)
			{
				return null;
			}
			return text;
		}

		// Token: 0x0600C2CE RID: 49870
		[DllImport("ole32.dll")]
		private static extern int ProgIDFromCLSID([In] ref Guid clsid, [MarshalAs(UnmanagedType.LPWStr)] out string lplpszProgID);
	}
}
