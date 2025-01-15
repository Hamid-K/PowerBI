using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000D05 RID: 3333
	[TypeLibType(4160)]
	[Guid("3050F5C9-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLAppBehavior2
	{
		// Token: 0x170075F5 RID: 30197
		// (get) Token: 0x060163C5 RID: 91077
		// (set) Token: 0x060163C4 RID: 91076
		[DispId(5014)]
		string contextMenu
		{
			[DispId(5014)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(5014)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170075F6 RID: 30198
		// (get) Token: 0x060163C7 RID: 91079
		// (set) Token: 0x060163C6 RID: 91078
		[DispId(5015)]
		string innerBorder
		{
			[DispId(5015)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(5015)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170075F7 RID: 30199
		// (get) Token: 0x060163C9 RID: 91081
		// (set) Token: 0x060163C8 RID: 91080
		[DispId(5016)]
		string scroll
		{
			[DispId(5016)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(5016)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170075F8 RID: 30200
		// (get) Token: 0x060163CB RID: 91083
		// (set) Token: 0x060163CA RID: 91082
		[DispId(5017)]
		string scrollFlat
		{
			[DispId(5017)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(5017)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170075F9 RID: 30201
		// (get) Token: 0x060163CD RID: 91085
		// (set) Token: 0x060163CC RID: 91084
		[DispId(5018)]
		string selection
		{
			[DispId(5018)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(5018)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}
	}
}
