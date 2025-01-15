using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CFA RID: 3322
	[ClassInterface(0)]
	[Guid("3050F667-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(2)]
	[ComImport]
	public class HTMLPopupClass : DispHTMLPopup, HTMLPopup, IHTMLPopup
	{
		// Token: 0x060163A2 RID: 91042
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLPopupClass();

		// Token: 0x060163A3 RID: 91043
		[DispId(27001)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void Show([In] int x, [In] int y, [In] int w, [In] int h, [MarshalAs(27)] [In] ref object pElement);

		// Token: 0x060163A4 RID: 91044
		[DispId(27002)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void Hide();

		// Token: 0x170075E4 RID: 30180
		// (get) Token: 0x060163A5 RID: 91045
		[DispId(27003)]
		public virtual extern IHTMLDocument document
		{
			[DispId(27003)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170075E5 RID: 30181
		// (get) Token: 0x060163A6 RID: 91046
		[DispId(27004)]
		public virtual extern bool isOpen
		{
			[DispId(27004)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x060163A7 RID: 91047
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLPopup_Show([In] int x, [In] int y, [In] int w, [In] int h, [MarshalAs(27)] [In] ref object pElement);

		// Token: 0x060163A8 RID: 91048
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLPopup_Hide();

		// Token: 0x170075E6 RID: 30182
		// (get) Token: 0x060163A9 RID: 91049
		public virtual extern IHTMLDocument IHTMLPopup_document
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170075E7 RID: 30183
		// (get) Token: 0x060163AA RID: 91050
		public virtual extern bool IHTMLPopup_isOpen
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}
	}
}
