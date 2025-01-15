using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CF3 RID: 3315
	[TypeLibType(4160)]
	[Guid("3050F5E4-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLModelessInit
	{
		// Token: 0x170075D1 RID: 30161
		// (get) Token: 0x06016382 RID: 91010
		[DispId(25000)]
		object parameters
		{
			[DispId(25000)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170075D2 RID: 30162
		// (get) Token: 0x06016383 RID: 91011
		[DispId(25001)]
		object optionString
		{
			[DispId(25001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170075D3 RID: 30163
		// (get) Token: 0x06016384 RID: 91012
		[DispId(25006)]
		object moniker
		{
			[DispId(25006)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(25)]
			get;
		}

		// Token: 0x170075D4 RID: 30164
		// (get) Token: 0x06016385 RID: 91013
		[DispId(25007)]
		object document
		{
			[DispId(25007)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(25)]
			get;
		}
	}
}
