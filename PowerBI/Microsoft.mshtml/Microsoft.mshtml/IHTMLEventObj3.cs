using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020007AD RID: 1965
	[Guid("3050F680-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLEventObj3
	{
		// Token: 0x17004687 RID: 18055
		// (get) Token: 0x0600D538 RID: 54584
		[DispId(1038)]
		bool contentOverflow
		{
			[DispId(1038)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004688 RID: 18056
		// (get) Token: 0x0600D53A RID: 54586
		// (set) Token: 0x0600D539 RID: 54585
		[DispId(1039)]
		bool shiftLeft
		{
			[DispId(1039)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(1039)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17004689 RID: 18057
		// (get) Token: 0x0600D53C RID: 54588
		// (set) Token: 0x0600D53B RID: 54587
		[DispId(1040)]
		bool altLeft
		{
			[DispId(1040)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(1040)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700468A RID: 18058
		// (get) Token: 0x0600D53E RID: 54590
		// (set) Token: 0x0600D53D RID: 54589
		[DispId(1041)]
		bool ctrlLeft
		{
			[DispId(1041)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(1041)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700468B RID: 18059
		// (get) Token: 0x0600D53F RID: 54591
		[ComAliasName("mshtml.LONG_PTR")]
		[DispId(1042)]
		int imeCompositionChange
		{
			[TypeLibFunc(1089)]
			[DispId(1042)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: ComAliasName("mshtml.LONG_PTR")]
			get;
		}

		// Token: 0x1700468C RID: 18060
		// (get) Token: 0x0600D540 RID: 54592
		[DispId(1043)]
		[ComAliasName("mshtml.LONG_PTR")]
		int imeNotifyCommand
		{
			[TypeLibFunc(1089)]
			[DispId(1043)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: ComAliasName("mshtml.LONG_PTR")]
			get;
		}

		// Token: 0x1700468D RID: 18061
		// (get) Token: 0x0600D541 RID: 54593
		[DispId(1044)]
		[ComAliasName("mshtml.LONG_PTR")]
		int imeNotifyData
		{
			[TypeLibFunc(1089)]
			[DispId(1044)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: ComAliasName("mshtml.LONG_PTR")]
			get;
		}

		// Token: 0x1700468E RID: 18062
		// (get) Token: 0x0600D542 RID: 54594
		[ComAliasName("mshtml.LONG_PTR")]
		[DispId(1046)]
		int imeRequest
		{
			[DispId(1046)]
			[TypeLibFunc(1089)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: ComAliasName("mshtml.LONG_PTR")]
			get;
		}

		// Token: 0x1700468F RID: 18063
		// (get) Token: 0x0600D543 RID: 54595
		[ComAliasName("mshtml.LONG_PTR")]
		[DispId(1047)]
		int imeRequestData
		{
			[DispId(1047)]
			[TypeLibFunc(1089)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: ComAliasName("mshtml.LONG_PTR")]
			get;
		}

		// Token: 0x17004690 RID: 18064
		// (get) Token: 0x0600D544 RID: 54596
		[DispId(1045)]
		[ComAliasName("mshtml.LONG_PTR")]
		int keyboardLayout
		{
			[DispId(1045)]
			[TypeLibFunc(1089)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: ComAliasName("mshtml.LONG_PTR")]
			get;
		}

		// Token: 0x17004691 RID: 18065
		// (get) Token: 0x0600D545 RID: 54597
		[DispId(1048)]
		int behaviorCookie
		{
			[DispId(1048)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004692 RID: 18066
		// (get) Token: 0x0600D546 RID: 54598
		[DispId(1049)]
		int behaviorPart
		{
			[DispId(1049)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004693 RID: 18067
		// (get) Token: 0x0600D547 RID: 54599
		[DispId(1050)]
		string nextPage
		{
			[DispId(1050)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}
	}
}
