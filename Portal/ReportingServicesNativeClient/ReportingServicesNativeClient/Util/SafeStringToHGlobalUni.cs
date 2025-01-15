using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace Util
{
	// Token: 0x02000019 RID: 25
	[StrongNameIdentityPermission(SecurityAction.LinkDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public class SafeStringToHGlobalUni : SafeHandle
	{
		// Token: 0x06000096 RID: 150 RVA: 0x00009C40 File Offset: 0x00009040
		public SafeStringToHGlobalUni()
			: base(IntPtr.Zero, true)
		{
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00009C6C File Offset: 0x0000906C
		public static SafeStringToHGlobalUni Create(string s)
		{
			SafeStringToHGlobalUni safeStringToHGlobalUni = new SafeStringToHGlobalUni();
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				IntPtr intPtr = Marshal.StringToHGlobalUni(s);
				safeStringToHGlobalUni.SetHandle(intPtr);
			}
			safeStringToHGlobalUni.m_cb = (ulong)((long)s.Length * 2L);
			return safeStringToHGlobalUni;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00007260 File Offset: 0x00006660
		public unsafe ushort* ToPointer()
		{
			return (ushort*)this.handle.ToPointer();
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00009FD0 File Offset: 0x000093D0
		public void ZeroString()
		{
			if (!this.IsInvalid)
			{
				<Module>.RtlSecureZeroMemory(this.handle.ToPointer(), this.m_cb);
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00009CCC File Offset: 0x000090CC
		[return: MarshalAs(UnmanagedType.U1)]
		protected override bool ReleaseHandle()
		{
			Marshal.FreeHGlobal(this.handle);
			return true;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00009AA4 File Offset: 0x00008EA4
		public override bool IsInvalid
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return this.handle == IntPtr.Zero;
			}
		}

		// Token: 0x04000057 RID: 87
		private ulong m_cb = 0UL;
	}
}
