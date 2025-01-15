using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000B1E RID: 2846
	[TypeLibType(4160)]
	[Guid("3050F24F-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLObjectElement
	{
		// Token: 0x1700613F RID: 24895
		// (get) Token: 0x06012735 RID: 75573
		[DispId(-2147415111)]
		object @object
		{
			[TypeLibFunc(64)]
			[DispId(-2147415111)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17006140 RID: 24896
		// (get) Token: 0x06012736 RID: 75574
		[DispId(-2147415110)]
		string classid
		{
			[DispId(-2147415110)]
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17006141 RID: 24897
		// (get) Token: 0x06012737 RID: 75575
		[DispId(-2147415109)]
		string data
		{
			[DispId(-2147415109)]
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17006142 RID: 24898
		// (get) Token: 0x06012739 RID: 75577
		// (set) Token: 0x06012738 RID: 75576
		[DispId(-2147415107)]
		object recordset
		{
			[DispId(-2147415107)]
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
			[TypeLibFunc(64)]
			[DispId(-2147415107)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(26)]
			[param: In]
			set;
		}

		// Token: 0x17006143 RID: 24899
		// (get) Token: 0x0601273B RID: 75579
		// (set) Token: 0x0601273A RID: 75578
		[DispId(-2147418039)]
		string align
		{
			[TypeLibFunc(20)]
			[DispId(-2147418039)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147418039)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17006144 RID: 24900
		// (get) Token: 0x0601273D RID: 75581
		// (set) Token: 0x0601273C RID: 75580
		[DispId(-2147418112)]
		string name
		{
			[DispId(-2147418112)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147418112)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17006145 RID: 24901
		// (get) Token: 0x0601273F RID: 75583
		// (set) Token: 0x0601273E RID: 75582
		[DispId(-2147415106)]
		string codeBase
		{
			[DispId(-2147415106)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147415106)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17006146 RID: 24902
		// (get) Token: 0x06012741 RID: 75585
		// (set) Token: 0x06012740 RID: 75584
		[DispId(-2147415105)]
		string codeType
		{
			[DispId(-2147415105)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147415105)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17006147 RID: 24903
		// (get) Token: 0x06012743 RID: 75587
		// (set) Token: 0x06012742 RID: 75586
		[DispId(-2147415104)]
		string code
		{
			[DispId(-2147415104)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147415104)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17006148 RID: 24904
		// (get) Token: 0x06012744 RID: 75588
		[DispId(-2147418110)]
		string BaseHref
		{
			[DispId(-2147418110)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17006149 RID: 24905
		// (get) Token: 0x06012746 RID: 75590
		// (set) Token: 0x06012745 RID: 75589
		[DispId(-2147415103)]
		string type
		{
			[TypeLibFunc(20)]
			[DispId(-2147415103)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147415103)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700614A RID: 24906
		// (get) Token: 0x06012747 RID: 75591
		[DispId(-2147416108)]
		IHTMLFormElement form
		{
			[DispId(-2147416108)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700614B RID: 24907
		// (get) Token: 0x06012749 RID: 75593
		// (set) Token: 0x06012748 RID: 75592
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

		// Token: 0x1700614C RID: 24908
		// (get) Token: 0x0601274B RID: 75595
		// (set) Token: 0x0601274A RID: 75594
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

		// Token: 0x1700614D RID: 24909
		// (get) Token: 0x0601274C RID: 75596
		[DispId(-2147415102)]
		int readyState
		{
			[TypeLibFunc(64)]
			[DispId(-2147415102)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700614E RID: 24910
		// (get) Token: 0x0601274E RID: 75598
		// (set) Token: 0x0601274D RID: 75597
		[DispId(-2147412087)]
		object onreadystatechange
		{
			[TypeLibFunc(20)]
			[DispId(-2147412087)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412087)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700614F RID: 24911
		// (get) Token: 0x06012750 RID: 75600
		// (set) Token: 0x0601274F RID: 75599
		[DispId(-2147412083)]
		object onerror
		{
			[TypeLibFunc(20)]
			[DispId(-2147412083)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412083)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17006150 RID: 24912
		// (get) Token: 0x06012752 RID: 75602
		// (set) Token: 0x06012751 RID: 75601
		[DispId(-2147415101)]
		string altHtml
		{
			[DispId(-2147415101)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147415101)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17006151 RID: 24913
		// (get) Token: 0x06012754 RID: 75604
		// (set) Token: 0x06012753 RID: 75603
		[DispId(-2147415100)]
		int vspace
		{
			[DispId(-2147415100)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(-2147415100)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17006152 RID: 24914
		// (get) Token: 0x06012756 RID: 75606
		// (set) Token: 0x06012755 RID: 75605
		[DispId(-2147415099)]
		int hspace
		{
			[DispId(-2147415099)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(-2147415099)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}
	}
}
