using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x020024B3 RID: 9395
	[ChildElementInfo(typeof(Transform2D), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(WordNonVisualContentPartShapeProperties), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(WordNonVisualContentPartProperties), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(OfficeArtExtensionList), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class ContentPart : OpenXmlCompositeElement
	{
		// Token: 0x17005257 RID: 21079
		// (get) Token: 0x06011637 RID: 71223 RVA: 0x002DF99D File Offset: 0x002DDB9D
		public override string LocalName
		{
			get
			{
				return "contentPart";
			}
		}

		// Token: 0x17005258 RID: 21080
		// (get) Token: 0x06011638 RID: 71224 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x17005259 RID: 21081
		// (get) Token: 0x06011639 RID: 71225 RVA: 0x002EDFAC File Offset: 0x002EC1AC
		internal override int ElementTypeId
		{
			get
			{
				return 12865;
			}
		}

		// Token: 0x0601163A RID: 71226 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x1700525A RID: 21082
		// (get) Token: 0x0601163B RID: 71227 RVA: 0x002EDFB3 File Offset: 0x002EC1B3
		internal override string[] AttributeTagNames
		{
			get
			{
				return ContentPart.attributeTagNames;
			}
		}

		// Token: 0x1700525B RID: 21083
		// (get) Token: 0x0601163C RID: 71228 RVA: 0x002EDFBA File Offset: 0x002EC1BA
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ContentPart.attributeNamespaceIds;
			}
		}

		// Token: 0x1700525C RID: 21084
		// (get) Token: 0x0601163D RID: 71229 RVA: 0x002DE40B File Offset: 0x002DC60B
		// (set) Token: 0x0601163E RID: 71230 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(52, "bwMode")]
		public EnumValue<BlackWhiteModeValues> BlackWhiteMode
		{
			get
			{
				return (EnumValue<BlackWhiteModeValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x1700525D RID: 21085
		// (get) Token: 0x0601163F RID: 71231 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06011640 RID: 71232 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(19, "id")]
		public StringValue RelationshipId
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

		// Token: 0x06011641 RID: 71233 RVA: 0x00293ECF File Offset: 0x002920CF
		public ContentPart()
		{
		}

		// Token: 0x06011642 RID: 71234 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ContentPart(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011643 RID: 71235 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ContentPart(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011644 RID: 71236 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ContentPart(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011645 RID: 71237 RVA: 0x002EDFC4 File Offset: 0x002EC1C4
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (52 == namespaceId && "nvContentPr" == name)
			{
				return new WordNonVisualContentPartProperties();
			}
			if (52 == namespaceId && "nvContentPartPr" == name)
			{
				return new WordNonVisualContentPartShapeProperties();
			}
			if (52 == namespaceId && "xfrm" == name)
			{
				return new Transform2D();
			}
			if (52 == namespaceId && "extLst" == name)
			{
				return new OfficeArtExtensionList();
			}
			return null;
		}

		// Token: 0x1700525E RID: 21086
		// (get) Token: 0x06011646 RID: 71238 RVA: 0x002EE032 File Offset: 0x002EC232
		internal override string[] ElementTagNames
		{
			get
			{
				return ContentPart.eleTagNames;
			}
		}

		// Token: 0x1700525F RID: 21087
		// (get) Token: 0x06011647 RID: 71239 RVA: 0x002EE039 File Offset: 0x002EC239
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ContentPart.eleNamespaceIds;
			}
		}

		// Token: 0x17005260 RID: 21088
		// (get) Token: 0x06011648 RID: 71240 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005261 RID: 21089
		// (get) Token: 0x06011649 RID: 71241 RVA: 0x002EE040 File Offset: 0x002EC240
		// (set) Token: 0x0601164A RID: 71242 RVA: 0x002EE049 File Offset: 0x002EC249
		public WordNonVisualContentPartProperties WordNonVisualContentPartProperties
		{
			get
			{
				return base.GetElement<WordNonVisualContentPartProperties>(0);
			}
			set
			{
				base.SetElement<WordNonVisualContentPartProperties>(0, value);
			}
		}

		// Token: 0x17005262 RID: 21090
		// (get) Token: 0x0601164B RID: 71243 RVA: 0x002EE053 File Offset: 0x002EC253
		// (set) Token: 0x0601164C RID: 71244 RVA: 0x002EE05C File Offset: 0x002EC25C
		public WordNonVisualContentPartShapeProperties WordNonVisualContentPartShapeProperties
		{
			get
			{
				return base.GetElement<WordNonVisualContentPartShapeProperties>(1);
			}
			set
			{
				base.SetElement<WordNonVisualContentPartShapeProperties>(1, value);
			}
		}

		// Token: 0x17005263 RID: 21091
		// (get) Token: 0x0601164D RID: 71245 RVA: 0x002EE066 File Offset: 0x002EC266
		// (set) Token: 0x0601164E RID: 71246 RVA: 0x002EE06F File Offset: 0x002EC26F
		public Transform2D Transform2D
		{
			get
			{
				return base.GetElement<Transform2D>(2);
			}
			set
			{
				base.SetElement<Transform2D>(2, value);
			}
		}

		// Token: 0x17005264 RID: 21092
		// (get) Token: 0x0601164F RID: 71247 RVA: 0x002EE079 File Offset: 0x002EC279
		// (set) Token: 0x06011650 RID: 71248 RVA: 0x002EE082 File Offset: 0x002EC282
		public OfficeArtExtensionList OfficeArtExtensionList
		{
			get
			{
				return base.GetElement<OfficeArtExtensionList>(3);
			}
			set
			{
				base.SetElement<OfficeArtExtensionList>(3, value);
			}
		}

		// Token: 0x06011651 RID: 71249 RVA: 0x002EE08C File Offset: 0x002EC28C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (52 == namespaceId && "bwMode" == name)
			{
				return new EnumValue<BlackWhiteModeValues>();
			}
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06011652 RID: 71250 RVA: 0x002EE0C6 File Offset: 0x002EC2C6
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ContentPart>(deep);
		}

		// Token: 0x0400799A RID: 31130
		private const string tagName = "contentPart";

		// Token: 0x0400799B RID: 31131
		private const byte tagNsId = 52;

		// Token: 0x0400799C RID: 31132
		internal const int ElementTypeIdConst = 12865;

		// Token: 0x0400799D RID: 31133
		private static string[] attributeTagNames = new string[] { "bwMode", "id" };

		// Token: 0x0400799E RID: 31134
		private static byte[] attributeNamespaceIds = new byte[] { 52, 19 };

		// Token: 0x0400799F RID: 31135
		private static readonly string[] eleTagNames = new string[] { "nvContentPr", "nvContentPartPr", "xfrm", "extLst" };

		// Token: 0x040079A0 RID: 31136
		private static readonly byte[] eleNamespaceIds = new byte[] { 52, 52, 52, 52 };
	}
}
