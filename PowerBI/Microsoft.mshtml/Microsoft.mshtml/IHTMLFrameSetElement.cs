using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000BE9 RID: 3049
	[TypeLibType(4160)]
	[Guid("3050F319-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLFrameSetElement
	{
		// Token: 0x1700709D RID: 28829
		// (get) Token: 0x0601524D RID: 86605
		// (set) Token: 0x0601524C RID: 86604
		[DispId(1000)]
		string rows
		{
			[TypeLibFunc(20)]
			[DispId(1000)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1000)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700709E RID: 28830
		// (get) Token: 0x0601524F RID: 86607
		// (set) Token: 0x0601524E RID: 86606
		[DispId(1001)]
		string cols
		{
			[TypeLibFunc(20)]
			[DispId(1001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(1001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700709F RID: 28831
		// (get) Token: 0x06015251 RID: 86609
		// (set) Token: 0x06015250 RID: 86608
		[DispId(1002)]
		object border
		{
			[DispId(1002)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(1002)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170070A0 RID: 28832
		// (get) Token: 0x06015253 RID: 86611
		// (set) Token: 0x06015252 RID: 86610
		[DispId(1003)]
		object borderColor
		{
			[DispId(1003)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(1003)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170070A1 RID: 28833
		// (get) Token: 0x06015255 RID: 86613
		// (set) Token: 0x06015254 RID: 86612
		[DispId(1004)]
		string frameBorder
		{
			[DispId(1004)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1004)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170070A2 RID: 28834
		// (get) Token: 0x06015257 RID: 86615
		// (set) Token: 0x06015256 RID: 86614
		[DispId(1005)]
		object frameSpacing
		{
			[DispId(1005)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(1005)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170070A3 RID: 28835
		// (get) Token: 0x06015259 RID: 86617
		// (set) Token: 0x06015258 RID: 86616
		[DispId(-2147418112)]
		string name
		{
			[TypeLibFunc(20)]
			[DispId(-2147418112)]
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

		// Token: 0x170070A4 RID: 28836
		// (get) Token: 0x0601525B RID: 86619
		// (set) Token: 0x0601525A RID: 86618
		[DispId(-2147412080)]
		object onload
		{
			[DispId(-2147412080)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412080)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170070A5 RID: 28837
		// (get) Token: 0x0601525D RID: 86621
		// (set) Token: 0x0601525C RID: 86620
		[DispId(-2147412079)]
		object onunload
		{
			[TypeLibFunc(20)]
			[DispId(-2147412079)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412079)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170070A6 RID: 28838
		// (get) Token: 0x0601525F RID: 86623
		// (set) Token: 0x0601525E RID: 86622
		[DispId(-2147412073)]
		object onbeforeunload
		{
			[TypeLibFunc(20)]
			[DispId(-2147412073)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412073)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}
	}
}
