using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.EnterpriseSingleSignOn.Interop
{
	// Token: 0x020004B0 RID: 1200
	[TypeLibType(TypeLibTypeFlags.FCanCreate)]
	[ClassInterface(ClassInterfaceType.None)]
	[Guid("DB8A01BE-D296-4756-909D-D473E0944FB5")]
	[ComImport]
	public class SSOTicket
	{
		// Token: 0x0600293E RID: 10558
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		public extern SSOTicket();
	}
}
