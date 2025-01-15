using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000A00 RID: 2560
	[TypeLibType(4160)]
	[Guid("3050F23D-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLTableCell
	{
		// Token: 0x1700558C RID: 21900
		// (get) Token: 0x060103F4 RID: 66548
		// (set) Token: 0x060103F3 RID: 66547
		[DispId(2001)]
		int rowSpan
		{
			[DispId(2001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(2001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700558D RID: 21901
		// (get) Token: 0x060103F6 RID: 66550
		// (set) Token: 0x060103F5 RID: 66549
		[DispId(2002)]
		int colSpan
		{
			[DispId(2002)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(2002)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700558E RID: 21902
		// (get) Token: 0x060103F8 RID: 66552
		// (set) Token: 0x060103F7 RID: 66551
		[DispId(-2147418040)]
		string align
		{
			[DispId(-2147418040)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147418040)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700558F RID: 21903
		// (get) Token: 0x060103FA RID: 66554
		// (set) Token: 0x060103F9 RID: 66553
		[DispId(-2147413081)]
		string vAlign
		{
			[DispId(-2147413081)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413081)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17005590 RID: 21904
		// (get) Token: 0x060103FC RID: 66556
		// (set) Token: 0x060103FB RID: 66555
		[DispId(-501)]
		object bgColor
		{
			[DispId(-501)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-501)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17005591 RID: 21905
		// (get) Token: 0x060103FE RID: 66558
		// (set) Token: 0x060103FD RID: 66557
		[DispId(-2147413107)]
		bool noWrap
		{
			[DispId(-2147413107)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(-2147413107)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17005592 RID: 21906
		// (get) Token: 0x06010400 RID: 66560
		// (set) Token: 0x060103FF RID: 66559
		[DispId(-2147413111)]
		string background
		{
			[DispId(-2147413111)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413111)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17005593 RID: 21907
		// (get) Token: 0x06010402 RID: 66562
		// (set) Token: 0x06010401 RID: 66561
		[DispId(-2147413084)]
		object borderColor
		{
			[DispId(-2147413084)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413084)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17005594 RID: 21908
		// (get) Token: 0x06010404 RID: 66564
		// (set) Token: 0x06010403 RID: 66563
		[DispId(-2147413083)]
		object borderColorLight
		{
			[DispId(-2147413083)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413083)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17005595 RID: 21909
		// (get) Token: 0x06010406 RID: 66566
		// (set) Token: 0x06010405 RID: 66565
		[DispId(-2147413082)]
		object borderColorDark
		{
			[DispId(-2147413082)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413082)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17005596 RID: 21910
		// (get) Token: 0x06010408 RID: 66568
		// (set) Token: 0x06010407 RID: 66567
		[DispId(-2147418107)]
		object width
		{
			[DispId(-2147418107)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147418107)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17005597 RID: 21911
		// (get) Token: 0x0601040A RID: 66570
		// (set) Token: 0x06010409 RID: 66569
		[DispId(-2147418106)]
		object height
		{
			[DispId(-2147418106)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147418106)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17005598 RID: 21912
		// (get) Token: 0x0601040B RID: 66571
		[DispId(2003)]
		int cellIndex
		{
			[DispId(2003)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}
	}
}
