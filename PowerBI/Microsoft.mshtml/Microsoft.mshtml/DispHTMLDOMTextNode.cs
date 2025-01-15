using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000072 RID: 114
	[InterfaceType(2)]
	[Guid("3050F565-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4112)]
	[ComImport]
	public interface DispHTMLDOMTextNode
	{
		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06000B6B RID: 2923
		// (set) Token: 0x06000B6A RID: 2922
		[DispId(1000)]
		string data
		{
			[DispId(1000)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1000)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x06000B6C RID: 2924
		[DispId(1001)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		string toString();

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06000B6D RID: 2925
		[DispId(1002)]
		int length
		{
			[DispId(1002)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06000B6E RID: 2926
		[DispId(1003)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode splitText([In] int offset);

		// Token: 0x06000B6F RID: 2927
		[DispId(1004)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		string substringData([In] int offset, [In] int Count);

		// Token: 0x06000B70 RID: 2928
		[DispId(1005)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void appendData([MarshalAs(19)] [In] string bstrstring);

		// Token: 0x06000B71 RID: 2929
		[DispId(1006)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void insertData([In] int offset, [MarshalAs(19)] [In] string bstrstring);

		// Token: 0x06000B72 RID: 2930
		[DispId(1007)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void deleteData([In] int offset, [In] int Count);

		// Token: 0x06000B73 RID: 2931
		[DispId(1008)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void replaceData([In] int offset, [In] int Count, [MarshalAs(19)] [In] string bstrstring);

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06000B74 RID: 2932
		[DispId(-2147417066)]
		int nodeType
		{
			[DispId(-2147417066)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06000B75 RID: 2933
		[DispId(-2147417065)]
		IHTMLDOMNode parentNode
		{
			[DispId(-2147417065)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x06000B76 RID: 2934
		[DispId(-2147417064)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool hasChildNodes();

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06000B77 RID: 2935
		[DispId(-2147417063)]
		object childNodes
		{
			[DispId(-2147417063)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06000B78 RID: 2936
		[DispId(-2147417062)]
		object attributes
		{
			[DispId(-2147417062)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x06000B79 RID: 2937
		[DispId(-2147417061)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x06000B7A RID: 2938
		[DispId(-2147417060)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06000B7B RID: 2939
		[DispId(-2147417059)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06000B7C RID: 2940
		[DispId(-2147417051)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode cloneNode([In] bool fDeep);

		// Token: 0x06000B7D RID: 2941
		[DispId(-2147417046)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode removeNode([In] bool fDeep = false);

		// Token: 0x06000B7E RID: 2942
		[DispId(-2147417044)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x06000B7F RID: 2943
		[DispId(-2147417045)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x06000B80 RID: 2944
		[DispId(-2147417039)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06000B81 RID: 2945
		[DispId(-2147417038)]
		string nodeName
		{
			[DispId(-2147417038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x06000B83 RID: 2947
		// (set) Token: 0x06000B82 RID: 2946
		[DispId(-2147417037)]
		object nodeValue
		{
			[DispId(-2147417037)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147417037)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x06000B84 RID: 2948
		[DispId(-2147417036)]
		IHTMLDOMNode firstChild
		{
			[DispId(-2147417036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06000B85 RID: 2949
		[DispId(-2147417035)]
		IHTMLDOMNode lastChild
		{
			[DispId(-2147417035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06000B86 RID: 2950
		[DispId(-2147417034)]
		IHTMLDOMNode previousSibling
		{
			[DispId(-2147417034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06000B87 RID: 2951
		[DispId(-2147417033)]
		IHTMLDOMNode nextSibling
		{
			[DispId(-2147417033)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06000B88 RID: 2952
		[DispId(-2147416999)]
		object ownerDocument
		{
			[DispId(-2147416999)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}
	}
}
