using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020007E4 RID: 2020
	[Guid("3050F80C-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLDocument5
	{
		// Token: 0x170048C4 RID: 18628
		// (get) Token: 0x0600DA9E RID: 55966
		// (set) Token: 0x0600DA9D RID: 55965
		[DispId(-2147412036)]
		object onmousewheel
		{
			[TypeLibFunc(20)]
			[DispId(-2147412036)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412036)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170048C5 RID: 18629
		// (get) Token: 0x0600DA9F RID: 55967
		[DispId(1098)]
		IHTMLDOMNode doctype
		{
			[DispId(1098)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170048C6 RID: 18630
		// (get) Token: 0x0600DAA0 RID: 55968
		[DispId(1099)]
		IHTMLDOMImplementation implementation
		{
			[DispId(1099)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600DAA1 RID: 55969
		[DispId(1100)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMAttribute createAttribute([MarshalAs(19)] [In] string bstrattrName);

		// Token: 0x0600DAA2 RID: 55970
		[DispId(1101)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode createComment([MarshalAs(19)] [In] string bstrdata);

		// Token: 0x170048C7 RID: 18631
		// (get) Token: 0x0600DAA4 RID: 55972
		// (set) Token: 0x0600DAA3 RID: 55971
		[DispId(-2147412021)]
		object onfocusin
		{
			[TypeLibFunc(20)]
			[DispId(-2147412021)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412021)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170048C8 RID: 18632
		// (get) Token: 0x0600DAA6 RID: 55974
		// (set) Token: 0x0600DAA5 RID: 55973
		[DispId(-2147412020)]
		object onfocusout
		{
			[TypeLibFunc(20)]
			[DispId(-2147412020)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412020)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170048C9 RID: 18633
		// (get) Token: 0x0600DAA8 RID: 55976
		// (set) Token: 0x0600DAA7 RID: 55975
		[DispId(-2147412025)]
		object onactivate
		{
			[DispId(-2147412025)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412025)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170048CA RID: 18634
		// (get) Token: 0x0600DAAA RID: 55978
		// (set) Token: 0x0600DAA9 RID: 55977
		[DispId(-2147412024)]
		object ondeactivate
		{
			[DispId(-2147412024)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170048CB RID: 18635
		// (get) Token: 0x0600DAAC RID: 55980
		// (set) Token: 0x0600DAAB RID: 55979
		[DispId(-2147412022)]
		object onbeforeactivate
		{
			[DispId(-2147412022)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412022)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170048CC RID: 18636
		// (get) Token: 0x0600DAAE RID: 55982
		// (set) Token: 0x0600DAAD RID: 55981
		[DispId(-2147412035)]
		object onbeforedeactivate
		{
			[DispId(-2147412035)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412035)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170048CD RID: 18637
		// (get) Token: 0x0600DAAF RID: 55983
		[DispId(1102)]
		string compatMode
		{
			[DispId(1102)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}
	}
}
