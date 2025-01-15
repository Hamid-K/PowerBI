using System;
using System.Runtime.InteropServices;

namespace Microsoft.ReportingServices.Editions
{
	// Token: 0x02000008 RID: 8
	[CLSCompliant(false)]
	public class PidgenXInterop
	{
		// Token: 0x0600001D RID: 29
		[DllImport("PidGenX.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern uint PidGenX([MarshalAs(UnmanagedType.LPWStr)] string pwszProductKey, [MarshalAs(UnmanagedType.LPWStr)] string pwszConfig, [MarshalAs(UnmanagedType.LPWStr)] string pwszMpc, [MarshalAs(UnmanagedType.LPWStr)] string pwszOemId, [Out] PidgenXInterop.DIGITALPID2 pPID2, [In] [Out] PidgenXInterop.DIGITALPID3 pPID3, [In] [Out] PidgenXInterop.DIGITALPID4 pPID4);

		// Token: 0x0400002F RID: 47
		public static uint INVALID_CONFIG_FORMAT_HR = 2315321345U;

		// Token: 0x04000030 RID: 48
		public static uint INVALID_KEY_HR = 2315321601U;

		// Token: 0x02000020 RID: 32
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4)]
		public class DIGITALPID4
		{
			// Token: 0x0400008D RID: 141
			public uint dwLength;

			// Token: 0x0400008E RID: 142
			public ushort wVersionMajor;

			// Token: 0x0400008F RID: 143
			public ushort wVersionMinor;

			// Token: 0x04000090 RID: 144
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
			public string szPid2Ex;

			// Token: 0x04000091 RID: 145
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
			public string szSku;

			// Token: 0x04000092 RID: 146
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
			public string szOemId;

			// Token: 0x04000093 RID: 147
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			public string szEditionId;

			// Token: 0x04000094 RID: 148
			public byte bIsUpgrade;

			// Token: 0x04000095 RID: 149
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
			public byte[] abReserved;

			// Token: 0x04000096 RID: 150
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
			public byte[] abCdKey;

			// Token: 0x04000097 RID: 151
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
			public byte[] abCdKeySHA256Hash;

			// Token: 0x04000098 RID: 152
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
			public byte[] abSHA256Hash;

			// Token: 0x04000099 RID: 153
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
			public string szPartNumber;

			// Token: 0x0400009A RID: 154
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
			public string szProductKeyType;

			// Token: 0x0400009B RID: 155
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
			public string szEulaType;
		}

		// Token: 0x02000021 RID: 33
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4)]
		public class DIGITALPID2
		{
			// Token: 0x0400009C RID: 156
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 24)]
			public string szPid2;
		}

		// Token: 0x02000022 RID: 34
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		public class DIGITALPID3
		{
			// Token: 0x0400009D RID: 157
			public uint dwLength;

			// Token: 0x0400009E RID: 158
			public short wVersionMajor;

			// Token: 0x0400009F RID: 159
			public short wVersionMinor;

			// Token: 0x040000A0 RID: 160
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
			public char[] szPid2;

			// Token: 0x040000A1 RID: 161
			public uint dwKeyIdx;

			// Token: 0x040000A2 RID: 162
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
			public char[] szSku;

			// Token: 0x040000A3 RID: 163
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
			public byte[] abCdKey;

			// Token: 0x040000A4 RID: 164
			public uint dwCloneStatus;

			// Token: 0x040000A5 RID: 165
			public uint dwTime;

			// Token: 0x040000A6 RID: 166
			public uint dwRandom;

			// Token: 0x040000A7 RID: 167
			public uint dwlt;

			// Token: 0x040000A8 RID: 168
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
			public uint[] adwLicenseData;

			// Token: 0x040000A9 RID: 169
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
			public char[] szOemId;

			// Token: 0x040000AA RID: 170
			public uint dwBundleId;

			// Token: 0x040000AB RID: 171
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
			public char[] aszHardwareIdStatic;

			// Token: 0x040000AC RID: 172
			public uint dwHardwareIdTypeStatic;

			// Token: 0x040000AD RID: 173
			public uint dwBiosChecksumStatic;

			// Token: 0x040000AE RID: 174
			public uint dwVolSerStatic;

			// Token: 0x040000AF RID: 175
			public uint dwTotalRamStatic;

			// Token: 0x040000B0 RID: 176
			public uint dwVideoBiosChecksumStatic;

			// Token: 0x040000B1 RID: 177
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
			public char[] aszHardwareIdDynamic;

			// Token: 0x040000B2 RID: 178
			public uint dwHardwareIdTypeDynamic;

			// Token: 0x040000B3 RID: 179
			public uint dwBiosChecksumDynamic;

			// Token: 0x040000B4 RID: 180
			public uint dwVolSerDynamic;

			// Token: 0x040000B5 RID: 181
			public uint dwTotalRamDynamic;

			// Token: 0x040000B6 RID: 182
			public uint dwVideoBiosChecksumDynamic;

			// Token: 0x040000B7 RID: 183
			public uint dwCrc32;
		}
	}
}
