using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.EnterpriseSingleSignOn.Interop
{
	// Token: 0x020004AB RID: 1195
	[TypeLibType(TypeLibTypeFlags.FCanCreate)]
	[ClassInterface(ClassInterfaceType.None)]
	[Guid("CF3C637A-0D4E-47BD-9210-DB40A33BD488")]
	[ComImport]
	public class SSOConfigStore
	{
		// Token: 0x06002936 RID: 10550
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		public extern SSOConfigStore();
	}
}
