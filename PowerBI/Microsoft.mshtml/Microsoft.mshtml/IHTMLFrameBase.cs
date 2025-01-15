using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020007B9 RID: 1977
	[TypeLibType(4160)]
	[Guid("3050F311-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLFrameBase
	{
		// Token: 0x17004796 RID: 18326
		// (get) Token: 0x0600D72D RID: 55085
		// (set) Token: 0x0600D72C RID: 55084
		[DispId(-2147415112)]
		string src
		{
			[DispId(-2147415112)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147415112)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17004797 RID: 18327
		// (get) Token: 0x0600D72F RID: 55087
		// (set) Token: 0x0600D72E RID: 55086
		[DispId(-2147418112)]
		string name
		{
			[DispId(-2147418112)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147418112)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17004798 RID: 18328
		// (get) Token: 0x0600D731 RID: 55089
		// (set) Token: 0x0600D730 RID: 55088
		[DispId(-2147415110)]
		object border
		{
			[DispId(-2147415110)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147415110)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17004799 RID: 18329
		// (get) Token: 0x0600D733 RID: 55091
		// (set) Token: 0x0600D732 RID: 55090
		[DispId(-2147415109)]
		string frameBorder
		{
			[DispId(-2147415109)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147415109)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700479A RID: 18330
		// (get) Token: 0x0600D735 RID: 55093
		// (set) Token: 0x0600D734 RID: 55092
		[DispId(-2147415108)]
		object frameSpacing
		{
			[DispId(-2147415108)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147415108)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700479B RID: 18331
		// (get) Token: 0x0600D737 RID: 55095
		// (set) Token: 0x0600D736 RID: 55094
		[DispId(-2147415107)]
		object marginWidth
		{
			[DispId(-2147415107)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147415107)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700479C RID: 18332
		// (get) Token: 0x0600D739 RID: 55097
		// (set) Token: 0x0600D738 RID: 55096
		[DispId(-2147415106)]
		object marginHeight
		{
			[DispId(-2147415106)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147415106)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700479D RID: 18333
		// (get) Token: 0x0600D73B RID: 55099
		// (set) Token: 0x0600D73A RID: 55098
		[DispId(-2147415105)]
		bool noResize
		{
			[DispId(-2147415105)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(-2147415105)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700479E RID: 18334
		// (get) Token: 0x0600D73D RID: 55101
		// (set) Token: 0x0600D73C RID: 55100
		[DispId(-2147415104)]
		string scrolling
		{
			[DispId(-2147415104)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147415104)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}
	}
}
