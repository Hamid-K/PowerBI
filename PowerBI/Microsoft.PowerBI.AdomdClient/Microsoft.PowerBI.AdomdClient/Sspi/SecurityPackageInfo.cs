using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Microsoft.AnalysisServices.AdomdClient.Interop;

namespace Microsoft.AnalysisServices.AdomdClient.Sspi
{
	// Token: 0x0200011E RID: 286
	internal sealed class SecurityPackageInfo
	{
		// Token: 0x06000F6F RID: 3951 RVA: 0x000351C0 File Offset: 0x000333C0
		private SecurityPackageInfo(IntPtr ptr)
		{
			this.capabilities = (SecurityPackageCapabilities)Marshal.ReadInt32(ptr, (int)Marshal.OffsetOf(typeof(SecPkgInfo), "fCapabilities"));
			this.version = Marshal.ReadInt16(ptr, (int)Marshal.OffsetOf(typeof(SecPkgInfo), "wVersion"));
			this.rpcId = Marshal.ReadInt16(ptr, (int)Marshal.OffsetOf(typeof(SecPkgInfo), "wRPCID"));
			this.maxTokenLength = Marshal.ReadInt32(ptr, (int)Marshal.OffsetOf(typeof(SecPkgInfo), "cbMaxToken"));
			IntPtr intPtr = Marshal.ReadIntPtr(ptr, (int)Marshal.OffsetOf(typeof(SecPkgInfo), "Name"));
			if (intPtr != IntPtr.Zero)
			{
				this.name = Marshal.PtrToStringUni(intPtr);
			}
			intPtr = Marshal.ReadIntPtr(ptr, (int)Marshal.OffsetOf(typeof(SecPkgInfo), "Comment"));
			if (intPtr != IntPtr.Zero)
			{
				this.comment = Marshal.PtrToStringUni(intPtr);
			}
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06000F70 RID: 3952 RVA: 0x000352D9 File Offset: 0x000334D9
		public SecurityPackageCapabilities Capabilities
		{
			get
			{
				return this.capabilities;
			}
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06000F71 RID: 3953 RVA: 0x000352E1 File Offset: 0x000334E1
		public short Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06000F72 RID: 3954 RVA: 0x000352E9 File Offset: 0x000334E9
		public short RpcId
		{
			get
			{
				return this.rpcId;
			}
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06000F73 RID: 3955 RVA: 0x000352F1 File Offset: 0x000334F1
		public int MaxTokenLength
		{
			get
			{
				return this.maxTokenLength;
			}
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06000F74 RID: 3956 RVA: 0x000352F9 File Offset: 0x000334F9
		public string Name
		{
			get
			{
				return this.name ?? string.Empty;
			}
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06000F75 RID: 3957 RVA: 0x0003530A File Offset: 0x0003350A
		public string Comment
		{
			get
			{
				return this.comment ?? string.Empty;
			}
		}

		// Token: 0x06000F76 RID: 3958 RVA: 0x0003531C File Offset: 0x0003351C
		public static SecurityPackageInfo[] EnumerateSecurityPackages()
		{
			uint num2;
			IntPtr intPtr;
			int num = NativeMethods.EnumerateSecurityPackages(out num2, out intPtr);
			if (num != 0)
			{
				throw new Win32Exception(num);
			}
			SecurityPackageInfo[] array2;
			try
			{
				SecurityPackageInfo[] array = new SecurityPackageInfo[num2];
				int num3 = 0;
				while ((long)num3 < (long)((ulong)num2))
				{
					array[num3] = new SecurityPackageInfo(InteropHelper.Add(intPtr, num3 * SecPkgInfo.Size));
					num3++;
				}
				array2 = array;
			}
			finally
			{
				NativeMethods.FreeContextBuffer(intPtr);
			}
			return array2;
		}

		// Token: 0x04000A1A RID: 2586
		private readonly SecurityPackageCapabilities capabilities;

		// Token: 0x04000A1B RID: 2587
		private readonly short version;

		// Token: 0x04000A1C RID: 2588
		private readonly short rpcId;

		// Token: 0x04000A1D RID: 2589
		private readonly int maxTokenLength;

		// Token: 0x04000A1E RID: 2590
		private readonly string name;

		// Token: 0x04000A1F RID: 2591
		private readonly string comment;
	}
}
