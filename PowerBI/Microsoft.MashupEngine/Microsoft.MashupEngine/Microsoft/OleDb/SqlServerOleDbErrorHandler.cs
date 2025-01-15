using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001F2E RID: 7982
	public class SqlServerOleDbErrorHandler : IOleDbCustomErrorHandler
	{
		// Token: 0x17002FC8 RID: 12232
		// (get) Token: 0x0600C39C RID: 50076 RVA: 0x00273014 File Offset: 0x00271214
		private static Guid IID_ISQLServerErrorInfo
		{
			get
			{
				return new Guid("5CF4CA12-EF21-11d0-97E7-00C04FC2AD98");
			}
		}

		// Token: 0x0600C39D RID: 50077 RVA: 0x000020FD File Offset: 0x000002FD
		private SqlServerOleDbErrorHandler()
		{
		}

		// Token: 0x17002FC9 RID: 12233
		// (get) Token: 0x0600C39E RID: 50078 RVA: 0x00273020 File Offset: 0x00271220
		public Guid InterfaceID
		{
			get
			{
				return SqlServerOleDbErrorHandler.IID_ISQLServerErrorInfo;
			}
		}

		// Token: 0x0600C39F RID: 50079 RVA: 0x00273028 File Offset: 0x00271228
		public OleDbError GetError(string source, string message, object customObject)
		{
			IntPtr zero = IntPtr.Zero;
			IntPtr zero2 = IntPtr.Zero;
			OleDbError oleDbError;
			try
			{
				SqlServerOleDbErrorHandler.ISQLServerErrorInfo isqlserverErrorInfo = customObject as SqlServerOleDbErrorHandler.ISQLServerErrorInfo;
				if (isqlserverErrorInfo != null && isqlserverErrorInfo.GetSQLInfo(out zero, out zero2) >= 0 && zero != IntPtr.Zero)
				{
					SqlServerOleDbErrorHandler.SSERRORINFO sserrorinfo = MarshalHelpers.PtrToStructure<SqlServerOleDbErrorHandler.SSERRORINFO>(zero);
					oleDbError = new SqlServerOleDbError(source, message, sserrorinfo.lNative, sserrorinfo.bClass, sserrorinfo.bState);
				}
				else
				{
					oleDbError = new OleDbError(source, message, null, -1);
				}
			}
			finally
			{
				if (zero != IntPtr.Zero)
				{
					Marshal.FreeCoTaskMem(zero);
				}
				if (zero2 != IntPtr.Zero)
				{
					Marshal.FreeCoTaskMem(zero2);
				}
			}
			return oleDbError;
		}

		// Token: 0x0400649A RID: 25754
		public static readonly IOleDbCustomErrorHandler Instance = new SqlServerOleDbErrorHandler();

		// Token: 0x02001F2F RID: 7983
		[StructLayout(LayoutKind.Sequential, Pack = 8)]
		private struct SSERRORINFO
		{
			// Token: 0x0400649B RID: 25755
			[MarshalAs(UnmanagedType.LPWStr)]
			public string pwszMessage;

			// Token: 0x0400649C RID: 25756
			[MarshalAs(UnmanagedType.LPWStr)]
			public string pwszServer;

			// Token: 0x0400649D RID: 25757
			[MarshalAs(UnmanagedType.LPWStr)]
			public string pwszProcedure;

			// Token: 0x0400649E RID: 25758
			public int lNative;

			// Token: 0x0400649F RID: 25759
			public byte bState;

			// Token: 0x040064A0 RID: 25760
			public byte bClass;

			// Token: 0x040064A1 RID: 25761
			public short wLineNumber;
		}

		// Token: 0x02001F30 RID: 7984
		[Guid("5CF4CA12-EF21-11d0-97E7-00C04FC2AD98")]
		[ComVisible(true)]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		private interface ISQLServerErrorInfo
		{
			// Token: 0x0600C3A1 RID: 50081
			[PreserveSig]
			int GetSQLInfo(out IntPtr ppErrorInfo, out IntPtr ppStringsBuffer);
		}
	}
}
