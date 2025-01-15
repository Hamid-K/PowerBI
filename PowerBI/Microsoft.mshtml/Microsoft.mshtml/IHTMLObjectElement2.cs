using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000B1F RID: 2847
	[Guid("3050F4CD-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLObjectElement2
	{
		// Token: 0x06012757 RID: 75607
		[DispId(-2147415098)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		object namedRecordset([MarshalAs(19)] [In] string dataMember, [MarshalAs(27)] [In] [Optional] ref object hierarchy);

		// Token: 0x17006153 RID: 24915
		// (get) Token: 0x06012759 RID: 75609
		// (set) Token: 0x06012758 RID: 75608
		[DispId(-2147415110)]
		string classid
		{
			[DispId(-2147415110)]
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(64)]
			[DispId(-2147415110)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17006154 RID: 24916
		// (get) Token: 0x0601275B RID: 75611
		// (set) Token: 0x0601275A RID: 75610
		[DispId(-2147415109)]
		string data
		{
			[TypeLibFunc(64)]
			[DispId(-2147415109)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(64)]
			[DispId(-2147415109)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}
	}
}
