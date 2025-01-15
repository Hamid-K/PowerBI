using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027EF RID: 10223
	[ChildElementInfo(typeof(LastColumn))]
	[ChildElementInfo(typeof(LastRow))]
	[ChildElementInfo(typeof(SoutheastCell))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(WholeTable))]
	[ChildElementInfo(typeof(Band1Horizontal))]
	[ChildElementInfo(typeof(Band2Horizontal))]
	[ChildElementInfo(typeof(Band1Vertical))]
	[ChildElementInfo(typeof(Band2Vertical))]
	[ChildElementInfo(typeof(FirstColumn))]
	[ChildElementInfo(typeof(TableBackground))]
	[ChildElementInfo(typeof(SouthwestCell))]
	[ChildElementInfo(typeof(FirstRow))]
	[ChildElementInfo(typeof(NortheastCell))]
	[ChildElementInfo(typeof(NorthwestCell))]
	[ChildElementInfo(typeof(ExtensionList))]
	internal abstract class TableStyleType : OpenXmlCompositeElement
	{
		// Token: 0x170064BE RID: 25790
		// (get) Token: 0x06013F43 RID: 81731 RVA: 0x0030DAE6 File Offset: 0x0030BCE6
		internal override string[] AttributeTagNames
		{
			get
			{
				return TableStyleType.attributeTagNames;
			}
		}

		// Token: 0x170064BF RID: 25791
		// (get) Token: 0x06013F44 RID: 81732 RVA: 0x0030DAED File Offset: 0x0030BCED
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TableStyleType.attributeNamespaceIds;
			}
		}

		// Token: 0x170064C0 RID: 25792
		// (get) Token: 0x06013F45 RID: 81733 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06013F46 RID: 81734 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "styleId")]
		public StringValue StyleId
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x170064C1 RID: 25793
		// (get) Token: 0x06013F47 RID: 81735 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06013F48 RID: 81736 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "styleName")]
		public StringValue StyleName
		{
			get
			{
				return (StringValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x06013F49 RID: 81737 RVA: 0x0030DAF4 File Offset: 0x0030BCF4
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "tblBg" == name)
			{
				return new TableBackground();
			}
			if (10 == namespaceId && "wholeTbl" == name)
			{
				return new WholeTable();
			}
			if (10 == namespaceId && "band1H" == name)
			{
				return new Band1Horizontal();
			}
			if (10 == namespaceId && "band2H" == name)
			{
				return new Band2Horizontal();
			}
			if (10 == namespaceId && "band1V" == name)
			{
				return new Band1Vertical();
			}
			if (10 == namespaceId && "band2V" == name)
			{
				return new Band2Vertical();
			}
			if (10 == namespaceId && "lastCol" == name)
			{
				return new LastColumn();
			}
			if (10 == namespaceId && "firstCol" == name)
			{
				return new FirstColumn();
			}
			if (10 == namespaceId && "lastRow" == name)
			{
				return new LastRow();
			}
			if (10 == namespaceId && "seCell" == name)
			{
				return new SoutheastCell();
			}
			if (10 == namespaceId && "swCell" == name)
			{
				return new SouthwestCell();
			}
			if (10 == namespaceId && "firstRow" == name)
			{
				return new FirstRow();
			}
			if (10 == namespaceId && "neCell" == name)
			{
				return new NortheastCell();
			}
			if (10 == namespaceId && "nwCell" == name)
			{
				return new NorthwestCell();
			}
			if (10 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x170064C2 RID: 25794
		// (get) Token: 0x06013F4A RID: 81738 RVA: 0x0030DC6A File Offset: 0x0030BE6A
		internal override string[] ElementTagNames
		{
			get
			{
				return TableStyleType.eleTagNames;
			}
		}

		// Token: 0x170064C3 RID: 25795
		// (get) Token: 0x06013F4B RID: 81739 RVA: 0x0030DC71 File Offset: 0x0030BE71
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return TableStyleType.eleNamespaceIds;
			}
		}

		// Token: 0x170064C4 RID: 25796
		// (get) Token: 0x06013F4C RID: 81740 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170064C5 RID: 25797
		// (get) Token: 0x06013F4D RID: 81741 RVA: 0x0030DC78 File Offset: 0x0030BE78
		// (set) Token: 0x06013F4E RID: 81742 RVA: 0x0030DC81 File Offset: 0x0030BE81
		public TableBackground TableBackground
		{
			get
			{
				return base.GetElement<TableBackground>(0);
			}
			set
			{
				base.SetElement<TableBackground>(0, value);
			}
		}

		// Token: 0x170064C6 RID: 25798
		// (get) Token: 0x06013F4F RID: 81743 RVA: 0x0030DC8B File Offset: 0x0030BE8B
		// (set) Token: 0x06013F50 RID: 81744 RVA: 0x0030DC94 File Offset: 0x0030BE94
		public WholeTable WholeTable
		{
			get
			{
				return base.GetElement<WholeTable>(1);
			}
			set
			{
				base.SetElement<WholeTable>(1, value);
			}
		}

		// Token: 0x170064C7 RID: 25799
		// (get) Token: 0x06013F51 RID: 81745 RVA: 0x0030DC9E File Offset: 0x0030BE9E
		// (set) Token: 0x06013F52 RID: 81746 RVA: 0x0030DCA7 File Offset: 0x0030BEA7
		public Band1Horizontal Band1Horizontal
		{
			get
			{
				return base.GetElement<Band1Horizontal>(2);
			}
			set
			{
				base.SetElement<Band1Horizontal>(2, value);
			}
		}

		// Token: 0x170064C8 RID: 25800
		// (get) Token: 0x06013F53 RID: 81747 RVA: 0x0030DCB1 File Offset: 0x0030BEB1
		// (set) Token: 0x06013F54 RID: 81748 RVA: 0x0030DCBA File Offset: 0x0030BEBA
		public Band2Horizontal Band2Horizontal
		{
			get
			{
				return base.GetElement<Band2Horizontal>(3);
			}
			set
			{
				base.SetElement<Band2Horizontal>(3, value);
			}
		}

		// Token: 0x170064C9 RID: 25801
		// (get) Token: 0x06013F55 RID: 81749 RVA: 0x0030DCC4 File Offset: 0x0030BEC4
		// (set) Token: 0x06013F56 RID: 81750 RVA: 0x0030DCCD File Offset: 0x0030BECD
		public Band1Vertical Band1Vertical
		{
			get
			{
				return base.GetElement<Band1Vertical>(4);
			}
			set
			{
				base.SetElement<Band1Vertical>(4, value);
			}
		}

		// Token: 0x170064CA RID: 25802
		// (get) Token: 0x06013F57 RID: 81751 RVA: 0x0030DCD7 File Offset: 0x0030BED7
		// (set) Token: 0x06013F58 RID: 81752 RVA: 0x0030DCE0 File Offset: 0x0030BEE0
		public Band2Vertical Band2Vertical
		{
			get
			{
				return base.GetElement<Band2Vertical>(5);
			}
			set
			{
				base.SetElement<Band2Vertical>(5, value);
			}
		}

		// Token: 0x170064CB RID: 25803
		// (get) Token: 0x06013F59 RID: 81753 RVA: 0x0030DCEA File Offset: 0x0030BEEA
		// (set) Token: 0x06013F5A RID: 81754 RVA: 0x0030DCF3 File Offset: 0x0030BEF3
		public LastColumn LastColumn
		{
			get
			{
				return base.GetElement<LastColumn>(6);
			}
			set
			{
				base.SetElement<LastColumn>(6, value);
			}
		}

		// Token: 0x170064CC RID: 25804
		// (get) Token: 0x06013F5B RID: 81755 RVA: 0x0030DCFD File Offset: 0x0030BEFD
		// (set) Token: 0x06013F5C RID: 81756 RVA: 0x0030DD06 File Offset: 0x0030BF06
		public FirstColumn FirstColumn
		{
			get
			{
				return base.GetElement<FirstColumn>(7);
			}
			set
			{
				base.SetElement<FirstColumn>(7, value);
			}
		}

		// Token: 0x170064CD RID: 25805
		// (get) Token: 0x06013F5D RID: 81757 RVA: 0x0030DD10 File Offset: 0x0030BF10
		// (set) Token: 0x06013F5E RID: 81758 RVA: 0x0030DD19 File Offset: 0x0030BF19
		public LastRow LastRow
		{
			get
			{
				return base.GetElement<LastRow>(8);
			}
			set
			{
				base.SetElement<LastRow>(8, value);
			}
		}

		// Token: 0x170064CE RID: 25806
		// (get) Token: 0x06013F5F RID: 81759 RVA: 0x0030DD23 File Offset: 0x0030BF23
		// (set) Token: 0x06013F60 RID: 81760 RVA: 0x0030DD2D File Offset: 0x0030BF2D
		public SoutheastCell SoutheastCell
		{
			get
			{
				return base.GetElement<SoutheastCell>(9);
			}
			set
			{
				base.SetElement<SoutheastCell>(9, value);
			}
		}

		// Token: 0x170064CF RID: 25807
		// (get) Token: 0x06013F61 RID: 81761 RVA: 0x0030DD38 File Offset: 0x0030BF38
		// (set) Token: 0x06013F62 RID: 81762 RVA: 0x0030DD42 File Offset: 0x0030BF42
		public SouthwestCell SouthwestCell
		{
			get
			{
				return base.GetElement<SouthwestCell>(10);
			}
			set
			{
				base.SetElement<SouthwestCell>(10, value);
			}
		}

		// Token: 0x170064D0 RID: 25808
		// (get) Token: 0x06013F63 RID: 81763 RVA: 0x0030DD4D File Offset: 0x0030BF4D
		// (set) Token: 0x06013F64 RID: 81764 RVA: 0x0030DD57 File Offset: 0x0030BF57
		public FirstRow FirstRow
		{
			get
			{
				return base.GetElement<FirstRow>(11);
			}
			set
			{
				base.SetElement<FirstRow>(11, value);
			}
		}

		// Token: 0x170064D1 RID: 25809
		// (get) Token: 0x06013F65 RID: 81765 RVA: 0x0030DD62 File Offset: 0x0030BF62
		// (set) Token: 0x06013F66 RID: 81766 RVA: 0x0030DD6C File Offset: 0x0030BF6C
		public NortheastCell NortheastCell
		{
			get
			{
				return base.GetElement<NortheastCell>(12);
			}
			set
			{
				base.SetElement<NortheastCell>(12, value);
			}
		}

		// Token: 0x170064D2 RID: 25810
		// (get) Token: 0x06013F67 RID: 81767 RVA: 0x0030DD77 File Offset: 0x0030BF77
		// (set) Token: 0x06013F68 RID: 81768 RVA: 0x0030DD81 File Offset: 0x0030BF81
		public NorthwestCell NorthwestCell
		{
			get
			{
				return base.GetElement<NorthwestCell>(13);
			}
			set
			{
				base.SetElement<NorthwestCell>(13, value);
			}
		}

		// Token: 0x170064D3 RID: 25811
		// (get) Token: 0x06013F69 RID: 81769 RVA: 0x0030DD8C File Offset: 0x0030BF8C
		// (set) Token: 0x06013F6A RID: 81770 RVA: 0x0030DD96 File Offset: 0x0030BF96
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(14);
			}
			set
			{
				base.SetElement<ExtensionList>(14, value);
			}
		}

		// Token: 0x06013F6B RID: 81771 RVA: 0x0030DDA1 File Offset: 0x0030BFA1
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "styleId" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "styleName" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013F6C RID: 81772 RVA: 0x00293ECF File Offset: 0x002920CF
		protected TableStyleType()
		{
		}

		// Token: 0x06013F6D RID: 81773 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected TableStyleType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013F6E RID: 81774 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected TableStyleType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013F6F RID: 81775 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected TableStyleType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013F70 RID: 81776 RVA: 0x0030DDD8 File Offset: 0x0030BFD8
		// Note: this type is marked as 'beforefieldinit'.
		static TableStyleType()
		{
			byte[] array = new byte[2];
			TableStyleType.attributeNamespaceIds = array;
			TableStyleType.eleTagNames = new string[]
			{
				"tblBg", "wholeTbl", "band1H", "band2H", "band1V", "band2V", "lastCol", "firstCol", "lastRow", "seCell",
				"swCell", "firstRow", "neCell", "nwCell", "extLst"
			};
			TableStyleType.eleNamespaceIds = new byte[]
			{
				10, 10, 10, 10, 10, 10, 10, 10, 10, 10,
				10, 10, 10, 10, 10
			};
		}

		// Token: 0x04008869 RID: 34921
		private static string[] attributeTagNames = new string[] { "styleId", "styleName" };

		// Token: 0x0400886A RID: 34922
		private static byte[] attributeNamespaceIds;

		// Token: 0x0400886B RID: 34923
		private static readonly string[] eleTagNames;

		// Token: 0x0400886C RID: 34924
		private static readonly byte[] eleNamespaceIds;
	}
}
