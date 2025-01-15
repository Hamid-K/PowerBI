using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200006B RID: 107
	[Guid("3050F810-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLDOMAttribute2
	{
		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06000B06 RID: 2822
		[DispId(1003)]
		string name
		{
			[DispId(1003)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06000B08 RID: 2824
		// (set) Token: 0x06000B07 RID: 2823
		[DispId(1004)]
		string value
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

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x06000B09 RID: 2825
		[DispId(1005)]
		bool expando
		{
			[DispId(1005)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06000B0A RID: 2826
		[DispId(1006)]
		int nodeType
		{
			[DispId(1006)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06000B0B RID: 2827
		[DispId(1007)]
		IHTMLDOMNode parentNode
		{
			[DispId(1007)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06000B0C RID: 2828
		[DispId(1008)]
		object childNodes
		{
			[DispId(1008)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06000B0D RID: 2829
		[DispId(1009)]
		IHTMLDOMNode firstChild
		{
			[DispId(1009)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06000B0E RID: 2830
		[DispId(1010)]
		IHTMLDOMNode lastChild
		{
			[DispId(1010)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06000B0F RID: 2831
		[DispId(1011)]
		IHTMLDOMNode previousSibling
		{
			[DispId(1011)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06000B10 RID: 2832
		[DispId(1012)]
		IHTMLDOMNode nextSibling
		{
			[DispId(1012)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06000B11 RID: 2833
		[DispId(1013)]
		object attributes
		{
			[DispId(1013)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06000B12 RID: 2834
		[DispId(1014)]
		object ownerDocument
		{
			[DispId(1014)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x06000B13 RID: 2835
		[DispId(1015)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x06000B14 RID: 2836
		[DispId(1016)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06000B15 RID: 2837
		[DispId(1017)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06000B16 RID: 2838
		[DispId(1018)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x06000B17 RID: 2839
		[DispId(1019)]
		[MethodImpl(4096, MethodCodeType = 3)]
		bool hasChildNodes();

		// Token: 0x06000B18 RID: 2840
		[DispId(1020)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMAttribute cloneNode([In] bool fDeep);
	}
}
