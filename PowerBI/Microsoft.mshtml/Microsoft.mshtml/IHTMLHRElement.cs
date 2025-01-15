using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020004CB RID: 1227
	[Guid("3050F1F4-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLHRElement
	{
		// Token: 0x1700268B RID: 9867
		// (get) Token: 0x0600725E RID: 29278
		// (set) Token: 0x0600725D RID: 29277
		[DispId(-2147418040)]
		string align
		{
			[DispId(-2147418040)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147418040)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700268C RID: 9868
		// (get) Token: 0x06007260 RID: 29280
		// (set) Token: 0x0600725F RID: 29279
		[DispId(-2147413110)]
		object color
		{
			[DispId(-2147413110)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413110)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700268D RID: 9869
		// (get) Token: 0x06007262 RID: 29282
		// (set) Token: 0x06007261 RID: 29281
		[DispId(1001)]
		bool noShade
		{
			[DispId(1001)]
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(1001)]
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700268E RID: 9870
		// (get) Token: 0x06007264 RID: 29284
		// (set) Token: 0x06007263 RID: 29283
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

		// Token: 0x1700268F RID: 9871
		// (get) Token: 0x06007266 RID: 29286
		// (set) Token: 0x06007265 RID: 29285
		[DispId(-2147418106)]
		object size
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
	}
}
