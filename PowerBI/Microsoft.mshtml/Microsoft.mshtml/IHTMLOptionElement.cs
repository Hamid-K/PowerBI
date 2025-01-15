using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000569 RID: 1385
	[Guid("3050F211-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLOptionElement
	{
		// Token: 0x17002CFA RID: 11514
		// (get) Token: 0x06008602 RID: 34306
		// (set) Token: 0x06008601 RID: 34305
		[DispId(1001)]
		bool selected
		{
			[DispId(1001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(1001)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17002CFB RID: 11515
		// (get) Token: 0x06008604 RID: 34308
		// (set) Token: 0x06008603 RID: 34307
		[DispId(1002)]
		string value
		{
			[DispId(1002)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1002)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17002CFC RID: 11516
		// (get) Token: 0x06008606 RID: 34310
		// (set) Token: 0x06008605 RID: 34309
		[DispId(1003)]
		bool defaultSelected
		{
			[DispId(1003)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(1003)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17002CFD RID: 11517
		// (get) Token: 0x06008608 RID: 34312
		// (set) Token: 0x06008607 RID: 34311
		[DispId(1005)]
		int index
		{
			[DispId(1005)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[DispId(1005)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17002CFE RID: 11518
		// (get) Token: 0x0600860A RID: 34314
		// (set) Token: 0x06008609 RID: 34313
		[DispId(1004)]
		string text
		{
			[DispId(1004)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1004)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17002CFF RID: 11519
		// (get) Token: 0x0600860B RID: 34315
		[DispId(1006)]
		IHTMLFormElement form
		{
			[DispId(1006)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}
	}
}
