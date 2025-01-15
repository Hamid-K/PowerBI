using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000028 RID: 40
	internal sealed class SafeSidPtr : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000128 RID: 296 RVA: 0x00005765 File Offset: 0x00003965
		private SafeSidPtr()
			: base(true)
		{
		}

		// Token: 0x06000129 RID: 297 RVA: 0x0000576E File Offset: 0x0000396E
		private SafeSidPtr(bool ownsHandle)
			: base(ownsHandle)
		{
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00005778 File Offset: 0x00003978
		internal static bool AllocateAndInitializeSid(byte nSubAuthorityCount, uint nSubAuthority0, uint nSubAuthority1, uint nSubAuthority2, uint nSubAuthority3, uint nSubAuthority4, uint nSubAuthority5, uint nSubAuthority6, uint nSubAuthority7, out SafeSidPtr pSid)
		{
			SafeLocalFree safeLocalFree = null;
			bool flag;
			try
			{
				SafeSidPtr.SID_IDENTIFIER_AUTHORITY sid_IDENTIFIER_AUTHORITY = new SafeSidPtr.SID_IDENTIFIER_AUTHORITY
				{
					m_Value0 = SafeSidPtr.SECURITY_NT_AUTHORITY[0],
					m_Value1 = SafeSidPtr.SECURITY_NT_AUTHORITY[1],
					m_Value2 = SafeSidPtr.SECURITY_NT_AUTHORITY[2],
					m_Value3 = SafeSidPtr.SECURITY_NT_AUTHORITY[3],
					m_Value4 = SafeSidPtr.SECURITY_NT_AUTHORITY[4],
					m_Value5 = SafeSidPtr.SECURITY_NT_AUTHORITY[5]
				};
				safeLocalFree = SafeLocalFree.LocalAlloc(Marshal.SizeOf<SafeSidPtr.SID_IDENTIFIER_AUTHORITY>(sid_IDENTIFIER_AUTHORITY));
				Marshal.StructureToPtr<SafeSidPtr.SID_IDENTIFIER_AUTHORITY>(sid_IDENTIFIER_AUTHORITY, safeLocalFree.DangerousGetHandle(), true);
				flag = NativeMemoryMethods.AllocateAndInitializeSid(safeLocalFree, nSubAuthorityCount, nSubAuthority0, nSubAuthority1, nSubAuthority2, nSubAuthority3, nSubAuthority4, nSubAuthority5, nSubAuthority6, nSubAuthority7, out pSid);
			}
			finally
			{
				if (safeLocalFree != null)
				{
					safeLocalFree.Close();
				}
			}
			return flag;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00005830 File Offset: 0x00003A30
		protected override bool ReleaseHandle()
		{
			return NativeMemoryMethods.FreeSid(this.handle) == IntPtr.Zero;
		}

		// Token: 0x0400009C RID: 156
		internal static readonly SafeSidPtr Zero = new SafeSidPtr(false);

		// Token: 0x0400009D RID: 157
		private static byte[] SECURITY_NT_AUTHORITY = new byte[] { 0, 0, 0, 0, 0, 5 };

		// Token: 0x0400009E RID: 158
		internal const uint SECURITY_BUILTIN_DOMAIN_RID = 32U;

		// Token: 0x0400009F RID: 159
		internal const uint DOMAIN_ALIAS_RID_ADMINS = 544U;

		// Token: 0x040000A0 RID: 160
		internal const uint SECURITY_LOCAL_SYSTEM_RID = 18U;

		// Token: 0x020000E0 RID: 224
		private struct SID_IDENTIFIER_AUTHORITY
		{
			// Token: 0x04000499 RID: 1177
			internal byte m_Value0;

			// Token: 0x0400049A RID: 1178
			internal byte m_Value1;

			// Token: 0x0400049B RID: 1179
			internal byte m_Value2;

			// Token: 0x0400049C RID: 1180
			internal byte m_Value3;

			// Token: 0x0400049D RID: 1181
			internal byte m_Value4;

			// Token: 0x0400049E RID: 1182
			internal byte m_Value5;
		}
	}
}
