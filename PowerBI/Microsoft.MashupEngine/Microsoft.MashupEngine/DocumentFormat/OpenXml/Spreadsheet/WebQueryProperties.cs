using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CD5 RID: 11477
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Tables))]
	internal class WebQueryProperties : OpenXmlCompositeElement
	{
		// Token: 0x17008586 RID: 34182
		// (get) Token: 0x060189B3 RID: 100787 RVA: 0x00327B40 File Offset: 0x00325D40
		public override string LocalName
		{
			get
			{
				return "webPr";
			}
		}

		// Token: 0x17008587 RID: 34183
		// (get) Token: 0x060189B4 RID: 100788 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008588 RID: 34184
		// (get) Token: 0x060189B5 RID: 100789 RVA: 0x0034302D File Offset: 0x0034122D
		internal override int ElementTypeId
		{
			get
			{
				return 11458;
			}
		}

		// Token: 0x060189B6 RID: 100790 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008589 RID: 34185
		// (get) Token: 0x060189B7 RID: 100791 RVA: 0x00343034 File Offset: 0x00341234
		internal override string[] AttributeTagNames
		{
			get
			{
				return WebQueryProperties.attributeTagNames;
			}
		}

		// Token: 0x1700858A RID: 34186
		// (get) Token: 0x060189B8 RID: 100792 RVA: 0x0034303B File Offset: 0x0034123B
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return WebQueryProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x1700858B RID: 34187
		// (get) Token: 0x060189B9 RID: 100793 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x060189BA RID: 100794 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "xml")]
		public BooleanValue XmlSource
		{
			get
			{
				return (BooleanValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x1700858C RID: 34188
		// (get) Token: 0x060189BB RID: 100795 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x060189BC RID: 100796 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "sourceData")]
		public BooleanValue SourceData
		{
			get
			{
				return (BooleanValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x1700858D RID: 34189
		// (get) Token: 0x060189BD RID: 100797 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x060189BE RID: 100798 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "parsePre")]
		public BooleanValue ParsePreTag
		{
			get
			{
				return (BooleanValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x1700858E RID: 34190
		// (get) Token: 0x060189BF RID: 100799 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x060189C0 RID: 100800 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "consecutive")]
		public BooleanValue Consecutive
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

		// Token: 0x1700858F RID: 34191
		// (get) Token: 0x060189C1 RID: 100801 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x060189C2 RID: 100802 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "firstRow")]
		public BooleanValue FirstRow
		{
			get
			{
				return (BooleanValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17008590 RID: 34192
		// (get) Token: 0x060189C3 RID: 100803 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x060189C4 RID: 100804 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "xl97")]
		public BooleanValue CreatedInExcel97
		{
			get
			{
				return (BooleanValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17008591 RID: 34193
		// (get) Token: 0x060189C5 RID: 100805 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x060189C6 RID: 100806 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "textDates")]
		public BooleanValue TextDates
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

		// Token: 0x17008592 RID: 34194
		// (get) Token: 0x060189C7 RID: 100807 RVA: 0x002CA736 File Offset: 0x002C8936
		// (set) Token: 0x060189C8 RID: 100808 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "xl2000")]
		public BooleanValue RefreshedInExcel2000
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

		// Token: 0x17008593 RID: 34195
		// (get) Token: 0x060189C9 RID: 100809 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x060189CA RID: 100810 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "url")]
		public StringValue Url
		{
			get
			{
				return (StringValue)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x17008594 RID: 34196
		// (get) Token: 0x060189CB RID: 100811 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x060189CC RID: 100812 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "post")]
		public StringValue Post
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

		// Token: 0x17008595 RID: 34197
		// (get) Token: 0x060189CD RID: 100813 RVA: 0x002C8762 File Offset: 0x002C6962
		// (set) Token: 0x060189CE RID: 100814 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "htmlTables")]
		public BooleanValue HtmlTables
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

		// Token: 0x17008596 RID: 34198
		// (get) Token: 0x060189CF RID: 100815 RVA: 0x00343042 File Offset: 0x00341242
		// (set) Token: 0x060189D0 RID: 100816 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "htmlFormat")]
		public EnumValue<HtmlFormattingValues> HtmlFormat
		{
			get
			{
				return (EnumValue<HtmlFormattingValues>)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x17008597 RID: 34199
		// (get) Token: 0x060189D1 RID: 100817 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x060189D2 RID: 100818 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "editPage")]
		public StringValue EditPage
		{
			get
			{
				return (StringValue)base.Attributes[12];
			}
			set
			{
				base.Attributes[12] = value;
			}
		}

		// Token: 0x060189D3 RID: 100819 RVA: 0x00293ECF File Offset: 0x002920CF
		public WebQueryProperties()
		{
		}

		// Token: 0x060189D4 RID: 100820 RVA: 0x00293ED7 File Offset: 0x002920D7
		public WebQueryProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060189D5 RID: 100821 RVA: 0x00293EE0 File Offset: 0x002920E0
		public WebQueryProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060189D6 RID: 100822 RVA: 0x00293EE9 File Offset: 0x002920E9
		public WebQueryProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060189D7 RID: 100823 RVA: 0x00343052 File Offset: 0x00341252
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "tables" == name)
			{
				return new Tables();
			}
			return null;
		}

		// Token: 0x17008598 RID: 34200
		// (get) Token: 0x060189D8 RID: 100824 RVA: 0x0034306D File Offset: 0x0034126D
		internal override string[] ElementTagNames
		{
			get
			{
				return WebQueryProperties.eleTagNames;
			}
		}

		// Token: 0x17008599 RID: 34201
		// (get) Token: 0x060189D9 RID: 100825 RVA: 0x00343074 File Offset: 0x00341274
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return WebQueryProperties.eleNamespaceIds;
			}
		}

		// Token: 0x1700859A RID: 34202
		// (get) Token: 0x060189DA RID: 100826 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700859B RID: 34203
		// (get) Token: 0x060189DB RID: 100827 RVA: 0x0034307B File Offset: 0x0034127B
		// (set) Token: 0x060189DC RID: 100828 RVA: 0x00343084 File Offset: 0x00341284
		public Tables Tables
		{
			get
			{
				return base.GetElement<Tables>(0);
			}
			set
			{
				base.SetElement<Tables>(0, value);
			}
		}

		// Token: 0x060189DD RID: 100829 RVA: 0x00343090 File Offset: 0x00341290
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "xml" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "sourceData" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "parsePre" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "consecutive" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "firstRow" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "xl97" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "textDates" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "xl2000" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "url" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "post" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "htmlTables" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "htmlFormat" == name)
			{
				return new EnumValue<HtmlFormattingValues>();
			}
			if (namespaceId == 0 && "editPage" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060189DE RID: 100830 RVA: 0x003431C3 File Offset: 0x003413C3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<WebQueryProperties>(deep);
		}

		// Token: 0x060189DF RID: 100831 RVA: 0x003431CC File Offset: 0x003413CC
		// Note: this type is marked as 'beforefieldinit'.
		static WebQueryProperties()
		{
			byte[] array = new byte[13];
			WebQueryProperties.attributeNamespaceIds = array;
			WebQueryProperties.eleTagNames = new string[] { "tables" };
			WebQueryProperties.eleNamespaceIds = new byte[] { 22 };
		}

		// Token: 0x0400A106 RID: 41222
		private const string tagName = "webPr";

		// Token: 0x0400A107 RID: 41223
		private const byte tagNsId = 22;

		// Token: 0x0400A108 RID: 41224
		internal const int ElementTypeIdConst = 11458;

		// Token: 0x0400A109 RID: 41225
		private static string[] attributeTagNames = new string[]
		{
			"xml", "sourceData", "parsePre", "consecutive", "firstRow", "xl97", "textDates", "xl2000", "url", "post",
			"htmlTables", "htmlFormat", "editPage"
		};

		// Token: 0x0400A10A RID: 41226
		private static byte[] attributeNamespaceIds;

		// Token: 0x0400A10B RID: 41227
		private static readonly string[] eleTagNames;

		// Token: 0x0400A10C RID: 41228
		private static readonly byte[] eleNamespaceIds;
	}
}
