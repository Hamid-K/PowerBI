using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace Util
{
	// Token: 0x02000017 RID: 23
	[StrongNameIdentityPermission(SecurityAction.LinkDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public class SafeToken : SafeHandle
	{
		// Token: 0x0600008D RID: 141 RVA: 0x00009A54 File Offset: 0x00008E54
		public SafeToken()
			: base(IntPtr.Zero, true)
		{
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00009A78 File Offset: 0x00008E78
		[return: MarshalAs(UnmanagedType.U1)]
		protected override bool ReleaseHandle()
		{
			return ((<Module>.CloseHandle(this.handle.ToPointer()) != 0) ? 1 : 0) != 0;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00009AA4 File Offset: 0x00008EA4
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
