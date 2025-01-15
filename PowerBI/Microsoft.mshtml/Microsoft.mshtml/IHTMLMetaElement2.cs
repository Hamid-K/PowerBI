using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000773 RID: 1907
	[TypeLibType(4160)]
	[Guid("3050F81F-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLMetaElement2
	{
		// Token: 0x17003856 RID: 14422
		// (get) Token: 0x0600AF87 RID: 44935
		// (set) Token: 0x0600AF86 RID: 44934
		[DispId(1020)]
		string scheme
		{
			[TypeLibFunc(20)]
			[DispId(1020)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(1020)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}
	}
}
