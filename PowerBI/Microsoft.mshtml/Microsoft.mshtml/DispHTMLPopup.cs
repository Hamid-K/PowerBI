using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CF9 RID: 3321
	[InterfaceType(2)]
	[Guid("3050F589-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4112)]
	[ComImport]
	public interface DispHTMLPopup
	{
		// Token: 0x0601639E RID: 91038
		[DispId(27001)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void Show([In] int x, [In] int y, [In] int w, [In] int h, [MarshalAs(27)] [In] ref object pElement);

		// Token: 0x0601639F RID: 91039
		[DispId(27002)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void Hide();

		// Token: 0x170075E2 RID: 30178
		// (get) Token: 0x060163A0 RID: 91040
		[DispId(27003)]
		IHTMLDocument document
		{
			[DispId(27003)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170075E3 RID: 30179
		// (get) Token: 0x060163A1 RID: 91041
		[DispId(27004)]
		bool isOpen
		{
			[DispId(27004)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}
	}
}
