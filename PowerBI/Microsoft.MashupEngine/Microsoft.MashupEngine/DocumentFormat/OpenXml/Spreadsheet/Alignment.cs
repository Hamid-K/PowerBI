using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C0B RID: 11275
	[GeneratedCode("DomGen", "2.0")]
	internal class Alignment : OpenXmlLeafElement
	{
		// Token: 0x17007FA7 RID: 32679
		// (get) Token: 0x06017BDB RID: 97243 RVA: 0x0033A8B3 File Offset: 0x00338AB3
		public override string LocalName
		{
			get
			{
				return "alignment";
			}
		}

		// Token: 0x17007FA8 RID: 32680
		// (get) Token: 0x06017BDC RID: 97244 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007FA9 RID: 32681
		// (get) Token: 0x06017BDD RID: 97245 RVA: 0x0033A8BA File Offset: 0x00338ABA
		internal override int ElementTypeId
		{
			get
			{
				return 11256;
			}
		}

		// Token: 0x06017BDE RID: 97246 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007FAA RID: 32682
		// (get) Token: 0x06017BDF RID: 97247 RVA: 0x0033A8C1 File Offset: 0x00338AC1
		internal override string[] AttributeTagNames
		{
			get
			{
				return Alignment.attributeTagNames;
			}
		}

		// Token: 0x17007FAB RID: 32683
		// (get) Token: 0x06017BE0 RID: 97248 RVA: 0x0033A8C8 File Offset: 0x00338AC8
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Alignment.attributeNamespaceIds;
			}
		}

		// Token: 0x17007FAC RID: 32684
		// (get) Token: 0x06017BE1 RID: 97249 RVA: 0x0033A8CF File Offset: 0x00338ACF
		// (set) Token: 0x06017BE2 RID: 97250 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "horizontal")]
		public EnumValue<HorizontalAlignmentValues> Horizontal
		{
			get
			{
				return (EnumValue<HorizontalAlignmentValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17007FAD RID: 32685
		// (get) Token: 0x06017BE3 RID: 97251 RVA: 0x0033A8DE File Offset: 0x00338ADE
		// (set) Token: 0x06017BE4 RID: 97252 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "vertical")]
		public EnumValue<VerticalAlignmentValues> Vertical
		{
			get
			{
				return (EnumValue<VerticalAlignmentValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17007FAE RID: 32686
		// (get) Token: 0x06017BE5 RID: 97253 RVA: 0x002E5814 File Offset: 0x002E3A14
		// (set) Token: 0x06017BE6 RID: 97254 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "textRotation")]
		public UInt32Value TextRotation
		{
			get
			{
				return (UInt32Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17007FAF RID: 32687
		// (get) Token: 0x06017BE7 RID: 97255 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x06017BE8 RID: 97256 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "wrapText")]
		public BooleanValue WrapText
		{
			get
			{
				return (BooleanValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17007FB0 RID: 32688
		// (get) Token: 0x06017BE9 RID: 97257 RVA: 0x002E6C42 File Offset: 0x002E4E42
		// (set) Token: 0x06017BEA RID: 97258 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "indent")]
		public UInt32Value Indent
		{
			get
			{
				return (UInt32Value)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17007FB1 RID: 32689
		// (get) Token: 0x06017BEB RID: 97259 RVA: 0x002ED371 File Offset: 0x002EB571
		// (set) Token: 0x06017BEC RID: 97260 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "relativeIndent")]
		public Int32Value RelativeIndent
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

		// Token: 0x17007FB2 RID: 32690
		// (get) Token: 0x06017BED RID: 97261 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x06017BEE RID: 97262 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "justifyLastLine")]
		public BooleanValue JustifyLastLine
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

		// Token: 0x17007FB3 RID: 32691
		// (get) Token: 0x06017BEF RID: 97263 RVA: 0x002CA736 File Offset: 0x002C8936
		// (set) Token: 0x06017BF0 RID: 97264 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "shrinkToFit")]
		public BooleanValue ShrinkToFit
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

		// Token: 0x17007FB4 RID: 32692
		// (get) Token: 0x06017BF1 RID: 97265 RVA: 0x002F6806 File Offset: 0x002F4A06
		// (set) Token: 0x06017BF2 RID: 97266 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "readingOrder")]
		public UInt32Value ReadingOrder
		{
			get
			{
				return (UInt32Value)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x17007FB5 RID: 32693
		// (get) Token: 0x06017BF3 RID: 97267 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x06017BF4 RID: 97268 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "mergeCell")]
		public StringValue MergeCell
		{
			get
			{
				return (StringValue)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x06017BF6 RID: 97270 RVA: 0x0033A8F0 File Offset: 0x00338AF0
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "horizontal" == name)
			{
				return new EnumValue<HorizontalAlignmentValues>();
			}
			if (namespaceId == 0 && "vertical" == name)
			{
				return new EnumValue<VerticalAlignmentValues>();
			}
			if (namespaceId == 0 && "textRotation" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "wrapText" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "indent" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "relativeIndent" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "justifyLastLine" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "shrinkToFit" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "readingOrder" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "mergeCell" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017BF7 RID: 97271 RVA: 0x0033A9E1 File Offset: 0x00338BE1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Alignment>(deep);
		}

		// Token: 0x06017BF8 RID: 97272 RVA: 0x0033A9EC File Offset: 0x00338BEC
		// Note: this type is marked as 'beforefieldinit'.
		static Alignment()
		{
			byte[] array = new byte[10];
			Alignment.attributeNamespaceIds = array;
		}

		// Token: 0x04009D68 RID: 40296
		private const string tagName = "alignment";

		// Token: 0x04009D69 RID: 40297
		private const byte tagNsId = 22;

		// Token: 0x04009D6A RID: 40298
		internal const int ElementTypeIdConst = 11256;

		// Token: 0x04009D6B RID: 40299
		private static string[] attributeTagNames = new string[] { "horizontal", "vertical", "textRotation", "wrapText", "indent", "relativeIndent", "justifyLastLine", "shrinkToFit", "readingOrder", "mergeCell" };

		// Token: 0x04009D6C RID: 40300
		private static byte[] attributeNamespaceIds;
	}
}
