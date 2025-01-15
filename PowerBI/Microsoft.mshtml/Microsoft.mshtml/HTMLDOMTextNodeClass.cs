using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000073 RID: 115
	[ClassInterface(0)]
	[TypeLibType(2)]
	[Guid("3050F4BA-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public class HTMLDOMTextNodeClass : DispHTMLDOMTextNode, HTMLDOMTextNode, IHTMLDOMTextNode, IHTMLDOMTextNode2, IHTMLDOMNode, IHTMLDOMNode2
	{
		// Token: 0x06000B89 RID: 2953
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLDOMTextNodeClass();

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06000B8B RID: 2955
		// (set) Token: 0x06000B8A RID: 2954
		[DispId(1000)]
		public virtual extern string data
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

		// Token: 0x06000B8C RID: 2956
		[DispId(1001)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string toString();

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06000B8D RID: 2957
		[DispId(1002)]
		public virtual extern int length
		{
			[DispId(1002)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06000B8E RID: 2958
		[DispId(1003)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode splitText([In] int offset);

		// Token: 0x06000B8F RID: 2959
		[DispId(1004)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string substringData([In] int offset, [In] int Count);

		// Token: 0x06000B90 RID: 2960
		[DispId(1005)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void appendData([MarshalAs(19)] [In] string bstrstring);

		// Token: 0x06000B91 RID: 2961
		[DispId(1006)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertData([In] int offset, [MarshalAs(19)] [In] string bstrstring);

		// Token: 0x06000B92 RID: 2962
		[DispId(1007)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void deleteData([In] int offset, [In] int Count);

		// Token: 0x06000B93 RID: 2963
		[DispId(1008)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void replaceData([In] int offset, [In] int Count, [MarshalAs(19)] [In] string bstrstring);

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06000B94 RID: 2964
		[DispId(-2147417066)]
		public virtual extern int nodeType
		{
			[DispId(-2147417066)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06000B95 RID: 2965
		[DispId(-2147417065)]
		public virtual extern IHTMLDOMNode parentNode
		{
			[DispId(-2147417065)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x06000B96 RID: 2966
		[DispId(-2147417064)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool hasChildNodes();

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06000B97 RID: 2967
		[DispId(-2147417063)]
		public virtual extern object childNodes
		{
			[DispId(-2147417063)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06000B98 RID: 2968
		[DispId(-2147417062)]
		public virtual extern object attributes
		{
			[DispId(-2147417062)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x06000B99 RID: 2969
		[DispId(-2147417061)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x06000B9A RID: 2970
		[DispId(-2147417060)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06000B9B RID: 2971
		[DispId(-2147417059)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06000B9C RID: 2972
		[DispId(-2147417051)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode cloneNode([In] bool fDeep);

		// Token: 0x06000B9D RID: 2973
		[DispId(-2147417046)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeNode([In] bool fDeep = false);

		// Token: 0x06000B9E RID: 2974
		[DispId(-2147417044)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x06000B9F RID: 2975
		[DispId(-2147417045)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x06000BA0 RID: 2976
		[DispId(-2147417039)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06000BA1 RID: 2977
		[DispId(-2147417038)]
		public virtual extern string nodeName
		{
			[DispId(-2147417038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06000BA3 RID: 2979
		// (set) Token: 0x06000BA2 RID: 2978
		[DispId(-2147417037)]
		public virtual extern object nodeValue
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

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x06000BA4 RID: 2980
		[DispId(-2147417036)]
		public virtual extern IHTMLDOMNode firstChild
		{
			[DispId(-2147417036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x06000BA5 RID: 2981
		[DispId(-2147417035)]
		public virtual extern IHTMLDOMNode lastChild
		{
			[DispId(-2147417035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x06000BA6 RID: 2982
		[DispId(-2147417034)]
		public virtual extern IHTMLDOMNode previousSibling
		{
			[DispId(-2147417034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06000BA7 RID: 2983
		[DispId(-2147417033)]
		public virtual extern IHTMLDOMNode nextSibling
		{
			[DispId(-2147417033)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06000BA8 RID: 2984
		[DispId(-2147416999)]
		public virtual extern object ownerDocument
		{
			[DispId(-2147416999)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06000BAA RID: 2986
		// (set) Token: 0x06000BA9 RID: 2985
		public virtual extern string IHTMLDOMTextNode_data
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x06000BAB RID: 2987
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLDOMTextNode_toString();

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06000BAC RID: 2988
		public virtual extern int IHTMLDOMTextNode_length
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06000BAD RID: 2989
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMTextNode_splitText([In] int offset);

		// Token: 0x06000BAE RID: 2990
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLDOMTextNode2_substringData([In] int offset, [In] int Count);

		// Token: 0x06000BAF RID: 2991
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLDOMTextNode2_appendData([MarshalAs(19)] [In] string bstrstring);

		// Token: 0x06000BB0 RID: 2992
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLDOMTextNode2_insertData([In] int offset, [MarshalAs(19)] [In] string bstrstring);

		// Token: 0x06000BB1 RID: 2993
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLDOMTextNode2_deleteData([In] int offset, [In] int Count);

		// Token: 0x06000BB2 RID: 2994
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLDOMTextNode2_replaceData([In] int offset, [In] int Count, [MarshalAs(19)] [In] string bstrstring);

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06000BB3 RID: 2995
		public virtual extern int IHTMLDOMNode_nodeType
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x06000BB4 RID: 2996
		public virtual extern IHTMLDOMNode IHTMLDOMNode_parentNode
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x06000BB5 RID: 2997
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLDOMNode_hasChildNodes();

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06000BB6 RID: 2998
		public virtual extern object IHTMLDOMNode_childNodes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06000BB7 RID: 2999
		public virtual extern object IHTMLDOMNode_attributes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x06000BB8 RID: 3000
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x06000BB9 RID: 3001
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06000BBA RID: 3002
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06000BBB RID: 3003
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_cloneNode([In] bool fDeep);

		// Token: 0x06000BBC RID: 3004
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeNode([In] bool fDeep = false);

		// Token: 0x06000BBD RID: 3005
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x06000BBE RID: 3006
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x06000BBF RID: 3007
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06000BC0 RID: 3008
		public virtual extern string IHTMLDOMNode_nodeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x06000BC2 RID: 3010
		// (set) Token: 0x06000BC1 RID: 3009
		public virtual extern object IHTMLDOMNode_nodeValue
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x06000BC3 RID: 3011
		public virtual extern IHTMLDOMNode IHTMLDOMNode_firstChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x06000BC4 RID: 3012
		public virtual extern IHTMLDOMNode IHTMLDOMNode_lastChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06000BC5 RID: 3013
		public virtual extern IHTMLDOMNode IHTMLDOMNode_previousSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x06000BC6 RID: 3014
		public virtual extern IHTMLDOMNode IHTMLDOMNode_nextSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x06000BC7 RID: 3015
		public virtual extern object IHTMLDOMNode2_ownerDocument
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}
	}
}
