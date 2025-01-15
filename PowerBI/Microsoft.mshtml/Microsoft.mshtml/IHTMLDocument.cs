using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000096 RID: 150
	[Guid("626FC520-A41E-11CF-A731-00A0C9082637")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLDocument
	{
		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06000D18 RID: 3352
		[DispId(1001)]
		object Script
		{
			[TypeLibFunc(1088)]
			[DispId(1001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}
	}
}
