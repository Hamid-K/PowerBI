using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002812 RID: 10258
	[ChildElementInfo(typeof(PictureBullet))]
	[ChildElementInfo(typeof(BulletSizePercentage))]
	[ChildElementInfo(typeof(BulletSizePoints))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(LineSpacing))]
	[ChildElementInfo(typeof(SpaceBefore))]
	[ChildElementInfo(typeof(SpaceAfter))]
	[ChildElementInfo(typeof(BulletColorText))]
	[ChildElementInfo(typeof(BulletColor))]
	[ChildElementInfo(typeof(BulletSizeText))]
	[ChildElementInfo(typeof(BulletFontText))]
	[ChildElementInfo(typeof(BulletFont))]
	[ChildElementInfo(typeof(NoBullet))]
	[ChildElementInfo(typeof(AutoNumberedBullet))]
	[ChildElementInfo(typeof(CharacterBullet))]
	[ChildElementInfo(typeof(TabStopList))]
	[ChildElementInfo(typeof(DefaultRunProperties))]
	[ChildElementInfo(typeof(ExtensionList))]
	internal abstract class TextParagraphPropertiesType : OpenXmlCompositeElement
	{
		// Token: 0x17006565 RID: 25957
		// (get) Token: 0x060140F9 RID: 82169 RVA: 0x0030EBA3 File Offset: 0x0030CDA3
		internal override string[] AttributeTagNames
		{
			get
			{
				return TextParagraphPropertiesType.attributeTagNames;
			}
		}

		// Token: 0x17006566 RID: 25958
		// (get) Token: 0x060140FA RID: 82170 RVA: 0x0030EBAA File Offset: 0x0030CDAA
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TextParagraphPropertiesType.attributeNamespaceIds;
			}
		}

		// Token: 0x17006567 RID: 25959
		// (get) Token: 0x060140FB RID: 82171 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x060140FC RID: 82172 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "marL")]
		public Int32Value LeftMargin
		{
			get
			{
				return (Int32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17006568 RID: 25960
		// (get) Token: 0x060140FD RID: 82173 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x060140FE RID: 82174 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "marR")]
		public Int32Value RightMargin
		{
			get
			{
				return (Int32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17006569 RID: 25961
		// (get) Token: 0x060140FF RID: 82175 RVA: 0x002E1683 File Offset: 0x002DF883
		// (set) Token: 0x06014100 RID: 82176 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "lvl")]
		public Int32Value Level
		{
			get
			{
				return (Int32Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x1700656A RID: 25962
		// (get) Token: 0x06014101 RID: 82177 RVA: 0x002BFA76 File Offset: 0x002BDC76
		// (set) Token: 0x06014102 RID: 82178 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "indent")]
		public Int32Value Indent
		{
			get
			{
				return (Int32Value)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x1700656B RID: 25963
		// (get) Token: 0x06014103 RID: 82179 RVA: 0x0030EBB1 File Offset: 0x0030CDB1
		// (set) Token: 0x06014104 RID: 82180 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "algn")]
		public EnumValue<TextAlignmentTypeValues> Alignment
		{
			get
			{
				return (EnumValue<TextAlignmentTypeValues>)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x1700656C RID: 25964
		// (get) Token: 0x06014105 RID: 82181 RVA: 0x002ED371 File Offset: 0x002EB571
		// (set) Token: 0x06014106 RID: 82182 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "defTabSz")]
		public Int32Value DefaultTabSize
		{
			get
			{
				return (Int32Value)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x1700656D RID: 25965
		// (get) Token: 0x06014107 RID: 82183 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x06014108 RID: 82184 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "rtl")]
		public BooleanValue RightToLeft
		{
			get
			{
				return (BooleanValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x1700656E RID: 25966
		// (get) Token: 0x06014109 RID: 82185 RVA: 0x002CA736 File Offset: 0x002C8936
		// (set) Token: 0x0601410A RID: 82186 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "eaLnBrk")]
		public BooleanValue EastAsianLineBreak
		{
			get
			{
				return (BooleanValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x1700656F RID: 25967
		// (get) Token: 0x0601410B RID: 82187 RVA: 0x0030EBC0 File Offset: 0x0030CDC0
		// (set) Token: 0x0601410C RID: 82188 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "fontAlgn")]
		public EnumValue<TextFontAlignmentValues> FontAlignment
		{
			get
			{
				return (EnumValue<TextFontAlignmentValues>)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x17006570 RID: 25968
		// (get) Token: 0x0601410D RID: 82189 RVA: 0x002C92EA File Offset: 0x002C74EA
		// (set) Token: 0x0601410E RID: 82190 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "latinLnBrk")]
		public BooleanValue LatinLineBreak
		{
			get
			{
				return (BooleanValue)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x17006571 RID: 25969
		// (get) Token: 0x0601410F RID: 82191 RVA: 0x002C8762 File Offset: 0x002C6962
		// (set) Token: 0x06014110 RID: 82192 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "hangingPunct")]
		public BooleanValue Height
		{
			get
			{
				return (BooleanValue)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x06014111 RID: 82193 RVA: 0x0030EBD0 File Offset: 0x0030CDD0
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "lnSpc" == name)
			{
				return new LineSpacing();
			}
			if (10 == namespaceId && "spcBef" == name)
			{
				return new SpaceBefore();
			}
			if (10 == namespaceId && "spcAft" == name)
			{
				return new SpaceAfter();
			}
			if (10 == namespaceId && "buClrTx" == name)
			{
				return new BulletColorText();
			}
			if (10 == namespaceId && "buClr" == name)
			{
				return new BulletColor();
			}
			if (10 == namespaceId && "buSzTx" == name)
			{
				return new BulletSizeText();
			}
			if (10 == namespaceId && "buSzPct" == name)
			{
				return new BulletSizePercentage();
			}
			if (10 == namespaceId && "buSzPts" == name)
			{
				return new BulletSizePoints();
			}
			if (10 == namespaceId && "buFontTx" == name)
			{
				return new BulletFontText();
			}
			if (10 == namespaceId && "buFont" == name)
			{
				return new BulletFont();
			}
			if (10 == namespaceId && "buNone" == name)
			{
				return new NoBullet();
			}
			if (10 == namespaceId && "buAutoNum" == name)
			{
				return new AutoNumberedBullet();
			}
			if (10 == namespaceId && "buChar" == name)
			{
				return new CharacterBullet();
			}
			if (10 == namespaceId && "buBlip" == name)
			{
				return new PictureBullet();
			}
			if (10 == namespaceId && "tabLst" == name)
			{
				return new TabStopList();
			}
			if (10 == namespaceId && "defRPr" == name)
			{
				return new DefaultRunProperties();
			}
			if (10 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17006572 RID: 25970
		// (get) Token: 0x06014112 RID: 82194 RVA: 0x0030ED76 File Offset: 0x0030CF76
		internal override string[] ElementTagNames
		{
			get
			{
				return TextParagraphPropertiesType.eleTagNames;
			}
		}

		// Token: 0x17006573 RID: 25971
		// (get) Token: 0x06014113 RID: 82195 RVA: 0x0030ED7D File Offset: 0x0030CF7D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return TextParagraphPropertiesType.eleNamespaceIds;
			}
		}

		// Token: 0x17006574 RID: 25972
		// (get) Token: 0x06014114 RID: 82196 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006575 RID: 25973
		// (get) Token: 0x06014115 RID: 82197 RVA: 0x0030ED84 File Offset: 0x0030CF84
		// (set) Token: 0x06014116 RID: 82198 RVA: 0x0030ED8D File Offset: 0x0030CF8D
		public LineSpacing LineSpacing
		{
			get
			{
				return base.GetElement<LineSpacing>(0);
			}
			set
			{
				base.SetElement<LineSpacing>(0, value);
			}
		}

		// Token: 0x17006576 RID: 25974
		// (get) Token: 0x06014117 RID: 82199 RVA: 0x0030ED97 File Offset: 0x0030CF97
		// (set) Token: 0x06014118 RID: 82200 RVA: 0x0030EDA0 File Offset: 0x0030CFA0
		public SpaceBefore SpaceBefore
		{
			get
			{
				return base.GetElement<SpaceBefore>(1);
			}
			set
			{
				base.SetElement<SpaceBefore>(1, value);
			}
		}

		// Token: 0x17006577 RID: 25975
		// (get) Token: 0x06014119 RID: 82201 RVA: 0x0030EDAA File Offset: 0x0030CFAA
		// (set) Token: 0x0601411A RID: 82202 RVA: 0x0030EDB3 File Offset: 0x0030CFB3
		public SpaceAfter SpaceAfter
		{
			get
			{
				return base.GetElement<SpaceAfter>(2);
			}
			set
			{
				base.SetElement<SpaceAfter>(2, value);
			}
		}

		// Token: 0x0601411B RID: 82203 RVA: 0x0030EDC0 File Offset: 0x0030CFC0
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "marL" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "marR" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "lvl" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "indent" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "algn" == name)
			{
				return new EnumValue<TextAlignmentTypeValues>();
			}
			if (namespaceId == 0 && "defTabSz" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "rtl" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "eaLnBrk" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "fontAlgn" == name)
			{
				return new EnumValue<TextFontAlignmentValues>();
			}
			if (namespaceId == 0 && "latinLnBrk" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "hangingPunct" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601411C RID: 82204 RVA: 0x00293ECF File Offset: 0x002920CF
		protected TextParagraphPropertiesType()
		{
		}

		// Token: 0x0601411D RID: 82205 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected TextParagraphPropertiesType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601411E RID: 82206 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected TextParagraphPropertiesType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601411F RID: 82207 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected TextParagraphPropertiesType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014120 RID: 82208 RVA: 0x0030EEC8 File Offset: 0x0030D0C8
		// Note: this type is marked as 'beforefieldinit'.
		static TextParagraphPropertiesType()
		{
			byte[] array = new byte[11];
			TextParagraphPropertiesType.attributeNamespaceIds = array;
			TextParagraphPropertiesType.eleTagNames = new string[]
			{
				"lnSpc", "spcBef", "spcAft", "buClrTx", "buClr", "buSzTx", "buSzPct", "buSzPts", "buFontTx", "buFont",
				"buNone", "buAutoNum", "buChar", "buBlip", "tabLst", "defRPr", "extLst"
			};
			TextParagraphPropertiesType.eleNamespaceIds = new byte[]
			{
				10, 10, 10, 10, 10, 10, 10, 10, 10, 10,
				10, 10, 10, 10, 10, 10, 10
			};
		}

		// Token: 0x040088DF RID: 35039
		private static string[] attributeTagNames = new string[]
		{
			"marL", "marR", "lvl", "indent", "algn", "defTabSz", "rtl", "eaLnBrk", "fontAlgn", "latinLnBrk",
			"hangingPunct"
		};

		// Token: 0x040088E0 RID: 35040
		private static byte[] attributeNamespaceIds;

		// Token: 0x040088E1 RID: 35041
		private static readonly string[] eleTagNames;

		// Token: 0x040088E2 RID: 35042
		private static readonly byte[] eleNamespaceIds;
	}
}
