using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200030A RID: 778
	[TypeLibType(4160)]
	[Guid("3050F5C5-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLBodyElement2
	{
		// Token: 0x17001032 RID: 4146
		// (get) Token: 0x06002F57 RID: 12119
		// (set) Token: 0x06002F56 RID: 12118
		[DispId(-2147412046)]
		object onbeforeprint
		{
			[TypeLibFunc(20)]
			[DispId(-2147412046)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412046)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17001033 RID: 4147
		// (get) Token: 0x06002F59 RID: 12121
		// (set) Token: 0x06002F58 RID: 12120
		[DispId(-2147412045)]
		object onafterprint
		{
			[DispId(-2147412045)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412045)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}
	}
}
