using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace Util
{
	// Token: 0x0200001B RID: 27
	[StrongNameIdentityPermission(SecurityAction.LinkDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public class SafeGetIUnknownForObject : SafeHandle
	{
		// Token: 0x060000A2 RID: 162 RVA: 0x00009A54 File Offset: 0x00008E54
		public SafeGetIUnknownForObject()
			: base(IntPtr.Zero, true)
		{
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00009E70 File Offset: 0x00009270
		public static SafeGetIUnknownForObject Create(object obj)
		{
			SafeGetIUnknownForObject safeGetIUnknownForObject = new SafeGetIUnknownForObject();
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				IntPtr iunknownForObject = Marshal.GetIUnknownForObject(obj);
				safeGetIUnknownForObject.SetHandle(iunknownForObject);
			}
			return safeGetIUnknownForObject;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000072A0 File Offset: 0x000066A0
		public unsafe IUnknown* ToPointer()
		{
			return (IUnknown*)this.handle.ToPointer();
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00009EC0 File Offset: 0x000092C0
		[return: MarshalAs(UnmanagedType.U1)]
		protected override bool ReleaseHandle()
		{
			Marshal.Release(this.handle);
			return true;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00009AA4 File Offset: 0x00008EA4
		public override bool IsInvalid
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return this.handle == IntPtr.Zero;
			}
		}
	}
}
