using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Microsoft.AnalysisServices.Interop;

namespace Microsoft.AnalysisServices.Sspi
{
	// Token: 0x02000113 RID: 275
	internal sealed class SecurityPackageInfo
	{
		// Token: 0x0600100A RID: 4106 RVA: 0x00037DF4 File Offset: 0x00035FF4
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

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x0600100B RID: 4107 RVA: 0x00037F0D File Offset: 0x0003610D
		public SecurityPackageCapabilities Capabilities
		{
			get
			{
				return this.capabilities;
			}
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x0600100C RID: 4108 RVA: 0x00037F15 File Offset: 0x00036115
		public short Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x0600100D RID: 4109 RVA: 0x00037F1D File Offset: 0x0003611D
		public short RpcId
		{
			get
			{
				return this.rpcId;
			}
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x0600100E RID: 4110 RVA: 0x00037F25 File Offset: 0x00036125
		public int MaxTokenLength
		{
			get
			{
				return this.maxTokenLength;
			}
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x0600100F RID: 4111 RVA: 0x00037F2D File Offset: 0x0003612D
		public string Name
		{
			get
			{
				return this.name ?? string.Empty;
			}
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06001010 RID: 4112 RVA: 0x00037F3E File Offset: 0x0003613E
		public string Comment
		{
			get
			{
				return this.comment ?? string.Empty;
			}
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x00037F50 File Offset: 0x00036150
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

		// Token: 0x040009E0 RID: 2528
		private readonly SecurityPackageCapabilities capabilities;

		// Token: 0x040009E1 RID: 2529
		private readonly short version;

		// Token: 0x040009E2 RID: 2530
		private readonly short rpcId;

		// Token: 0x040009E3 RID: 2531
		private readonly int maxTokenLength;

		// Token: 0x040009E4 RID: 2532
		private readonly string name;

		// Token: 0x040009E5 RID: 2533
		private readonly string comment;
	}
}
