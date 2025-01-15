using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DB5 RID: 3509
	[InterfaceType(1)]
	[Guid("00000100-0000-0000-C000-000000000046")]
	[ComImport]
	public interface IEnumUnknown
	{
		// Token: 0x060174BD RID: 95421
		[MethodImpl(4096, MethodCodeType = 3)]
		void RemoteNext([In] uint celt, [MarshalAs(25)] out object rgelt, out uint pceltFetched);

		// Token: 0x060174BE RID: 95422
		[MethodImpl(4096, MethodCodeType = 3)]
		void Skip([In] uint celt);

		// Token: 0x060174BF RID: 95423
		[MethodImpl(4096, MethodCodeType = 3)]
		void reset();

		// Token: 0x060174C0 RID: 95424
		[MethodImpl(4096, MethodCodeType = 3)]
		void Clone([MarshalAs(28)] out IEnumUnknown ppEnum);
	}
}
