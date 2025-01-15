using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FAB RID: 12203
	[ChildElementInfo(typeof(TableStyleConditionalFormattingTableProperties))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(StyleParagraphProperties))]
	[ChildElementInfo(typeof(RunPropertiesBaseStyle))]
	[ChildElementInfo(typeof(TableStyleConditionalFormattingTableRowProperties))]
	[ChildElementInfo(typeof(TableStyleConditionalFormattingTableCellProperties))]
	internal class TableStyleProperties : OpenXmlCompositeElement
	{
		// Token: 0x17009330 RID: 37680
		// (get) Token: 0x0601A6BC RID: 108220 RVA: 0x00362048 File Offset: 0x00360248
		public override string LocalName
		{
			get
			{
				return "tblStylePr";
			}
		}

		// Token: 0x17009331 RID: 37681
		// (get) Token: 0x0601A6BD RID: 108221 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009332 RID: 37682
		// (get) Token: 0x0601A6BE RID: 108222 RVA: 0x0036204F File Offset: 0x0036024F
		internal override int ElementTypeId
		{
			get
			{
				return 11910;
			}
		}

		// Token: 0x0601A6BF RID: 108223 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17009333 RID: 37683
		// (get) Token: 0x0601A6C0 RID: 108224 RVA: 0x00362056 File Offset: 0x00360256
		internal override string[] AttributeTagNames
		{
			get
			{
				return TableStyleProperties.attributeTagNames;
			}
		}

		// Token: 0x17009334 RID: 37684
		// (get) Token: 0x0601A6C1 RID: 108225 RVA: 0x0036205D File Offset: 0x0036025D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TableStyleProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x17009335 RID: 37685
		// (get) Token: 0x0601A6C2 RID: 108226 RVA: 0x00362064 File Offset: 0x00360264
		// (set) Token: 0x0601A6C3 RID: 108227 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "type")]
		public EnumValue<TableStyleOverrideValues> Type
		{
			get
			{
				return (EnumValue<TableStyleOverrideValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601A6C4 RID: 108228 RVA: 0x00293ECF File Offset: 0x002920CF
		public TableStyleProperties()
		{
		}

		// Token: 0x0601A6C5 RID: 108229 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TableStyleProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A6C6 RID: 108230 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TableStyleProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A6C7 RID: 108231 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TableStyleProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601A6C8 RID: 108232 RVA: 0x00362074 File Offset: 0x00360274
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "pPr" == name)
			{
				return new StyleParagraphProperties();
			}
			if (23 == namespaceId && "rPr" == name)
			{
				return new RunPropertiesBaseStyle();
			}
			if (23 == namespaceId && "tblPr" == name)
			{
				return new TableStyleConditionalFormattingTableProperties();
			}
			if (23 == namespaceId && "trPr" == name)
			{
				return new TableStyleConditionalFormattingTableRowProperties();
			}
			if (23 == namespaceId && "tcPr" == name)
			{
				return new TableStyleConditionalFormattingTableCellProperties();
			}
			return null;
		}

		// Token: 0x17009336 RID: 37686
		// (get) Token: 0x0601A6C9 RID: 108233 RVA: 0x003620FA File Offset: 0x003602FA
		internal override string[] ElementTagNames
		{
			get
			{
				return TableStyleProperties.eleTagNames;
			}
		}

		// Token: 0x17009337 RID: 37687
		// (get) Token: 0x0601A6CA RID: 108234 RVA: 0x00362101 File Offset: 0x00360301
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return TableStyleProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17009338 RID: 37688
		// (get) Token: 0x0601A6CB RID: 108235 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17009339 RID: 37689
		// (get) Token: 0x0601A6CC RID: 108236 RVA: 0x00362108 File Offset: 0x00360308
		// (set) Token: 0x0601A6CD RID: 108237 RVA: 0x00362111 File Offset: 0x00360311
		public StyleParagraphProperties StyleParagraphProperties
		{
			get
			{
				return base.GetElement<StyleParagraphProperties>(0);
			}
			set
			{
				base.SetElement<StyleParagraphProperties>(0, value);
			}
		}

		// Token: 0x1700933A RID: 37690
		// (get) Token: 0x0601A6CE RID: 108238 RVA: 0x0036211B File Offset: 0x0036031B
		// (set) Token: 0x0601A6CF RID: 108239 RVA: 0x00362124 File Offset: 0x00360324
		public RunPropertiesBaseStyle RunPropertiesBaseStyle
		{
			get
			{
				return base.GetElement<RunPropertiesBaseStyle>(1);
			}
			set
			{
				base.SetElement<RunPropertiesBaseStyle>(1, value);
			}
		}

		// Token: 0x1700933B RID: 37691
		// (get) Token: 0x0601A6D0 RID: 108240 RVA: 0x0036212E File Offset: 0x0036032E
		// (set) Token: 0x0601A6D1 RID: 108241 RVA: 0x00362137 File Offset: 0x00360337
		public TableStyleConditionalFormattingTableProperties TableStyleConditionalFormattingTableProperties
		{
			get
			{
				return base.GetElement<TableStyleConditionalFormattingTableProperties>(2);
			}
			set
			{
				base.SetElement<TableStyleConditionalFormattingTableProperties>(2, value);
			}
		}

		// Token: 0x1700933C RID: 37692
		// (get) Token: 0x0601A6D2 RID: 108242 RVA: 0x00362141 File Offset: 0x00360341
		// (set) Token: 0x0601A6D3 RID: 108243 RVA: 0x0036214A File Offset: 0x0036034A
		public TableStyleConditionalFormattingTableRowProperties TableStyleConditionalFormattingTableRowProperties
		{
			get
			{
				return base.GetElement<TableStyleConditionalFormattingTableRowProperties>(3);
			}
			set
			{
				base.SetElement<TableStyleConditionalFormattingTableRowProperties>(3, value);
			}
		}

		// Token: 0x1700933D RID: 37693
		// (get) Token: 0x0601A6D4 RID: 108244 RVA: 0x00362154 File Offset: 0x00360354
		// (set) Token: 0x0601A6D5 RID: 108245 RVA: 0x0036215D File Offset: 0x0036035D
		public TableStyleConditionalFormattingTableCellProperties TableStyleConditionalFormattingTableCellProperties
		{
			get
			{
				return base.GetElement<TableStyleConditionalFormattingTableCellProperties>(4);
			}
			set
			{
				base.SetElement<TableStyleConditionalFormattingTableCellProperties>(4, value);
			}
		}

		// Token: 0x0601A6D6 RID: 108246 RVA: 0x00362167 File Offset: 0x00360367
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "type" == name)
			{
				return new EnumValue<TableStyleOverrideValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601A6D7 RID: 108247 RVA: 0x00362189 File Offset: 0x00360389
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TableStyleProperties>(deep);
		}

		// Token: 0x0400ACE8 RID: 44264
		private const string tagName = "tblStylePr";

		// Token: 0x0400ACE9 RID: 44265
		private const byte tagNsId = 23;

		// Token: 0x0400ACEA RID: 44266
		internal const int ElementTypeIdConst = 11910;

		// Token: 0x0400ACEB RID: 44267
		private static string[] attributeTagNames = new string[] { "type" };

		// Token: 0x0400ACEC RID: 44268
		private static byte[] attributeNamespaceIds = new byte[] { 23 };

		// Token: 0x0400ACED RID: 44269
		private static readonly string[] eleTagNames = new string[] { "pPr", "rPr", "tblPr", "trPr", "tcPr" };

		// Token: 0x0400ACEE RID: 44270
		private static readonly byte[] eleNamespaceIds = new byte[] { 23, 23, 23, 23, 23 };
	}
}
