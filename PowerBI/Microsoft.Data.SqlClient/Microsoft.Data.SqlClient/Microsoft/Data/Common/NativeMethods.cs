using System;
using System.Runtime.InteropServices;

namespace Microsoft.Data.Common
{
	// Token: 0x02000184 RID: 388
	internal static class NativeMethods
	{
		// Token: 0x06001CF8 RID: 7416
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		internal static extern IntPtr MapViewOfFile(IntPtr hFileMappingObject, int dwDesiredAccess, int dwFileOffsetHigh, int dwFileOffsetLow, IntPtr dwNumberOfBytesToMap);

		// Token: 0x06001CF9 RID: 7417
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true)]
		internal static extern IntPtr OpenFileMappingA(int dwDesiredAccess, bool bInheritHandle, [MarshalAs(UnmanagedType.LPStr)] string lpName);

		// Token: 0x06001CFA RID: 7418
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true)]
		internal static extern IntPtr CreateFileMappingA(IntPtr hFile, IntPtr pAttr, int flProtect, int dwMaximumSizeHigh, int dwMaximumSizeLow, [MarshalAs(UnmanagedType.LPStr)] string lpName);

		// Token: 0x06001CFB RID: 7419
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		internal static extern bool UnmapViewOfFile(IntPtr lpBaseAddress);

		// Token: 0x06001CFC RID: 7420
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		internal static extern bool CloseHandle(IntPtr handle);

		// Token: 0x06001CFD RID: 7421
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		internal static extern bool AllocateAndInitializeSid(IntPtr pIdentifierAuthority, byte nSubAuthorityCount, int dwSubAuthority0, int dwSubAuthority1, int dwSubAuthority2, int dwSubAuthority3, int dwSubAuthority4, int dwSubAuthority5, int dwSubAuthority6, int dwSubAuthority7, ref IntPtr pSid);

		// Token: 0x06001CFE RID: 7422
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		internal static extern int GetLengthSid(IntPtr pSid);

		// Token: 0x06001CFF RID: 7423
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		internal static extern bool InitializeAcl(IntPtr pAcl, int nAclLength, int dwAclRevision);

		// Token: 0x06001D00 RID: 7424
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		internal static extern bool AddAccessDeniedAce(IntPtr pAcl, int dwAceRevision, int AccessMask, IntPtr pSid);

		// Token: 0x06001D01 RID: 7425
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		internal static extern bool AddAccessAllowedAce(IntPtr pAcl, int dwAceRevision, uint AccessMask, IntPtr pSid);

		// Token: 0x06001D02 RID: 7426
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		internal static extern bool InitializeSecurityDescriptor(IntPtr pSecurityDescriptor, int dwRevision);

		// Token: 0x06001D03 RID: 7427
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		internal static extern bool SetSecurityDescriptorDacl(IntPtr pSecurityDescriptor, bool bDaclPresent, IntPtr pDacl, bool bDaclDefaulted);

		// Token: 0x06001D04 RID: 7428
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		internal static extern IntPtr FreeSid(IntPtr pSid);

		// Token: 0x0200027F RID: 639
		[Guid("0C733A5E-2A1C-11CE-ADE5-00AA0044773D")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		internal interface ITransactionJoin
		{
			// Token: 0x06001F49 RID: 8009
			[Obsolete("not used", true)]
			[PreserveSig]
			int GetOptionsObject();

			// Token: 0x06001F4A RID: 8010
			void JoinTransaction([MarshalAs(UnmanagedType.Interface)] [In] object punkTransactionCoord, [In] int isoLevel, [In] int isoFlags, [In] IntPtr pOtherOptions);
		}
	}
}
