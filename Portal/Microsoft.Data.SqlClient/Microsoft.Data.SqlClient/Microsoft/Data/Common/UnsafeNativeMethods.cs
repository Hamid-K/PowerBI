using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Data.Common
{
	// Token: 0x02000186 RID: 390
	[SuppressUnmanagedCodeSecurity]
	internal static class UnsafeNativeMethods
	{
		// Token: 0x06001D18 RID: 7448
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		internal static extern uint GetEffectiveRightsFromAclW(byte[] pAcl, ref UnsafeNativeMethods.Trustee pTrustee, out uint pAccessMask);

		// Token: 0x06001D19 RID: 7449
		[DllImport("advapi32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool CheckTokenMembership(IntPtr tokenHandle, byte[] sidToCheck, out bool isMember);

		// Token: 0x06001D1A RID: 7450
		[DllImport("advapi32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool ConvertSidToStringSidW(IntPtr sid, out IntPtr stringSid);

		// Token: 0x06001D1B RID: 7451
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern int CreateWellKnownSid(int sidType, byte[] domainSid, [Out] byte[] resultSid, ref uint resultSidLength);

		// Token: 0x06001D1C RID: 7452
		[DllImport("advapi32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool GetTokenInformation(IntPtr tokenHandle, uint token_class, IntPtr tokenStruct, uint tokenInformationLength, ref uint tokenString);

		// Token: 0x06001D1D RID: 7453
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		internal static extern int lstrlenW(IntPtr ptr);

		// Token: 0x02000281 RID: 641
		[Guid("00000567-0000-0010-8000-00AA006D2EA4")]
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		[SuppressUnmanagedCodeSecurity]
		[ComImport]
		internal interface ADORecordConstruction
		{
			// Token: 0x06001F4D RID: 8013
			[return: MarshalAs(UnmanagedType.Interface)]
			object get_Row();
		}

		// Token: 0x02000282 RID: 642
		[Guid("00000283-0000-0010-8000-00AA006D2EA4")]
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		[SuppressUnmanagedCodeSecurity]
		[ComImport]
		internal interface ADORecordsetConstruction
		{
			// Token: 0x06001F4E RID: 8014
			[return: MarshalAs(UnmanagedType.Interface)]
			object get_Rowset();

			// Token: 0x06001F4F RID: 8015
			[Obsolete("not used", true)]
			void put_Rowset();

			// Token: 0x06001F50 RID: 8016
			IntPtr get_Chapter();
		}

		// Token: 0x02000283 RID: 643
		[Guid("0C733A64-2A1C-11CE-ADE5-00AA0044773D")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[SuppressUnmanagedCodeSecurity]
		[ComImport]
		internal interface ICommandWithParameters
		{
			// Token: 0x06001F51 RID: 8017
			[Obsolete("not used", true)]
			void GetParameterInfo();

			// Token: 0x06001F52 RID: 8018
			[Obsolete("not used", true)]
			void MapParameterNames();
		}

		// Token: 0x02000284 RID: 644
		[Guid("2206CCB1-19C1-11D1-89E0-00C04FD7A829")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[SuppressUnmanagedCodeSecurity]
		[ComImport]
		internal interface IDataInitialize
		{
		}

		// Token: 0x02000285 RID: 645
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct Trustee
		{
			// Token: 0x06001F53 RID: 8019 RVA: 0x0007FCBD File Offset: 0x0007DEBD
			internal Trustee(string name)
			{
				this._pMultipleTrustee = IntPtr.Zero;
				this._MultipleTrusteeOperation = 0;
				this._TrusteeForm = 1;
				this._TrusteeType = 1;
				this._name = name;
			}

			// Token: 0x040017AF RID: 6063
			internal IntPtr _pMultipleTrustee;

			// Token: 0x040017B0 RID: 6064
			internal int _MultipleTrusteeOperation;

			// Token: 0x040017B1 RID: 6065
			internal int _TrusteeForm;

			// Token: 0x040017B2 RID: 6066
			internal int _TrusteeType;

			// Token: 0x040017B3 RID: 6067
			[MarshalAs(UnmanagedType.LPTStr)]
			internal string _name;
		}
	}
}
