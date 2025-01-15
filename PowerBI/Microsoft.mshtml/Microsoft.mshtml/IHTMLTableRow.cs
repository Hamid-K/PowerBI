using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020009FC RID: 2556
	[TypeLibType(4160)]
	[Guid("3050F23C-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLTableRow
	{
		// Token: 0x1700557C RID: 21884
		// (get) Token: 0x060103D9 RID: 66521
		// (set) Token: 0x060103D8 RID: 66520
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

		// Token: 0x1700557D RID: 21885
		// (get) Token: 0x060103DB RID: 66523
		// (set) Token: 0x060103DA RID: 66522
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

		// Token: 0x1700557E RID: 21886
		// (get) Token: 0x060103DD RID: 66525
		// (set) Token: 0x060103DC RID: 66524
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

		// Token: 0x1700557F RID: 21887
		// (get) Token: 0x060103DF RID: 66527
		// (set) Token: 0x060103DE RID: 66526
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

		// Token: 0x17005580 RID: 21888
		// (get) Token: 0x060103E1 RID: 66529
		// (set) Token: 0x060103E0 RID: 66528
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

		// Token: 0x17005581 RID: 21889
		// (get) Token: 0x060103E3 RID: 66531
		// (set) Token: 0x060103E2 RID: 66530
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

		// Token: 0x17005582 RID: 21890
		// (get) Token: 0x060103E4 RID: 66532
		[DispId(1000)]
		int rowIndex
		{
			[DispId(1000)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005583 RID: 21891
		// (get) Token: 0x060103E5 RID: 66533
		[DispId(1001)]
		int sectionRowIndex
		{
			[DispId(1001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005584 RID: 21892
		// (get) Token: 0x060103E6 RID: 66534
		[DispId(1002)]
		IHTMLElementCollection cells
		{
			[DispId(1002)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x060103E7 RID: 66535
		[DispId(1003)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		object insertCell([In] int index = -1);

		// Token: 0x060103E8 RID: 66536
		[DispId(1004)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void deleteCell([In] int index = -1);
	}
}
