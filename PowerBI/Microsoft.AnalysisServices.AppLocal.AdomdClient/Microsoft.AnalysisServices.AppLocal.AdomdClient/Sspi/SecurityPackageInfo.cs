using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Microsoft.AnalysisServices.AdomdClient.Interop;

namespace Microsoft.AnalysisServices.AdomdClient.Sspi
{
	// Token: 0x0200011E RID: 286
	internal sealed class SecurityPackageInfo
	{
		// Token: 0x06000F7C RID: 3964 RVA: 0x000354F0 File Offset: 0x000336F0
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

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06000F7D RID: 3965 RVA: 0x00035609 File Offset: 0x00033809
		public SecurityPackageCapabilities Capabilities
		{
			get
			{
				return this.capabilities;
			}
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06000F7E RID: 3966 RVA: 0x00035611 File Offset: 0x00033811
		public short Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06000F7F RID: 3967 RVA: 0x00035619 File Offset: 0x00033819
		public short RpcId
		{
			get
			{
				return this.rpcId;
			}
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06000F80 RID: 3968 RVA: 0x00035621 File Offset: 0x00033821
		public int MaxTokenLength
		{
			get
			{
				return this.maxTokenLength;
			}
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06000F81 RID: 3969 RVA: 0x00035629 File Offset: 0x00033829
		public string Name
		{
			get
			{
				return this.name ?? string.Empty;
			}
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06000F82 RID: 3970 RVA: 0x0003563A File Offset: 0x0003383A
		public string Comment
		{
			get
			{
				return this.comment ?? string.Empty;
			}
		}

		// Token: 0x06000F83 RID: 3971 RVA: 0x0003564C File Offset: 0x0003384C
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

		// Token: 0x04000A27 RID: 2599
		private readonly SecurityPackageCapabilities capabilities;

		// Token: 0x04000A28 RID: 2600
		private readonly short version;

		// Token: 0x04000A29 RID: 2601
		private readonly short rpcId;

		// Token: 0x04000A2A RID: 2602
		private readonly int maxTokenLength;

		// Token: 0x04000A2B RID: 2603
		private readonly string name;

		// Token: 0x04000A2C RID: 2604
		private readonly string comment;
	}
}
