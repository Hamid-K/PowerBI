using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using Microsoft.Data.Common;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000E4 RID: 228
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.None)]
	[Guid("afef65ad-4577-447a-a148-83acadd3d4b9")]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public sealed class SQLDebugging : ISQLDebug
	{
		// Token: 0x0600112F RID: 4399 RVA: 0x0003F338 File Offset: 0x0003D538
		private IntPtr CreateSD(ref IntPtr pDacl)
		{
			IntPtr intPtr = IntPtr.Zero;
			IntPtr zero = IntPtr.Zero;
			IntPtr zero2 = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			bool flag = false;
			intPtr2 = Marshal.AllocHGlobal(6);
			if (!(intPtr2 == IntPtr.Zero))
			{
				Marshal.WriteInt32(intPtr2, 0, 0);
				Marshal.WriteByte(intPtr2, 4, 0);
				Marshal.WriteByte(intPtr2, 5, 5);
				flag = NativeMethods.AllocateAndInitializeSid(intPtr2, 1, 11, 0, 0, 0, 0, 0, 0, 0, ref zero);
				if (flag && !(zero == IntPtr.Zero))
				{
					flag = NativeMethods.AllocateAndInitializeSid(intPtr2, 2, 32, 544, 0, 0, 0, 0, 0, 0, ref zero2);
					if (flag && !(zero2 == IntPtr.Zero))
					{
						flag = false;
						intPtr = Marshal.AllocHGlobal(20);
						if (!(intPtr == IntPtr.Zero))
						{
							for (int i = 0; i < 20; i++)
							{
								Marshal.WriteByte(intPtr, i, 0);
							}
							int num = 44 + NativeMethods.GetLengthSid(zero) + NativeMethods.GetLengthSid(zero2);
							pDacl = Marshal.AllocHGlobal(num);
							if (!(pDacl == IntPtr.Zero) && NativeMethods.InitializeAcl(pDacl, num, 2) && NativeMethods.AddAccessDeniedAce(pDacl, 2, 262144, zero) && NativeMethods.AddAccessAllowedAce(pDacl, 2, 2147483648U, zero) && NativeMethods.AddAccessAllowedAce(pDacl, 2, 268435456U, zero2) && NativeMethods.InitializeSecurityDescriptor(intPtr, 1) && NativeMethods.SetSecurityDescriptorDacl(intPtr, true, pDacl, false))
							{
								flag = true;
							}
						}
					}
				}
			}
			if (intPtr2 != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(intPtr2);
			}
			if (zero2 != IntPtr.Zero)
			{
				NativeMethods.FreeSid(zero2);
			}
			if (zero != IntPtr.Zero)
			{
				NativeMethods.FreeSid(zero);
			}
			if (flag)
			{
				return intPtr;
			}
			if (intPtr != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return IntPtr.Zero;
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x0003F4F8 File Offset: 0x0003D6F8
		bool ISQLDebug.SQLDebug(int dwpidDebugger, int dwpidDebuggee, [MarshalAs(UnmanagedType.LPStr)] string pszMachineName, [MarshalAs(UnmanagedType.LPStr)] string pszSDIDLLName, int dwOption, int cbData, byte[] rgbData)
		{
			bool flag = false;
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			IntPtr intPtr3 = IntPtr.Zero;
			IntPtr intPtr4 = IntPtr.Zero;
			IntPtr zero = IntPtr.Zero;
			if (pszMachineName == null || pszSDIDLLName == null)
			{
				return false;
			}
			if (pszMachineName.Length > 32 || pszSDIDLLName.Length > 16)
			{
				return false;
			}
			Encoding encoding = Encoding.GetEncoding(1252);
			byte[] bytes = encoding.GetBytes(pszMachineName);
			byte[] bytes2 = encoding.GetBytes(pszSDIDLLName);
			if (rgbData != null && cbData > 255)
			{
				return false;
			}
			string text;
			if (ADP.s_isPlatformNT5)
			{
				text = "Global\\SqlClientSSDebug";
			}
			else
			{
				text = "SqlClientSSDebug";
			}
			text += dwpidDebuggee.ToString(CultureInfo.InvariantCulture);
			intPtr3 = this.CreateSD(ref zero);
			intPtr4 = Marshal.AllocHGlobal(12);
			if (intPtr3 == IntPtr.Zero || intPtr4 == IntPtr.Zero)
			{
				return false;
			}
			Marshal.WriteInt32(intPtr4, 0, 12);
			Marshal.WriteIntPtr(intPtr4, 4, intPtr3);
			Marshal.WriteInt32(intPtr4, 8, 0);
			intPtr = NativeMethods.CreateFileMappingA(ADP.s_invalidPtr, intPtr4, 4, 0, Marshal.SizeOf(typeof(MEMMAP)), text);
			if (!(IntPtr.Zero == intPtr))
			{
				intPtr2 = NativeMethods.MapViewOfFile(intPtr, 6, 0, 0, IntPtr.Zero);
				if (!(IntPtr.Zero == intPtr2))
				{
					int num = 0;
					Marshal.WriteInt32(intPtr2, num, dwpidDebugger);
					num += 4;
					Marshal.WriteInt32(intPtr2, num, dwOption);
					num += 4;
					Marshal.Copy(bytes, 0, ADP.IntPtrOffset(intPtr2, num), bytes.Length);
					num += 32;
					Marshal.Copy(bytes2, 0, ADP.IntPtrOffset(intPtr2, num), bytes2.Length);
					num += 16;
					Marshal.WriteInt32(intPtr2, num, cbData);
					num += 4;
					if (rgbData != null)
					{
						Marshal.Copy(rgbData, 0, ADP.IntPtrOffset(intPtr2, num), cbData);
					}
					NativeMethods.UnmapViewOfFile(intPtr2);
					flag = true;
				}
			}
			if (!flag && intPtr != IntPtr.Zero)
			{
				NativeMethods.CloseHandle(intPtr);
			}
			if (intPtr4 != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(intPtr4);
			}
			if (intPtr3 != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(intPtr3);
			}
			if (zero != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(zero);
			}
			return flag;
		}

		// Token: 0x0400070F RID: 1807
		private const int STANDARD_RIGHTS_REQUIRED = 983040;

		// Token: 0x04000710 RID: 1808
		private const int DELETE = 65536;

		// Token: 0x04000711 RID: 1809
		private const int READ_CONTROL = 131072;

		// Token: 0x04000712 RID: 1810
		private const int WRITE_DAC = 262144;

		// Token: 0x04000713 RID: 1811
		private const int WRITE_OWNER = 524288;

		// Token: 0x04000714 RID: 1812
		private const int SYNCHRONIZE = 1048576;

		// Token: 0x04000715 RID: 1813
		private const int FILE_ALL_ACCESS = 2032127;

		// Token: 0x04000716 RID: 1814
		private const uint GENERIC_READ = 2147483648U;

		// Token: 0x04000717 RID: 1815
		private const uint GENERIC_WRITE = 1073741824U;

		// Token: 0x04000718 RID: 1816
		private const uint GENERIC_EXECUTE = 536870912U;

		// Token: 0x04000719 RID: 1817
		private const uint GENERIC_ALL = 268435456U;

		// Token: 0x0400071A RID: 1818
		private const int SECURITY_DESCRIPTOR_REVISION = 1;

		// Token: 0x0400071B RID: 1819
		private const int ACL_REVISION = 2;

		// Token: 0x0400071C RID: 1820
		private const int SECURITY_AUTHENTICATED_USER_RID = 11;

		// Token: 0x0400071D RID: 1821
		private const int SECURITY_LOCAL_SYSTEM_RID = 18;

		// Token: 0x0400071E RID: 1822
		private const int SECURITY_BUILTIN_DOMAIN_RID = 32;

		// Token: 0x0400071F RID: 1823
		private const int SECURITY_WORLD_RID = 0;

		// Token: 0x04000720 RID: 1824
		private const byte SECURITY_NT_AUTHORITY = 5;

		// Token: 0x04000721 RID: 1825
		private const int DOMAIN_GROUP_RID_ADMINS = 512;

		// Token: 0x04000722 RID: 1826
		private const int DOMAIN_ALIAS_RID_ADMINS = 544;

		// Token: 0x04000723 RID: 1827
		private const int sizeofSECURITY_ATTRIBUTES = 12;

		// Token: 0x04000724 RID: 1828
		private const int sizeofSECURITY_DESCRIPTOR = 20;

		// Token: 0x04000725 RID: 1829
		private const int sizeofACCESS_ALLOWED_ACE = 12;

		// Token: 0x04000726 RID: 1830
		private const int sizeofACCESS_DENIED_ACE = 12;

		// Token: 0x04000727 RID: 1831
		private const int sizeofSID_IDENTIFIER_AUTHORITY = 6;

		// Token: 0x04000728 RID: 1832
		private const int sizeofACL = 8;
	}
}
