using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000C9D RID: 3229
	[Guid("08C03412-F96B-11D0-A475-00AA006BCC59")]
	[InterfaceType(1)]
	[ComImport]
	public interface IEnumRegisterWordA
	{
		// Token: 0x0601624F RID: 90703
		[MethodImpl(4096, MethodCodeType = 3)]
		void Clone([MarshalAs(28)] out IEnumRegisterWordA ppEnum);

		// Token: 0x06016250 RID: 90704
		[MethodImpl(4096, MethodCodeType = 3)]
		void Next([In] uint ulCount, out __MIDL___MIDL_itf_mshtml_0250_0001 rgRegisterWord, out uint pcFetched);

		// Token: 0x06016251 RID: 90705
		[MethodImpl(4096, MethodCodeType = 3)]
		void reset();

		// Token: 0x06016252 RID: 90706
		[MethodImpl(4096, MethodCodeType = 3)]
		void Skip([In] uint ulCount);
	}
}
