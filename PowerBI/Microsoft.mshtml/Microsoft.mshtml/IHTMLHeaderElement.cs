using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020004D8 RID: 1240
	[Guid("3050F1F6-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLHeaderElement
	{
		// Token: 0x170029AF RID: 10671
		// (get) Token: 0x06007ACB RID: 31435
		// (set) Token: 0x06007ACA RID: 31434
		[DispId(-2147418040)]
		string align
		{
			[DispId(-2147418040)]
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(4)]
			[DispId(-2147418040)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}
	}
}
