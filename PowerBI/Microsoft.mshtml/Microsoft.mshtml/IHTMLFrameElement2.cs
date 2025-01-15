using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000BCE RID: 3022
	[TypeLibType(4160)]
	[Guid("3050F7F5-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLFrameElement2
	{
		// Token: 0x1700668B RID: 26251
		// (get) Token: 0x060137ED RID: 79853
		// (set) Token: 0x060137EC RID: 79852
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

		// Token: 0x1700668C RID: 26252
		// (get) Token: 0x060137EF RID: 79855
		// (set) Token: 0x060137EE RID: 79854
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
	}
}
