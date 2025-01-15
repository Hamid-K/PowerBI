using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020007E3 RID: 2019
	[TypeLibType(4160)]
	[Guid("3050F69A-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface IHTMLDocument4
	{
		// Token: 0x0600DA8F RID: 55951
		[DispId(1089)]
		[MethodImpl(4096, MethodCodeType = 3)]
		void focus();

		// Token: 0x0600DA90 RID: 55952
		[DispId(1090)]
		[MethodImpl(4096, MethodCodeType = 3)]
		bool hasFocus();

		// Token: 0x170048BF RID: 18623
		// (get) Token: 0x0600DA92 RID: 55954
		// (set) Token: 0x0600DA91 RID: 55953
		[DispId(-2147412032)]
		object onselectionchange
		{
			[TypeLibFunc(20)]
			[DispId(-2147412032)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412032)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170048C0 RID: 18624
		// (get) Token: 0x0600DA93 RID: 55955
		[DispId(1091)]
		object namespaces
		{
			[DispId(1091)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x0600DA94 RID: 55956
		[DispId(1092)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDocument2 createDocumentFromUrl([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(19)] [In] string bstrOptions);

		// Token: 0x170048C1 RID: 18625
		// (get) Token: 0x0600DA96 RID: 55958
		// (set) Token: 0x0600DA95 RID: 55957
		[DispId(1093)]
		string media
		{
			[DispId(1093)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1093)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x0600DA97 RID: 55959
		[DispId(1094)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLEventObj CreateEventObject([MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x0600DA98 RID: 55960
		[DispId(1095)]
		[MethodImpl(4096, MethodCodeType = 3)]
		bool FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x0600DA99 RID: 55961
		[DispId(1096)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLRenderStyle createRenderStyle([MarshalAs(19)] [In] string v);

		// Token: 0x170048C2 RID: 18626
		// (get) Token: 0x0600DA9B RID: 55963
		// (set) Token: 0x0600DA9A RID: 55962
		[DispId(-2147412033)]
		object oncontrolselect
		{
			[TypeLibFunc(20)]
			[DispId(-2147412033)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412033)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170048C3 RID: 18627
		// (get) Token: 0x0600DA9C RID: 55964
		[DispId(1097)]
		string URLUnencoded
		{
			[DispId(1097)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}
	}
}
