using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000070 RID: 112
	[ClassInterface(0)]
	[Guid("3050F4B2-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(2)]
	[ComImport]
	public class HTMLDOMAttributeClass : DispHTMLDOMAttribute, HTMLDOMAttribute, IHTMLDOMAttribute, IHTMLDOMAttribute2
	{
		// Token: 0x06000B3B RID: 2875
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLDOMAttributeClass();

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06000B3C RID: 2876
		[DispId(1000)]
		public virtual extern string nodeName
		{
			[DispId(1000)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06000B3E RID: 2878
		// (set) Token: 0x06000B3D RID: 2877
		[DispId(1002)]
		public virtual extern object nodeValue
		{
			[DispId(1002)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(1002)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06000B3F RID: 2879
		[DispId(1001)]
		public virtual extern bool specified
		{
			[DispId(1001)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06000B40 RID: 2880
		[DispId(1003)]
		public virtual extern string name
		{
			[DispId(1003)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06000B42 RID: 2882
		// (set) Token: 0x06000B41 RID: 2881
		[DispId(1004)]
		public virtual extern string value
		{
			[DispId(1004)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1004)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06000B43 RID: 2883
		[DispId(1005)]
		public virtual extern bool expando
		{
			[DispId(1005)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06000B44 RID: 2884
		[DispId(1006)]
		public virtual extern int nodeType
		{
			[DispId(1006)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06000B45 RID: 2885
		[DispId(1007)]
		public virtual extern IHTMLDOMNode parentNode
		{
			[DispId(1007)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x06000B46 RID: 2886
		[DispId(1008)]
		public virtual extern object childNodes
		{
			[DispId(1008)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x06000B47 RID: 2887
		[DispId(1009)]
		public virtual extern IHTMLDOMNode firstChild
		{
			[DispId(1009)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06000B48 RID: 2888
		[DispId(1010)]
		public virtual extern IHTMLDOMNode lastChild
		{
			[DispId(1010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x06000B49 RID: 2889
		[DispId(1011)]
		public virtual extern IHTMLDOMNode previousSibling
		{
			[DispId(1011)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06000B4A RID: 2890
		[DispId(1012)]
		public virtual extern IHTMLDOMNode nextSibling
		{
			[DispId(1012)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06000B4B RID: 2891
		[DispId(1013)]
		public virtual extern object attributes
		{
			[DispId(1013)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06000B4C RID: 2892
		[DispId(1014)]
		public virtual extern object ownerDocument
		{
			[DispId(1014)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x06000B4D RID: 2893
		[DispId(1015)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x06000B4E RID: 2894
		[DispId(1016)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06000B4F RID: 2895
		[DispId(1017)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06000B50 RID: 2896
		[DispId(1018)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x06000B51 RID: 2897
		[DispId(1019)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool hasChildNodes();

		// Token: 0x06000B52 RID: 2898
		[DispId(1020)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute cloneNode([In] bool fDeep);

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x06000B53 RID: 2899
		public virtual extern string IHTMLDOMAttribute_nodeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06000B55 RID: 2901
		// (set) Token: 0x06000B54 RID: 2900
		public virtual extern object IHTMLDOMAttribute_nodeValue
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06000B56 RID: 2902
		public virtual extern bool IHTMLDOMAttribute_specified
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x06000B57 RID: 2903
		public virtual extern string IHTMLDOMAttribute2_name
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06000B59 RID: 2905
		// (set) Token: 0x06000B58 RID: 2904
		public virtual extern string IHTMLDOMAttribute2_value
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06000B5A RID: 2906
		public virtual extern bool IHTMLDOMAttribute2_expando
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06000B5B RID: 2907
		public virtual extern int IHTMLDOMAttribute2_nodeType
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x06000B5C RID: 2908
		public virtual extern IHTMLDOMNode IHTMLDOMAttribute2_parentNode
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06000B5D RID: 2909
		public virtual extern object IHTMLDOMAttribute2_childNodes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x06000B5E RID: 2910
		public virtual extern IHTMLDOMNode IHTMLDOMAttribute2_firstChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06000B5F RID: 2911
		public virtual extern IHTMLDOMNode IHTMLDOMAttribute2_lastChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06000B60 RID: 2912
		public virtual extern IHTMLDOMNode IHTMLDOMAttribute2_previousSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06000B61 RID: 2913
		public virtual extern IHTMLDOMNode IHTMLDOMAttribute2_nextSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06000B62 RID: 2914
		public virtual extern object IHTMLDOMAttribute2_attributes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06000B63 RID: 2915
		public virtual extern object IHTMLDOMAttribute2_ownerDocument
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x06000B64 RID: 2916
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMAttribute2_insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x06000B65 RID: 2917
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMAttribute2_replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06000B66 RID: 2918
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMAttribute2_removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06000B67 RID: 2919
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMAttribute2_appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x06000B68 RID: 2920
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLDOMAttribute2_hasChildNodes();

		// Token: 0x06000B69 RID: 2921
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLDOMAttribute2_cloneNode([In] bool fDeep);
	}
}
