using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Wordprocessing
{
	// Token: 0x0200223B RID: 8763
	[GeneratedCode("DomGen", "2.0")]
	internal class TextWrap : OpenXmlLeafElement
	{
		// Token: 0x1700396F RID: 14703
		// (get) Token: 0x0600E08E RID: 57486 RVA: 0x002BFE87 File Offset: 0x002BE087
		public override string LocalName
		{
			get
			{
				return "wrap";
			}
		}

		// Token: 0x17003970 RID: 14704
		// (get) Token: 0x0600E08F RID: 57487 RVA: 0x002BFE26 File Offset: 0x002BE026
		internal override byte NamespaceId
		{
			get
			{
				return 28;
			}
		}

		// Token: 0x17003971 RID: 14705
		// (get) Token: 0x0600E090 RID: 57488 RVA: 0x002BFE8E File Offset: 0x002BE08E
		internal override int ElementTypeId
		{
			get
			{
				return 12434;
			}
		}

		// Token: 0x0600E091 RID: 57489 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17003972 RID: 14706
		// (get) Token: 0x0600E092 RID: 57490 RVA: 0x002BFE95 File Offset: 0x002BE095
		internal override string[] AttributeTagNames
		{
			get
			{
				return TextWrap.attributeTagNames;
			}
		}

		// Token: 0x17003973 RID: 14707
		// (get) Token: 0x0600E093 RID: 57491 RVA: 0x002BFE9C File Offset: 0x002BE09C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TextWrap.attributeNamespaceIds;
			}
		}

		// Token: 0x17003974 RID: 14708
		// (get) Token: 0x0600E094 RID: 57492 RVA: 0x002BFEA3 File Offset: 0x002BE0A3
		// (set) Token: 0x0600E095 RID: 57493 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "type")]
		public EnumValue<WrapValues> Type
		{
			get
			{
				return (EnumValue<WrapValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17003975 RID: 14709
		// (get) Token: 0x0600E096 RID: 57494 RVA: 0x002BFEB2 File Offset: 0x002BE0B2
		// (set) Token: 0x0600E097 RID: 57495 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "side")]
		public EnumValue<WrapSideValues> Side
		{
			get
			{
				return (EnumValue<WrapSideValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17003976 RID: 14710
		// (get) Token: 0x0600E098 RID: 57496 RVA: 0x002BFEC1 File Offset: 0x002BE0C1
		// (set) Token: 0x0600E099 RID: 57497 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "anchorx")]
		public EnumValue<HorizontalAnchorValues> AnchorX
		{
			get
			{
				return (EnumValue<HorizontalAnchorValues>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17003977 RID: 14711
		// (get) Token: 0x0600E09A RID: 57498 RVA: 0x002BFED0 File Offset: 0x002BE0D0
		// (set) Token: 0x0600E09B RID: 57499 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "anchory")]
		public EnumValue<VerticalAnchorValues> AnchorY
		{
			get
			{
				return (EnumValue<VerticalAnchorValues>)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x0600E09D RID: 57501 RVA: 0x002BFEE0 File Offset: 0x002BE0E0
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "type" == name)
			{
				return new EnumValue<WrapValues>();
			}
			if (namespaceId == 0 && "side" == name)
			{
				return new EnumValue<WrapSideValues>();
			}
			if (namespaceId == 0 && "anchorx" == name)
			{
				return new EnumValue<HorizontalAnchorValues>();
			}
			if (namespaceId == 0 && "anchory" == name)
			{
				return new EnumValue<VerticalAnchorValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600E09E RID: 57502 RVA: 0x002BFF4D File Offset: 0x002BE14D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TextWrap>(deep);
		}

		// Token: 0x0600E09F RID: 57503 RVA: 0x002BFF58 File Offset: 0x002BE158
		// Note: this type is marked as 'beforefieldinit'.
		static TextWrap()
		{
			byte[] array = new byte[4];
			TextWrap.attributeNamespaceIds = array;
		}

		// Token: 0x04006E59 RID: 28249
		private const string tagName = "wrap";

		// Token: 0x04006E5A RID: 28250
		private const byte tagNsId = 28;

		// Token: 0x04006E5B RID: 28251
		internal const int ElementTypeIdConst = 12434;

		// Token: 0x04006E5C RID: 28252
		private static string[] attributeTagNames = new string[] { "type", "side", "anchorx", "anchory" };

		// Token: 0x04006E5D RID: 28253
		private static byte[] attributeNamespaceIds;
	}
}
