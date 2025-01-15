using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200056A RID: 1386
	[TypeLibType(4160)]
	[Guid("3050F820-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLOptionElement3
	{
		// Token: 0x17002D00 RID: 11520
		// (get) Token: 0x0600860D RID: 34317
		// (set) Token: 0x0600860C RID: 34316
		[DispId(1007)]
		string label
		{
			[DispId(1007)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(1007)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}
	}
}
