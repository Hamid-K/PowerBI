using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CF6 RID: 3318
	[TypeLibType(2)]
	[ClassInterface(0)]
	[Guid("3050F28A-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public class HTMLDialogClass : IHTMLDialog, HTMLDialog
	{
		// Token: 0x0601638B RID: 91019
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLDialogClass();

		// Token: 0x170075D9 RID: 30169
		// (get) Token: 0x0601638D RID: 91021
		// (set) Token: 0x0601638C RID: 91020
		[DispId(-2147418108)]
		public virtual extern object dialogTop
		{
			[TypeLibFunc(4)]
			[DispId(-2147418108)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(4)]
			[DispId(-2147418108)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170075DA RID: 30170
		// (get) Token: 0x0601638F RID: 91023
		// (set) Token: 0x0601638E RID: 91022
		[DispId(-2147418109)]
		public virtual extern object dialogLeft
		{
			[TypeLibFunc(4)]
			[DispId(-2147418109)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147418109)]
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170075DB RID: 30171
		// (get) Token: 0x06016391 RID: 91025
		// (set) Token: 0x06016390 RID: 91024
		[DispId(-2147418107)]
		public virtual extern object dialogWidth
		{
			[TypeLibFunc(4)]
			[DispId(-2147418107)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147418107)]
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170075DC RID: 30172
		// (get) Token: 0x06016393 RID: 91027
		// (set) Token: 0x06016392 RID: 91026
		[DispId(-2147418106)]
		public virtual extern object dialogHeight
		{
			[DispId(-2147418106)]
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147418106)]
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170075DD RID: 30173
		// (get) Token: 0x06016394 RID: 91028
		[DispId(25000)]
		public virtual extern object dialogArguments
		{
			[DispId(25000)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170075DE RID: 30174
		// (get) Token: 0x06016395 RID: 91029
		[DispId(25013)]
		public virtual extern object menuArguments
		{
			[DispId(25013)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170075DF RID: 30175
		// (get) Token: 0x06016397 RID: 91031
		// (set) Token: 0x06016396 RID: 91030
		[DispId(25001)]
		public virtual extern object returnValue
		{
			[DispId(25001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(25001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x06016398 RID: 91032
		[DispId(25011)]
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void close();

		// Token: 0x06016399 RID: 91033
		[DispId(25012)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string toString();
	}
}
