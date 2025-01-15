using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CC3 RID: 3267
	[InterfaceType(1)]
	[Guid("3050F5FA-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IMarkupTextFrags
	{
		// Token: 0x060162FD RID: 90877
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetTextFragCount(out int pcFrags);

		// Token: 0x060162FE RID: 90878
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetTextFrag([In] int iFrag, [MarshalAs(19)] out string pbstrFrag, [MarshalAs(28)] [In] IMarkupPointer pPointerFrag);

		// Token: 0x060162FF RID: 90879
		[MethodImpl(4096, MethodCodeType = 3)]
		void RemoveTextFrag([In] int iFrag);

		// Token: 0x06016300 RID: 90880
		[MethodImpl(4096, MethodCodeType = 3)]
		void InsertTextFrag([In] int iFrag, [MarshalAs(19)] [In] string bstrInsert, [MarshalAs(28)] [In] IMarkupPointer pPointerInsert);

		// Token: 0x06016301 RID: 90881
		[MethodImpl(4096, MethodCodeType = 3)]
		void FindTextFragFromMarkupPointer([MarshalAs(28)] [In] IMarkupPointer pPointerFind, out int piFrag, out int pfFragFound);
	}
}
