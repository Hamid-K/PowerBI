using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace Microsoft.ReportingServices.Utilities
{
	// Token: 0x02000027 RID: 39
	[StrongNameIdentityPermission(SecurityAction.LinkDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public class MetaUtil
	{
		// Token: 0x060000D7 RID: 215 RVA: 0x000098A8 File Offset: 0x00008CA8
		[return: MarshalAs(UnmanagedType.U1)]
		public unsafe bool IsAnonymousAllowed(string pMetabasePath)
		{
			bool flag = false;
			ushort* ptr = (ushort*)Marshal.StringToHGlobalUni(pMetabasePath).ToPointer();
			int num = <Module>.AllowsAnonymous(ptr, &flag);
			if (ptr != null)
			{
				<Module>.CoTaskMemFree((void*)ptr);
			}
			if (num < 0)
			{
				Marshal.ThrowExceptionForHR(num);
			}
			return flag;
		}
	}
}
