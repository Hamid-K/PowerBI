using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FDE RID: 12254
	[GeneratedCode("DomGen", "2.0")]
	internal class StylePaneFormatFilter : OpenXmlLeafElement
	{
		// Token: 0x170094AD RID: 38061
		// (get) Token: 0x0601A9F5 RID: 109045 RVA: 0x00365079 File Offset: 0x00363279
		public override string LocalName
		{
			get
			{
				return "stylePaneFormatFilter";
			}
		}

		// Token: 0x170094AE RID: 38062
		// (get) Token: 0x0601A9F6 RID: 109046 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170094AF RID: 38063
		// (get) Token: 0x0601A9F7 RID: 109047 RVA: 0x00365080 File Offset: 0x00363280
		internal override int ElementTypeId
		{
			get
			{
				return 11984;
			}
		}

		// Token: 0x0601A9F8 RID: 109048 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170094B0 RID: 38064
		// (get) Token: 0x0601A9F9 RID: 109049 RVA: 0x00365087 File Offset: 0x00363287
		internal override string[] AttributeTagNames
		{
			get
			{
				return StylePaneFormatFilter.attributeTagNames;
			}
		}

		// Token: 0x170094B1 RID: 38065
		// (get) Token: 0x0601A9FA RID: 109050 RVA: 0x0036508E File Offset: 0x0036328E
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return StylePaneFormatFilter.attributeNamespaceIds;
			}
		}

		// Token: 0x170094B2 RID: 38066
		// (get) Token: 0x0601A9FB RID: 109051 RVA: 0x002EA130 File Offset: 0x002E8330
		// (set) Token: 0x0601A9FC RID: 109052 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public HexBinaryValue Val
		{
			get
			{
				return (HexBinaryValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x170094B3 RID: 38067
		// (get) Token: 0x0601A9FD RID: 109053 RVA: 0x003480EF File Offset: 0x003462EF
		// (set) Token: 0x0601A9FE RID: 109054 RVA: 0x002BD47A File Offset: 0x002BB67A
		[OfficeAvailability(FileFormatVersions.Office2010)]
		[SchemaAttr(23, "allStyles")]
		public OnOffValue AllStyles
		{
			get
			{
				return (OnOffValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x170094B4 RID: 38068
		// (get) Token: 0x0601A9FF RID: 109055 RVA: 0x003461ED File Offset: 0x003443ED
		// (set) Token: 0x0601AA00 RID: 109056 RVA: 0x002BD494 File Offset: 0x002BB694
		[OfficeAvailability(FileFormatVersions.Office2010)]
		[SchemaAttr(23, "customStyles")]
		public OnOffValue CustomStyles
		{
			get
			{
				return (OnOffValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x170094B5 RID: 38069
		// (get) Token: 0x0601AA01 RID: 109057 RVA: 0x003474AC File Offset: 0x003456AC
		// (set) Token: 0x0601AA02 RID: 109058 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(23, "latentStyles")]
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public OnOffValue LatentStyles
		{
			get
			{
				return (OnOffValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x170094B6 RID: 38070
		// (get) Token: 0x0601AA03 RID: 109059 RVA: 0x002EB443 File Offset: 0x002E9643
		// (set) Token: 0x0601AA04 RID: 109060 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(23, "stylesInUse")]
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public OnOffValue StylesInUse
		{
			get
			{
				return (OnOffValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x170094B7 RID: 38071
		// (get) Token: 0x0601AA05 RID: 109061 RVA: 0x003461FC File Offset: 0x003443FC
		// (set) Token: 0x0601AA06 RID: 109062 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[OfficeAvailability(FileFormatVersions.Office2010)]
		[SchemaAttr(23, "headingStyles")]
		public OnOffValue HeadingStyles
		{
			get
			{
				return (OnOffValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x170094B8 RID: 38072
		// (get) Token: 0x0601AA07 RID: 109063 RVA: 0x00353104 File Offset: 0x00351304
		// (set) Token: 0x0601AA08 RID: 109064 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[OfficeAvailability(FileFormatVersions.Office2010)]
		[SchemaAttr(23, "numberingStyles")]
		public OnOffValue NumberingStyles
		{
			get
			{
				return (OnOffValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x170094B9 RID: 38073
		// (get) Token: 0x0601AA09 RID: 109065 RVA: 0x00348E89 File Offset: 0x00347089
		// (set) Token: 0x0601AA0A RID: 109066 RVA: 0x002BD516 File Offset: 0x002BB716
		[OfficeAvailability(FileFormatVersions.Office2010)]
		[SchemaAttr(23, "tableStyles")]
		public OnOffValue TableStyles
		{
			get
			{
				return (OnOffValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x170094BA RID: 38074
		// (get) Token: 0x0601AA0B RID: 109067 RVA: 0x00348E98 File Offset: 0x00347098
		// (set) Token: 0x0601AA0C RID: 109068 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(23, "directFormattingOnRuns")]
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public OnOffValue DirectFormattingOnRuns
		{
			get
			{
				return (OnOffValue)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x170094BB RID: 38075
		// (get) Token: 0x0601AA0D RID: 109069 RVA: 0x00353113 File Offset: 0x00351313
		// (set) Token: 0x0601AA0E RID: 109070 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(23, "directFormattingOnParagraphs")]
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public OnOffValue DirectFormattingOnParagraphs
		{
			get
			{
				return (OnOffValue)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x170094BC RID: 38076
		// (get) Token: 0x0601AA0F RID: 109071 RVA: 0x00353123 File Offset: 0x00351323
		// (set) Token: 0x0601AA10 RID: 109072 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[OfficeAvailability(FileFormatVersions.Office2010)]
		[SchemaAttr(23, "directFormattingOnNumbering")]
		public OnOffValue DirectFormattingOnNumbering
		{
			get
			{
				return (OnOffValue)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x170094BD RID: 38077
		// (get) Token: 0x0601AA11 RID: 109073 RVA: 0x00353133 File Offset: 0x00351333
		// (set) Token: 0x0601AA12 RID: 109074 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[OfficeAvailability(FileFormatVersions.Office2010)]
		[SchemaAttr(23, "directFormattingOnTables")]
		public OnOffValue DirectFormattingOnTables
		{
			get
			{
				return (OnOffValue)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x170094BE RID: 38078
		// (get) Token: 0x0601AA13 RID: 109075 RVA: 0x00353143 File Offset: 0x00351343
		// (set) Token: 0x0601AA14 RID: 109076 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[OfficeAvailability(FileFormatVersions.Office2010)]
		[SchemaAttr(23, "clearFormatting")]
		public OnOffValue ClearFormatting
		{
			get
			{
				return (OnOffValue)base.Attributes[12];
			}
			set
			{
				base.Attributes[12] = value;
			}
		}

		// Token: 0x170094BF RID: 38079
		// (get) Token: 0x0601AA15 RID: 109077 RVA: 0x00365095 File Offset: 0x00363295
		// (set) Token: 0x0601AA16 RID: 109078 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(23, "top3HeadingStyles")]
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public OnOffValue Top3HeadingStyles
		{
			get
			{
				return (OnOffValue)base.Attributes[13];
			}
			set
			{
				base.Attributes[13] = value;
			}
		}

		// Token: 0x170094C0 RID: 38080
		// (get) Token: 0x0601AA17 RID: 109079 RVA: 0x00345AD1 File Offset: 0x00343CD1
		// (set) Token: 0x0601AA18 RID: 109080 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(23, "visibleStyles")]
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public OnOffValue VisibleStyles
		{
			get
			{
				return (OnOffValue)base.Attributes[14];
			}
			set
			{
				base.Attributes[14] = value;
			}
		}

		// Token: 0x170094C1 RID: 38081
		// (get) Token: 0x0601AA19 RID: 109081 RVA: 0x003650A5 File Offset: 0x003632A5
		// (set) Token: 0x0601AA1A RID: 109082 RVA: 0x002BE241 File Offset: 0x002BC441
		[OfficeAvailability(FileFormatVersions.Office2010)]
		[SchemaAttr(23, "alternateStyleNames")]
		public OnOffValue AlternateStyleNames
		{
			get
			{
				return (OnOffValue)base.Attributes[15];
			}
			set
			{
				base.Attributes[15] = value;
			}
		}

		// Token: 0x0601AA1C RID: 109084 RVA: 0x003650B8 File Offset: 0x003632B8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new HexBinaryValue();
			}
			if (23 == namespaceId && "allStyles" == name)
			{
				return new OnOffValue();
			}
			if (23 == namespaceId && "customStyles" == name)
			{
				return new OnOffValue();
			}
			if (23 == namespaceId && "latentStyles" == name)
			{
				return new OnOffValue();
			}
			if (23 == namespaceId && "stylesInUse" == name)
			{
				return new OnOffValue();
			}
			if (23 == namespaceId && "headingStyles" == name)
			{
				return new OnOffValue();
			}
			if (23 == namespaceId && "numberingStyles" == name)
			{
				return new OnOffValue();
			}
			if (23 == namespaceId && "tableStyles" == name)
			{
				return new OnOffValue();
			}
			if (23 == namespaceId && "directFormattingOnRuns" == name)
			{
				return new OnOffValue();
			}
			if (23 == namespaceId && "directFormattingOnParagraphs" == name)
			{
				return new OnOffValue();
			}
			if (23 == namespaceId && "directFormattingOnNumbering" == name)
			{
				return new OnOffValue();
			}
			if (23 == namespaceId && "directFormattingOnTables" == name)
			{
				return new OnOffValue();
			}
			if (23 == namespaceId && "clearFormatting" == name)
			{
				return new OnOffValue();
			}
			if (23 == namespaceId && "top3HeadingStyles" == name)
			{
				return new OnOffValue();
			}
			if (23 == namespaceId && "visibleStyles" == name)
			{
				return new OnOffValue();
			}
			if (23 == namespaceId && "alternateStyleNames" == name)
			{
				return new OnOffValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601AA1D RID: 109085 RVA: 0x0036524D File Offset: 0x0036344D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<StylePaneFormatFilter>(deep);
		}

		// Token: 0x0400ADC5 RID: 44485
		private const string tagName = "stylePaneFormatFilter";

		// Token: 0x0400ADC6 RID: 44486
		private const byte tagNsId = 23;

		// Token: 0x0400ADC7 RID: 44487
		internal const int ElementTypeIdConst = 11984;

		// Token: 0x0400ADC8 RID: 44488
		private static string[] attributeTagNames = new string[]
		{
			"val", "allStyles", "customStyles", "latentStyles", "stylesInUse", "headingStyles", "numberingStyles", "tableStyles", "directFormattingOnRuns", "directFormattingOnParagraphs",
			"directFormattingOnNumbering", "directFormattingOnTables", "clearFormatting", "top3HeadingStyles", "visibleStyles", "alternateStyleNames"
		};

		// Token: 0x0400ADC9 RID: 44489
		private static byte[] attributeNamespaceIds = new byte[]
		{
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23
		};
	}
}
