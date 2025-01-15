using System;
using System.Runtime.InteropServices;

namespace Microsoft.EnterpriseSingleSignOn.Interop
{
	// Token: 0x020004A5 RID: 1189
	[Guid("55272A00-42CB-11CE-8135-00AA004BB851")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IPropertyBag
	{
		// Token: 0x06002917 RID: 10519
		void Read([MarshalAs(UnmanagedType.LPWStr)] [In] string propName, [MarshalAs(UnmanagedType.Struct)] out object ptrVar, int errorLog);

		// Token: 0x06002918 RID: 10520
		void Write([MarshalAs(UnmanagedType.LPWStr)] [In] string propName, [MarshalAs(UnmanagedType.Struct)] [In] ref object ptrVar);
	}
}
